using System;
using System.Threading;

namespace ServerCore
{
    class Program
    {
        static volatile int count = 0;
        static Lock _lock = new Lock();
        static void Main(string[] args)
        {
            Task t1 = new Task(delegate ()
            {
                for (int i = 0; i < 10000; ++i)
                {
                    _lock.WriteLock();
                    count++;
                    _lock.WriteUnlock();
                }
            });
            Task t2 = new Task(delegate ()
            {
                for (int i = 0; i < 10000; ++i)
                {
                    _lock.WriteLock();
                    count--;
                    _lock.WriteUnlock();
                }
            });

            t1.Start();
            t2.Start();
            Task.WaitAll(t1, t2);
            Console.WriteLine(count);
        }
    }
}