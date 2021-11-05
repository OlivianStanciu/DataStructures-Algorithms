using System;
using C_InANutShell.Arrays;
using C_InANutShell.LinkedLists;

namespace C_InANutShell.Testing
{
    public class LinkedListTest : ITest
    {
        public void Execute()
        {
            DoublyLinkedList<string> linkedList = new DoublyLinkedList<string>();
            linkedList.AddAtIndex(index: 0, "str1");
            linkedList.AddAtIndex(index: 0, "str2");
            linkedList.AddAtIndex(index: 1, "str3", "str4");
            linkedList.AddAtIndex(index: 2, new string[] {"surprisee"});
            
            var x = linkedList.RemoveAtIndex(2);
            System.Console.WriteLine(linkedList.ToString());

            var z = linkedList.RemoveAtIndex(0);
            System.Console.WriteLine(linkedList.ToString());
        }
    }
}