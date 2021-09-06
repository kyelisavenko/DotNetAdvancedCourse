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
        //https://docs.microsoft.com/en-us/dotnet/api/system.threading.tasks.taskscheduler.unobservedtaskexception?view=net-5.0
        static void Main(string[] args)
        {
            TaskScheduler.UnobservedTaskException += TaskScheduler_UnobservedTaskException;
            Task.Factory.StartNew(() => throw new NotImplementedException());
            Thread.Sleep(1000);
            GC.Collect();
            GC.WaitForPendingFinalizers();
            Console.ReadKey();
        }

        private static void TaskScheduler_UnobservedTaskException(object sender,
            UnobservedTaskExceptionEventArgs e)
        {
            Console.WriteLine("Catch Exception {0}",
                e.Exception.Message);
        }

    }
}
