using System;
using System.Threading;

namespace TestarwKaiGoustarw.MethodsAsParameters
{
    public class EncryptionRelatedMethods
    {
        public void Start()
        {
            Console.WriteLine("Encryption thread started......\n");

            EncryptFile();
            Thread.Sleep(3000);
        }

        private bool EncryptFile()
        {
            Console.WriteLine("Encrypt filed exekuted......\n");

            return true;
        }

        
    }
}
