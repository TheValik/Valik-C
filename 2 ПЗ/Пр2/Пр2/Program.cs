using System;
using System.IO;
using System.Text;
using System.Linq;

namespace Пр2
{
    class Program
    {
        static void Main(string[] args)
        {

            byte key = 2;

            //зчитування елементів файлу в масив перетворюючи елементи в байти
            byte[] decData = File.ReadAllBytes("file.TXT").ToArray();
            //створення пустих масивів типу byte
            byte[] arr1 = new byte[3];
            byte[] arr2 = new byte[3];
            Console.WriteLine("-------------");
            //виведення елементів тексту
            Console.WriteLine("Не зашифрований текст");
            for (int i = 0; i < 3; i++)
            {
                Console.WriteLine((char)decData[i]);
            }
            Console.WriteLine("-------------");
            Console.WriteLine("Зашифрований текст");
            //шифрування
            for (int i = 0; i < 3; i++)
            {
                arr1[i] = (byte)(decData[i] ^ key);
            }
            //виведення зашифрованого
            for (int i = 0; i < 3; i++)
            {
                Console.WriteLine((char)arr1[i]);
            }
            Console.WriteLine("-------------");

            File.WriteAllBytes("text.dat", arr1);
            //----------------------------------

            Console.WriteLine("-------------");
            Console.WriteLine("Розшифрований файл");
            byte[] shyfr = File.ReadAllBytes("text.dat").ToArray();
            for (int i = 0; i < 3; i++)
            {
                arr2[i] = (byte)(arr1[i] ^ key);
                Console.WriteLine((char)arr2[i]);

            }
        }
    }
}
