using System;
using System.Security.Cryptography;
using System.Text;

namespace ПЗ_3
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

        public static byte[] ComputeHashSha1(byte[] toBeHashed)
        {
            using (var sha1 = SHA1.Create())
            {
                return sha1.ComputeHash(toBeHashed);
            }
        }

        public static byte[] ComputeHashSha256(byte[] toBeHashed)
        {
            using (var sha256 = SHA256.Create())
            {
                return sha256.ComputeHash(toBeHashed);
            }
        }

        public static byte[] ComputeHashSha384(byte[] toBeHashed)
        {
            using (var sha384 = SHA384.Create())
            {
                return sha384.ComputeHash(toBeHashed);
            }
        }

        public static byte[] ComputeHashSha512(byte[] toBeHashed)
        {
            using (var sha512 = SHA512.Create())
            {
                return sha512.ComputeHash(toBeHashed);
            }
        }


        static void Main(string[] args)
        {
            //Md5
            Console.WriteLine("MD5");
            const string strForHash1 = "Hello World!";
            const string strForHash2 = "Hello World!";
            const string strForHash3 = "Hello World>";

            var md5ForStr1 = ComputeHashMd5(Encoding.Unicode.GetBytes(strForHash1));
            var md5ForStr2 = ComputeHashMd5(Encoding.Unicode.GetBytes(strForHash2));
            var md5ForStr3 = ComputeHashMd5(Encoding.Unicode.GetBytes(strForHash3));

            Guid guid1 = new Guid(md5ForStr1),
                guid2 = new Guid(md5ForStr2),
                guid3 = new Guid(md5ForStr3);
            Console.WriteLine($"Str:{strForHash1}");
            Console.WriteLine($"Hash MD5:{Convert.ToBase64String(md5ForStr1)}");
            Console.WriteLine($"GUID:{guid1}");
            Console.WriteLine("-------------");

            Console.WriteLine($"Str:{strForHash2}");
            Console.WriteLine($"Hash MD5:{Convert.ToBase64String(md5ForStr2)}");
            Console.WriteLine($"GUID:{guid2}");
            Console.WriteLine("-------------");

            Console.WriteLine($"Str:{strForHash3}");
            Console.WriteLine($"Hash MD5:{Convert.ToBase64String(md5ForStr3)}");
            Console.WriteLine($"GUID:{guid3}");
            Console.WriteLine("-------------");

            Console.WriteLine("---------------------------------------------------");
            //--------------------------------------------------
            //SHA1
            Console.WriteLine("SHA1");
            var SAH1ForStr1 = ComputeHashSha1(Encoding.Unicode.GetBytes(strForHash1));
            var SHA1ForStr2 = ComputeHashSha1(Encoding.Unicode.GetBytes(strForHash2));
            var SHA1ForStr3 = ComputeHashSha1(Encoding.Unicode.GetBytes(strForHash3));

            Console.WriteLine($"Str:{strForHash1}");
            Console.WriteLine($"Hash sha1:{Convert.ToBase64String(SAH1ForStr1)}");
            Console.WriteLine("-------------");

            Console.WriteLine($"Str:{strForHash2}");
            Console.WriteLine($"Hash sha1:{Convert.ToBase64String(SHA1ForStr2)}");
            Console.WriteLine("-------------");

            Console.WriteLine($"Str:{strForHash3}");
            Console.WriteLine($"Hash sha1:{Convert.ToBase64String(SHA1ForStr3)}");
            Console.WriteLine("-------------");

            Console.WriteLine("---------------------------------------------------");

            //--------------------------------------------------
            //SHA256
            Console.WriteLine("SHA256");
            var SAH256ForStr1 = ComputeHashSha256(Encoding.Unicode.GetBytes(strForHash1));
            var SHA256ForStr2 = ComputeHashSha256(Encoding.Unicode.GetBytes(strForHash2));
            var SHA256ForStr3 = ComputeHashSha256(Encoding.Unicode.GetBytes(strForHash3));

            Console.WriteLine($"Str:{strForHash1}");
            Console.WriteLine($"Hash SHA256:{Convert.ToBase64String(SAH256ForStr1)}");
            Console.WriteLine("-------------");

            Console.WriteLine($"Str:{strForHash2}");
            Console.WriteLine($"Hash SHA256:{Convert.ToBase64String(SHA256ForStr2)}");
            Console.WriteLine("-------------");

            Console.WriteLine($"Str:{strForHash3}");
            Console.WriteLine($"Hash SHA256:{Convert.ToBase64String(SHA256ForStr3)}");
            Console.WriteLine("-------------");

            Console.WriteLine("---------------------------------------------------");

            //--------------------------------------------------
            //SHA384
            Console.WriteLine("SHA384");
            var SAH384ForStr1 = ComputeHashSha384(Encoding.Unicode.GetBytes(strForHash1));
            var SHA384ForStr2 = ComputeHashSha384(Encoding.Unicode.GetBytes(strForHash2));
            var SHA384ForStr3 = ComputeHashSha384(Encoding.Unicode.GetBytes(strForHash3));

            Console.WriteLine($"Str:{strForHash1}");
            Console.WriteLine($"Hash SHA384:{Convert.ToBase64String(SAH384ForStr1)}");
            Console.WriteLine("-------------");

            Console.WriteLine($"Str:{strForHash2}");
            Console.WriteLine($"Hash SHA384:{Convert.ToBase64String(SHA384ForStr2)}");
            Console.WriteLine("-------------");

            Console.WriteLine($"Str:{strForHash3}");
            Console.WriteLine($"Hash SHA384:{Convert.ToBase64String(SHA384ForStr3)}");
            Console.WriteLine("-------------");

            Console.WriteLine("---------------------------------------------------");

            //--------------------------------------------------
            //SHA512
            Console.WriteLine("SHA512");
            var SAH512ForStr1 = ComputeHashSha512(Encoding.Unicode.GetBytes(strForHash1));
            var SHA512ForStr2 = ComputeHashSha512(Encoding.Unicode.GetBytes(strForHash2));
            var SHA512ForStr3 = ComputeHashSha512(Encoding.Unicode.GetBytes(strForHash3));

            Console.WriteLine($"Str:{strForHash1}");
            Console.WriteLine($"Hash SHA512:{Convert.ToBase64String(SAH512ForStr1)}");
            Console.WriteLine("-------------");

            Console.WriteLine($"Str:{strForHash2}");
            Console.WriteLine($"Hash SHA512:{Convert.ToBase64String(SHA512ForStr2)}");
            Console.WriteLine("-------------");

            Console.WriteLine($"Str:{strForHash3}");
            Console.WriteLine($"Hash SHA512:{Convert.ToBase64String(SHA512ForStr3)}");
            Console.WriteLine("-------------");

            Console.WriteLine("---------------------------------------------------");
        }
    }
}
