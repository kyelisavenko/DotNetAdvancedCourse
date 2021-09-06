using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace CalculateTasks
{
    
    class Program
    {
        static public Random rnd = new Random(100);
        static void Main(string[] args)
        {
            Task<int>[] tasks = new Task<int>[5];
            for (int i = 0; i < tasks.Length; i++)
            {
                tasks[i] = new Task<int>(() =>
                {
                  Thread.Sleep(1000);
                  return  rnd.Next(100);
                });
            }
            for (int i = 0; i < tasks.Length; i++)
            {
                tasks[i].Start();
            }
            Task<int> r = Task<int>.Factory.ContinueWhenAll(tasks, (res) =>
             {
                 int Total = 0;
                 foreach (var current in res)
                 {
                     Total += current.Result;
                 }
                 return Total;
             });
            Console.WriteLine("Total sum: {0}", r.Result);
        }

    }
}
