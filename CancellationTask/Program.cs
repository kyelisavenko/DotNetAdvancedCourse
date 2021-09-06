using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace CancellationTask
{
    class Program
    {
        static void Main(string[] args)
        {
            CancellationTokenSource cs = new CancellationTokenSource();
            CancellationToken token = cs.Token;
            Task t = Task.Factory.StartNew(() =>
           {
               try
               {
                   
                   token.ThrowIfCancellationRequested();
                   Console.WriteLine("Start long operation");
                    while(true)
                   {
                    Console.Write(".");
                    Thread.Sleep(100);
                    if(token.IsCancellationRequested)
                       {
                           Console.WriteLine("Request task cancellation");
                           token.ThrowIfCancellationRequested();
                       }
                       
                   }
               }
               catch(OperationCanceledException e)
               {
                   Console.WriteLine("Task is cancelled");
               }
           }, token);
            Thread.Sleep(TimeSpan.FromSeconds(10));
            cs.Cancel();
            t.Wait();
        }
    }
}
