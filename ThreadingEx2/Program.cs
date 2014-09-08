using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

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


        /*
         * This Main is running the tasks right away after creating them, adding them to a list and then awaits their completion.
         */
        //static void Main()
        //{
            
        //    List<int> list = new List<int>();
        //    List<Task> tlist = new List<Task>();

            
        //    //Parallel.ForEach()???


        //    foreach (int[] data in Data)
        //    {
        //        //Tasks are started at this point
        //        Task t = Task.Run(() =>
        //        {
        //            int smallest = FindSmallest(data);
        //            Console.WriteLine("\t" + String.Join(", ", data) + "\n-> " + smallest);
        //            list.Add(smallest);
        //        });
                
        //        //Add task to list
        //        tlist.Add(t);
                
        //    }

        //    //Wait for all task added to the list to complete
        //    Task.WaitAll(tlist.ToArray());
        //    foreach (Task t in tlist)
        //    {
        //        Console.WriteLine(t.Id + " -- " + t.Status);
        //    }
            

        //    //Convert List to Array, find the smallest number in array and print to screen
        //    int[] array = list.ToArray();
        //    int smallestofthesmallest = FindSmallest(array);
        //    Console.WriteLine("The Smallest of the Smallest is: " + smallestofthesmallest);

        //} //End of Main()

        /*
         * This Main is creating the tasks and adds them to a list - then when all tasks are added, it runs a foreach loop to start
         * the tasks...
         */
        //static void Main()
        //{

        //    List<int> list = new List<int>();
        //    List<Task> tlist = new List<Task>();


        //    //Parallel.ForEach()???


        //    foreach (int[] data in Data)
        //    {
        //        Task t = new Task(() =>
        //        {
        //            int smallest = FindSmallest(data);
        //            Console.WriteLine("\t" + String.Join(", ", data) + "\n-> " + smallest);
        //            list.Add(smallest);
        //        });
               
        //        //Add task to list
        //        tlist.Add(t);

        //    }

        //    //Tasks is started at this point
        //    foreach (Task t in tlist)
        //    {
        //        t.Start();
        //    }

        //    //Wait for all task added to the list to complete
        //    Task.WaitAll(tlist.ToArray());
        //    foreach (Task t in tlist)
        //    {
        //        Console.WriteLine(t.Id + " -- " + t.Status);
        //    }


        //    //Convert List to Array, find the smallest number in array and print to screen
        //    int[] array = list.ToArray();
        //    int smallestofthesmallest = FindSmallest(array);
        //    Console.WriteLine("The Smallest of the Smallest is: " + smallestofthesmallest);

        //} //End of Main()


        static void Main()
        {

            List<int> list = new List<int>();
            List<Task<int>> tlist = new List<Task<int>>();
         

            //Parallel.ForEach()???


            foreach (int[] data in Data)
            {
                Task<int> t = new Task<int>(() =>
                {
                    int smallest = FindSmallest(data);
                    Console.WriteLine("\t" + String.Join(", ", data) + "\n-> " + smallest);
                    return smallest;
                });
                
                //Add task to list
                tlist.Add(t);

            }
            
            
            DateTime start = DateTime.Now;
            
            //Tasks is started at this point
            foreach (Task<int> t in tlist)
            {
                
                t.Start();
            }


            //Wait for all task added to the list to complete
            Task.WaitAll(tlist.ToArray());

            DateTime slut = DateTime.Now;
            TimeSpan total = slut - start;

            foreach (Task<int> t in tlist)
            {
                Console.WriteLine(t.Id + " -- " + t.Status + " -- Result: " + t.Result);
                list.Add(t.Result);
            }


            //Convert List to Array, find the smallest number in array and print to screen
            int[] array = list.ToArray();
            int smallestofthesmallest = FindSmallest(array);
            Console.WriteLine("The Smallest of the Smallest is: " + smallestofthesmallest);

            Console.WriteLine("\n" + "Time used to complete tasks: " + total);

        } //End of Main()


    } //End of class
} // End of namespace
