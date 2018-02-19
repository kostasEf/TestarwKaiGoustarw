using System;
using System.Collections.Concurrent;

namespace TestarwKaiGoustarw.Queue
{
    public class QueueService
    {
        private readonly ConcurrentQueue<Guid> queue;
        private readonly QueueService instance;
        private Guid backupDisGuid;
        private bool doWork;

        public QueueService()
        {
            queue = new ConcurrentQueue<Guid>();
            instance = this;
            Start();
        }

        private void Start()
        {
            while (doWork)
            {
                ConsumeNext();
            }
        }

        public QueueService GetInstance()
        {
            return instance;
        }

        public void Add(Guid fileGuid)
        {
            queue.Enqueue(fileGuid);
        }

        protected virtual void ConsumeNext()
        {
            queue.TryDequeue(out backupDisGuid);
            // Do the work
        }


        public void Stop()
        {
            doWork = false;
        }
    }
}
