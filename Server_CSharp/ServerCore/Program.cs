
namespace ServerCore
{
    class Program
    {
        static void MainThread(object state)
        {
            Console.WriteLine($"{state}");
        }

        static void Main(string[] args)
        {
            ThreadPool.QueueUserWorkItem(MainThread, "Hello from the thread pool.");
            ThreadPool.backgra
            Console.WriteLine("Main thread does some work, then sleeps.");

            ThreadPool.
            Console.WriteLine("Main thread exits.");
        }
    }
}