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

 

        public HashTableWithStraightAddress(HashFuncType hashFuncType)
        {
            _size = 10000;
             GetHash = HashFunc.GetHashFunc(hashFuncType);
            _items = new KeyValuePair<TKey, TValue?>[_size];
            _removed = new bool[_size];
            Count = 0;
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




    }
}
