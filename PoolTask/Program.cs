using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace PoolTask
{
    class Program
    {
        
        static void Main(string[] args)
        {
            Task delayedTask = new Task(() =>
             {
                 for (int i = 0; i < 1000; i++)
                     Console.Write(".");
             });
            Console.WriteLine("Simulate waiting for Request...");
            Thread.Sleep(10000);
            delayedTask.Start();
            delayedTask.Wait();
            Request[] requests =
            {
                new Request() {Data = "#" },
                new Request() {Data = "$" },
                new Request() {Data = "%" },
                new Request() {Data = "!" },
            };
            Task[] pool = {
             new Task(requests[0].TaskHandler),
             new Task(requests[1].TaskHandler),
             new Task(requests[2].TaskHandler),
             new Task(requests[3].TaskHandler)
            };
            foreach (var task in pool)
            {
                Console.WriteLine("\nWait for request...");
                Thread.Sleep(1000);
                task.Start();
            }
            Task.WaitAll(pool);
            Console.WriteLine("\nExecution finished");
            Console.ReadKey();
        }
    }
}
