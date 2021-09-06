using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EventBaseAsyncPattern
{
    class Program
    {
        static void Main(string[] args)
        {
            var demo = new EAPDemo();
            demo.Method1Async("Processing...", "Operation 1");
            demo.Method1Async("Processing...", "Operation 2");
            demo.Method1Completed += Demo_Method1Completed;
            Console.ReadKey();
        }

        private static void Demo_Method1Completed(object sender, Method1CompletedEventArgs args)
        {
            Console.WriteLine($"Finished execution of operation {args.UserState}");
        }
    }
}
