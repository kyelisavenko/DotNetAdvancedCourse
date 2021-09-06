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
        static async void  RunAsyncTask()
        {
            Console.WriteLine("Thread {0}", Thread.CurrentThread.ManagedThreadId);
            Task<int> t = Task<int>.Factory.StartNew(() =>
            {
                Thread.Sleep(TimeSpan.FromSeconds(10));
                return 2;
            });
            int result = await t;
            Console.WriteLine("Result: {0}, Thread {1}",
                result, Thread.CurrentThread.ManagedThreadId);
        }
        static void Main(string[] args)
        {
            RunAsyncTask();
            Console.WriteLine("Main Thread {0}:", Thread.CurrentThread.ManagedThreadId);
            Thread.Sleep(TimeSpan.FromSeconds(30));
            Console.WriteLine("Main Thread {0}:", Thread.CurrentThread.ManagedThreadId);
            Console.ReadKey();
        }
    }
}
