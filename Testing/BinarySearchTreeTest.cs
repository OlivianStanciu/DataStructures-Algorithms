using System;
using System.Text;
using C_InANutShell.Trees;

namespace C_InANutShell.Testing
{
    class BinarySearchTree : ITest
    {
        public void Execute()
        {
            BinarySearchTree<int> bst = new BinarySearchTree<int>();
            Random rand = new Random();
            
            // for (int i = 0; i < 10; i++)
            // {
            //     bst.Insert(rand.Next(0, 15));
            // }

            bst.Insert(14);
            bst.Insert(11);
            bst.Insert(15);
            bst.Insert(7);
            bst.Insert(13);
            bst.Insert(16);
            bst.Insert(10);
            bst.Insert(12);
            bst.Insert(8);
            bst.Insert(9);
            //bst.Insert(1);

            System.Console.WriteLine(bst.ToString());
            
            bst.Traverse(TraverseOrder.PreOrder);
            bst.Traverse(TraverseOrder.InOrder); // sorts the tree
            bst.Traverse(TraverseOrder.PostOrder);
            bst.Traverse(TraverseOrder.LevelOrder); // Breath First Search 

            TraverseOrder tOrder = TraverseOrder.LevelOrder;
            using (var enumerator = bst.GetEnumerator(tOrder))
            {
                var sb = new StringBuilder();
                while (enumerator.MoveNext())
                {
                    sb.Append($"{enumerator.Current} ");
                }
                System.Console.WriteLine($"Traversal order[{tOrder.ToString()}]: {sb.ToString()}");
            }

            

            System.Console.WriteLine(bst.Delete(5));
            System.Console.WriteLine(bst.ToString());

            bst.GetEnumerator();

        }
    }
}