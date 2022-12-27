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

        KeyValuePair<TKey, TValue?>[] _items;

        double NowCoefOfFull() => (double)Count / _size;

        public int Count{ get; private set; }

        Func<object, int, int> GetHash;

        

        HashFuncType _hashType;

        
    }
}
