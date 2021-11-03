using System;
using System.Text;
using System.Security.Cryptography;
using System.Diagnostics;

namespace Lab5._2
{
    public class PBKDF2
    {
        public static byte[] GenerateSalt()
        {
            using (var randomNumberGenerator = new RNGCryptoServiceProvider())
            {
                var randomNumber = new byte[32];
                randomNumberGenerator.GetBytes(randomNumber);
                return randomNumber;
            }
        }
        public static byte[] HashPassword(byte[] toBeHashed, byte[] salt, int numberOfRounds, System.Security.Cryptography.HashAlgorithmName hashAlgorithm, Int32 NumberOfBytes)
        {
            using (var rfc2898 = new Rfc2898DeriveBytes(toBeHashed, salt, numberOfRounds, hashAlgorithm))
            {
                return rfc2898.GetBytes(NumberOfBytes);
            }
        }
    }
    class Program
    {
        static void Main(string[] args)
        {
            const string passwordToHash = "VeryComplexPassword";
            Console.WriteLine("Algoritm SHA512");
            HashPassword(passwordToHash, 110000);
            HashPassword(passwordToHash, 160000);
            HashPassword(passwordToHash, 210000);
            HashPassword(passwordToHash, 260000);
            HashPassword(passwordToHash, 310000);
            HashPassword(passwordToHash, 360000);
            HashPassword(passwordToHash, 410000);
            HashPassword(passwordToHash, 460000);
            HashPassword(passwordToHash, 510000);
            HashPassword(passwordToHash, 560000);
        }
        public static void HashPassword(string passwordToHash, int numberOfRounds)
        {
            var sw = new Stopwatch();
            sw.Start();
            var hashedPassword = PBKDF2.HashPassword(Encoding.UTF8.GetBytes(passwordToHash), PBKDF2.GenerateSalt(), numberOfRounds, HashAlgorithmName.SHA256, 64);
            sw.Stop();
            Console.WriteLine();
            Console.WriteLine("Password to hash : " + passwordToHash);
            Console.WriteLine("Hashed Password : " + Convert.ToBase64String(hashedPassword));
            Console.WriteLine("Iterations <" + numberOfRounds + "> Elapsed Time: " + sw.ElapsedMilliseconds + "ms");
        }
    }

}