using System;
using System.Collections.Generic;
using System.Linq;
using System.Reactive.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Generator
{
    class Program
    {
        static void Main(string[] args)
        {
            IObservable<int> xs = Observable.Generate(0,
                                    x => x < 10,
                                    x => x + 1,
                                    x => x);
            Task<int> t = Task.Factory.StartNew(() =>
            {
                int result = 0 ;
                xs.Subscribe(x => 
                    { result += x * 2;
                      Thread.Sleep(TimeSpan.FromSeconds(2));
                    },
                    () => { result *= 3; });
                Console.WriteLine("Task completed");
                return result;
            });
            Console.WriteLine("Result = {0}", t.Result);
        }
    }
}
