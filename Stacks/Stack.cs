using System;
using System.Text;
using C_InANutShell.LinkedLists;

namespace C_InANutShell.Stacks
{
    public class Stack<T>
    {
        DoublyLinkedList<T> _linkedList;
        public int Count => _linkedList.Size;
        public bool IsEmpty() => _linkedList.IsEmpty();

        public Stack()
        {
            this._linkedList = new DoublyLinkedList<T>();
        }

        public void Push(T item)
        {   
            //add at head
            _linkedList.AddAtIndex(0, item);
        }

        public T Pop()
        {
            if (_linkedList.IsEmpty())
                throw new NullReferenceException("Cannot pop an element from an empty stack");

            //remove from head
            return _linkedList.RemoveAtIndex(0);
        }

        public T Peek()
        {
            if (_linkedList.IsEmpty())
                throw new NullReferenceException("Cannot peek in an empty stack");
            
            //peek at head
            return _linkedList.PeekAtIndex(0);
        }

        public override string ToString()
        {
            StringBuilder sb = new StringBuilder();
            sb.AppendLine("--");
            for (int i = 0; i < _linkedList.Size; i++)
            {
                sb.AppendLine(_linkedList.PeekAtIndex(i).ToString());
            }
            sb.AppendLine("--");
            return sb.ToString();
        }
    }
}