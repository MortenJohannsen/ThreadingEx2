using System;
using System.Collections.Generic;
using System.Threading;

namespace FindSmallest
{
    class Program
    {

        private static readonly int[][] Data = new int[][]{
            new[]{1,5,4,2}, 
            new[]{3,2,4,11,4},
            new[]{33,2,3,-1, 10},
            new[]{3,2,8,9,-1},
            new[]{1, 22,1,9,-3, 5}
        };

        private static int FindSmallest(int[] numbers)
        {
            if (numbers.Length < 1)
            {
                throw new ArgumentException("There must be at least one element in the array");
            }

            int smallestSoFar = numbers[0];
            foreach (int number in numbers)
            {
                if (number < smallestSoFar)
                {
                    smallestSoFar = number;
                }
            }
            return smallestSoFar;
        }


        static void Main()
        {
            Thread t;
            List<int> list = new List<int>();

            foreach (int[] data in Data)
            {
                t = new Thread(() =>
                {
                    int smallest = FindSmallest(data);
                    Console.WriteLine("\t" + String.Join(", ", data) + "\n-> " + smallest);
                    list.Add(smallest);
                });
                t.Start();

                //Wait for all threads to complete
                t.Join();

                //Convert List to Array
                int[] array = list.ToArray();

                int smallestofthesmallest = FindSmallest(array);

                Console.WriteLine("The Smallest of the Smallest is: " + smallestofthesmallest);

            }
        }
    }
}
