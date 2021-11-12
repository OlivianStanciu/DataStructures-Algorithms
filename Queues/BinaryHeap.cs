using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using C_InANutShell.Arrays;

namespace C_InANutShell.Queues
{
    public enum HeapType
    {
        MinHeap = 1,
        MaxHeap = -1
    }


    /// <summary>
    /// A heap is a tree based data structure; Tree is a graph without cycles
    /// Is a complete binary tree, meaning that all nodes (except the last), is completely filled (has 2 children) and all the nodes are as far left as possible
    /// Satisfies the heap invariant (If A is a parent node of B, than A is ordered with respect to B, for all nodes A, B in the heap)
    /// </summary>
    //implemented as a MinHeap, but if MaxHeap, than the Comparer can be negated (* -1)
    class BinaryHeap<T> where T : IComparable
    {
        //stores the items of the heap
        DynamicArray<T> _tree;
        //stores the item, and the indexes where the item is found
        Dictionary<T, HashSet<int>> _indexMap;
        HeapType _heapType;

        public BinaryHeap() : this(HeapType.MinHeap) {}

        public BinaryHeap(HeapType heapType)
        {
            _tree = new DynamicArray<T>();
            _indexMap = new Dictionary<T, HashSet<int>>();
            _heapType = heapType;
        }

        public bool IsEmpty() => _tree.Length == 0;
        public int Size => _tree.Length;


        //the result would be the same, only if using (-1) 
        //for both childs stored at parentIndex * 2 + 1 / +2,
        private int GetParentIndex(int childIndex) => (childIndex - 1) / 2;
        private int GetLeftChildIndex(int parentIndex) => parentIndex * 2 + 1;
        private int GetLeftRightIndex(int parentIndex) => parentIndex * 2 + 2; 


        public void Insert(T item)
        {
            _tree.Append(item);
            int itemIndex = this.Size - 1;

            if (this.Contains(item))
            {
                _indexMap[item].Add(itemIndex);
            }
            else
            {
                _indexMap[item] = new HashSet<int>();
                _indexMap[item].Add(itemIndex);
            }
                
            if (itemIndex > 0)
                Swim(itemIndex);

        }

        //remove root node
        public T Poll()
        {
           if(this.IsEmpty())
                throw new NullReferenceException("Cannot poll from an empty heap");
            T item = _tree[0];
            this.Delete(_tree[0]);
            return item;
        }
        public bool Delete(T item)
        {
            if(this.IsEmpty())
                throw new NullReferenceException("Cannot delete from an empty heap");
            if (!this.Contains(item))
                return false;

            if (this.Size == 1)
            {
                _tree.Remove(0);
                _indexMap.Remove(item);
                return true;
            }


            int itemIndex = _indexMap[item].First();
            SwapItemsAtIndexes(itemIndex, this.Size - 1);

            //delete the last node
            _tree.Remove(this.Size - 1);
            _indexMap[item].Remove(itemIndex);
            if (_indexMap[item].Count == 0)
                _indexMap.Remove(item);
            
            Swim(itemIndex);
            Sink(itemIndex);
            
            return true;
        }

        public bool Contains(T item)
        {
            return _indexMap.ContainsKey(item);
        }
        public T Peek()
        {
            if(this.IsEmpty())
                throw new NullReferenceException("Cannot peek in an empty heap");
            
            return _tree[0]; 
        }

        private void SwapItemsAtIndexes(int index1, int index2)
        {
            _indexMap[_tree[index1]].Remove(index1);
            _indexMap[_tree[index1]].Add(index2);
            _indexMap[_tree[index2]].Remove(index2);    
            _indexMap[_tree[index2]].Add(index1);

            T temp = _tree[index1];
            _tree[index1] = _tree[index2];
            _tree[index2] = temp;

        }

        private bool HasChildAtIndex(int parentIndex, int childIndex)
        {
            return GetParentIndex(childIndex) == parentIndex && childIndex < this.Size ? true : false;
        }

