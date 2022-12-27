using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HashTables
{
    class DataWorker
    {
        public static List<int> GenerateIntKeys(int length)
        {

            Random rnd = new Random();
            List<int> list = new List<int>();
            int randomValue = 0;

            while (list.Count != length)
            {

                randomValue = rnd.Next();
                if (!list.Contains(randomValue))
                {

                    list.Add(randomValue);

                }
            }


            return list;

        }

        public static List<string> GenerateStringKeys(int length)
        {

            Random rnd = new Random();
            List<string> list = new List<string>();
            string randomValue;

            while (list.Count != length)
            {

                randomValue = rnd.Next().ToString();
                if (!list.Contains(randomValue))
                {

                    list.Add(randomValue);

                }
            }


            return list;

        }

        public static void AddDataOnHashTable<T>(List<T> keys, HashTableWithChains<T, string> ht)
        {
            foreach (T key in keys)
            {
                ht.Add(key, key.ToString());
            }
        }

        public static void AddDataOnHashTable<T>(List<T> keys, HashTableWithStraightAddress<T, string> ht)
        {

            foreach (T key in keys)
            {
                ht.Add(key, key.ToString());
            }

        }
    }
}
