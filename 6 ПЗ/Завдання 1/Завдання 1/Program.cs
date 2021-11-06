using System;
using System.IO;
using System.Security.Cryptography;
using System.Text;

namespace Пз_6
{
    class aesChipher
    {
        public static byte[] GenerateRandomNumber(int length)
        {
            using (var randomNumberGenerator = new RNGCryptoServiceProvider())
            {
                var randomNumber = new byte[length];
                randomNumberGenerator.GetBytes(randomNumber);
                return randomNumber;
            }
        }
        //Метод для зашифровування може мати вигляд:
        public static byte[] Encrypt(byte[] dataToEncrypt, byte[] key, byte[] iv)
        {
            using (var aes = new AesCryptoServiceProvider())
            {
                aes.Mode = CipherMode.CBC;
                aes.Padding = PaddingMode.PKCS7;
                aes.Key = key;
                aes.IV = iv;
                using (var memoryStream = new MemoryStream())
                {
                    var cryptoStream = new CryptoStream(memoryStream, aes.CreateEncryptor(), CryptoStreamMode.Write);
                    cryptoStream.Write(dataToEncrypt, 0, dataToEncrypt.Length);
                    cryptoStream.FlushFinalBlock();
                    return memoryStream.ToArray();
                }
            }
        }

        public static byte[] Decrypt(byte[] dataToDencrypt, byte[] key, byte[] iv)
        {
            using (var aes = new AesCryptoServiceProvider())
            {
                aes.Mode = CipherMode.CBC;
                aes.Padding = PaddingMode.PKCS7;
                aes.Key = key;
                aes.IV = iv;
                using (var memoryStream = new MemoryStream())
                {
                    var cryptoStream = new CryptoStream(memoryStream, aes.CreateDecryptor(), CryptoStreamMode.Write);
                    cryptoStream.Write(dataToDencrypt, 0, dataToDencrypt.Length);
                    cryptoStream.FlushFinalBlock();
                    return memoryStream.ToArray();
                }
            }
        }
        //---------------------------DES--------------------------------
        public static byte[] EncryptDes(byte[] dataToEncrypt, byte[] key, byte[] iv)
        {
            using (var aes = new DESCryptoServiceProvider())
            {
                aes.Mode = CipherMode.CBC;
                aes.Padding = PaddingMode.PKCS7;
                aes.Key = key;
                aes.IV = iv;
                using (var memoryStream = new MemoryStream())
                {
                    var cryptoStream = new CryptoStream(memoryStream, aes.CreateEncryptor(), CryptoStreamMode.Write);
                    cryptoStream.Write(dataToEncrypt, 0, dataToEncrypt.Length);
                    cryptoStream.FlushFinalBlock();
                    return memoryStream.ToArray();
                }
            }
        }

        public static byte[] DecryptDes(byte[] dataToDencrypt, byte[] key, byte[] iv)
        {
            using (var aes = new DESCryptoServiceProvider())
            {
                aes.Mode = CipherMode.CBC;
                aes.Padding = PaddingMode.PKCS7;
                aes.Key = key;
                aes.IV = iv;
                using (var memoryStream = new MemoryStream())
                {
                    var cryptoStream = new CryptoStream(memoryStream, aes.CreateDecryptor(), CryptoStreamMode.Write);
                    cryptoStream.Write(dataToDencrypt, 0, dataToDencrypt.Length);
                    cryptoStream.FlushFinalBlock();
                    return memoryStream.ToArray();
                }
            }
        }

        //---------------------------Triple_DES--------------------------------
        public static byte[] EncryptTriple_DES(byte[] dataToEncrypt, byte[] key, byte[] iv)
        {
            using (var aes = new TripleDESCryptoServiceProvider())
            {
                aes.Mode = CipherMode.CBC;
                aes.Padding = PaddingMode.PKCS7;
                aes.Key = key;
                aes.IV = iv;
                using (var memoryStream = new MemoryStream())
                {
                    var cryptoStream = new CryptoStream(memoryStream, aes.CreateEncryptor(), CryptoStreamMode.Write);
                    cryptoStream.Write(dataToEncrypt, 0, dataToEncrypt.Length);
                    cryptoStream.FlushFinalBlock();
                    return memoryStream.ToArray();
                }
            }
        }

