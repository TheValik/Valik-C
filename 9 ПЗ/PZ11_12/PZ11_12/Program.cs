using System;
using System.Collections.Generic;
using System.Security;
using System.Security.Cryptography;
using System.Security.Principal;
using System.Text;
using System.Threading;

namespace PZ11_12
{
    class User
    {
        public string Login { get; set; }
        public byte[] PasswordHash { get; set; }
        public byte [] Salt { get; set; }
        public string[] Roles { get; set; }
    }

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
        public static byte[] HashPassword(byte[] toBeHashed, byte[] salt, Int32 NumberOfBytes)
        {
            var numberOfRounds = 10000;

            using (var rfc2898 = new Rfc2898DeriveBytes(toBeHashed, salt, numberOfRounds,HashAlgorithmName.SHA256))
            {
                return rfc2898.GetBytes(NumberOfBytes);
            }
        }
    }

    class Protector
    {
        private static Dictionary<string, User> _users = new Dictionary<string,User>();
        public static void Register(string userName, string password, string[] roles = null) 
        {
            if (_users.ContainsKey(userName))
            {
                Console.WriteLine("Такий користувач вже присутнiй");
            }

            else
            {
                var user = new User();
                user.Login = userName;
                user.Salt = PBKDF2.GenerateSalt();
                user.PasswordHash = PBKDF2.HashPassword(Encoding.Unicode.GetBytes(password), user.Salt, 32);
                user.Roles = roles;
                _users.Add(userName, user);
            }

        }
        public static bool  CheckPassword(string userName, string password)
        {

            var UserKeyValue = _users[userName];
            var Hesh = UserKeyValue.PasswordHash;

            if (Convert.ToBase64String( Hesh)== Convert.ToBase64String(PBKDF2.HashPassword(Encoding.Unicode.GetBytes(password), UserKeyValue.Salt, 32)))
            {
                return true;
            }

            else
            {
                return false;
            } 
        }

        public static void LogIn(string userName, string password)
        {
            // Перевірка пароля
            if (CheckPassword(userName, password))
            {
                // Створюється екземпляр автентифікованого користувача
                var identity = new GenericIdentity(userName, "OIBAuth");
                // Виконується прив’язка до ролей, до яких належить користувач
                var principal = new GenericPrincipal(identity, _users[userName].Roles);
                // Створений екземпляр автентифікованого користувача з відповідними
                // ролями присвоюється потоку, в якому виконується програма
                System.Threading.Thread.CurrentPrincipal = principal;
                Console.WriteLine("Ви увiйшли");
            }
            else
            {
                Console.WriteLine("Не вiрний пароль");
            }
            
        }
        public static void OnlyForAdminsFeature()
        {
            // Перевірка того, що потік програми виконується автентифікованим користувачем із
            if (Thread.CurrentPrincipal == null)
            {
                throw new SecurityException("Thread.CurrentPrincipal cannot be null.");
            }
            // Перевірка того, що автентифікований користувач належить до ролі "Admins"
            if (Thread.CurrentPrincipal.IsInRole("Guest"))
            {
                Console.WriteLine("You have access to Guest.");
            }
            else
            {
                Console.WriteLine("You DON`T have access to Guest.");
            }
            if (Thread.CurrentPrincipal.IsInRole("User"))
            {
                Console.WriteLine("You have access to User.");
            }
            else
            {
                Console.WriteLine("You DON`T have access to User.");
            }
            if (Thread.CurrentPrincipal.IsInRole("First level Admin"))
            {
                Console.WriteLine("You have access to this First level Admin feature.");
            }
            else
            {
                Console.WriteLine("You DON`T have access to this First level Admin feature.");
            }
            if (Thread.CurrentPrincipal.IsInRole("Admin"))
            {
                Console.WriteLine("You have access to Admin feature.");
            }
            else
            {
                Console.WriteLine("You DON`T have access to Admin feature.");
            }


        }


    }

    class Program
    {        
        
        static void Main(string[] args)
        {
            Console.WriteLine("Реєстрацiя аккаунтiв");

            string LogMem;
            string PassMem;
            
            int RolCount;

            Console.WriteLine("");
            Console.WriteLine("Ролi користувачiв");
            Console.WriteLine("1) Guest");
            Console.WriteLine("2) User");
            Console.WriteLine("3) First level Admin");
            Console.WriteLine("4) Admin");
            Console.WriteLine("");
            Console.WriteLine("");
            // лічильник ітерацій, щоб було 4 користувача
            int lich = 0;
            while(lich<4)
            {
                Console.Write("Введiть логiн: ");
                LogMem = Console.ReadLine();

                Console.Write("Введiть пароль: ");
                PassMem= Console.ReadLine();

                Console.WriteLine("Скiльки хочете додати ролей?");
                RolCount = Int32.Parse(Console.ReadLine());
                while (0 > RolCount || RolCount>5)
                {
                    Console.WriteLine("Введiть число вiд 1 до 4");
                    RolCount = Int32.Parse(Console.ReadLine());
                }
                string[] RolMem= new string[RolCount];
                for(int i=0; i < RolCount; i++)
                {
                    Console.Write("Введiть роль: ");
                    RolMem[i] = Console.ReadLine();
                    Console.WriteLine("");
                }

                Protector.Register(LogMem, PassMem, RolMem);
                lich++;
                Console.WriteLine("");
                Console.WriteLine("");
                
            }


            // login
            while (true)
            {
                Console.WriteLine("Вхiд до аккаунту");

                Console.Write("Введiть логiн: ");
                LogMem = Console.ReadLine();
                    
                Console.Write("Введiть пароль: ");
                PassMem = Console.ReadLine();

                Protector.LogIn(LogMem, PassMem);
                Protector.OnlyForAdminsFeature();
            }
        }
    }
}
