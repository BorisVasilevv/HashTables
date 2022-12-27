using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HashTables
{
    class HashTableWithChains<TKey, TValue>
    {

        private readonly int _size;

        const double CoefStartAugmentation = 0.75;


        double NowCoefOfFull() => (double)Count / _size;

        public int Count { get; private set; }

        Func<object, int, int> GetHash;

        private  LinkedList<Node<TKey, TValue?>>?[] _items;

        public HashTableWithChains(HashFuncType hashFuncType)
        {
            _size = 1000;
            GetHash = HashFunc.GetHashFunc(hashFuncType);
            _items = new LinkedList<Node<TKey, TValue?>>?[_size];

            Count = 0;
        }

        public void Add(TKey key, TValue value)
        {
            if (key == null) throw new ArgumentNullException(nameof(key));
            Node<TKey, TValue?> item = new Node<TKey, TValue?>(key, value);
            Insert(item);
        }


        public void Insert(Node<TKey, TValue> item)
        {
            int arrayIndex = GetHash(item.Key, _size);

            LinkedList<Node<TKey, TValue?>> list = _items[arrayIndex];

            foreach(Node<TKey, TValue?> pair in list)
            {
                if(item.Key.Equals(pair.Key)) throw new ArgumentException("Элемент по указанному ключу уже существует.");
            }

            list.AddLast(item);
            Count++;
        }


        public void SetValue(TKey key, TValue value)
        {
            int arrayIndex = GetHash(key, _size);
            LinkedList<Node<TKey, TValue?>> list = _items[arrayIndex];

            foreach (Node<TKey, TValue?> pair in list)
            {
                if (key.Equals(pair.Key))
                {
                    pair.Value = value;
                }
            }

        }

        


       



    }
}
