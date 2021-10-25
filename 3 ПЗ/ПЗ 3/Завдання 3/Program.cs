using System;
using System.Security.Cryptography;
using System.Text;

namespace Завдання_3
{
    class Program
    {
        public static byte[] ComputeHmacsha1(byte[] toBeHashed, byte[] key)
        {
            using (var hmac = new HMACSHA1(key))
            {
                return hmac.ComputeHash(toBeHashed);
            }
        }

        static void Main(string[] args)
        {
            Console.WriteLine("Початкове повiдомлення: Hello World!");

            const string message = "Hello World!";
            const string password = "haha123";

            var heshkode = ComputeHmacsha1(Encoding.Unicode.GetBytes(message), Encoding.Unicode.GetBytes(password));

            Console.WriteLine($"Hash HMAC: {Convert.ToBase64String(heshkode)}");

            const string message1 = "Hello World!";
            const string message2 = "Hello World>";
            var peredHesh = heshkode;

            var heshkode1 = ComputeHmacsha1(Encoding.Unicode.GetBytes(message1), Encoding.Unicode.GetBytes(password));

            if (Convert.ToBase64String(peredHesh) == Convert.ToBase64String(heshkode1))
            {
                Console.WriteLine("Повiдомлення: "+message1 +" не змiнене");
            }
            else
            {
                Console.WriteLine("Повiдомлення: " + message1 + " змiнене");
            }

            var heshkode2 = ComputeHmacsha1(Encoding.Unicode.GetBytes(message2), Encoding.Unicode.GetBytes(password));

            if (Convert.ToBase64String(heshkode2) == Convert.ToBase64String(peredHesh))
            {
                Console.WriteLine("Повiдомлення: " + message2 + " не змiнене");
            }
            else
            {
                Console.WriteLine("Повiдомлення: " + message2 + " змiнене");
            }

        }
    }
}
