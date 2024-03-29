﻿using System;
using System.Collections;
using System.Diagnostics.CodeAnalysis;

namespace HashTableLib
{
    public class OpenAddressHashTable<TKey, TValue> : IEnumerable<KeyValuePair<TKey, TValue>>, IDictionary<TKey, TValue> where TKey : IEquatable<TKey>
    {
        Pair<TKey, TValue>[] _table;
        private int _capacity;
        HashMaker<TKey> _hashMaker1, _hashMaker2;
        public int Count { get; private set; }

        public ICollection<TKey> Keys => throw new NotImplementedException();

        public ICollection<TValue> Values => throw new NotImplementedException();

        public bool IsReadOnly { get; private set; }

        private const double FillFactor = 0.65;
        private readonly GetPrimeNumber _primeNumber = new GetPrimeNumber();

        public OpenAddressHashTable()
        {
            _capacity = _primeNumber.GetMin();
            _table = new Pair<TKey, TValue>[_capacity];
            _hashMaker1 = new HashMaker<TKey>(_capacity);
            _hashMaker2 = new HashMaker<TKey>(_capacity - 1);
            Count = 0;
        }
        public OpenAddressHashTable(int m)
        {
            _table = new Pair<TKey, TValue>[m];
            _capacity = m;
            _hashMaker1 = new HashMaker<TKey>(_capacity);
            _hashMaker2 = new HashMaker<TKey>(_capacity - 1);
            Count = 0;
        }
        public void Add(TKey key, TValue value)
        {
            var hash = _hashMaker1.ReturnHash(key);

            if (!TryToPut(hash, key, value))
            {
                int iterationNumber = 1;
                while (true)
                {
                    var place = (hash + iterationNumber * (1 + _hashMaker2.ReturnHash(key))) % _capacity;
                    if (TryToPut(place, key, value))
                        break;
                    iterationNumber++;
                    if (iterationNumber >= _capacity)
                        throw new ApplicationException("HashTable full!!!");
                }
            }
            if ((double)Count / _capacity >= FillFactor)
            {
                IncreaseTable();
            }
        }

        private bool TryToPut(int place, TKey key, TValue value)
        {
            if (_table[place] == null || _table[place].IsDeleted())
            {
                _table[place] = new Pair<TKey, TValue>(key, value);
                Count++;
                return true;
            }
            if (_table[place].Key.Equals(key))
            {
                throw new ArgumentException();
            }
            return false;
        }

        private Pair<TKey, TValue> Find(TKey x)
        {
            var hash = _hashMaker1.ReturnHash(x);
            if (_table[hash] == null)
                return null;
            if (!_table[hash].IsDeleted() && _table[hash].Key.Equals(x))
            {
                return _table[hash];
            }
            int iterationNumber = 1;
            while (true)
            {
                var place = (hash + iterationNumber * (1 + _hashMaker2.ReturnHash(x))) % _capacity;
                if (_table[place] == null)
                    return null;
                if (!_table[place].IsDeleted() && _table[place].Key.Equals(x))
                {
                    return _table[place];
                }
                iterationNumber++;
                if (iterationNumber >= _capacity)
                    return null;
            }
        }
        public TValue this[TKey key]
        {
            get
            {
                var pair = Find(key);
                if (pair == null)
                    throw new KeyNotFoundException();
                return pair.Value;
            }

            set
            {
                var pair = Find(key) ?? throw new KeyNotFoundException();
                pair.Value = value;
            }
        }

        private void IncreaseTable()
        {
            if ((int)_primeNumber == _primeNumber.Count - 1)
            {
                throw new ArgumentException();
            }

            _capacity = _primeNumber.Next();

            var tempTable = _table;

            _table = new Pair<TKey, TValue>[_capacity];

            _hashMaker1 = new HashMaker<TKey>(_capacity);
            _hashMaker2 = new HashMaker<TKey>(_capacity - 1);

            Count = 0;

            foreach (var pair in tempTable)
            {
                if (pair != null && !pair.IsDeleted())
                {
                    Add(pair.Key, pair.Value);
                }
            }
        }

        public bool ContainsKey(TKey key)
        {
            return Find(key) != null;
        }

        public IEnumerator<KeyValuePair<TKey, TValue>> GetEnumerator()
        {
            return (from pair in _table where pair != null && !pair.IsDeleted() select new KeyValuePair<TKey, TValue>(pair.Key, pair.Value)).GetEnumerator();
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return GetEnumerator();
        }

        public bool Remove(TKey key)
        {
            var item = Find(key);

            if (item == null)
            {
                throw new NullReferenceException();
            }

            item.DeletePair();
            Count--;
            return true;
        }

        public bool TryGetValue(TKey key, [MaybeNullWhen(false)] out TValue value)
        {
            if(key  == null)
            {
                throw new NullReferenceException();
            }

            if (Find(key) != null)
            {
                value = Find(key).Value;
                return true;
            }

            value = default;
            return false;
        }

        public void Add(KeyValuePair<TKey, TValue> item)
        {
            Add(item.Key, item.Value);
        }

        public void Clear()
        {
            _capacity = _primeNumber.GetMin();
            _table = new Pair<TKey, TValue>[_capacity];
            _hashMaker1 = new HashMaker<TKey>(_capacity);
            _hashMaker2 = new HashMaker<TKey>(_capacity - 1);
            Count = 0;
        }

        public bool Contains(KeyValuePair<TKey, TValue> item)
        {
            return ContainsKey(item.Key);
        }

        public void CopyTo(KeyValuePair<TKey, TValue>[] array, int arrayIndex)
        {
            if (array == null)
            {
                throw new NullReferenceException();
            }

            if (arrayIndex < 0 || arrayIndex > array.Length)
            {
                throw new ArgumentOutOfRangeException();
            }

            if (array.Length - arrayIndex < Count)
            {
                throw new ArgumentException();
            }

            for (int i = 0; i < _table.Length; i++)
            {
                if (_table[i] is not null)
                {
                    array[arrayIndex++] = new KeyValuePair<TKey, TValue>(_table[i].Key, _table[i].Value);
                }
            }
        }

        public bool Remove(KeyValuePair<TKey, TValue> item)
        {
            return Remove(item.Key);
        }
    }
}
