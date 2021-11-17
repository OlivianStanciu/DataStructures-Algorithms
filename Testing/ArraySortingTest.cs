using System;
using C_InANutShell.Arrays.Sorting;

namespace C_InANutShell.Testing
{
    class ArraySortingTest : ITest
    {
        public void Execute()
        {
            var arr = new int[] {2, 6, 5, 3, 8, 7, 1, 0, 5, 2, -1};//{7, 12, 3, 56, 1, 2, 3, 9, 4};
            SortingPerformer.SortArray<int>(ref arr, new QuickSort<int>());

            for (int i = 0; i < arr.Length; i++)
            {
                System.Console.Write($"{arr[i]} ");
            }
            System.Console.WriteLine();
        } 
        
    }
}