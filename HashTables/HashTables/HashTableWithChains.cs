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

        const double CoefFullnessToExctention = 0.75;


        int AmountOfLists = 0;
        double CoefOfFullnessNow { get; set; }

        bool isSizeFull=false;

        private readonly int _not_full_size;

        double NowCoefOfFull() => (double)Count / _size;

        public int Count { get; private set; }

        Func<object, int, int> GetHash { get; set; }

        private LinkedList<Node<TKey, TValue?>>?[] _items;

        public HashTableWithChains(HashFuncType hashFuncType)
        {
            _size = 1000;
            GetHash = HashFunc.GetHashFunc(hashFuncType);
            _items = new LinkedList<Node<TKey, TValue?>>?[_size];
            
            _not_full_size = (int)(_size * CoefFullnessToExctention);
            Count = 0;
        }
       
        public void Add(TKey key, TValue value)
        {
            if (key == null) throw new ArgumentNullException(nameof(key));
            Node<TKey, TValue?> item = new Node<TKey, TValue?>(key, value);
            Insert(item);
        }


        private void Insert(Node<TKey, TValue?> item)
        {

            int arrayIndex = GetHash(item.Key, isSizeFull?_size: _not_full_size);

            LinkedList<Node<TKey, TValue?>> list = GetList(arrayIndex);

            foreach(Node<TKey, TValue?> pair in list)
            {
                if(item.Key.Equals(pair.Key)) throw new ArgumentException("Элемент по указанному ключу уже существует.");
            }

            list.AddLast(item);
            Count++;


        }        

        private LinkedList<Node<TKey,TValue?>> GetList(int index)
        {
            LinkedList<Node<TKey, TValue?>> list = _items[index];
            if(list == null)
            {
                list = new LinkedList<Node<TKey, TValue?>>();
                _items[index]=list;
                AmountOfLists++;
                CoefOfFullnessNow = (double)AmountOfLists / _size;
                if(CoefOfFullnessNow>=CoefFullnessToExctention&&!isSizeFull)
                {
                    isSizeFull = true;
                    RewriteTable();
                }
            }
            return list;
        }



        private void RewriteTable()
        {
            List<LinkedList<Node<TKey, TValue?>>?> oldData = new List<LinkedList<Node<TKey, TValue?>>?>( _items );



            _items = new LinkedList<Node<TKey, TValue?>>?[_size];
            foreach(LinkedList<Node<TKey, TValue?>> list in oldData)
            {
                if (list != null)
                {
                    foreach (Node<TKey, TValue?> node in list)
                    {
                        Insert(node);
                    }
                }
            }

            oldData = null;
        }

        public void SetValue(TKey key, TValue value)
        {
            int arrayIndex = GetHash(key, isSizeFull ? _size : _not_full_size);
            LinkedList<Node<TKey, TValue?>> list = GetList(arrayIndex);
            bool isKeyFound = false;


            foreach (Node<TKey, TValue?> pair in list)
            {
                if (key.Equals(pair.Key))
                {
                    pair.Value = value;
                    isKeyFound = true;
                    break;
                }
            }

            if(!isKeyFound) throw new ArgumentException("Неправильный ключ, такого ключа нет в таблице");

        }

        public TValue this[TKey key]
        {
            get => GetValue(key);
            set => SetValue(key, value);
        }
        

        public TValue? GetValue(TKey key)
        {
            int arrayIndex = GetHash(key, isSizeFull ? _size : _not_full_size);
            LinkedList<Node<TKey, TValue?>> list = GetList(arrayIndex);



            foreach (Node<TKey, TValue?> pair in list)
            {
                if (key.Equals(pair.Key))
                {
                    return pair.Value;
                    
                }
            }

            throw new ArgumentException("Неправильный ключ, такого ключа нет в таблице");
        }

       
        public void Remove(TKey key)
        {
            int arrayIndex = GetHash(key, isSizeFull ? _size : _not_full_size);
            LinkedList<Node<TKey, TValue?>> list = GetList(arrayIndex);
            Node<TKey, TValue?> node=null;

            foreach (Node<TKey, TValue?> pair in list)
            {
                if (key.Equals(pair.Key))
                {
                    node=pair;
                    
                    break;
                }
            }

            if(node==null) throw new ArgumentException("Неправильный ключ, такого ключа нет в таблице");
            list.Remove(node);
        }


    }
}
