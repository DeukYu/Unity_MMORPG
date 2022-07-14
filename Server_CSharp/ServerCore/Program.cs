
namespace ServerCore
{
    class Program
    {
        static int x = 0;
        static int y = 0;
        static int r1 = 0;
        static int r2 = 0;

        static void Thread_1()
        {
            y = 1; // Store 
            Thread.MemoryBarrier(); // 메모리 배리어
            r1 = x; // Load
        }

        static void Thread_2()
        {
            x = 1; // Store
            Thread.MemoryBarrier(); // 메모리 배리어
            r2 = y; // Load 
        }

        static void Main(string[] args)
        {
            int count = 0;

            while (true)
            {
                x = y = r1 = r2 = 0;
                count++;

                Task t1 = new Task(Thread_1);
                Task t2 = new Task(Thread_2);

                t1.Start();
                t2.Start();

                Task.WaitAll(t1, t2); // 제공된 모든 Task 개체의 실행이 완료되기를 기다립니다.

                if (r1 == 0 && r2 == 0)
                {
                    break;
                }
            }

            Console.WriteLine($"{count}번 만에 빠져 나옴");
        }
    }
}