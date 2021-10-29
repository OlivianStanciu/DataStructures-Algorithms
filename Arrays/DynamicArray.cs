using System;
using System.Collections;
using System.Collections.Generic;

namespace C_InANutShell.Arrays
{
    public class DynamicArray<T>
    {
        private T[] _array;
        private int _length = 0;
        public int Length => _length;
        public bool IsEmpty => (_length > 0) ? true : false;
        public DynamicArray() : this(1)
        {
        }
        public DynamicArray(int capacity)
        {
            if (capacity <= 0)
                throw new ArgumentOutOfRangeException("The capacity should be a positive integer greater than 0");

            _array = new T[capacity];
        }

        private void DoubleSize()
        {
            T[] newArr = new T[_length * 2];
            for (int i = 0; i < _array.Length; i++)
            {
                newArr[i] = _array[i];
            }
            _array = newArr;
        }      
        public T Get(int index)
        {
            if (index < 0 || index >= _length)
            {
                throw new ArgumentOutOfRangeException("Index should be greater or equal to 0 and lower that Length");
            }
            
            return _array[index];
        }
        
        public void Set(T item, int index)
        {
            if (index < 0 || index >= _length)
            {
                throw new ArgumentOutOfRangeException("Index should be greater or equal to 0 and lower that Length");
            }
            
            _array[index] = item;
        }
        public void Append(T item)
        {   
            if (_length == _array.Length)
            {
                DoubleSize();
            }

            _array[_length++] = item;
        }
        public void Append(T[] items)
        {
            if ((items?.Length ?? 0) == 0)
            {
                throw new ArgumentNullException("items can not be null or empty");
            }

            foreach (var item in items)
            {
                Append(item);
            }
        }
        public int IndexOf(T item)
        {
            for (int i = 0; i < _length; i++)
            {
                if (_array[i].Equals(item))
                {
                    return i;
                }
            }
            return -1;
        }
        public bool Contains(T item)
        {
            return (IndexOf(item) >= 0) ? true : false;
        }

        public void Remove(int index) 
        {
            if (index < 0 || index >= _length)
            {
                throw new ArgumentOutOfRangeException("Index should be greater or equal to 0 and lower that Length");
            }
            
            for (int i = index; i < _length - 1; i++)
            {
                _array[index] = _array[index + 1];
            }
            _length--;
        }

        public void Clear()
        {
            for (int i = 0; i < _length; i++)
            {
                _array[i] = default(T);
            };
            _length = 0;
        }

        #region Iterators
        public T this [int index]
        {
            get => this.Get(index);
            set => this.Set(value, index); 
        }
        #endregion

        public override string ToString()
        {
            return base.ToString();
        }
    }
}