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

/////////////////////////////////// GRAPH ///////////////////////////////////////
//                                           14  
//                                          /  \
//                                      ___11   15  
//                                     /     \   \
//                                    7      13   16
//                                     \     /
//                                     10   12        
//                                     /  
//                                    8
//                                     \         
//                                      9        
/////////////////////////////////// GRAPH ///////////////////////////////////////

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
            
            bst.TraverseAndPrint(TraverseOrder.PreOrder);
            bst.TraverseAndPrint(TraverseOrder.InOrder); // sorts the tree
            bst.TraverseAndPrint(TraverseOrder.PostOrder);
            bst.TraverseAndPrint(TraverseOrder.LevelOrder); // Breath First Search 

            TraverseOrder tOrder = TraverseOrder.PostOrder;
            using (var enumerator = bst.GetEnumerator(tOrder))
            {
                var sb = new StringBuilder();
                while (enumerator.MoveNext())
                {
                    sb.Append($"{enumerator.Current} ");
                }
                System.Console.WriteLine($"Traversal[{tOrder.ToString()}]: {sb.ToString()}");
            }

            System.Console.WriteLine(bst.Delete(11));
            System.Console.WriteLine(bst.ToString());

        }
    }
}