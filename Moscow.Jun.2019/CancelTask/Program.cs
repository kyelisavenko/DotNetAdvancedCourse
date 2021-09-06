using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace CancelTask
{
    class Program
    {
        static void Main(string[] args)
        {
            CancellationTokenSource cs = new CancellationTokenSource();
            CancellationToken ct = cs.Token;
            Task t = Task.Factory.StartNew((c) => {
                CancellationToken cx = (CancellationToken)c;
                try
                {

                    cx.ThrowIfCancellationRequested();
                    for (; ; )
                    {
                        if (cx.IsCancellationRequested)
                        {
                            Console.WriteLine("Rollback");
                            cx.ThrowIfCancellationRequested();
                        }
                        Console.Write("*");
                    }
                }
                catch(OperationCanceledException)
                {
                    Console.WriteLine("task cancelled");
                }                
                    },ct);
            Thread.Sleep(TimeSpan.FromSeconds(5));
            cs.Cancel();
            t.Wait();
        }
    }
}
