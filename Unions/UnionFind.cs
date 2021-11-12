using System;
using System.Collections.Generic;

namespace C_InANutShell.Unions
{
    class UnionFind<T>
    {
        Dictionary<T, int> _indexMap;

        //if _set[i] == i -> i is rootNode
        private int[] _set;
        //stores the size for each subSet
        private int[] _subSetSize;
        private bool _usePathCompression;
        private int _componentsCounts;

        public int ComponentsCount => _componentsCounts;
        public UnionFind(T[] items, bool usePathCompression = true)
        {
            if (items == null)
            {
                throw new NullReferenceException("items cannot be null");
            }

            _usePathCompression = usePathCompression;
            _componentsCounts = items.Length;

            _indexMap = new Dictionary<T, int>(items.Length);
            _set = new int[items.Length];
            _subSetSize = new int[items.Length];

            for (int i = 0; i < items.Length; i++)
            {
                _indexMap.Add(items[i], i);
                _set[i] = i;
                _subSetSize[i] = 1;
            }
        }

        //union the 2 disjoints sets
        public void Union(T item1, T item2)
        {
            //if already in the same Group, return, so that a cycle would not be created
            if (this.Find(item1, item2))
            {
                return;
            }

            //assume smaller is set containing item2
            var biggerSubSetRoot = FindRoot(_indexMap[item1]);
            var smallerSubSetRoot = FindRoot(_indexMap[item2]); 

            //correct if item2 < item1
            if (GetSubSetSize(smallerSubSetRoot) > GetSubSetSize(biggerSubSetRoot))
            {
                int temp = smallerSubSetRoot;
                smallerSubSetRoot = biggerSubSetRoot;
                biggerSubSetRoot = temp;
            }

            // set smallerRoot to poin to biggerRoot
            _set[smallerSubSetRoot] = biggerSubSetRoot;

            if (_usePathCompression)
            {
                // [support for path compression], so setting the both item1, and item2 to point to the root of the bigger sub set;
                _set[_indexMap[item1]] = _set[_indexMap[item2]] = biggerSubSetRoot;
            }

            //update the size
            _subSetSize[biggerSubSetRoot] += GetSubSetSize(smallerSubSetRoot);
            _subSetSize[smallerSubSetRoot] = 0;
            
            _componentsCounts--;
        }

        //returns true if item1 is connected to item2
        public bool Find(T item1, T item2)
        {
            return FindRoot(_indexMap[item1]) == FindRoot(_indexMap[item2]);
        }

        private int FindRoot(int itemIndex)
        {
            //if root return itself
            if (_set[itemIndex] == itemIndex)
            {
                return itemIndex;
            }
            if (_usePathCompression)
            {
                // [support for path compression], so if the item is not the root, setting the item to the root of it's parent
                _set[itemIndex] = _set[_set[itemIndex]];
            }
            //else find root of parent
            return FindRoot(_set[itemIndex]);
        }
        
        private int GetSubSetSize(int rootIndex)
        {
            return _subSetSize[rootIndex];
        }
        
    }      
}