using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Runtime.CompilerServices;

namespace Organizer
{
    public class Program
    {
        public static void Main(string[] _)
        {
            // Press <F5> to run this code, when "Hello World!" appears in a black box, remove the line below and write your code below.
            Console.Write("Enter size of array to be sorted: ");
            var input = Console.ReadLine();
            if (int.TryParse(input, out int size)) {
                Console.WriteLine("Sorting array of size " + size);
            } else {
                Console.WriteLine("Incorrect input. Exiting...");
                System.Environment.Exit(0);
            }

            Stopwatch sw = Stopwatch.StartNew();
            Random rng = new Random();
            List<int> l = new List<int>(size);
            for (int i = 0; i < size; i++) {
                l.Add(rng.Next(-99, 100));
            }

            ShowList("Unsorted", l);
            ShiftHighestSort shift = new ShiftHighestSort();
            sw.Start();
            List<int> sorted = shift.Sort(l);
            sw.Stop();
            ShowList("Sorted Shift", sorted);
            Console.WriteLine("Time: " + sw.Elapsed);

            RotateSort<int> quick = new RotateSort<int>();
            sw.Restart();
            sorted = quick.Sort(l, Comparer<int>.Default);
            sw.Stop();
            ShowList("Sorted Quick",sorted);
            Console.WriteLine("Time: " + sw.Elapsed);
            //ShowList("Example of ShowList", new List<int>() { -33, 3, 2, 2, 3, 34, 34, 32, 1, 3, 5, 3, -22, -99, 33, -22, 11, 3, 33, 12, -2, -21, 4, 34, 22, 15, 34,-22 });
        }


        /* Example of a static function */

        /// <summary>
        /// Show the list in lines of 20 numbers each
        /// </summary>
        /// <param name="label">The label for this list</param>
        /// <param name="theList">The list to show</param>
        public static void ShowList(string label, List<int> theList)
        {
            int count = theList.Count;
            if (count > 200)
            {
                count = 200; // Do not show more than 200 numbers
            }
            Console.WriteLine();
            Console.Write(label);
            Console.Write(':');
            for (int index = 0; index < count; index++)
            {
                if (index % 20 == 0) // when index can be divided by 20 exactly, start a new line
                {
                    Console.WriteLine();
                }
                Console.Write(string.Format("{0,3}, ", theList[index]));  // Show each number right aligned within 3 characters, with a comma and a space
            }
            Console.WriteLine();
        }
    }
}
