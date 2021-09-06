using System;
using System.Collections.Generic;
using System.Linq;
using System.Reactive.Linq;
using System.Reactive.Threading.Tasks;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace PlayingWithRx
{
    class Demo3
    {
        private static int LongRunningFunc(string s)
        {
            Thread.Sleep(TimeSpan.FromSeconds(5));
            return s.Length;
        }

        public static void FromAsyncPattern()
        {
            Func<string, int> longRunningFunc = LongRunningFunc;

            Func<string, IObservable<int>> funcHelper =
                Observable.FromAsyncPattern<string, int>(longRunningFunc.BeginInvoke, longRunningFunc.EndInvoke);

            IObservable<int> xs = funcHelper("Hello, String");
            xs.Subscribe(x => Console.WriteLine("Length is " + x));
        }

        public static void TaskBasedAsynchrony()
        {
            Func<string, int> longRunningFunc = LongRunningFunc;

            string s = "Hello, String";

            Task<int> task = longRunningFunc.ToTask(s);
            task.Wait();
            Console.WriteLine("Length is " + task.Result);
        }

        public static void FromTask()
        {
            Func<string, int> longRunningFunc = LongRunningFunc;

            string s = "Hello, String";

            IObservable<int> xs = longRunningFunc.ToTask(s).ToObservable();

            xs.Subscribe(x => Console.WriteLine("Length is " + x),
                () => Console.WriteLine("Task is finished"));
        }
    }

    static class FuncExtensions
    {
        internal static Task<int> ToTask(this Func<string, int> func, string s)
        {
            return Task<int>.Factory.FromAsync(
                func.BeginInvoke, func.EndInvoke, s, null);
        }
    }
}