        private void Swim(int itemIndex)
        {
            int parentIndex = GetParentIndex(itemIndex);

            if (itemIndex == 0 || Compare(_tree[itemIndex], _tree[parentIndex]) >= 0)
                return;
            
            SwapItemsAtIndexes(itemIndex, parentIndex);
            Swim(parentIndex);
        }
        private void Sink(int itemIndex)
        {
            int leftChildIndex = GetLeftChildIndex(itemIndex);
            int rightChildIndex = GetLeftRightIndex(itemIndex);

            //if item is last index
            if (itemIndex == this.Size - 1)
                return;
            
            //assume that the smallest is leftChild
            int smallestChildIndex = leftChildIndex;
            //correct the assumption if has rightChild && rightChild > leftChild
            if (HasChildAtIndex(itemIndex, rightChildIndex) && 
                Compare(_tree[rightChildIndex], _tree[leftChildIndex]) < 0)
            {
                smallestChildIndex = rightChildIndex;
            }
            //stop if does not have a leftChild (so cannot sink anymore)
            //  (implicitly not a rightChild as well since rightChildIndex < leftChildIndex)
            //or if item <= lefChildIndex
            if (!HasChildAtIndex(itemIndex, leftChildIndex) ||
                Compare(_tree[itemIndex], _tree[leftChildIndex]) <= 0)
            {
                return;
            }

            SwapItemsAtIndexes(itemIndex, smallestChildIndex);
            Sink(smallestChildIndex);


            ////SecondImplementation
            ////item does not have children or item <= left && item <= right
            // if ((!HasChildAtIndex(itemIndex, leftChildIndex) || 
            //         Compare(_tree[itemIndex], _tree[leftChildIndex]) <= 0) &&
            //     (!HasChildAtIndex(itemIndex, rightChildIndex) || 
            //         Compare(_tree[itemIndex], _tree[rightChildIndex]) <= 0))
            //     return;
            
            // if (HasChildAtIndex(itemIndex, leftChildIndex) && 
            //     HasChildAtIndex(itemIndex, rightChildIndex) && 
            //     Compare(_tree[leftChildIndex], _tree[rightChildIndex]) <= 0)
            // {
            //     SwapItemsAtIndexes(itemIndex, leftChildIndex);
            //     Sink(leftChildIndex);
            // } 
            // else if (HasChildAtIndex(itemIndex, rightChildIndex))
            // {
            //     SwapItemsAtIndexes(itemIndex, rightChildIndex);
            //     Sink(rightChildIndex);
            // }
            // else
            // {
            //     SwapItemsAtIndexes(itemIndex, leftChildIndex);
            //     Sink(leftChildIndex);
            // }
        }

        private int Compare(T item1, T item2)
        {
            //negate the comparer if MaxHeap is selected
            return (int)_heapType * item1.CompareTo(item2);
        }

        public override string ToString()
        {
            StringBuilder sb = new StringBuilder();
            ConstructFlatStringvisualization(0, ref sb);
            string flatVisualisation = sb.ToString();
            // flatVisualisation.TrimStart('/');
            // flatVisualisation.TrimEnd('\\');
            // var items = flatVisualisation.Split(new char[] {'/', '\\'}, StringSplitOptions.RemoveEmptyEntries);
            
            // sb.Clear();
            
            // int depth = 1;
            // for (int index = this.Size - 1; index > 0; index = GetParentIndex(index))
            // {
            //     depth++;
            // }

            // //print flatvisualisation for each depth, but only the elements contained in that depth 
            // while(depth > 0)
            // {
            //     int nrOfElemsPerDepth = (int)Math.Pow(2, depth - 1);
            //     for (int i = 0; i < length; i++)
            //     {
                    
            //     }
            //     depth--;
            // }

            return flatVisualisation;
        }

        private void ConstructFlatStringvisualization(int rootIndex, ref StringBuilder sb) 
        {
            int leftChildIndex = rootIndex * 2 + 1;
            int rightChildIndex = rootIndex * 2 + 2;

            sb.Append("[");

            if (HasChildAtIndex(rootIndex, leftChildIndex))
            {
                ConstructFlatStringvisualization(leftChildIndex, ref sb);
            }

            sb.Append($" {_tree[rootIndex]} ");

            if (HasChildAtIndex(rootIndex, rightChildIndex))
            {
                ConstructFlatStringvisualization(rightChildIndex, ref sb);
            }

            sb.Append("]");
            
        }
    }
}