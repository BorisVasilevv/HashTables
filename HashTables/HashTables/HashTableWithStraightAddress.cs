using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HashTables
{
    class HashTableWithStraightAddress<TKey, TValue>
    {
        private readonly int _size;

        Node<TKey, TValue?>[] _items;



        public int Count { get; private set; }

        Func<object, int, int> GetHash;

        private bool[] _removed;



        public HashTableWithStraightAddress(HashFuncType hashFuncType)
        {
            _size = 10000;
            GetHash = HashFunc.GetHashFunc(hashFuncType);
            _items = new Node<TKey, TValue?>[_size];
            _removed = new bool[_size];
            Count = 0;
        }

        public HashTableWithStraightAddress(HashFuncType hashFuncType, int size)
        {
            _size = size;
            GetHash = HashFunc.GetHashFunc(hashFuncType);
            _items = new Node<TKey, TValue?>[_size];
            _removed = new bool[_size];
            Count = 0;
        }

        public int GetHashStep(int hash)
        {

            double m = (Math.Sqrt(5) - 1) / 2;
            int nextIndex = (int)(31 * ((hash * m) % 1));
            return _size % nextIndex == 0
                ? nextIndex * (int)((Math.Sqrt(_size) % 1) - 1)
                : nextIndex;
        }


        public void Add(TKey key, TValue value)
        {
            if (key == null) throw new ArgumentNullException(nameof(key));
            if (IsKeyExsist(key)) throw new ArgumentException("Такой ключ уже существует");
            Node<TKey, TValue?> item = new Node<TKey, TValue?>(key, value);
            Insert(item);
        }


        private void Insert(Node<TKey, TValue?> item)
        {

            int arrayIndex = GetHash(item.Key, _size);


            int step = GetHashStep(arrayIndex);
            while (_items[arrayIndex] != null || _removed[arrayIndex])
            {
                arrayIndex += step;
                if (arrayIndex >= _size) arrayIndex = arrayIndex % _size;
            }
            _items[arrayIndex] = item;
            _removed[arrayIndex] = false;
            Count++;
        }

        private bool IsKeyExsist(TKey key)
        {
            for (int i = 0; i < _items.Length; i++)
            {
                if (_items[i] == null) continue;
                if (_items[i].Key.Equals(key) && !_removed[i]) return true;
            }
            return false;
        }

        public TValue? this[TKey key]
        {
            get => GetValue(key);
            set => SetValue(key, value);
        }


        public TValue? GetValue(TKey key)
        {
            int arrayIndex = GetHash(key, _size);
            if (!IsKeyExsist(key)) throw new ArgumentException("Такого ключа не существует");

            int step = GetHashStep(arrayIndex);
            while (_items[arrayIndex]==null||!_items[arrayIndex].Key.Equals(key))
            {
                do
                {
                    arrayIndex += step;
                    if (arrayIndex >= _size) arrayIndex = arrayIndex % _size;
                } while (_removed[arrayIndex] == true);
            }
            return _items[arrayIndex].Value;
        }

        public void SetValue(TKey key, TValue value)
        {
            int arrayIndex = GetHash(key, _size);
            if (!IsKeyExsist(key)) throw new ArgumentException("Такого ключа не существует");

            int step = GetHashStep(arrayIndex);
            while (!_items[arrayIndex].Key.Equals(key))
            {
                do
                {
                    arrayIndex += step;
                    if (arrayIndex >= _size) arrayIndex = arrayIndex % _size;
                } while (_removed[arrayIndex] == true);
            }

            _items[arrayIndex].Value = value;
        }

        public void Remove(TKey key)
        {
            int arrayIndex = GetHash(key, _size);
            if (!IsKeyExsist(key)) throw new ArgumentException("Такого ключа не существует");

            int step = GetHashStep(arrayIndex);
            while (!_items[arrayIndex].Key.Equals(key))
            {
                do
                {
                    arrayIndex += step;
                    if (arrayIndex >= _size) arrayIndex = arrayIndex % _size;
                } while (_removed[arrayIndex] == true);
            }

            _removed[arrayIndex] = true;
        }


        public int MaxClasterLength()
        {
            int max = 0;
            int current = 0;
            for(int i=0;i<_items.Length;i++)
            {
                if (_items[i] != null && !_removed[i])
                {
                    current++;
                }
                else
                {
                    max = Math.Max(max, current);
                    current = 0;
                }
            }
            max = Math.Max(max, current);
            return max;
        }
    }
}
