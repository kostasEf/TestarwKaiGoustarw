using System;
using System.IO;
using OneFile.ClassLibrary;

namespace CoreApp
{
    public static class TestNuget
    {
        private static readonly Guid dataControllerId = Guid.Parse("2aa2aad6-8c2f-497d-8301-e6dfe054f270");
        private static FileManagement fm;
        private static void Initialize()
        {
            fm = new FileManagement();
            fm.Configure("net.tcp://localhost:9000/FileManagement/");
        }

        public static void Save(String path)
        {
            Initialize();
            Console.WriteLine(fm.SaveFile(path, dataControllerId));
            Console.WriteLine("Has finished");
            Console.ReadLine();
        }

        public static void Get(Guid id)
        {
            Initialize();

            MemoryStream str = (MemoryStream)fm.GetFile(id, dataControllerId);

            Console.WriteLine($@"C:\FileStorageSource\ReceivedFiles\ {id} .txt {str.Length}");

            Console.WriteLine("Has finished");
            Console.ReadLine();
        }

        public static void Delete(Guid id)
        {
            Initialize();

            bool isDeleted = fm.DeleteFile(id, dataControllerId);

            Console.WriteLine(isDeleted);

            Console.WriteLine("Has finished");
            Console.ReadLine();
        }
    }
}