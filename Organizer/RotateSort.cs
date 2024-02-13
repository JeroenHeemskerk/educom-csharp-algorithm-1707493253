using System;
using System.Collections.Generic;
using System.Linq;

namespace Organizer
{
	public class RotateSort
	{

        private List<int> array = new List<int>();

        /// <summary>
        /// Sort an array using the functions below
        /// </summary>
        /// <param name="input">The unsorted array</param>
        /// <returns>The sorted array</returns>
        public List<int> Sort(List<int> input)
        {
            array = new List<int>(input);

            SortFunction(0, array.Count - 1);
            return array;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="low">De index within this.array to start with</param>
        /// <param name="high">De index within this.array to stop with</param>
        private void SortFunction(int low, int high)
        {
            if (low >= high) { return; }
            int index = Partitioning(low, high);
            SortFunction(low, index - 2);
            SortFunction(index, high);
        }

        /// 
        /// Partition the array in a group 'low' digits (e.a. lower than a chosen pivot) and a group 'high' digits
        /// </summary>
        /// <param name="low">De index within this.array to start with</param>
        /// <param name="high">De index within this.array to stop with</param>
        /// <returns>The index in the array of the first of the 'high' digits</returns>
        private int Partitioning(int low, int high)
        {
            int pivot = array[high];

            for (int i = low; i < high; i++) {
                int result = array[i];
                if (result > pivot) {
                    array.RemoveAt(i);
                    array.Insert(high, result);
                    i--;
                    high--;
                }
            }
            return high+1;
        }
    }
}
