using System;
using C_InANutShell.Arrays;

namespace C_InANutShell.Testing
{
    public class DynamicArrayTest : ITest
    {
        public void Execute()
        {
            DynamicArray<int> arr = new DynamicArray<int>();
            System.Console.WriteLine(arr.Length);
            arr.Append(new int[] {1, 2});
            System.Console.WriteLine(arr.Length);

            System.Console.WriteLine(arr[1]);
            System.Console.WriteLine(arr.Contains(2));

            System.Console.WriteLine(arr.Length);
            arr.Clear();
            System.Console.WriteLine(arr.Length);
            arr.Append(new int[] {1, 2});
            System.Console.WriteLine(arr.Length);
        }
    }
}