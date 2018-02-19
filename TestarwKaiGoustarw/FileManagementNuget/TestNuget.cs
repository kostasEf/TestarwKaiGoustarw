using System;
using System.Collections.Generic;
using FileManagementWCF;

public static class TestNuget
{
    //2AA2AAD6-8C2F-497D-8301-E6DFE054F270
    private static readonly Guid dataControllerId = Guid.Parse("2AA2AAD6-8C2F-497D-8301-E6DFE054F270");
    private static FileManagement.ClassLibrary.FileManagement fm;
    private static void Initialize()
    {
        fm = new FileManagement.ClassLibrary.FileManagement("net.tcp://localhost:9000/FileManagement/");
        //fm.Configure("net.tcp://localhost:9000/FileManagement/");
    }

    //public static void Save(String path)
    //{
    //    Initialize();
    //    fm.SaveFile(path, dataControllerId, 1);
    //}

    //public static void Get(Guid id)
    //{
    //    Initialize();

    //    Stream str = fm.GetFile(id, dataControllerId);

    //    Console.WriteLine($@"C:\FileStorageSource\ReceivedFiles\ {id} .txt {str.Length}");

    //    Console.WriteLine("Has finished");
    //    Console.ReadLine();
    //}

    //public static void Delete(Guid id)
    //{
    //    Initialize();

    //    bool isDeleted = fm.DeleteFile(id, dataControllerId);

    //    Console.WriteLine(isDeleted);

    //    Console.WriteLine("Has finished");
    //    Console.ReadLine();
    //}

    public static void GetControllers()
    {
        Initialize();
        IList<DataController> list = fm.GetDataContollers("D33pInsid3W3AllMissTh3Val!nat0r", 1);

        foreach (var VARIABLE in list)
        {
            Console.WriteLine(VARIABLE.Title);
        }
    }
}