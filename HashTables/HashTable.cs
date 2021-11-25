using System;
using System.Collections.Generic;

namespace C_InANutShell.HashTables
{
    // TODO: Implement functionality for HAshTable, and maybe separate chaining & open addressing
    static class HashTableDefaults
    {
        public static readonly int Capacity = 4;
        public static readonly double LoadFactorResizePercentage = 0.75;
    }

    class HashItem<Tkey, TVal>
    {
        public Tkey Key { get; set; }
        public TVal Value {get; set;}
        public int Hash { get; set; }
    }

    public class HashTable<TKey, TVal>
    {   
        private int _size, _capacity;
        
        // determines at which percentage the array should be resized
        private double _loadFactor;

        // _capacity * _loadFactor
        private double _resizeThreshold => _capacity * _loadFactor;

        private LinkedList<HashItem<TKey, TVal>>[] _hashTable;

        public HashTable() : this(HashTableDefaults.Capacity, HashTableDefaults.LoadFactorResizePercentage)
        {
        }
        public HashTable(int capacity) : this(capacity, HashTableDefaults.LoadFactorResizePercentage)
        {
        }
        public HashTable(int capacity, double loadFactorResizePercentage)
        {
            if (capacity <= 0)
                throw new ArgumentException("Capacity cannot be smaller or equal to 0");
            if(loadFactorResizePercentage <= 0 || loadFactorResizePercentage > 1)
                throw new ArgumentException("Load factor must be adouble between (0, 1), exclusive");

            _size = 0;
            _capacity = capacity > HashTableDefaults.Capacity ? capacity : HashTableDefaults.Capacity;
            _loadFactor = loadFactorResizePercentage;

            _hashTable = new LinkedList<HashItem<TKey, TVal>>[_capacity];
        }

        public int Size => _size;

        public bool IsEmpty() => _size == 0; 
        
        public bool ContainsKey(TKey key) => GetHashItemForKey(key) != null;

        public bool Contains(TKey key) => ContainsKey(key);

        private HashItem<TKey, TVal> GetHashItemForKey(TKey key)
        {
            if(this.IsEmpty())
                return null;

            var keyHashIndex = ComputeHashIndexFromKey(key);
            var linkedList = _hashTable[keyHashIndex];

            if(linkedList == null || linkedList.Count == 0)
                return null;

            var linkedListEnumerator = linkedList.GetEnumerator();
            while(linkedListEnumerator.MoveNext())
            {
                if(linkedListEnumerator.Current.Key.Equals(key))
                {
                    return linkedListEnumerator.Current;
                }
            }

            return null;
        }

        public void Add(TKey key, TVal value)
        {
            if(key == null)
                throw new NullReferenceException("Key cannot be null");

            if(this.Contains(key))
                throw new ArgumentException("The HashMap already contains the provided key");

            // if the treshold is met, double the capacity
            
            if (_size >= _resizeThreshold)
                DoubleHashTableCapacity();
            
            int hash = ComputeHash(key);
            int hashIndex = ComputeHashIndex(hash);
            
            // lazy initialization
            if(_hashTable[hashIndex] == null)
                _hashTable[hashIndex] = new LinkedList<HashItem<TKey, TVal>>();
            
            //check if contains

            _hashTable[hashIndex].AddLast(new HashItem<TKey, TVal>()
            {
                Key = key,
                Value = value,
                Hash = hash
            });

            _size++;
        }

        public TVal Remove(TKey key)
        {
            if(key == null)
                throw new NullReferenceException("Key cannot be null");
            if(this.IsEmpty())
                throw new InvalidOperationException("Cannot remove from an empty HashTable");

            var hashItem = GetHashItemForKey(key);

            if(hashItem == null)
                throw new ArgumentException("The HashMap does not contain the provided key");

            var result = hashItem.Value;
            var keyHashIndex = ComputeHashIndex(hashItem.Hash);

            // remove the hashItem from the linked list
            _hashTable[keyHashIndex].Remove(hashItem);

            // if the linked list remained empty, set it to null
            if(_hashTable[keyHashIndex].Count == 0)
                _hashTable[keyHashIndex] = null;

            _size--;

            return result;
        }
        
        public TVal Get(TKey key)
        {
            HashItem<TKey, TVal> hashItem = GetHashItemForKey(key);

            if(hashItem == null)
                throw new ArgumentException("The HashMap does not contain the provided key");

            return hashItem.Value;
        }

        private int ComputeHash(TKey key)
        {
            // compute object's hash;
            return key.GetHashCode();
        }
        private int ComputeHashIndex(int hash)
        {
             // strip the negative sign; since an int is 8 bytes, just negate the first bit (0111 1111 1111...) => 7FFFFFF
            hash = hash & 0x7FFFFFF;
            // convert to interval [0, capacity], inclusive
            hash = hash % _capacity;
            return hash;
        }
        private int ComputeHashIndexFromKey(TKey key)
        {
            int hash = ComputeHash(key);
            return ComputeHashIndex(hash);
        }

        private void DoubleHashTableCapacity()
        {
            _capacity = _hashTable.Length * 2;
            _size = 0;

            var currentHashTable = _hashTable;
            _hashTable = new LinkedList<HashItem<TKey, TVal>>[_capacity];
            
            foreach (var linkedList in currentHashTable)
            {
                if(linkedList == null)
                    continue;
                
                var linkedListEnumerator = linkedList.GetEnumerator();
                while(linkedListEnumerator.MoveNext())
                {
                    this.Add(linkedListEnumerator.Current.Key, linkedListEnumerator.Current.Value);
                }
            }
        }

        #region Indexer
        public TVal this[TKey key]
        {
            get => this.Get(key);
        }
        #endregion
    }
}