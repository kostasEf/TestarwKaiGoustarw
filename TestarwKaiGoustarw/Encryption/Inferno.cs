using System;
using System.IO;
using System.Security.Cryptography;
using SecurityDriven.Inferno;

namespace TestarwKaiGoustarw.Encryption
{
    public static class Inferno
    {
        static CryptoRandom random = new CryptoRandom();
        //static byte[] key = random.NextBytes(32);
        static byte[] key = new byte[] {1, 2, 3, 4, 5, 6, 7, 8, 9, 0};

        public static void Encrypt(string originalFilename)
        {
            string encryptedFilename = originalFilename + ".enc" + Path.GetExtension(originalFilename);

            using (var originalStream = new FileStream(originalFilename, FileMode.Open))
            using (var encryptedStream = new FileStream(encryptedFilename, FileMode.Create))
            using (var encTransform = new EtM_EncryptTransform(key: key))
            using (var cryptoStream = new CryptoStream(encryptedStream, encTransform, CryptoStreamMode.Write))
            {
                originalStream.CopyTo(cryptoStream);
            }

            File.Delete(originalFilename);
        }

        public static void Decrypt(string originalFilename)
        {
            string encryptedFilename = originalFilename + ".enc" + Path.GetExtension(originalFilename);
            string decryptedFilename = originalFilename;

            using (var encryptedStream = new FileStream(encryptedFilename, FileMode.Open))
            using (var decryptedStream = new FileStream(decryptedFilename, FileMode.Create))
            using (var decTransform = new EtM_DecryptTransform(key: key))
            {
                using (var cryptoStream = new CryptoStream(encryptedStream, decTransform, CryptoStreamMode.Read))
                    cryptoStream.CopyTo(decryptedStream);

                if (!decTransform.IsComplete) throw new Exception("Not all blocks are decrypted.");
            }

            File.Delete(encryptedFilename);
        }

        public static void Authenticate(string originalFilename)
        {
            string encryptedFilename = originalFilename + ".enc" + Path.GetExtension(originalFilename);

            using (var encryptedStream = new FileStream(encryptedFilename, FileMode.Open))
            using (var decTransform = new EtM_DecryptTransform(key: key, authenticateOnly: true))
            {
                using (var cryptoStream = new CryptoStream(encryptedStream, decTransform, CryptoStreamMode.Read))
                    cryptoStream.CopyTo(Stream.Null);

                if (!decTransform.IsComplete) throw new Exception("Not all blocks are authenticated.");
            }
        }


        public static void ShortEncrypt()
        {
            
        }
    }
}
