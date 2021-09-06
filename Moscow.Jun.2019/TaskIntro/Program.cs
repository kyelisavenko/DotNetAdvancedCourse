using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace TaskIntro
{
    class Program
    {
        static void TaskHandler()
        {
            for (int i = 0; i < 100; i++)
                Console.Write("*");
        }
        static void Main(string[] args)
        {
            Task[] tasks = new Task[]
            {
               new TaskFactory().StartNew(TaskHandler),
               new TaskFactory().StartNew(() =>
            {
                for (int i = 0; i < 100; i++)
                {
                    Console.Write("#");
                    Thread.Sleep(10);
                }
            })
            };
            //Task.WaitAny(tasks);
            Task calculate = Task.Factory.ContinueWhenAny(tasks,
                (res) =>
                {
                    for (int i = 0; i < 100; i++)
                    {
                        Console.Write("@");
                        Thread.Sleep(10);
                    }
                });

            calculate.Wait();
            Task<int> t = Task.Run(() => {
                int i = 0;
                for (; i < 100; i++)
                    Console.Write("!");
                return i;
            });
            Task x = t.ContinueWith((prev) =>
            {
                for (int i = 0; i < 100; i++)
                    Console.Write("{0} ", prev.Result);
            });
            Console.WriteLine("Continue main thread...");
            Console.WriteLine("Task result = {0}", t.Result);
            x.Wait();
        }
    }
}
