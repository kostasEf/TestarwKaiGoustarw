using System;
using TestarwKaiGoustarw.Thumbnails;

namespace TestarwKaiGoustarw
{
    class Program
    {
        static void Main()
        {
            ThumbnailGenerator thumbnail = new ThumbnailGenerator();
            thumbnail.ResizeAndKeepAspect();

            Console.WriteLine("Press any key to exit");
            Console.ReadLine();

            Console.ReadLine();
        }


    }


}
