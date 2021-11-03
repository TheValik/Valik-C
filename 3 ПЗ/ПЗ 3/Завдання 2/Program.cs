using System;
using System.Security.Cryptography;
using System.Text;

namespace ПЗ_3__2
{
    class Program
    {
        static byte[] ComputeHashMd5(byte[] dataForHash)
        {
            using (var md5 = MD5.Create())
            {
                return md5.ComputeHash(dataForHash);
            }
        }

        static void Main(string[] args)
        {
            //Console.WriteLine("Hello World!");
            Guid guid1 = new Guid("564c8da6-0440-88ec-d453-0bbad57c6036");
            //Console.WriteLine(guid1);

            string a = 12345678.ToString();

            for (int i = 100000000; i < 200000000; i++)
            {
                var md5 = ComputeHashMd5(Encoding.Unicode.GetBytes(i.ToString().Substring(1, 8)));
                if (new Guid(md5) == guid1)
                {
                    Console.WriteLine("Password= " + i);
                }
            }


        }
    }
}
й