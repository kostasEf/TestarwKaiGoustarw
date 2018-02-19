using System;

namespace TestarwKaiGoustarw.Hashing
{
    public static class HashTest
    {
        public static void RunTest()
        {
            string path = @"C:\FileStorageSource\abc.txt";

        string checksumMd5 = HashingAlgorithms.GetHashFromFile(path, HashingAlgorithms.Md5);
        string checksumSha1 = HashingAlgorithms.GetHashFromFile(path, HashingAlgorithms.Sha1);
        string checksumSha256 = HashingAlgorithms.GetHashFromFile(path, HashingAlgorithms.Sha256);
        string checksumSha384 = HashingAlgorithms.GetHashFromFile(path, HashingAlgorithms.Sha384);
        string checksumSha512 = HashingAlgorithms.GetHashFromFile(path, HashingAlgorithms.Sha512);
        string checksumRipemd160 = HashingAlgorithms.GetHashFromFile(path, HashingAlgorithms.Ripemd160);

        Console.WriteLine("checksumMd5 -> " + checksumMd5);
        Console.WriteLine("checksumSha1 -> " + checksumSha1);
        Console.WriteLine("checksumSha256 -> " + checksumSha256);
        Console.WriteLine("checksumSha384 -> " + checksumSha384);
        Console.WriteLine("checksumSha512 -> " + checksumSha512);
        Console.WriteLine("checksumRipemd160 -> " + checksumRipemd160);

        Console.WriteLine("Speak friend and enter");
        Console.ReadLine();
        }
        
    }
}
