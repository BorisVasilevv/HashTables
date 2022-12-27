using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HashTables
{
    class Node<TKey,TValue>
    {
        

        public TKey Key { get; private set; }

        public TValue? Value { get; set; }

        public Node(TKey key, TValue? value)
        {
            this.Key = key;
            this.Value = value;
        }
    }
}
