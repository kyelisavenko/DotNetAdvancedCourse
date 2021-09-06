using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace UnobservedException
{
    class Program
    {
        static void Main(string[] args)
        {
            TaskScheduler.UnobservedTaskException +=
                TaskScheduler_UnobservedTaskException;
            Task.Factory.StartNew(() =>
            {
                throw new NullReferenceException();
            });
            Thread.Sleep(1000);
            GC.Collect();
            GC.WaitForPendingFinalizers();
            Console.ReadKey();
        }

        private static void TaskScheduler_UnobservedTaskException(object sender, UnobservedTaskExceptionEventArgs e)
        {
            Console.WriteLine(e.Exception.Message);
        }
    }
}
