using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace NET_003Live
{
    class Program
    {
        static void Main(string[] args)
        {
            //Task t = Task.Factory.StartNew(
            //    () => Console.WriteLine("Hello from task"));

            //Task t2 = Task.Factory.StartNew(
            //    () => Console.WriteLine("hello from second task"));
            //t2.Wait();
            //t.Wait();
            Task t = Task.Factory.StartNew(
                () =>
                {
                    for (int i = 0; i < 100; i++)
                        Console.Write("*");
                }, TaskCreationOptions.LongRunning
                );
            Task t2 = Task.Factory.StartNew(
                () =>
                {
                    for (int i = 0; i < 100; i++)
                        Console.Write(".");
                    throw new NotImplementedException();
                }, TaskCreationOptions.LongRunning
                );
            Task<int> tr = Task<int>.Factory.StartNew(
                () =>
                {
                    Thread.Sleep(10000);
                    return 2;
                });
            t.Wait();
            try
            {
                t2.Wait();
                Console.WriteLine("ok");
            }
            catch (Exception) { };
            Console.WriteLine("t2 Is cancelled? {0}", t2.IsCanceled);
            Console.WriteLine("t2 Is completed? {0}", t2.IsCompleted);
            Console.WriteLine("t2 Is Crashed? {0}", t2.IsFaulted);

            Console.WriteLine("------");
            Console.WriteLine(tr.Result);
        }
    }
}
