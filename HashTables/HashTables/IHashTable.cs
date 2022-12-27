using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HashTables
{
    interface IHashTable<TKey, TValue>
    {
        public int Count { get; }

        public TValue this[TKey key] { get; set; }

        public TValue GetValue(TKey key);

        public void SetValue(TKey key, TValue value);

        public void Remove(TKey key);

        public void Add(TKey key, TValue value);
    }
}
