using System;
using System.IO;

namespace TestarwKaiGoustarw.Files
{
    public static class CopyFile
    {
        public static void Copy()
        {

            string file = @"C:\FileStorageSource\abc.txt";

            string destiation = @"C:\NewFolder\" + Guid.NewGuid();

            try
            {
                //If directory does not exist, it does not get created automaticaly
                if (!Directory.Exists(@"C:\NewFolder\"))
                {
                    Directory.CreateDirectory(@"C:\NewFolder\");
                }

                File.Copy(file, destiation);
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
            }

        }
    }
}
