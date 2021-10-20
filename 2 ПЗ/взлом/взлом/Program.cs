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


            StreamReader sorse = new StreamReader("C:\\encfile5.dat");
            string text = sorse.ReadToEnd();
            string slov = "Mit21";
            int lich = 1;
            int stop = 0;
            int k = 0;

            var password = new StringBuilder(5);
            for(char a = 'd'; a <= '~'; a++)
            {
                for(char b = '!'; b <= '~'; b++)
                {
                    for(char c='!'; c <= '~'; c++)
                    {
                        for(char d = '!'; d <= '~'; d++)
                        {
                            for(char e = '!'; e <= '~'; e++)
                            {
                                password.Clear();


                                password.Append(a);
                                password.Append(b);
                                password.Append(c);
                                password.Append(d);
                                password.Append(e);

                                for(int i =0;i< text.Length; i++)
                                {

                                    if((char)(byte)(text[i] ^ password[0])==slov[0])
                                    {
                                        for(int per = 1; per < slov.Length; per++)
                                        {
                                            if((char)(byte)(text[i+per] ^ password[per]) == slov[per])
                                            {
                                                lich++;
                                            }


                                                //умова виходу
                                            if (lich == 5)
                                            {
                                                Console.WriteLine(password);
                                                Console.WriteLine("gotovo");
                                                for (int p = 0; p < text.Length; p++)
                                                {
                                                    Console.Write((char)(byte)(text[p] ^ password[k]));
                                                    k++;
                                                    if (k > 4)
                                                    {
                                                        k = 0;
                                                    }
                                                }
                                                k = 0;
                                                Console.WriteLine("");
                                                Console.WriteLine("--------------------------");
                                                // stop = 1;
                                            }

                                        }
                                        //Скидання лічильника
                                        lich = 1;
                                    }
                                    if (stop == 1)
                                    {
                                        break;
                                    }
                                }
                                if (stop == 1)
                                {
                                    break;
                                }
                            }
                            
                            if (stop == 1)
                            {
                                break;
                            }
                        }
                        if (stop == 1)
                        {
                            break;
                        }
                    }
                    if (stop == 1)
                    {
                        break;
                    }
                }
                Console.WriteLine("Алгоритм пройшов ключ який починається на --  " + a);
                if (stop == 1)
                {
                    break;
                }
            }
            Console.WriteLine(password);

 





        }
    }
}
