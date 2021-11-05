using System;
using C_InANutShell.Arrays;
using C_InANutShell.Stacks;

namespace C_InANutShell.Testing
{
    public class StackTest : ITest
    {
        public void Execute()
        {
            Stack<int> stack = new Stack<int>();
            //stack.Peek();
            stack.Push(1);
            System.Console.WriteLine(stack.ToString());
            stack.Push(2);
            System.Console.WriteLine(stack.ToString());
            stack.Pop();
            stack.Push(3);
            stack.Push(4);
            
            System.Console.WriteLine(stack.Peek());

            System.Console.WriteLine(stack.ToString());
        }
    }
}