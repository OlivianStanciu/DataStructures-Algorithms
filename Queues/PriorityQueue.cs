using System;

namespace C_InANutShell.Queues
{
    class PriorityQueue<T> where T : IComparable
    {
        BinaryHeap<T> _binaryHeap;
        public PriorityQueue() : this(HeapType.MinHeap) {}

        public PriorityQueue(HeapType type)
        {
            _binaryHeap = new BinaryHeap<T>(type);
        }

        //removes the root element, the one with the highest priority
        ////log(n), because of swim()/sink()
        public T Poll()
        {
            return _binaryHeap.Poll();
        }

        //adds a new item to the priority queue
        //log(n), because of swim()/sink()
        public void Enqueue(T item)
        {
            _binaryHeap.Insert(item);
        }

        //O(1) since we are looking atr the root node
        public T Peek()
        {
            return _binaryHeap.Peek();
        }


        //implementation using a HashSet<T> so that time complexity is contant: O(1)
        public bool Contains(T item)
        {
            return _binaryHeap.Contains(item);
        }

        //implementation using a HashSet<T>, so that the search time complexity for an element is constant O(1), not linear
        //therefore the removal process would be search: O(1) + swim()/sink() which is log(n) => log(n)
        public bool RemoveItem(T item)
        {
            return _binaryHeap.Delete(item);
        }
    }
}