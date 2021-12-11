using System;
using System.Collections.Generic;
using System.Security;
using System.Security.Cryptography;
using System.Security.Principal;
using System.Text;
using System.Threading;
using NLog;

namespace Pz13_14
{
    class Program
    {
        class User
        {
            public string Login { get; set; }
            public byte[] PasswordHash { get; set; }
            public byte[] Salt { get; set; }
            public string[] Roles { get; set; }
        }

        public class PBKDF2
        {
            private static Logger log = NLog.LogManager.GetCurrentClassLogger();

            public static byte[] GenerateSalt()
            {
                log.Trace("Генерацiя солi");
                using (var randomNumberGenerator = new RNGCryptoServiceProvider())
                {
                    var randomNumber = new byte[32];
                    randomNumberGenerator.GetBytes(randomNumber);
                    return randomNumber;
                }
            }
            public static byte[] HashPassword(byte[] toBeHashed, byte[] salt, Int32 NumberOfBytes)
            {
                log.Trace("Хешування паролю з сiллю");
                var numberOfRounds = 10000;

                using (var rfc2898 = new Rfc2898DeriveBytes(toBeHashed, salt, numberOfRounds, HashAlgorithmName.SHA256))
                {
                    return rfc2898.GetBytes(NumberOfBytes);
                }
            }
        }

        class Protector
        {
            private static Logger log = NLog.LogManager.GetCurrentClassLogger();

