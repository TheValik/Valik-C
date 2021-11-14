using System;
using System.IO;
using System.Security.Cryptography;
using System.Text;

namespace ПЗ_7
{

    class Program
    {

        public class RSAWithRSAParameterKey
        {
            private static RSAParameters _publicKey;
            private static RSAParameters _privateKey;

            // In-Memory Keys
            public static void AssignNewKey()
            {
                using (var rsa = new RSACryptoServiceProvider(4096))
                {
                    rsa.PersistKeyInCsp = false;
                    _publicKey = rsa.ExportParameters(false);
                    _privateKey = rsa.ExportParameters(true);
                }
            }

            // Шифрування
            public static byte[] EncryptData(byte[] dataToEncrypt)
            {
                byte[] cypherBytes;
                using (var rsa = new RSACryptoServiceProvider())
                {
                    rsa.PersistKeyInCsp = true;
                    rsa.ImportParameters(_publicKey);
                    cypherBytes = rsa.Encrypt(dataToEncrypt, true);
                }
                return cypherBytes;
            }


            // Розшифрування
            public static byte[] DecryptData(byte[] dataToDecrypt)
            {
                byte[] plainBytes;
                using (var rsa = new RSACryptoServiceProvider())
                {
                    rsa.PersistKeyInCsp = true;
                    rsa.ImportParameters(_privateKey);
                    plainBytes = rsa.Decrypt(dataToDecrypt, true);
                }
                return plainBytes;
            }
        }

        static void Main(string[] args)
        {
            string a = "Hello World!";
            RSAWithRSAParameterKey.AssignNewKey();
            var EncrB = RSAWithRSAParameterKey.EncryptData(Encoding.Unicode.GetBytes(a));
            var DecrB = RSAWithRSAParameterKey.DecryptData(EncrB);

            

            Console.WriteLine("Просте повiдомлення:  " + a);
            Console.WriteLine("");
            Console.WriteLine("Зашифроване повiдомлення:  " + Encoding.Unicode.GetString(EncrB));
            Console.WriteLine("");
            Console.WriteLine("Розшифроване повiдомлення:  " + Encoding.Unicode.GetString(DecrB));
            Console.WriteLine("");

        }
    }
}
