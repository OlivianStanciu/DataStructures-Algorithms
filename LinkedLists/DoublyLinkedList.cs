using System;
using System.Collections.Generic;
using System.Text;

namespace C_InANutShell.LinkedLists
{
    internal class Node<T>
    {
        public T Data { get; set; }
        public Node<T> Previous {get; set;}
        public Node<T> Next {get; set;}
        
        public Node(T data, Node<T> prev, Node<T> next)
        {
            this.Data = data;
            this.Next = next;
            this.Previous = prev;
        }
        public Node()
        {
        }
    }
    public class DoublyLinkedList<T>
    {
        private int _size = 0;
        public int Size { get => _size;}
        private Node<T> Head;
        private Node<T> Tail;
        public DoublyLinkedList()
        {
            this.Head = null;
            this.Tail = null;
        }

        public void Clear()
        {
            var currentNode = this.Head;
            while (currentNode != null)
            {
                var nextNode = currentNode.Next;
                currentNode.Data = default(T);
                currentNode.Previous = currentNode.Next = null;
                currentNode = nextNode;
            }
            this.Head = this.Tail = null;
            _size = 0;
        }
        
        public bool IsEmpty()
        {
            return (this.Size == 0);
        }

        private Node<T> GetNodeAtIndex(int index)
        {
            if (index < 0 || (!this.IsEmpty() && index >= _size))
            {
                throw new ArgumentOutOfRangeException("Index should be greater or equal to 0 and lower that Size");
            }
            if (this.IsEmpty())
            {
                throw new NullReferenceException("Cannot get a node from an empty list");
            }
            
            Node<T> nodeAtIndex;
            if (index < _size / 2) //earch from Head
            {
                nodeAtIndex = this.Head;
                for (int tempIndex = 0; tempIndex != index; tempIndex++)
                {
                    nodeAtIndex = nodeAtIndex.Next;
                }
            }
            else //search from Tail
            {
                nodeAtIndex = this.Tail;
                for (int tempIndex = _size - 1; tempIndex != index; tempIndex--)
                {
                    nodeAtIndex = nodeAtIndex.Previous;
                }
            }

            return nodeAtIndex;
        }
        public T Get(int index) =>
            GetNodeAtIndex(index).Data;

        public bool Contains(T item)
        {
            if (this.IsEmpty())
            {
                return false;
            } else
            {
                if (this.Tail.Data.Equals(item))
                    return true;

                for (Node<T> node = this.Head; node.Next != null; node = node.Next)
                {
                    if (node.Data.Equals(item))
                        return true;
                }
            }
            return false;
        }
        public void AddAtIndex(int index, T item)
        {
            if (index < 0 || index > _size)
            {
                throw new ArgumentOutOfRangeException("Index should be greater or equal to 0 and lower that Size");
            }

            if (this.IsEmpty())
            {
                this.Head = this.Tail = new Node<T>(data: item, prev: null, next: null);
            }
            else
            {
                if (index == 0) //if is a new head
                {
                    Node<T> oldHead = this.Head;
                    this.Head = new Node<T>(data: item, prev: null, next: oldHead);
                    oldHead.Previous = this.Head;
                }
                else if(index == this.Size) //if is a new tail
                {
                    Node<T> oldTail = this.Tail;
                    this.Tail = new Node<T>(data: item, prev: oldTail, next: null);
                    oldTail.Next = this.Tail;
                }
                else //is a new node between head and tail
                {
                    Node<T> currentNode = GetNodeAtIndex(index);
                    var newNode = new Node<T>(data: item, prev: currentNode.Previous, next: currentNode);
                    currentNode.Previous.Next = newNode;
                    currentNode.Previous = newNode;
                }
            }

            _size++;
        }

        public void AddAtIndex(int index, IEnumerable<T> items)
        {
            if (index < 0 || index > _size)
            {
                throw new ArgumentOutOfRangeException("Index should be greater or equal to 0 and lower that Size");
            }
            if (items == null)
            {
                throw new ArgumentNullException("Parameter items can not be null or empty");
            }
            IEnumerator<T> enumerator = items.GetEnumerator();
            while (enumerator.MoveNext())
            {
                this.AddAtIndex(index, enumerator.Current);
                index++;
            }
        }

        public void AddAtIndex(int index, params T[] items) => 
            this.AddAtIndex(index, (IEnumerable<T>)items);

        public T RemoveAtIndex(int index)
        {
            if (index < 0 || (!this.IsEmpty() && index >= _size))
            {
                throw new ArgumentOutOfRangeException("Index should be greater or equal to 0 and lower that Size");
            }
            if (this.IsEmpty())
            {
                throw new NullReferenceException("Cannot remove from an empty list");
            }

            T result;
            if (this.Size == 1) //head & tail
            {
                result = this.Head.Data;
                this.Head = this.Tail = null;
            }
            else
            {
                if (index == 0) //head
                {
                    result = this.Head.Data;
                    this.Head = this.Head.Next;
                    this.Head.Previous = null;
                }
                else if(index == this.Size - 1) //tail
                {
                    result = this.Tail.Data;
                    this.Tail = this.Tail.Previous;
                    this.Tail.Next = null;
                }
                else //middle
                {
                    Node<T> currentNode = GetNodeAtIndex(index);
                    result = currentNode.Data;

                    currentNode.Previous.Next = currentNode.Next;
                    currentNode.Next.Previous = currentNode.Previous;
                    currentNode = null;
                }
            }

            _size--;
            return result;
        }

        public T PeekAtIndex(int index)
        {
            return this.GetNodeAtIndex(index).Data;
        }

        public override string ToString()
        {
            StringBuilder sb = new StringBuilder();
            string delimiter = " <-> ";
            for (Node<T> node = this.Head;
                node != null; 
                node = node.Next)
            {
                sb.Append($"{node.Data.ToString()}{delimiter}");
            }

            sb.Remove(sb.Length - delimiter.Length, delimiter.Length);
            return sb.ToString();
        }
    }
}