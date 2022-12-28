using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HashTables
{
    class HashTableWithStraightAddress<TKey, TValue> : IHashTable<TKey, TValue?>
    {
        private readonly int _size;

        Node<TKey, TValue?>[] _items;



        public int Count { get; private set; }

        Func<object, int, int> GetHash;

        private bool[] _removed;


        readonly StepSearchMethodType _typeFunc;

        public HashTableWithStraightAddress(HashFuncType hashFuncType, StepSearchMethodType typeSearch)
        {
            _size = 10000;
            _typeFunc = typeSearch;
            GetHash = HashFunc.GetHashFunc(hashFuncType);
            _items = new Node<TKey, TValue?>[_size];
            _removed = new bool[_size];
            Count = 0;
        }

        public HashTableWithStraightAddress(HashFuncType hashFuncType, int size, StepSearchMethodType typeSearch)
        {
            _typeFunc = typeSearch;
            _size = size;
            GetHash = HashFunc.GetHashFunc(hashFuncType);
            _items = new Node<TKey, TValue?>[_size];
            _removed = new bool[_size];
            Count = 0;
        }

        public TKey[] GetKeys(int amountOfKeys)
        {
            List<TKey> keys = new List<TKey>();


            foreach (Node<TKey, TValue> node in _items)
            {
                keys.Add(node.Key);
                if (keys.Count >= amountOfKeys) break;
            }


            return keys.ToArray();
        }

        public int GetHashStepQuerty(int hash, int idStep)
        {
            double m = (Math.Sqrt(5) - 1) / 2;
            double n = 2.71828;
            if (hash == 0) return 31;
            int nextIndex = hash + (int)(idStep * m) + (int)(Math.Pow(idStep, 2) * n) % _size;

            return nextIndex;
        }


        private int GetHashStepLinely()
        {
            return 1;
        }

        public int GetHashStepDoubleHash(int hash, int idStep, TKey key)
        {
            
            hash += idStep * HashFunc.FAQ6(key);

            return hash;

        }


        public int GetHGashStep(int hash, int idStep, TKey key)
        {
            if(_typeFunc==StepSearchMethodType.Sqr)
            {
                return GetHashStepQuerty(hash, idStep);
            }
            else if(_typeFunc==StepSearchMethodType.Linear)
            {
                return GetHashStepLinely();
            }
            else
            {
                return GetHashStepDoubleHash(hash, idStep, key);
                //_typeFunc=StepSearchMethodType.DoubleHash
            }            
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
            int idStep = 0;

            int arrayIndex = GetHash(item.Key, _size);



            while (_items[arrayIndex] != null || _removed[arrayIndex])
            {
                int step = GetHGashStep(arrayIndex, idStep, item.Key);
                arrayIndex += step;
                if (arrayIndex >= _size) arrayIndex = arrayIndex % _size;
                idStep++;
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
            int idStep = 0;

            int arrayIndex = GetHash(key, _size);
            if (!IsKeyExsist(key)) throw new ArgumentException("Такого ключа не существует");
            if (Count == _size) throw new Exception("Таблица полность заполнена");

            while (_items[arrayIndex] == null || !_items[arrayIndex].Key.Equals(key))
            {
                do
                {
                    int step = GetHGashStep(arrayIndex, idStep, key); 
                    arrayIndex += step;
                    if (arrayIndex >= _size) arrayIndex = arrayIndex % _size;
                    idStep++;
                } while (_removed[arrayIndex] == true);
            }
            return _items[arrayIndex].Value;
        }

        public void SetValue(TKey key, TValue value)
        {
            int idStep = 0;


            int arrayIndex = GetHash(key, _size);
            if (!IsKeyExsist(key)) throw new ArgumentException("Такого ключа не существует");

            while (_items[arrayIndex] == null || !_items[arrayIndex].Key.Equals(key))
            {
                do
                {
                    int step = GetHGashStep(arrayIndex, idStep, key);

                    arrayIndex += step;
                    if (arrayIndex >= _size) arrayIndex = arrayIndex % _size;
                    idStep++;
                } while (_removed[arrayIndex] == true);
            }

            _items[arrayIndex].Value = value;
        }

        public void Remove(TKey key)
        {
            int idStep = 0;

            int arrayIndex = GetHash(key, _size);
            if (!IsKeyExsist(key)) throw new ArgumentException("Такого ключа не существует");

            while (_items[arrayIndex] == null || !_items[arrayIndex].Key.Equals(key))
            {
                do
                {
                    int step = GetHGashStep(arrayIndex, idStep, key);

                    arrayIndex += step;
                    if (arrayIndex >= _size) arrayIndex = arrayIndex % _size;
                    idStep++;
                } while (_removed[arrayIndex] == true);
            }

            _removed[arrayIndex] = true;
            Count--;
        }


        public int MaxClasterLength()
        {
            int max = 0;
            int current = 0;
            for (int i = 0; i < _items.Length; i++)
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
