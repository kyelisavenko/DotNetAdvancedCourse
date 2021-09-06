using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace TaskException
{
    class Program
    {
        static void  TaskHandler()
        {
            Console.WriteLine("Start second task...");
            //делаем что-то доброе и светлое и падаем
            throw new NullReferenceException();
        }
        static void Main(string[] args)
        {
            try
            {
                //Task t = new TaskFactory().StartNew(TaskHandler);
                //t.Wait();
                //Console.WriteLine("Complete");
            }
            catch(AggregateException e)
            {
                Console.WriteLine("Ooops!");
                Console.WriteLine("Inner exception= {0}",
                    e.InnerException.Message);
            }
            //исключение в ContinueWith
            try
            {
                Task t = new TaskFactory().StartNew(() =>
                {
                    //Thread.Sleep(1000);
                    throw new NullReferenceException();
                });
                t.ContinueWith((prev) => 
                 {
                     if (prev.Status == TaskStatus.Faulted)
                     {
                         Console.WriteLine("prev task faulted");
                         Console.WriteLine(prev.Exception.GetType().Name);
                         Console.WriteLine(prev.Exception.InnerException.Message);
                     }
                     Console.WriteLine("continue");
                    // throw new NullReferenceException();
                 }).Wait();
            }
            catch(AggregateException )
            {
                Console.WriteLine(":-(");
            }

        }
    }
}
