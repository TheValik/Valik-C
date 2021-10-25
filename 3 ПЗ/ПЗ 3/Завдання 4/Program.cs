using System;
using System.Security.Cryptography;
using System.Text;
using System.Collections.Generic;

namespace Завдання_4
{
    class Program
    {
        public static byte[] ComputeHashSha256(byte[] toBeHashed)
        {
            using (var sha256 = SHA256.Create())
            {
                return sha256.ComputeHash(toBeHashed);
            }
        }

        static void Main(string[] args)
        {

            List <string> logins = new List<string>();
            List<string> passwords = new List<string>();



            Console.WriteLine("Реєстрацiя аккаунтiв");
            bool b = true;
            string um;
            string name;
            Console.Write("Введiть логiн: ");
            logins.Add(Console.ReadLine());
            while (b)
            {

                Console.Write("Введiть пароль: ");
                passwords.Add(Convert.ToBase64String(ComputeHashSha256(Encoding.Unicode.GetBytes(Console.ReadLine()))));


                Console.Write("Введiть '+' для реєстації ще одного аккаунту, введіть '-' для припинення реєстрації : ");
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
                        pass= Convert.ToBase64String(ComputeHashSha256(Encoding.Unicode.GetBytes(Console.ReadLine())));

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
