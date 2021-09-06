using System;
using System.Collections.Generic;
using System.Linq;
using System.Reactive.Subjects;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace DataConsumerTask
{
    class Program
    {
        public static Subject<int> DataProvider = new Subject<int>();
        static async void DoTask()
        {
            Task t = Task.Factory.StartNew(() =>
            {
                Random rnd = new Random();
                for (int i = 0; i < 1000; i++)
                {
                    DataProvider.OnNext(rnd.Next(200));
                }
                DataProvider.OnCompleted();
            });
            await t;
            Console.WriteLine("Task finished");

        }
        static void Main(string[] args)
        {
            DataProvider.Subscribe(x => Console.WriteLine(x),
                () => Console.WriteLine("Completed"));
            DoTask();
            Thread.Sleep(TimeSpan.FromSeconds(10));
        }
    }
}
