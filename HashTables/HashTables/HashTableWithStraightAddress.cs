using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HashTables
{
    class HashTableWithStraightAddress<TKey,TValue>
    {
        private readonly int _size;

        const double CoefStartAugmentation = 0.75;

        KeyValuePair<TKey, TValue?>[] _items;

        double NowCoefOfFull() => (double)Count / _size;

        public int Count { get; private set; }

        Func<object, int, int> GetHash;

        private bool[] _removed;

        HashFuncType _hashType;

        public HashTableWithStraightAddress(HashFuncType hashFuncType)
        {
            _size = 10000;
            _hashType = hashFuncType;
            _items = new KeyValuePair<TKey, TValue?>[_size];
            _removed = new bool[_size];      
        }


        public int MaxClusterLength()
        {


            int max = 0;
            int current = 0;
            foreach (var item in _items)
            {
                if (!item.Equals(default(KeyValuePair<TKey, TValue>)))
                {
                    current++;
                }
                else
                {
                    max = Math.Max(max, current);
                    current = 0;
                }
            }

            return Math.Max(max, current);

        }


        protected bool CheckUniqueKey(TKey key)
        {
            foreach (var item in _items)
            {
                if (item.Key != null && item.Key.Equals(key)) return false;
            }

            return true;
        }

        protected bool CheckOpenSpace()
        {
            var isOpen = false;
            for (var i = 0; i < _size; i++)
            {
                if (_items[i].Equals(default(KeyValuePair<TKey, TValue?>))) isOpen = true;
            }

            return isOpen;
        }

        public void Add(TKey key, TValue value)
        {
            if (key == null) throw new ArgumentNullException(nameof(key));

            if (!CheckOpenSpace()) throw new ArgumentOutOfRangeException("Хеш-таблица переполнена.");

            if (!CheckUniqueKey(key)) throw new ArgumentException("Элемент по указанному ключу уже существует.");

            Insert(key, value);
        }

        protected void Insert(TKey key, TValue value)
        {
            var index = 0;
            var hashCode = GetHash(key, index);

            while (!_items[hashCode].Equals(default(KeyValuePair<TKey, TValue>)) && !_items[hashCode].Key.Equals(key))
            {
                index++;
                hashCode = GetHash(key, index);
            }

            _items[hashCode] = new KeyValuePair<TKey, TValue?>(key, value);
            _removed[hashCode] = false;
            Count++;
        }


        public TValue? GetValue(TKey key)
        {
            if (key == null) throw new ArgumentNullException(nameof(key));

            var index = 0;
            var hashCode = GetHash(key, index);

            while ((!_items[hashCode].Equals(default(KeyValuePair<TKey, TValue>)) || _removed[hashCode]) && !_items[hashCode].Key.Equals(key))
            {
                index++;
                hashCode = GetHash(key, index);
            }

            return _items[hashCode].Value;
        }

        public bool Remove(TKey key)
        {
            var index = 0;
            var hashCode = GetHash(key, index);

            while ((!_items[hashCode].Equals(default(KeyValuePair<TKey, TValue>)) || _removed[hashCode]) && !_items[hashCode].Key.Equals(key))
            {
                index++;
                hashCode = GetHash(key, index);
            }

            if (_items[hashCode].Equals(default(KeyValuePair<TKey, TValue>)))
            {
                return false;
            }
            else
            {
                _items[hashCode] = default;
                _removed[hashCode] = true;
                Count--;
                return true;
            }
        }
    }
}
