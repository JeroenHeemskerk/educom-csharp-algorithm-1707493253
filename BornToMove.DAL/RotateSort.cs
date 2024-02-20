using System;
using System.Collections.Generic;
using System.Linq;

namespace Organizer
{
	public class RotateSort<T>
	{

        private List<T> array = new List<T>();
        IComparer<T>? Comparer;

        /// <summary>
        /// Sort an array using the functions below
        /// </summary>
        /// <param name="input">The unsorted array</param>
        /// <returns>The sorted array</returns>
        public List<T> Sort(List<T> input, IComparer<T> comparer)
        {
            array = new List<T>(input);
            Comparer = comparer;
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
            T pivot = array[high];

            for (int i = low; i < high; i++) {
                T result = array[i];
                
                if (Comparer.Compare(pivot, result) < 0) {
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
