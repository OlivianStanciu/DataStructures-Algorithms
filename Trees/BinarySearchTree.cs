using System;
using System.Collections.Generic;
using System.Text;

namespace C_InANutShell.Trees
{
    public enum PrintOrder
    {
        PreOrder,
        InOrder,
        PostOrder,
        LevelOrder
    }

    //binary tree that satisfies the binary search tree (BST) invariant
    //the left subtree has elements smaller than current node and
    //the right subtree has elements greater than current node
    public class BinarySearchTree<T> where T : IComparable
    {
        private class Node<T>
        {
            public Node<T> LeftChild { get; set; }
            public Node<T> RightChild { get; set; }
            public T Data { get; set; }
        }

        private Node<T> _root;
        
        private int _size;

        public int Size => _size;
        public bool IsEmpty() => this.Size == 0;

        public int Depth => GetDepth(_root);


        public BinarySearchTree()
        {
            _size = 0;
            _root = null;
        }

        public bool Insert(T item)
        {
            if (this.Contains(item))
            {
                return false;
            }

            _root = AddItem(_root, item);
            _size++;
            return true;
        }

        private Node<T> AddItem(Node<T> node, T item)
        {
            // this is the node where item should be inserted
            if (node == null)
            {
                return new Node<T>()
                {
                    Data = item,
                    LeftChild = null,
                    RightChild = null
                };
            }

            int comparationResult = item.CompareTo(node.Data);

            
            if (comparationResult < 0) // if item < node insert left
            {
                node.LeftChild = AddItem(node.LeftChild, item);
            }
            else if (comparationResult > 0) // if item > node, insert right
            {
                node.RightChild = AddItem(node.RightChild, item);
            }

            // if item == node.data, duplicate, therefore return the same node
            return node;
        }

        public bool Delete(T item)
        {
            if (!this.Contains(item))
            {
                return false;
            }

            _root = DeleteItem(_root, item);
            _size--;
            return true;
        }

        private Node<T> DeleteItem(Node<T> node, T item)
        {
            int comparationResult = item.CompareTo(node.Data);
            
            if (comparationResult < 0)
            {
                node.LeftChild = DeleteItem(node.LeftChild, item);
            }
            else if(comparationResult > 0)
            {
                node.RightChild = DeleteItem(node.RightChild, item);
            } 
            else  // comparationResult == 0 => found the node :D
            {
                // so, the node could be: a leaf, have 2 children, have only 1 child
                // leaf
                if (IsLeaf(node))
                {
                    return null;
                }

                // not a leaf, and has only a left child, therefore its RightChild mult be null.
                // so the succesor would be its left child
                if (node.RightChild == null)
                {
                    return node.LeftChild;
                }

                // not a leaf, and has only a right child, therefore its LightChild mult be null
                // so the succesor would be its left child
                else if (node.LeftChild == null)
                {
                    return node.RightChild;
                }

                // the node has 2 children, therefore 1 of it should be selected to suceed. Either the biggest from left, or the smallest from right;
                // implementation using the smallest from right;
                Node<T> smallest = node.RightChild;

                // therefore, for finding the smallest from right subtree, must dive as much left as possible;
                while (smallest.LeftChild != null)
                {
                    smallest = smallest.LeftChild;
                }

                node.Data = smallest.Data;
                node.RightChild = DeleteItem(node.RightChild, smallest.Data);
            }

            return node;
        }

        private bool IsLeaf(Node<T> node) => 
            node.LeftChild == null && node.RightChild == null;

        public bool Contains(T item)
        {
            return Search(item, _root) != null;
        }

