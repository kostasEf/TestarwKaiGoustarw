using System;
using System.Collections.Concurrent;

namespace TestarwKaiGoustarw.Queue
{
    public class EncryptionService
    {
        private bool doWork;
        private ConcurrentQueue<int> queue;
        public string name;

        public EncryptionService(ConcurrentQueue<int> queue, string name)
        {
            doWork = true;
            this.queue = queue;
            this.name = name;
        }

        private void Start()
        {
            while (doWork)
            {
                if (!queue.IsEmpty)
                {
                    //encrypt EncryptionQueue.GetNext();

                    int x;
                    queue.TryDequeue(out x);
                    Console.WriteLine(name + ": " + x);
                    //Send FileEncrypted event
                }
                else
                {
                    //Send queue empty
                    return;
                }
            }
        }

        public void RequestStart()
        {
            Start();
        }

        public void RequestStop()
        {
            doWork = false;
        }
    }
}
