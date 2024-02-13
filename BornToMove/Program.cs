using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Runtime.CompilerServices;

namespace Organizer {
    public class Program {
        public static void Main(string[] _) {
            // Press <F5> to run this code, when "Hello World!" appears in a black box, remove the line below and write your code below.
            Console.Write("Enter size of array to be sorted: ");
            var input = Console.ReadLine();
            if (int.TryParse(input, out int size)) {
                Console.WriteLine("Sorting array of size " + size);
            } else {
                Console.WriteLine("Incorrect input. Exiting...");
                System.Environment.Exit(0);
            }
        }
    }
}
