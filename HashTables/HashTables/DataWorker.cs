using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HashTables
{
    class DataWorker
    {
        public static List<int>  GenerateData(int length)
        {

            Random rnd = new Random();
            List<int> list = new List<int>();
            int randomValue = 0;

            while (list.Count != length)
            {
                String str = randomValue.ToString();
                randomValue = rnd.Next();
                if (!list.Contains(randomValue))
                {
                    str += randomValue.ToString();
                    list.Add(randomValue);
                    
                }
            }


            return list;

        }

        public static void AddDataOnHashTable(List<int> keys, HashTableWithChains<int, string> ht)
        {

        }

        public static void AddDataOnHashTable(List<int> keys, HashTableWithStraightAddress<int, string> ht)
        {

        }
    }
}