        public static byte[] DecryptTriple_DES(byte[] dataToDencrypt, byte[] key, byte[] iv)
        {
            using (var aes = new TripleDESCryptoServiceProvider())
            {
                aes.Mode = CipherMode.CBC;
                aes.Padding = PaddingMode.PKCS7;
                aes.Key = key;
                aes.IV = iv;
                using (var memoryStream = new MemoryStream())
                {
                    var cryptoStream = new CryptoStream(memoryStream, aes.CreateDecryptor(), CryptoStreamMode.Write);
                    cryptoStream.Write(dataToDencrypt, 0, dataToDencrypt.Length);
                    cryptoStream.FlushFinalBlock();
                    return memoryStream.ToArray();
                }
            }
        }


    }


    class Program
    {
        static void Main(string[] args)
        {
            var key = aesChipher.GenerateRandomNumber(32);
            var iv = aesChipher.GenerateRandomNumber(16);
            const string original = "Text to encrypt";
            var encrypted = aesChipher.Encrypt(Encoding.UTF8.GetBytes(original), key, iv);
            var decrypted = aesChipher.Decrypt(encrypted, key, iv);
            var decryptedMessage = Encoding.UTF8.GetString(decrypted);
            Console.WriteLine("AES Encryption in .NET");
            Console.WriteLine("----------------------");
            Console.WriteLine();
            Console.WriteLine("Original Text = " + original);
            Console.WriteLine("Encrypted Text = " + Convert.ToBase64String(encrypted));
            Console.WriteLine("Decrypted Text = " + decryptedMessage);


            //-----------------------------DES------------------------------------
            Console.WriteLine();
            Console.WriteLine("######################################################");
            Console.WriteLine();
            var key2 = aesChipher.GenerateRandomNumber(8);
            var iv2 = aesChipher.GenerateRandomNumber(8);
            const string original2 = "Text to encrypt";
            var encrypted2 = aesChipher.EncryptDes(Encoding.UTF8.GetBytes(original2), key2, iv2);
            var decrypted2 = aesChipher.DecryptDes(encrypted2, key2, iv2);
            var decryptedMessage2 = Encoding.UTF8.GetString(decrypted2);
            Console.WriteLine("DES Encryption in .NET");
            Console.WriteLine("----------------------");
            Console.WriteLine();
            Console.WriteLine("Original Text = " + original2);
            Console.WriteLine("Encrypted Text = " + Convert.ToBase64String(encrypted2));
            Console.WriteLine("Decrypted Text = " + decryptedMessage2);


            //-----------------------------Triple_DES------------------------------------
            Console.WriteLine();
            Console.WriteLine("######################################################");
            Console.WriteLine();
            var key3 = aesChipher.GenerateRandomNumber(24);
            var iv3 = aesChipher.GenerateRandomNumber(8);
            const string original3 = "Text to encrypt";
            var encrypted3 = aesChipher.EncryptTriple_DES(Encoding.UTF8.GetBytes(original3), key3, iv3);
            var decrypted3 = aesChipher.DecryptTriple_DES(encrypted3, key3, iv3);
            var decryptedMessage3 = Encoding.UTF8.GetString(decrypted3);
            Console.WriteLine("Triple-DES Encryption in .NET");
            Console.WriteLine("----------------------");
            Console.WriteLine();
            Console.WriteLine("Original Text = " + original3);
            Console.WriteLine("Encrypted Text = " + Convert.ToBase64String(encrypted3));
            Console.WriteLine("Decrypted Text = " + decryptedMessage3);
        }
    }
}