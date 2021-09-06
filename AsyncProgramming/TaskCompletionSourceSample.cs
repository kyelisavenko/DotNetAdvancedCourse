using System;
using System.ComponentModel;
using System.Threading;
using System.Threading.Tasks;

namespace AsyncProgramming
{
    class GetStringLengthEventArgs : AsyncCompletedEventArgs
    {
        public int Result { get; private set; }

        private GetStringLengthEventArgs(Exception error, bool cancelled)
            : base(error, cancelled, null)
        { }

        public static GetStringLengthEventArgs CreateResult(int result)
        {
            return new GetStringLengthEventArgs(null, false)
            {
                Result = result
            };
        }

        public static GetStringLengthEventArgs CreateFailure(Exception e)
        {
            return new GetStringLengthEventArgs(e, false);
        }
    }

    class StringConverter
    {
        public int GetStringLength(string str)
        {
            return str.Length;
        }

        public event EventHandler<GetStringLengthEventArgs> GetStringLengthCompleted;

        public void GetStringLengthAsync(string str)
        {
            var currentSyncContext = SynchronizationContext.Current;

            ThreadPool.QueueUserWorkItem(
                o =>
                {
                    Thread.Sleep(TimeSpan.FromSeconds(2));
                    try
                    {
                        if (str.Length == 10)
                            throw new InvalidOperationException("We're not support strings with length 10!");

                        OnGetStringLengthCompleted(currentSyncContext,
                            GetStringLengthEventArgs.CreateResult(str.Length));
                    }
                    catch (Exception e)
                    {
                        OnGetStringLengthCompleted(currentSyncContext,
                            GetStringLengthEventArgs.CreateFailure(e));
                    }

                });
        }

        private void OnGetStringLengthCompleted(
            SynchronizationContext synchronizationContext,
            GetStringLengthEventArgs e)
        {
            var handler = GetStringLengthCompleted;
            if (handler != null)
            {
                if (synchronizationContext == null)
                    handler(this, e);
                else
                    synchronizationContext.Post(o => handler(this, e), null);
            }
        }
    }

    public class TaskCompletionSourceSamples
    {
        public static void Run()
        {
            GetStringLength("123456789");
            GetStringLength("1234567890");
        }

        public static void GetStringSync(string str)
        {
            try
            {
                var stringConverter = new StringConverter();
                stringConverter.GetStringLength(str);
            }
            catch (Exception e)
            {
                Console.WriteLine("Error: {0}", e);
            }
        }

        public static void GetStringLength(string str)
        {
            var stringConverter = new StringConverter();
            stringConverter.GetStringLengthCompleted += stringConverter_GetStringLengthCompleted;
            stringConverter.GetStringLengthAsync(str);
        }

        private static void stringConverter_GetStringLengthCompleted(object sender,
            GetStringLengthEventArgs e)
        {
            if (e.Error != null)
            {
                Console.WriteLine("String converter failed! Error = {0}", e.Error);
            }
            else
            {
                Console.WriteLine("String converted. Result = {0}", e.Result);
            }

        }

        public static void GetStringLengthWithTask(string str)
        {
            var task = GetStringLengthTaskAsync(str);
            task.ContinueWith(t =>
            {
                if (t.IsFaulted)
                {
                    Console.WriteLine("Failed! {0}", t.Exception);
                }
                else
                {
                    Console.WriteLine("Result = {0}", t.Result);
                }
            });
        }

        public static async void GetStringLengthAsync(string str)
        {
            try
            {
                int result = await GetStringLengthTaskAsync(str);
                Console.WriteLine("Result = {0}", result);
            }
            catch (Exception e)
            {
                Console.WriteLine("Failed! {0}", e);
            }
        }

        public static Task<int> GetStringLengthTaskAsync(string str)
        {
            var tcs = new TaskCompletionSource<int>();

            var stringConverter = new StringConverter();
            stringConverter.GetStringLengthCompleted +=
                (s, e) =>
                {
                    if (e.Cancelled)
                    {
                        tcs.SetCanceled();
                    }
                    else if (e.Error != null)
                    {
                        tcs.SetException(e.Error);
                    }
                    else
                    {
                        tcs.SetResult(e.Result);
                    }
                };
            stringConverter.GetStringLengthAsync(str);

            return tcs.Task;
        }
    }
}