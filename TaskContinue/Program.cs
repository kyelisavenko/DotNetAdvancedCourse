using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace TaskContinue
{
    class Program
    {
        static void TaskHandler(string name)
        {
            Console.WriteLine($"Task: {name} is executing");
            Thread.Sleep(2000);
            Console.WriteLine($"Task: {name} finished execution");
        }
        static void Main(string[] args)
        {
            Task[] tasks = new Task[]
            {
               new TaskFactory().StartNew(()=>TaskHandler("One")),
               new TaskFactory().StartNew(()=>TaskHandler("Two")),
               new TaskFactory().StartNew(()=>TaskHandler("Three")),
            };
            //Task t = Task.WhenAll(tasks);

            Task calculate = Task.Factory.ContinueWhenAll(tasks,
                (res) => { Console.WriteLine("Ok"); });
            Console.WriteLine("Waiting tasks...");
            //t.Wait();
            calculate.Wait();
            Console.ReadKey();
        }
    }
}
