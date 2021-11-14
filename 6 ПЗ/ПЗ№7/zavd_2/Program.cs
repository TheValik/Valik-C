using System;
using System.IO;
using System.Security.Cryptography;
using System.Text;

namespace ПЗ_7
{
      public class RSAWithRSAParameterKey
        {
            
            // Збереження закритого ключа
            private readonly static string CspContainerName = "RsaContainer";
            public static void AssignNewKey3()
            {
                CspParameters cspParameters = new CspParameters(1)
                {
                    KeyContainerName = CspContainerName,

                    ProviderName = "Microsoft Strong Cryptographic Provider"
                };
                var rsa = new RSACryptoServiceProvider(cspParameters)
                {
                    PersistKeyInCsp = true
                };
                File.WriteAllText("F:/Навчання/С#/6 ПЗ/test5.xml", rsa.ToXmlString(false));
            }

            // Розшифрування
            public static byte[] DecryptData(byte[] dataToDecrypt)
            {
                byte[] plainBytes;
                var cspParams = new CspParameters
                {
                    KeyContainerName = CspContainerName,
                    
                };
                using (var rsa = new RSACryptoServiceProvider(cspParams))
                {
                    rsa.PersistKeyInCsp = true;
                    plainBytes = rsa.Decrypt(dataToDecrypt, false);
                }
                return plainBytes;
            }

            // Шифрування
            public static byte[] EncryptData2(string publicKeyPath,byte[] dataToEncrypt)
            {
                byte[] cipherbytes;
                using (var rsa = new RSACryptoServiceProvider(2048))
                {
                    rsa.PersistKeyInCsp = false;
                    rsa.FromXmlString(File.ReadAllText(publicKeyPath));
                    cipherbytes = rsa.Encrypt(dataToEncrypt, false);
                }
                return cipherbytes;
            }
        }

    class Program
    {

        static void Main(string[] args)
        {
            string a = "Hello World!";
            RSAWithRSAParameterKey.AssignNewKey3();
            var Encr = RSAWithRSAParameterKey.EncryptData2("F:/Навчання/С#/6 ПЗ/test5.xml", Encoding.Unicode.GetBytes(a));
            var Decr = RSAWithRSAParameterKey.DecryptData(Encr);


            Console.WriteLine("Просте повiдомлення:  " + a);
            Console.WriteLine("");
            Console.WriteLine("Зашифроване повiдомлення:  " + Encoding.Unicode.GetString(Encr));
            Console.WriteLine("");
            Console.WriteLine("Розшифроване повiдомлення:  " + Encoding.Unicode.GetString(Decr));
            Console.WriteLine("");

        }
    }
}