            private static Dictionary<string, User> _users = new Dictionary<string, User>();
            public static void Register(string userName, string password, string[] roles = null)
            {
                log.Trace("Перевiрка на записаного користувача");
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
            public static bool CheckPassword(string userName, string password)
            {
                try
                {
                    var UserKeyValue = _users[userName];
                    var Hesh = UserKeyValue.PasswordHash;
                    log.Trace("Перевiрка на правильнiсть пароль");
                    if (Convert.ToBase64String(Hesh) == Convert.ToBase64String(PBKDF2.HashPassword(Encoding.Unicode.GetBytes(password), UserKeyValue.Salt, 32)))
                    {
                        return true;
                    }

                    else
                    {
                        log.Warn($"Невiрний пароль. Пароль у словнику = {Convert.ToBase64String(Hesh)} . Пароль введений користувачем = {Convert.ToBase64String(PBKDF2.HashPassword(Encoding.Unicode.GetBytes(password), UserKeyValue.Salt, 32))}");
                        return false;
                    }
                }
                catch (Exception ex)
                {
                    log.Error(ex, "Помилка: введено неiснуючого користувача");
                    return false;
                }
                //FATALL при неiснуючому користувачу



            }

            public static void LogIn(string userName, string password)
            {
                // Перевiрка пароля
                log.Trace("LogIn");
                log.Trace("Виклик функцiї CheckPassword");
                if (CheckPassword(userName, password))
                {
                    // Створюється екземпляр автентифiкованого користувача
                    var identity = new GenericIdentity(userName, "OIBAuth");
                    // Виконується прив’язка до ролей, до яких належить користувач
                    var principal = new GenericPrincipal(identity, _users[userName].Roles);
                    // Створений екземпляр автентифiкованого користувача з вiдповiдними
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
                // Перевiрка того, що потiк програми виконується автентифiкованим користувачем iз

                if (Thread.CurrentPrincipal == null)
                {
                    log.Fatal("Thread.CurrentPrincipal cannot be null.");
                    throw new SecurityException("Thread.CurrentPrincipal cannot be null.");

                }
                log.Trace("Перевiрка того, що автентифiкований користувач належить до ролi 'Guest'");
                if (Thread.CurrentPrincipal.IsInRole("Guest"))
                {
                    Console.WriteLine("You have access to Guest.");
                }
                else
                {
                    Console.WriteLine("You DON`T have access to Guest.");
                }
                log.Trace("Перевiрка того, що автентифiкований користувач належить до ролi 'User'");
                if (Thread.CurrentPrincipal.IsInRole("User"))
                {
                    Console.WriteLine("You have access to User.");
                }
                else
                {
                    Console.WriteLine("You DON`T have access to User.");
                }
                log.Trace("Перевiрка того, що автентифiкований користувач належить до ролi 'First level Admin'");
                if (Thread.CurrentPrincipal.IsInRole("First level Admin"))
                {
                    Console.WriteLine("You have access to this First level Admin feature.");
                }
                else
                {
                    Console.WriteLine("You DON`T have access to this First level Admin feature.");
                }
                log.Trace("Перевiрка того, що автентифiкований користувач належить до ролi 'Admin'");
                if (Thread.CurrentPrincipal.IsInRole("Admin"))
                {
                    Console.WriteLine("You have access to Admin feature.");
                }
                else
                {
                    Console.WriteLine("You DON`T have access to Admin feature.");
                }


            }

            static void Main(string[] args)
            {
                Logger log = LogManager.GetCurrentClassLogger();
                //log.Trace("trace message.");
                //log.Debug("Debug m");
                //log.Info("Info m");
                //log.Warn("warn m");
                //log.Error("error m");
                //log.Fatal("fatal m");

                Console.WriteLine("Реєстрацiя аккаунтiв");


                string LogMem;
                string PassMem;
                int RolCount;

                string LogMem2 = null;
                string PassMem2 = null;
                int RolCount2 = 0;

                Console.WriteLine("");
                Console.WriteLine("Ролi користувачiв");
                Console.WriteLine("1) Guest");
                Console.WriteLine("2) User");
                Console.WriteLine("3) First level Admin");
                Console.WriteLine("4) Admin");
                Console.WriteLine("");
                Console.WriteLine("");
                // лiчильник iтерацiй, щоб було 4 користувача
                int lich = 0;
                int lich2 = 0;

                while (lich < 2)
                {
                    log.Info("Реєстрацiя користувача");
                    Console.Write("Введiть логiн: ");
                    LogMem = Console.ReadLine();
                    Console.WriteLine("");
                    log.Trace($"Присвоєння змiннiй LogMem логiну користувача. Минуле значення змiнної = '{LogMem2}' Нове значення змiнної = '{LogMem}'");
                    log.Debug($"Присвоєння змiннiй LogMem логiну користувача. Минуле значення змiнної = '{LogMem2}' Нове значення змiнної = '{LogMem}'");
                    Console.WriteLine("");
                    LogMem2 = LogMem;

                    Console.Write("Введiть пароль: ");
                    PassMem = Console.ReadLine();
                    Console.WriteLine("");
                    log.Trace($"Присвоєння змiннiй PassMem паролю користувача. Минуле значення змiнної = '{PassMem2}' Нове значення змiнної = '{PassMem}' ");
                    log.Debug($"Присвоєння змiннiй PassMem паролю користувача. Минуле значення змiнної = '{PassMem2}' Нове значення змiнної = '{PassMem}' ");
                    Console.WriteLine("");
                    PassMem2 = PassMem;

                    Console.WriteLine("Скiльки хочете додати ролей?");
                    try
                    {
                        RolCount = Int32.Parse(Console.ReadLine());
                    }
                    catch (Exception ex)
                    {
                        log.Error(ex, "Проблема при введеннi кiлькостi ролей");
                        Console.WriteLine("Скiльки хочете додати ролей? Введiть будт ласка число");
                        RolCount = Int32.Parse(Console.ReadLine());
                    }

                    Console.WriteLine("");
                    log.Trace($"Присвоєння змiннiй RolCount кiлькостi ролей у користувача. Минуле значення змiнної = '{RolCount2}' Нове значення змiнної = '{RolCount}' ");
                    log.Debug($"Присвоєння змiннiй RolCount кiлькостi ролей у користувача. Минуле значення змiнної = '{RolCount2}' Нове значення змiнної = '{RolCount}' ");
                    Console.WriteLine("");
                    RolCount2 = RolCount;

                    while (0 > RolCount || RolCount > 5)
                    {
                        Console.WriteLine("");
                        log.Trace("Входження в цикл while, якщо значення змiнної RolCount менше 1 або бiльше 5 ");
                        Console.WriteLine("");
                        Console.WriteLine("Введiть число вiд 1 до 4");
                        RolCount = Int32.Parse(Console.ReadLine());
                        Console.WriteLine("");
                        log.Trace($"Присвоєння змiннiй RolCount кiлькостi ролей у користувача. Минуле значення змiнної = '{RolCount2}' Нове значення змiнної = '{RolCount}' ");
                        log.Debug($"Присвоєння змiннiй RolCount кiлькостi ролей у користувача. Минуле значення змiнної = '{RolCount2}' Нове значення змiнної = '{RolCount}' ");
                        Console.WriteLine("");
                        RolCount2 = RolCount;
                    }

                    string[] RolMem = new string[RolCount];
                    for (int i = 0; i < RolCount; i++)
                    {
                        Console.WriteLine("");
                        log.Trace("Ведення ролей у масив RolMem");
                        Console.WriteLine("");
                        Console.Write("Введiть роль: ");
                        RolMem[i] = Console.ReadLine();
                        Console.WriteLine("");
                        log.Trace($"Ведення ролi у масив RolMem. Минуле значення змiнної у масивi = '' Нове значення змiнної у масивi = '{RolMem[i]}'");
                        log.Debug($"Ведення ролi у масив RolMem. Минуле значення змiнної у масивi = '' Нове значення змiнної у масивi = '{RolMem[i]}'");
                        Console.WriteLine("");
                    }
                    Console.WriteLine("");
                    log.Trace("Реєстрацiя користувача");
                    Console.WriteLine("");
                    Protector.Register(LogMem, PassMem, RolMem);
                    lich++;
                    Console.WriteLine("");
                    log.Trace($"Перехiд до наступної iтерацiї циклу. Минуле значення змiнної = '{lich2}' Нове значення змiнної = '{lich}'");
                    log.Debug($"Перехiд до наступної iтерацiї циклу. Минуле значення змiнної = '{lich2}' Нове значення змiнної = '{lich}'");
                    Console.WriteLine("");
                    lich2 = lich;
                    Console.WriteLine("");
                    Console.WriteLine("");

                }


                // login
                log.Info("Вхiд у аккаунт користувача");
                Console.WriteLine("");
                log.Trace("Вхiд в бескiнчений цикл реєстрацiї");
                Console.WriteLine("");
                while (true)
                {
                    Console.WriteLine("Вхiд до аккаунту");

                    Console.Write("Введiть логiн: ");
                    LogMem = Console.ReadLine();
                    Console.WriteLine("");
                    log.Trace($"Присвоєння змiннiй LogMem логiну користувача. Минуле значення змiнної = '{LogMem2}' Нове значення змiнної = '{LogMem}'");
                    log.Debug($"Присвоєння змiннiй LogMem логiну користувача. Минуле значення змiнної = '{LogMem2}' Нове значення змiнної = '{LogMem}'");
                    Console.WriteLine("");
                    LogMem2 = LogMem;

                    Console.Write("Введiть пароль: ");
                    PassMem = Console.ReadLine();
                    Console.WriteLine("");
                    log.Trace($"Присвоєння змiннiй PassMem паролю користувача. Минуле значення змiнної = '{PassMem2}' Нове значення змiнної = '{PassMem}' ");
                    log.Debug($"Присвоєння змiннiй PassMem паролю користувача. Минуле значення змiнної = '{PassMem2}' Нове значення змiнної = '{PassMem}' ");
                    Console.WriteLine("");
                    PassMem2 = PassMem;

                    Console.WriteLine("");
                    log.Trace("Виклик функцiї LogIn");
                    Console.WriteLine("");
                    Protector.LogIn(LogMem, PassMem);

                    Console.WriteLine("");
                    log.Trace("Виклик функцiї OnlyForAdminsFeature");
                    Console.WriteLine("");
                    Protector.OnlyForAdminsFeature();
                }
            }
        }
    }
}