        public void Print(PrintOrder printOrder)
        {   
            if (this.IsEmpty())
            {
                System.Console.WriteLine("empty");
            }
            
            var sb = new StringBuilder();
            sb.Append($"{printOrder.ToString()}: ");

            if (printOrder != PrintOrder.LevelOrder)
            {
                PrintRecursive(_root, printOrder, sb);
            }
            else // breath first search
            {
                Queue<Node<T>> queue = new Queue<Node<T>>();
                queue.Enqueue(_root);
                
                // while !queue.IesEmpty();
                while (queue.TryDequeue(out Node<T> node))
                {
                    sb.Append($"{node.Data} ");
                    // enqueue left child
                    if(node.LeftChild != null)
                        queue.Enqueue(node.LeftChild);
                    // enqueue right child
                    if(node.RightChild != null)
                        queue.Enqueue(node.RightChild);
                }
            }
            
            System.Console.WriteLine(sb.ToString());
        }
        private void PrintRecursive(Node<T> node, PrintOrder printOrder, StringBuilder sb)
        {
            if (node == null)
            {
                return;
            }
            
            var printStr = string.Empty;
            if (printOrder == PrintOrder.PreOrder) // preorder
            {
                sb.Append($"{node.Data} ");
                PrintRecursive(node.LeftChild, printOrder, sb);
                PrintRecursive(node.RightChild, printOrder, sb);
            } 
            else if(printOrder == PrintOrder.InOrder) // inrder => retusns the items sorted (ascending)
            {
                PrintRecursive(node.LeftChild, printOrder, sb);
                sb.Append($"{node.Data} ");
                PrintRecursive(node.RightChild, printOrder, sb);
            }
            else //postorder
            {
                PrintRecursive(node.LeftChild, printOrder, sb);
                PrintRecursive(node.RightChild, printOrder, sb);
                sb.Append($"{node.Data} ");
            }
        }
        private Node<T> Search(T item, Node<T> node)
        {
            if (node == null)
            {
                return null;
            }

            int comparationResult = item.CompareTo(node.Data);

            if (comparationResult < 0)
            {
                return Search(item, node.LeftChild);
            }
            if (comparationResult > 0)
            {
                return Search(item, node.RightChild);
            }

            // comparationResult == 0;
            return node;
        }
        
        
        public override string ToString()
        {
            var depth = this.Depth;
            if (depth == 0)
            {
                return string.Empty;
            }

            List<string>[] nodesPerDepth = new List<string>[depth];
            for (int i = 0; i < depth; i++)
            {
                nodesPerDepth[i] = new List<string>();
            }

            var sb = new StringBuilder();

            ComposeTree(_root, ref sb, ref nodesPerDepth);

            var displayArray = sb.ToString().Split(' ', StringSplitOptions.RemoveEmptyEntries);
        
            var finalBuilder = new StringBuilder();

            foreach (var listOfNodes in nodesPerDepth)
            {
                var nodesInCurrentDepth = new HashSet<string>();
                //let only the nodes in current depth, and replace the rest on nodes with " "
                foreach (var node in listOfNodes)
                {
                    nodesInCurrentDepth.Add(node);
                }

                foreach (var displayNode in displayArray)
                {
                    var toAppend = nodesInCurrentDepth.Contains(displayNode) ? displayNode : GetSpaces(displayNode.Length);
                    finalBuilder.Append(toAppend);
                }

                finalBuilder.AppendLine();
            }
            
            finalBuilder.AppendLine($"depth: {this.Depth}");
            return finalBuilder.ToString();

            void ComposeTree(Node<T> root, ref StringBuilder sb, ref List<string>[] nodesPerDepth)
            {
                if (root == null)
                {
                    sb.Append(" ");
                    return;
                }

                if (root.LeftChild != null)
                { 
                    ComposeTree(root.LeftChild, ref sb, ref nodesPerDepth);
                }

                sb.Append($"{root.Data.ToString()} ");              
    
                var nodeLevel = SearchNodeIndex(root.Data, _root);
                nodesPerDepth[nodeLevel].Add(root.Data.ToString());

                if (root.RightChild != null)
                {
                    
                    ComposeTree(root.RightChild, ref sb, ref nodesPerDepth);
                }
            }
            
            string GetSpaces(int noOfSpaces)
            {
                var result = string.Empty;
                for (int i = 0; i < noOfSpaces; i++)
                    result += " ";
                return result;
            }

            int SearchNodeIndex(T item, Node<T> node)
            {
            if (item.CompareTo(node.Data) == 0)
            {
                return 0;
            }
    
            if (item.CompareTo(node.Data) < 0)
            {
                return node.LeftChild == null ? -1 : SearchNodeIndex(item, node.LeftChild) + 1;
            }

            if (item.CompareTo(node.Data) > 0)
            {
                return node.RightChild == null ? -1 : SearchNodeIndex(item, node.RightChild) + 1;
            }

            return -1;
        }
        }

        private int GetDepth(Node<T> root)
        {
            if (root == null)
                return 0;
            else
            {
                /* compute the depth of each subtree */
                int leftDepth = GetDepth(root.LeftChild);
                int rightDepth = GetDepth(root.RightChild);
    
                return Math.Max(leftDepth, rightDepth) + 1;
            }
        }
    }
}