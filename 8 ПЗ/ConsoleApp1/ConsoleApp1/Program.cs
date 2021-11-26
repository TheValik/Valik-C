using System;
using System.IO;
using System.Security.Cryptography;
using System.Text;

namespace ConsoleApp1
{
    class DigitalSignature
    {
        public static byte[] ComputeHashSha256(byte[] toBeHashed)
        {
            using (var sha256 = SHA256.Create())
            {
                return sha256.ComputeHash(toBeHashed);
            }
        }

        private readonly static string CspContainerName = "RsaContainer";
        public void AssignNewKey()
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

        public byte[] SignData(byte[] hashOfDataToSign)
        {

            var cspParams = new CspParameters
            {
                KeyContainerName = CspContainerName,

            };

            using (var rsa = new RSACryptoServiceProvider(cspParams))
            {
                rsa.PersistKeyInCsp = true;
                var rsaFormatter = new RSAPKCS1SignatureFormatter(rsa);
                rsaFormatter.SetHashAlgorithm("SHA256");
                return rsaFormatter.CreateSignature(hashOfDataToSign);
            }
        }
        public bool VerifySignature(byte[] hashOfDataToSign,byte[] signature)
        {
            using (var rsa = new RSACryptoServiceProvider())
            {
                rsa.PersistKeyInCsp = false;
                rsa.FromXmlString(File.ReadAllText("F:/Навчання/С#/7 ПЗ/KovalValentyn.xml"));
                var rsaDeformatter = new RSAPKCS1SignatureDeformatter(rsa);
                rsaDeformatter.SetHashAlgorithm("SHA256");
                return rsaDeformatter.VerifySignature(hashOfDataToSign, signature);
            }
        }

        static void Main(string[] args)
        {
            var document = Encoding.UTF8.GetBytes("Document to Sign");
            byte[] hashedDocument = ComputeHashSha256(document);
            var digitalSignature = new DigitalSignature();
            digitalSignature.AssignNewKey();
            var signature = digitalSignature.SignData(hashedDocument);
            var verified = digitalSignature.VerifySignature(hashedDocument, signature);


            //False document
            var documentFalse = Encoding.UTF8.GetBytes("Document to Sign2");
            byte[] hashedDocumentFalse = ComputeHashSha256(documentFalse);
            var digitalSignatureFalse = new DigitalSignature();
            digitalSignatureFalse.AssignNewKey();
            var signatureFalse = digitalSignature.SignData(hashedDocumentFalse);
            var verifiedFalse1 = digitalSignature.VerifySignature(hashedDocumentFalse, signature);

            Console.WriteLine("Цифровий пiдпис у .NET");
            Console.WriteLine("---------------------------------------");
            Console.WriteLine(" Original Text = " + Encoding.Default.GetString(document));
            Console.WriteLine();
            Console.WriteLine(" False Text = " + Encoding.Default.GetString(documentFalse));
            Console.WriteLine();
            Console.WriteLine("Перевiка оригiнального цифрового пiдпису");
            if (verified)
            {
                Console.WriteLine("Цифровий пiдпис коректний");
            }else
            {
                Console.WriteLine("Цифровий пiдпис не коректний");
            }
            Console.WriteLine();
            Console.WriteLine();

            Console.WriteLine("Перевiка хибного документу");
            if (verifiedFalse1)
            {
                Console.WriteLine("Документ коректний");
            }
            else
            {
                Console.WriteLine("Документ не коректний");
            }
            Console.WriteLine();
            Console.WriteLine();


        }
            
    }
}
