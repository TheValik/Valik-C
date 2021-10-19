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

            var password = new StringBuilder(5);
            for(char a = '!'; a < '~'; a++)
            {
                for(char b = '!'; b < '~'; b++)
                {
                    for(char c='!'; c < '~'; c++)
                    {
                        for(char d = '!'; d < '~'; d++)
                        {
                            for(char e = '!'; e < '~'; e++)
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
                                                Console.WriteLine("gotovo");
                                                stop = 1;
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
                Console.WriteLine("a= " + a);
                if (stop == 1)
                {
                    break;
                }
            }
            Console.WriteLine(password);

            int k = 0;
           for(int i = 0; i < text.Length; i++)
            {
                Console.Write((char)(byte)(text[i] ^ password[k]));
                k++;
                if (k > 4)
                {
                    k = 0;
                }
            }
 





        }
    }
}
