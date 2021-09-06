using System;
using System.Collections.Generic;
using System.Linq;
using System.Reactive;
using System.Reactive.Linq;
using System.Reactive.Subjects;
using System.Text;
using System.Threading.Tasks;

namespace RoleModel
{
    class Program
    {
        private static async Task StartProducer(ISubject<int> producer)
        {
            Task t1 = Task.Factory.StartNew(() =>
            {  try
                {
                    int val = 0;
                    do
                    {
                        Console.Write(">");
                        val = Int32.Parse(Console.ReadLine());
                        producer.OnNext(val);
                    } while (val != 0);
                    producer.OnCompleted();
                }
                catch (FormatException e)
                {
                    producer.OnError(e);
                }
            });
            await t1;
        }
        private static async Task StartConsumer(IObservable<int> consumer)
        {
            await Task.Factory.StartNew(() =>
            {
                int count = 0;
                consumer.Distinct()
                    .Where(x => x > 0 && x < 10)
                    //.TakeLast(3)
                    .Subscribe(x => { Console.WriteLine(x * 2); count++; },
                    e => { Console.WriteLine("Stop by Error"); },
                    () => { Console.WriteLine("Completed with count = {0}",count); });
            });
        }
        static  IObservable<Timestamped<long>> StartTimer()
        {
            return Observable.Timer(DateTime.Now + TimeSpan.FromSeconds(5), 
                TimeSpan.FromSeconds(5)).Timestamp();
        }
        static void Main(string[] args)
        {
            var  p = new Subject<int>();
            var timer = StartTimer();
            Task[] tasks = new Task[]
            {
                StartProducer(p),
                StartConsumer(p)
            };
            timer.Subscribe(x => Console.WriteLine(x.Timestamp));
            Console.WriteLine("Main started...");
            Task.WaitAll(tasks);
            
        }
    }
}
