using System;

namespace C_InANutShell.Arrays.Sorting
{
    public static class SortingPerformer
    {
        public static void SortArray<T>(ref T[] array, ISortingAlgorithm<T> sortingAlgorithm)
            where T : IComparable
        {
            sortingAlgorithm.Array = array;
            sortingAlgorithm.Execute();
        }       
    }

    public interface ISortingAlgorithm<T>
        where T : IComparable
    {
        T[] Array { get; set; }
        void Execute();
    }

    public class QuickSort<T> : ISortingAlgorithm<T>
        where T : IComparable
    {
        public T[] Array { get; set; }

        public void Execute()
        {
            if (Array == null)
                throw new NullReferenceException("Cannot sort a null array");
            if(Array.Length == 0)
                return;

            RunQuickQuickSort(0, Array.Length - 1);
        }
        
        public void RunQuickQuickSort(int startIndex, int endIndex)
        {
            // if only one element (endIndex == startIndex); this is already sorted, so return
            // also could be invalid if endIndex < startIndex
            if (endIndex <= startIndex)
                return;

            // chose the index of the pivot to be the last element in the array
            int pivotIndex = endIndex;

            // partition the array, moving all the elems smaller tha the pivot to the left, and all the elems greater or equal to the right;
            pivotIndex = Partition(pivotIndex, startIndex, endIndex);

            // quick sort the left part of the array, 0 to pivotIndex - 1
            RunQuickQuickSort(startIndex, pivotIndex - 1);

            // quick sort the right part of the array, pivotIndex + 1 to endIndex
            RunQuickQuickSort(pivotIndex + 1, endIndex);
        }

        // returns the index of the pivot after rearrangement
        private int Partition(int pivotIndex, int startIndex, int endIndex)
        {   
            // if pivotIndex < endIndex => move the pivot to the end Of the array
            // this allow for chosing a different pivot in RunQuickQuickSort()
            if (pivotIndex < endIndex)
            {
                Swap(ref Array[pivotIndex], ref Array[endIndex]);
                pivotIndex = endIndex;
            }

            // keep track of the first element greater than the pivot in the array
            int firstGreaterElement = startIndex;
            
            // if currentElement is greater than pivot, let it in its place
            // if currentElement is less than pivot, Swap(currentElement, firstGreaterElement), therefore adding it to the left
            //i < endIndex, because Array[endIndex] = pivot
            for (int i = startIndex; i < endIndex; i++)
            {
                // currentItem < pivot
                if (Array[i].CompareTo(Array[pivotIndex]) < 0)
                {
                    Swap(ref Array[i], ref Array[firstGreaterElement]);
                    // shift the counter 1 index to the right
                    firstGreaterElement++;
                }                
            }

            // lastly swap the pivot with the firstGreaterElement
            Swap(ref Array[pivotIndex], ref Array[firstGreaterElement]);

            // return the new index of the pivot, firstGreaterElement;
            return firstGreaterElement;
        }
        
        private void Swap(ref T item1, ref T item2)
        {
            var temp = item1;
            item1 = item2;
            item2 = temp;
        }
    }
}