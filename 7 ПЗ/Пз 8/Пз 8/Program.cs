using System;
using System.IO;
using System.Security.Cryptography;
using System.Text;

namespace ПЗ_7
{
    public class RSAWithRSAParameterKey
    {

        // Збереження закритого ключа
        private readonly static string CspContainerName = "RsaContainer";
        public static void AssignNewKey3()
        {
            CspParameters cspParameters = new CspParameters(1)
            {
                KeyContainerName = CspContainerName,

                ProviderName = "Microsoft Strong Cryptographic Provider"
            };
            var rsa = new RSACryptoServiceProvider(cspParameters)
            {
                PersistKeyInCsp = true
            };
            File.WriteAllText("F:/Навчання/С#/7 ПЗ/KovalValentyn.xml", rsa.ToXmlString(false));
        }


        // Шифрування
        public static byte[] EncryptData2(string publicKeyPath, byte[] dataToEncrypt)
        {
            byte[] cipherbytes;
            using (var rsa = new RSACryptoServiceProvider(2048))
            {
                rsa.PersistKeyInCsp = false;
                rsa.FromXmlString(File.ReadAllText(publicKeyPath));
                cipherbytes = rsa.Encrypt(dataToEncrypt, false);
            }
            return cipherbytes;
        }


        // Розшифрування
        public static byte[] DecryptData(byte[] dataToDecrypt)
        {
            byte[] plainBytes;
            var cspParams = new CspParameters
            {
                KeyContainerName = CspContainerName,

            };
            using (var rsa = new RSACryptoServiceProvider(cspParams))
            {
                rsa.PersistKeyInCsp = true;
                plainBytes = rsa.Decrypt(dataToDecrypt, false);
            }
            return plainBytes;
        }

    }

    class Program
    {

        static void Main(string[] args)
        {
            string a = "I am VALENTYN!!!";
            // Створення ключів
            //RSAWithRSAParameterKey.AssignNewKey3();

            // Ключі для шифрування повідомлень
            string PublickMyKey1 = "F:/Навчання/С#/7 ПЗ/Key_for_Encr/KovalValentyn.xml";
            string PublickKeyBorysenko = "F:/Навчання/С#/7 ПЗ/Key_for_Encr/Borysenko_Public.xml";
            string PublickKeyBolshakovArtem = "F:/Навчання/С#/7 ПЗ/Key_for_Encr/BolshakovArtem.xml";

            //Шлях до зашифрованих файлів
            string MyFile = "F:/Навчання/С#/7 ПЗ/File_to_decr/EncrByteText.dat";
            string BorysenkoFile = "F:/Навчання/С#/7 ПЗ/File_to_decr/MyMessage(maks).dat";
            string BolshakovArtemFile = "F:/Навчання/С#/7 ПЗ/File_to_decr/encryptMessageBArtem.dat";


            // Шифрування повідомлення для когось
            //var Encr = RSAWithRSAParameterKey.EncryptData2(PublickKeyKozachenko, Encoding.Unicode.GetBytes(a));
            //// Запис зашифрованого повідомлення у файл
            //File.WriteAllBytes("F:/Навчання/С#/7 ПЗ/File_to_send/EncrByteTextKozachenko.dat", Encr);



            // Розшифрування отриманих файлів
            var EncrFile = File.ReadAllBytes(BolshakovArtemFile);
            var DecrFile = RSAWithRSAParameterKey.DecryptData(EncrFile);


            Console.WriteLine("Зашифроване повiдомлення з файлу:  " + Encoding.Unicode.GetString(EncrFile));
            Console.WriteLine("");
            Console.WriteLine("Розшифроване повiдомлення з файлу:  " + Encoding.Unicode.GetString(DecrFile));


        }
    }
}