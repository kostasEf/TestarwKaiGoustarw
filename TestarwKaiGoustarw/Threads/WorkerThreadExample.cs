using System;
using System.Diagnostics;
using System.Threading;

namespace TestarwKaiGoustarw.Threads
{
    public class EncryptionServices
    {
        // This method will be called when the thread is started. 
        public void DoWork()
        {
            while (!shouldStop)
            {
                Thread.Sleep(5000);
                Console.WriteLine("EncryptionThread: encrypting...");
            }
            Console.WriteLine("EncryptionThread: terminating gracefully.");
        }
        public void RequestStop()
        {
            shouldStop = true;
        }
        // Volatile is used as hint to the compiler that this data 
        // member will be accessed by multiple threads. 
        private volatile bool shouldStop;
    }

    public class BackupService
    {
        // This method will be called when the thread is started. 
        public void DoWork()
        {
            while (!shouldStop)
            {
                Thread.Sleep(22000);
                Console.WriteLine("BackupThread: encrypting...");
            }
            Console.WriteLine("BackupThread: terminating gracefully.");
        }
        public void RequestStop()
        {
            shouldStop = true;
        }
        // Volatile is used as hint to the compiler that this data 
        // member will be accessed by multiple threads. 
        private volatile bool shouldStop;
    }

    public static class WorkerThreadExample
    {
        public static void Do()
        {
            // Create the thread object. This does not start the thread.
            EncryptionServices encryptionService = new EncryptionServices();
            BackupService backupService = new BackupService();

            Thread EncryptionThread = new Thread(encryptionService.DoWork);
            Thread EncryptionThread2 = new Thread(encryptionService.DoWork);
            Thread EncryptionThread3 = new Thread(encryptionService.DoWork);
            Thread BackupThread = new Thread(backupService.DoWork);

            // Start the worker thread.
            EncryptionThread.Start();
            EncryptionThread2.Start();
            EncryptionThread3.Start();
            Console.WriteLine("EncryptionThread: Starting worker thread...");

            // Start the worker thread.
            BackupThread.Start();
            Console.WriteLine("BackupThread: Starting worker thread...");

            // Loop until worker thread activates. 
            while (!EncryptionThread.IsAlive)
            {
            }

            // Loop until worker thread activates. 
            while (!BackupThread.IsAlive)
            {
            }

            // Put the main thread to sleep for 1 millisecond to 
            // allow the worker thread to do some work:
            //Thread.Sleep(10000);

            // Request that the worker thread stop itself:
            encryptionService.RequestStop();
            backupService.RequestStop();


            // Use the Join method to block the current thread  
            // until the object's thread terminates.
            EncryptionThread.Join();
            Console.WriteLine("EncryptionThread: Worker thread has terminated.");

            // Use the Join method to block the current thread  
            // until the object's thread terminates.
            BackupThread.Join();
            Console.WriteLine("BackupThread: Worker thread has terminated.");
        }

        public static void Join()
        {
            var stopwatch = Stopwatch.StartNew();
            // Create an array of Thread references.
            Thread array =  new Thread(new ThreadStart(Start));
            array.Start();
            // Join all the threads.
            
            array.Join();
            
            Console.WriteLine("DONE: {0}", stopwatch.ElapsedMilliseconds);
        }

        static void Start()
        {
            // This method takes ten seconds to terminate.
            for (int i = 0; i < 10000; i++)
            {
                Console.WriteLine(i);
            }
        }
    }
}
