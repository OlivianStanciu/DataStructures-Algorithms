using System;
using C_InANutShell.Trees;

namespace C_InANutShell.Testing
{
    class BinarySearchTree : ITest
    {
        public void Execute()
        {
            BinarySearchTree<int> bst = new BinarySearchTree<int>();
            Random rand = new Random();
            
            for (int i = 0; i < 10; i++)
            {
                bst.Insert(rand.Next(0, 15));
            }

            System.Console.WriteLine(bst.ToString());
            
            bst.Print(PrintOrder.PreOrder);
            bst.Print(PrintOrder.InOrder); // sorts the tree
            bst.Print(PrintOrder.PostOrder);
            bst.Print(PrintOrder.LevelOrder); // Breath First Search 

            System.Console.WriteLine(bst.Delete(5));
            System.Console.WriteLine(bst.ToString());

        }
    }
}