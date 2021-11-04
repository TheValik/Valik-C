using System;
using System.Security.Cryptography;
using System.Text;
using System.Collections.Generic;

namespace Завдання_4
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

            List<string> logins = new List<string>();
            List<string> passwords = new List<string>();
            List<string> salt = new List<string>();

            Console.WriteLine("Реєстрацiя аккаунтiв");
            bool b = true;
            string um;
            string name;
            string memPass2;
            Console.Write("Введiть логiн: ");
            logins.Add(Console.ReadLine());
            while (b)
            {

                Console.Write("Введiть пароль: ");


                var MemSalt = PBKDF2.GenerateSalt();
                salt.Add(Convert.ToBase64String(MemSalt));

                // збереження шешованого паролю з сіллю
                var memPass = Console.ReadLine();
                passwords.Add(Convert.ToBase64String(PBKDF2.HashPassword(Encoding.Unicode.GetBytes(memPass), MemSalt, 110000, HashAlgorithmName.SHA256, 32)));
                //passwords.Add(Convert.ToBase64String(ComputeHashSha256(Encoding.Unicode.GetBytes(memPass+MemSalt))));

                Console.Write("Введiть '+' для реєстацiї ще одного аккаунту, введiть '-' для припинення реєстрацiї : ");
                um = Console.ReadLine();

                if (um != "+" && um != "-")
                {
                    while (um != "+" && um != "-")
                    {
                        Console.WriteLine("Введiть знак ще раз");
                        um = Console.ReadLine();
                    }

                }

                if (um == "-")
                {
                    b = false;
                }

                if (um == "+")
                {
                    Console.Write("Введiть логiн: ");
                    name = Console.ReadLine();
                    while (logins.Contains(name))
                    {
                        Console.Write("Введiть логiн вже присутнiй. Введiть логiн ще раз: ");
                        name = Console.ReadLine();
                    }
                    logins.Add(name);
                }
            }


            Console.WriteLine("");
            Console.WriteLine("");

            Console.WriteLine("Авторизацiя");
            bool a = true;
            string log;
            string pass;
            int ID;

            Console.Write("Введiть логiн: ");
            log = Console.ReadLine();
            while (a)
            {

                if (logins.Contains(log))
                {
                    while (a)
                    {
                        ID = logins.IndexOf(log);
                        Console.Write("Введiть пароль: ");
                        memPass2 = Console.ReadLine();
                        pass = Convert.ToBase64String( PBKDF2.HashPassword(Encoding.Unicode.GetBytes(memPass2), Convert.FromBase64String(salt[ID]), 110000, HashAlgorithmName.SHA256, 32));
                        
                        if (passwords[ID] == pass)
                        {
                            Console.WriteLine("Ви авторизувались");
                            a = false;
                        }
                        else
                        {
                            Console.Write("Не правильний пароль ");
                            Console.WriteLine("Введiть пароль ще раз ");
                        }
                    }
                }
                else
                {
                    Console.Write("Такого логiну не iснує. ");
                    Console.WriteLine("Введiть логiн ще раз ");
                    log = Console.ReadLine();
                }
            }

        }
    }
}
