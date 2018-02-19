using System;
using System.Collections.Concurrent;

namespace TestarwKaiGoustarw.Queue
{
    public static class EncryptionQueue
    {
        private static readonly ConcurrentQueue<Guid> queue = new ConcurrentQueue<Guid>();

        public static void Add(Guid fileGuid)
        {
            queue.Enqueue(fileGuid);
        }

        public static Guid GetNext()
        {
            queue.TryDequeue(out Guid result);
            return result;
        }

        public static bool IsEmpty()
        {
            return queue.IsEmpty;
        }
    }
}
