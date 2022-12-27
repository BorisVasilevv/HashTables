using System;
using System.Collections.Generic;
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

                return Math.Abs(inthash)%sizeTable; 
            }
        }

        static int Sha256(object randomString, int sizeTable)
        {
            var crypt = new System.Security.Cryptography.SHA256Managed();
            var hash = new StringBuilder();
            byte[] crypto = crypt.ComputeHash(Encoding.UTF8.GetBytes(randomString.ToString()));

            return Math.Abs(BitConverter.ToInt32(crypto, 0)) % sizeTable;
        }
    }
}
