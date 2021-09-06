using System;
using System.Collections.Specialized;
using System.ComponentModel;
using System.Threading;

namespace EventBaseAsyncPattern
{
    internal delegate void Method1CompletedEventHandler(object sender, Method1CompletedEventArgs args);

    public class Method1CompletedEventArgs : AsyncCompletedEventArgs

    {
        public Method1CompletedEventArgs(Exception ex, bool canceled, object userState) : base(ex, canceled, userState)

        {

        }
    }

    class EAPDemo
    {

        //delegate will execute main worker method asynchronously
        private delegate void WorkerEventHandler(string message, AsyncOperation asyncOp);

        //This delegate raise the event post completing the async operation.
        private readonly SendOrPostCallback _onCompletedDelegate;

        //To allow async method to call multiple time, We need to store tasks in the list
        //so we can send back the proper value back to main thread
        private HybridDictionary tasks = new HybridDictionary();

        //Event will we captured by the main thread.
        public event Method1CompletedEventHandler Method1Completed;

        public EAPDemo()
        {
            _onCompletedDelegate = CompletedDelegateFunc;
        }

        /// <summary>
        /// This function will be called by SendOrPostCallback to raise Method1Completed Event
        /// </summary>
        /// <param name="operationState">Method1CompletedEventArgs object</param>
        private void CompletedDelegateFunc(object operationState)
        {
            Method1CompletedEventArgs e = operationState as Method1CompletedEventArgs;

            Method1Completed?.Invoke(this, e);
        }

        /// <summary>
        /// Synchronous version of the method
        /// </summary>
        /// <param name="message">just simple message to display</param>
        public void Method1(string message)
        {
            for (int i = 0; i < 10; i++)
            {
                Thread.Sleep(2000);
                Console.WriteLine(message + " " + i.ToString());
                //Do some time consuming process
            }
        }

        /// <summary>
        /// Asynchronous version of the method
        /// </summary>
        /// <param name="message">just simple message to display</param>
        /// <param name="userState">Unique value to maintain the task</param>
        public void Method1Async(string message, object userState)
        {
            AsyncOperation asyncOp = AsyncOperationManager.CreateOperation(userState);

            //Multiple threads will access the task dictionary, so it must be locked to serialize access
            lock (tasks.SyncRoot)
            {
                if (tasks.Contains(userState))
                {
                    throw new ArgumentException("User state parameter must be unique", "userState");
                }

                tasks[userState] = asyncOp;
            }

            WorkerEventHandler worker = Method1Worker;

            //Execute process Asynchronously
            worker.BeginInvoke(message, asyncOp, null, null);

        }

        /// <summary>
        /// This method does the actual work
        /// </summary>
        /// <param name="message"></param>
        /// <param name="asyncOp"></param>
        private void Method1Worker(string message, AsyncOperation asyncOp)
        {
            for (int i = 0; i < 10; i++)
            {
                Thread.Sleep(2000);
                Console.WriteLine(message + " " + i);
                //Do some time consuming process
            }
            lock (tasks.SyncRoot)
            {
                tasks.Remove(asyncOp.UserSuppliedState);
            }

            Method1CompletedEventArgs e = new Method1CompletedEventArgs(null, false, asyncOp.UserSuppliedState);

            asyncOp.PostOperationCompleted(_onCompletedDelegate, e);

        }
    }
}

