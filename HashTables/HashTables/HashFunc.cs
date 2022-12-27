using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HashTables
{
    class HashFunc
    {
        public static int HashFunction(object obj, int sizeTable)
        {


            return 0;
        }



        public static Func<object,int,int> GetHashFunc(HashFuncType hashFuncType)
        {

            switch (hashFuncType)
            {
                case HashFuncType.First:
                    return HashFunction;
            
            }

            return null;
        }

        //        public static string CreateMD5(string input)
        //        {
        //            // Use input string to calculate MD5 hash
        //            using (System.Security.Cryptography.MD5 md5 = System.Security.Cryptography.MD5.Create())
        //            {
        //                byte[] inputBytes = System.Text.Encoding.ASCII.GetBytes(input);
        //                byte[] hashBytes = md5.ComputeHash(inputBytes);

        //                return Convert.ToHexString(hashBytes); // .NET 5 +

        //                // Convert the byte array to hexadecimal string prior to .NET 5
        //                // StringBuilder sb = new System.Text.StringBuilder();
        //                // for (int i = 0; i < hashBytes.Length; i++)
        //                // {
        //                //     sb.Append(hashBytes[i].ToString("X2"));
        //                // }
        //                // return sb.ToString();
        //            }
        //        }


        //        static string sha256(string randomString)
        //        {
        //            var crypt = new System.Security.Cryptography.SHA256Managed();
        //            var hash = new System.Text.StringBuilder();
        //            byte[] crypto = crypt.ComputeHash(Encoding.UTF8.GetBytes(randomString));
        //            foreach (byte theByte in crypto)
        //            {
        //                hash.Append(theByte.ToString("x2"));
        //            }
        //            return hash.ToString();
        //        }



        //        static int HashRs(string str)
        //        {

        //            int b = 378551;
        //            int a = 63689;
        //            int hash = 0;

        //            for (int i = 0; i < str.Length; i++)
        //            {
        //                hash = hash * a + (str[i]);
        //                a *= b;
        //            }

        //            return hash;

        //        }




        //        unsigned int HashFAQ6(const char* str)
        //{

        //    unsigned int hash = 0;

        //    for (; *str; str++)
        //    {
        //        hash += (unsigned char)(* str);
        //hash += (hash << 10);
        //hash ^= (hash >> 6);
        //    }
        //    hash += (hash << 3);
        //hash ^= (hash >> 11);
        //hash += (hash << 15);

        //return hash;

        //}
    }
}
