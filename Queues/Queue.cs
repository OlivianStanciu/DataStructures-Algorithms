using System;
using System.Collections.Generic;
using System.Text;
using C_InANutShell.LinkedLists;

namespace C_InANutShell.Queues
{
    public class Queue<T>
    {
        DoublyLinkedList<T> _linkedList;
        
        public int Count => _linkedList.Size;
        public Queue()
        {
            _linkedList = new DoublyLinkedList<T>();
            LinkedList<T> l = new LinkedList<T>();
        }

        public bool IsEmpty() 
        {
            return _linkedList.IsEmpty();
        }

        public bool Contains(T item)
        {
            if (this.IsEmpty())
                throw new NullReferenceException("Cannot search an item in an empty queue");

            return _linkedList.Contains(item);
        }
        
        public void Enqueue(T item)
        {
            //add at tail
            _linkedList.AddAtIndex(this.Count, item);
        }

        public T Dequeue()
        {
            if (this.IsEmpty())
                throw new NullReferenceException("Cannot dequeue an item from an empty queue");

            //remove from head
            return _linkedList.RemoveAtIndex(0);
        }

        public T Peek()
        {
            if (this.IsEmpty())
                throw new NullReferenceException("Cannot peek an item in an empty queue");

            //peek at tail
            return _linkedList.PeekAtIndex(this.Count);
        }

        public override string ToString()
        {
            StringBuilder sb = new StringBuilder("<-{");
            string delimiter = ", ";
            for (int i = 0; i < this.Count; i++)
            {
                sb.Append(_linkedList.PeekAtIndex(i).ToString() + delimiter);
            }
            if (!this.IsEmpty())
            {
                sb.Remove(sb.Length - delimiter.Length, delimiter.Length);
            }
            sb.Append("}<-");
            return sb.ToString();
        }
    }
}