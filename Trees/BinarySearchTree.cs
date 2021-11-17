using System;
using System.Collections;
using System.Collections.Generic;
using System.Text;

namespace C_InANutShell.Trees
{
    public enum TraverseOrder
    {
        PreOrder,
        InOrder,
        PostOrder,
        LevelOrder
    }

    //binary tree that satisfies the binary search tree (BST) invariant
    //the left subtree has elements smaller than current node and
    //the right subtree has elements greater than current node
    public partial class BinarySearchTree<T> : IEnumerable<T>
        where T : IComparable
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

        // TODO: Implement iterators for each order :D 
        internal void TraverseAndPrint(TraverseOrder traverseOrder)
        {   
            if (this.IsEmpty())
            {
                System.Console.WriteLine("empty");
            }
            
            var sb = new StringBuilder();
            sb.Append($"{traverseOrder.ToString()}: ");

            if (traverseOrder != TraverseOrder.LevelOrder)
            {
                TraverseRecursive(_root, traverseOrder, sb);
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
        private void TraverseRecursive(Node<T> node, TraverseOrder printOrder, StringBuilder sb)
        {
            if (node == null)
            {
                return;
            }
            
            var printStr = string.Empty;
            if (printOrder == TraverseOrder.PreOrder) // preorder
            {
                sb.Append($"{node.Data} ");
                TraverseRecursive(node.LeftChild, printOrder, sb);
                TraverseRecursive(node.RightChild, printOrder, sb);
            } 
            else if(printOrder == TraverseOrder.InOrder) // inrder => retusns the items sorted (ascending)
            {
                TraverseRecursive(node.LeftChild, printOrder, sb);
                sb.Append($"{node.Data} ");
                TraverseRecursive(node.RightChild, printOrder, sb);
            }
            else //postorder
            {
                TraverseRecursive(node.LeftChild, printOrder, sb);
                TraverseRecursive(node.RightChild, printOrder, sb);
                sb.Append($"{node.Data} ");
            }
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

            void ComposeTree(Node<T> root, ref StringBuilder sb, ref List<string>[] depthNodes)
            {
                if (root == null)
                {
                    sb.Append(" ");
                    return;
                }

                if (root.LeftChild != null)
                { 
                    ComposeTree(root.LeftChild, ref sb, ref depthNodes);
                }

                sb.Append($"{root.Data.ToString()} ");              
    
                var nodeLevel = SearchNodeIndex(root.Data, _root);
                depthNodes[nodeLevel].Add(root.Data.ToString());

                if (root.RightChild != null)
                {
                    
                    ComposeTree(root.RightChild, ref sb, ref depthNodes);
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
                    return 0;
        
                if (item.CompareTo(node.Data) < 0)
                    return node.LeftChild == null ? -1 : SearchNodeIndex(item, node.LeftChild) + 1;

                if (item.CompareTo(node.Data) > 0)
                    return node.RightChild == null ? -1 : SearchNodeIndex(item, node.RightChild) + 1;

                return -1;
            }
        }

        private int GetDepth(Node<T> node)
        {
            if (node == null)
                return 0;
            else
            {
                /* compute the depth of each subtree */
                int leftDepth = GetDepth(node.LeftChild);
                int rightDepth = GetDepth(node.RightChild);
    
                return Math.Max(leftDepth, rightDepth) + 1;
            }
        }

        private Node<T> GetRoot() => _root;

        /// <summary>
        /// Returns a default InOrderEnumerator
        /// </summary>
        /// <returns></returns>
        public IEnumerator<T> GetEnumerator()
        {
            return new InOrderEnumerator(this);
        }
        
        // compatibility; returns an object
        IEnumerator IEnumerable.GetEnumerator()
        {
            return this.GetEnumerator();
        }

        public IEnumerator<T> GetEnumerator(TraverseOrder traverseOrder)
        {
            return traverseOrder switch
            {
                TraverseOrder.InOrder => new InOrderEnumerator(this),
                TraverseOrder.PreOrder => new PreOrderEnumerator(this),
                TraverseOrder.PostOrder => new PostOrderEnumerator(this),
                TraverseOrder.LevelOrder => new LeverOrderEnumerater(this),
                _ => new InOrderEnumerator(this)
            };
        }


        #region Ennumerator abstraction
        private abstract class EnumeratorBase : IEnumerator<T>
        {
            protected Node<T> _node;
            protected BinarySearchTree<T> _bst;

             public T Current => _node != null ? 
                _node.Data : throw new NullReferenceException("The Enumerator is empty. Call MoveNext() first");
            object IEnumerator.Current => this.Current;
            public EnumeratorBase(BinarySearchTree<T> bst)
            {
                _bst = bst;
            }

            public virtual void Dispose()
            {
                _node = null;
                _bst = null;
            }

            public abstract bool MoveNext();

            public abstract void Reset();
        }
        private abstract class StackEnumeratorBase : EnumeratorBase
        {
            protected Stack<Node<T>> _stack;

            public StackEnumeratorBase(BinarySearchTree<T> bst) : base(bst)
            {
                _stack = new Stack<Node<T>>();
            }

            public override void Dispose()
            {
                base.Dispose();
                _stack = null;
            }
        }
        private abstract class QueueEnumeratorBase : EnumeratorBase
        {
            protected Queue<Node<T>> _queue;

            public QueueEnumeratorBase(BinarySearchTree<T> bst) : base(bst)
            {
                _queue = new Queue<Node<T>>();
            }

            public override void Dispose()
            {
                base.Dispose();
                _queue = null;
            }
        }
        #endregion
        // Root, Left, Right
        private class PreOrderEnumerator : StackEnumeratorBase
        {
            public PreOrderEnumerator(BinarySearchTree<T> bst) : base(bst)
            {
                this.Reset();
            }

            public override bool MoveNext()
            {
                // all nodes were already processed, or bst is empty
                if (_stack.Count == 0)
                    return false;

                _node = _stack.Pop();
                
                // push the left child last, so it would be the one popped next time
                if (_node.RightChild != null)
                    _stack.Push(_node.RightChild);
                if (_node.LeftChild != null)
                    _stack.Push(_node.LeftChild);

                return true;
            }
            public override void Reset()
            {
                _stack.Clear();

                var root = _bst.GetRoot();
                if (root != null)
                    _stack.Push(root);
                
                _node = null;
            }
        }
        
        // Left, Root, Right
        private class InOrderEnumerator : StackEnumeratorBase
        {
            public InOrderEnumerator(BinarySearchTree<T> bst) : base(bst)
            {
                this.Reset();
            }

            public override bool MoveNext()
            {
                // all nodes were processed, or bst is empty
                if (_stack.Count == 0)
                    return false;
                
                // smallest node
                _node = _stack.Pop();

                // add the smallest elem in the right subtree to the stack
                if (_node.RightChild != null)
                {
                    DigLeft(_node.RightChild);
                }
                
                return true;
            }

            public override void Reset()
            {
                _stack.Clear();
                DigLeft(_bst.GetRoot());
                _node = null;
            }

            // dig to the most left node, and add all the nodes to the stack;
            private void DigLeft(Node<T> node)
            {
                while (node != null)
                {
                    _stack.Push(node);
                    node = node.LeftChild;
                }
            }
        }

        // Left, Right, Root
        private class PostOrderEnumerator : StackEnumeratorBase
        {           
            HashSet<Node<T>> _nodesWithUnsolvedRightTree;
            public PostOrderEnumerator(BinarySearchTree<T> bst) : base(bst)
            {
                _nodesWithUnsolvedRightTree = new HashSet<Node<T>>();
                this.Reset();
            }

            public override bool MoveNext()
            {
                // all nodes were already processed, or bst is empty
                if (_stack.Count == 0)
                {
                    return false;
                }

                Node<T> node = _stack.Peek();

                // if node has its rightChild unresolved, resolve it first 
                if (_nodesWithUnsolvedRightTree.Contains(node))
                {
                    ResolveToBottomLeft(node.RightChild);
                    // remove the mark
                    _nodesWithUnsolvedRightTree.Remove(node);
                }

                _node = _stack.Pop();

                return true;
            }

            public override void Reset()
            {
                _node = null;
                _stack.Clear();
                _nodesWithUnsolvedRightTree.Clear();
                //add all the nodes to the stack
                ResolveToBottomLeft(_bst.GetRoot());
                
            }

            // dig to the lowest element in the left subtree, 
            //and if a node has 2 children, chose the left one, and mark the right one as not resolved
            private void ResolveToBottomLeft(Node<T> node)
            {
                while (node != null)
                {   
                    _stack.Push(node);
                    if (node.LeftChild != null && node.RightChild != null)
                    {
                        //mark the rightChild as unsolved
                        _nodesWithUnsolvedRightTree.Add(node);
                        node = node.LeftChild;
                    }
                    else if(node.LeftChild != null)
                        node = node.LeftChild;
                    else
                        node = node.RightChild;
                }
            }


            public override void Dispose()
            {
                base.Dispose();
                _nodesWithUnsolvedRightTree = null;
            }
        }

        // Breath First Search
        private class LeverOrderEnumerater : QueueEnumeratorBase
        {
            public LeverOrderEnumerater(BinarySearchTree<T> bst) : base(bst)
            {
                this.Reset();
            }

            public override bool MoveNext()
            {
                // all items were processed, or the queue is empty
                if (_queue.Count == 0)
                {
                    return false;
                }

                _node = _queue.Dequeue();
                if(_node.LeftChild != null)
                    _queue.Enqueue(_node.LeftChild);
                if(_node.RightChild != null)
                    _queue.Enqueue(_node.RightChild);

                return true;
            }
            
            public override void Reset()
            {
                _queue.Clear();
                
                var root = _bst.GetRoot();
                if (root != null)
                    _queue.Enqueue(root);
                
                _node = null;
            }
        }
    }
}