using System;
using System.Collections.Generic;
using System.Diagnostics.Metrics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HashTables
{
    class HashFunc
    {

        public static Func<object, int, int> GetHashFunc(HashFuncType hashFuncType)
        {

            switch (hashFuncType)
            {
                case HashFuncType.Rs:
                    return HashRs;
                case HashFuncType.Div:
                    return HashDiv;
                case HashFuncType.MD5:
                    return CreateMD5;
                case HashFuncType.Sha256:
                    return Sha256;
                case HashFuncType.RemOfDivLast4:
                    return RemainderOfDivision;

            }

            return null;
        }

        public static int HashRs(object obj, int sizeTable)
        {
            String str = obj.ToString();
            int b = 378551;
            int a = 63689;
            int hash = 0;

            for (int i = 0; i < str.Length; i++)
            {
                hash = hash * a + (str[i]);
                a *= b;
            }

            return Math.Abs(hash) % sizeTable;

        }

        public static int HashDiv(object obj, int sizeTable)
        {
            String str = obj.ToString();
            int hash = 0;
            for (int i = 0; i < str.Length; i++)
            {
                hash = (hash << 5) - hash + (int)str[i];
            }
            return Math.Abs(hash) % sizeTable;
        }




        public static int CreateMD5(object input, int sizeTable)
        {

            using (System.Security.Cryptography.MD5 md5 = System.Security.Cryptography.MD5.Create())
            {
                byte[] inputBytes = Encoding.ASCII.GetBytes(input.ToString());
                byte[] hashBytes = md5.ComputeHash(inputBytes);
                int inthash = BitConverter.ToInt32(hashBytes, 0);

                return Math.Abs(inthash) % sizeTable;
            }
        }

        static int Sha256(object randomString, int sizeTable)
        {
            var crypt = new System.Security.Cryptography.SHA256Managed();

            byte[] crypto = crypt.ComputeHash(Encoding.UTF8.GetBytes(randomString.ToString()));

            return Math.Abs(BitConverter.ToInt32(crypto, 0)) % sizeTable;
        }


        public static int RemainderOfDivision(object obj, int size)
        {
            
            byte[] inputBytes = Encoding.ASCII.GetBytes(obj.ToString());
            int i = inputBytes.Length - 1;
            int result = 0;
            int counter = 0;
            while (counter<4)
            {
                if(i>=0)
                {

                    result += inputBytes[i] * (int)(Math.Pow(256, counter));
                    i--;
                }
                else
                {
                    result += 0;
                }
                
                counter++;
            }

            return Math.Abs(result) % size;
        }


        public static int FAQ6(object obj, int sizeTable)
        {
            int hash = 0;
            string str = obj.ToString();
            for (int i = 0; i < str.Length; i++)
            {
                hash += str[i];
                hash += hash << 10;
                hash ^= hash >> 6;
            }
            hash += (hash << 3);
            hash ^= (hash >> 11);
            hash += (hash << 15);
            return Math.Abs(hash )% sizeTable;
        }
    }


}
