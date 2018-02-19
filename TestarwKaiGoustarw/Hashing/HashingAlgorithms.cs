using System;
using System.IO;
using System.Security.Cryptography;

namespace TestarwKaiGoustarw.Hashing
{
    public static class HashingAlgorithms
    {
        public static readonly HashAlgorithm Md5 = new MD5CryptoServiceProvider();
        public static readonly HashAlgorithm Sha1 = new SHA1Managed();
        public static readonly HashAlgorithm Sha256 = new SHA256Managed();
        public static readonly HashAlgorithm Sha384 = new SHA384Managed();
        public static readonly HashAlgorithm Sha512 = new SHA512Managed();
        public static readonly HashAlgorithm Ripemd160 = new RIPEMD160Managed();


        public static string GetHashFromFile(string filePath, HashAlgorithm algorithm)
        {
            using (BufferedStream stream = new BufferedStream(File.OpenRead(filePath)))
            {
                return BitConverter.ToString(algorithm.ComputeHash(stream)).Replace("-", string.Empty);
            }
        }
    }
}
