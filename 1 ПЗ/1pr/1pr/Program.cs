using System;
using System.Security.Cryptography;

namespace _1pr
{
    class Program
    {
        static void Main(string[] args)
        {
            Random r0 = new Random(0);
            Random r1 = new Random(0);
            for (int i = 0; i < 5; i++)
            {
                Console.WriteLine(r0.Next(0, 100));
            }
            Console.WriteLine("---------------------");
            for (int i = 0; i < 5; i++)
            {
                Console.WriteLine(r1.Next(0, 100));
            }
            Console.WriteLine("---------------------");
            Random r2 = new Random(1);
            for (int i = 0; i < 5; i++)
            {
                Console.WriteLine(r2.Next(0, 100));
            }
            Console.WriteLine("---------------------");
            var rnd0 = new RNGCryptoServiceProvider();
            var rndGen = new byte[10];

            for(int i=0; i<5; i++)
            {
                rnd0.GetBytes(rndGen);
                string text = Convert.ToBase64String(rndGen);
                Console.WriteLine(text);
            }

        }
    }
}
