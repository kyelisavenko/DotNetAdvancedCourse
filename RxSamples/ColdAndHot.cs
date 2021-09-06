//-----------------------------------------------------------------------------------------------//
// DESCRIPTION
//
// Author:    Sergey Teplyakov
// Date:      October 24, 2011
//-----------------------------------------------------------------------------------------------//


using System;
using System.Reactive.Concurrency;
using System.Reactive.Linq;
using System.Threading;

namespace PlayingWithRx
{
    /// <summary>
    /// DESCRIPTION
    /// </summary>
    public class ColdAndHot
    {
        private static event Action<int> A = delegate { };



        public static void Play()
        {
            int i = 0;
            Timer t = new Timer(
                o =>
                    {
                        A(i);
                        Interlocked.Increment(ref i);
                    });

            t.Change(TimeSpan.FromMilliseconds(500), TimeSpan.FromMilliseconds(500));

            var rrr = Observable.FromEvent<int>(
                h => A += h, h => A -= h).Publish();
            rrr.Subscribe(x =>
                              {
                                  Console.WriteLine("begin " + x);
                                  Thread.Sleep(100000);
                                  Console.WriteLine("end " + x);
                              });
            //rrr.Connect();
            Console.ReadLine();

            //var r = Observable.Timer(TimeSpan.FromSeconds(1), TimeSpan.FromSeconds(1)).Select(x => (int)x);

            var r = Observable.Interval(TimeSpan.FromSeconds(1)).Select(x => (int) x);

            //IObservable<int> r = Observable.Generate(0,


            //                                         x =>
            //                                             {
            //                                                 if (x == 0)
            //                                                     Console.WriteLine("Starting...");
            //                                                 return x < 10;
            //                                             }
            //                                         ,
            //                                         x => x + 1,
            //                                         x => x,
            //                                         x => TimeSpan.FromSeconds(1),
            //                                         Scheduler.ThreadPool);
            
            var rr = r.Publish();
            var d = rr.Connect();

            Thread.Sleep(1100);
            rr.Subscribe(x => 
                {

                    Console.WriteLine("From the first subscription: " + x);
                    Thread.Sleep(2000);
                    Console.WriteLine("First finished...");
                }

    );
            rr.Subscribe(x => Console.WriteLine("From second subscription: " + x));

            
        }
    }
}