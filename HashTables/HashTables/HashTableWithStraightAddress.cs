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
            
        public int GetHashStep()
        {

            return 0;
        }


    }
}
