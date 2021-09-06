using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace AwaitIntro
{
    class Program
    {
        static async void DoWork()
        {
            Task<int> t = Task.Factory.StartNew(() =>
            {
                Console.WriteLine("TaskId={0} ThreadId={1}", 
                    Task.CurrentId, Thread.CurrentThread.ManagedThreadId);
                for (int i = 0; i < 100; i++)
                    Console.Write("*");
                return 100;
            });
            Task<int> s = Task.Factory.StartNew(() =>
            {
                Console.WriteLine("TaskId={0} ThreadId={1}",
                    Task.CurrentId, Thread.CurrentThread.ManagedThreadId);
                for (int i = 0; i < 10; i++)
                    Console.Write("#");
                return 200;
            });
            Console.WriteLine("DoWork ThreadId={0}", 
                Thread.CurrentThread.ManagedThreadId);
            int result1 = await t;
            Console.WriteLine("DoWork ThreadId={0}",
                Thread.CurrentThread.ManagedThreadId);
            int result2 = await s;
            Console.WriteLine("DoWork ThreadId={0}",
                Thread.CurrentThread.ManagedThreadId);
            Console.WriteLine("DoWork for t finished with result {0}",result1);
    
            Console.WriteLine("DoWork for s finished with result {0}", result2);
        }
        static void Main(string[] args)
        {
            Console.WriteLine("Main ThreadId={0}",
                Thread.CurrentThread.ManagedThreadId);
            DoWork();
            Console.WriteLine("Main finished");
            Console.ReadKey();
        }
    }
}
