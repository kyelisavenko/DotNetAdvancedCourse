using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Net;
using System.Threading;
using System.Threading.Tasks;

namespace AsyncProgramming
{
    public class AwaitSample
    {
        private static readonly List<string> urls = new List<string>{
    "https://www.lipsum.com/", "https://generator.lorem-ipsum.info/", "https://en.wikipedia.org/wiki/Lorem_ipsum"};


        public static void RunSample()
        {
            var sw = Stopwatch.StartNew();
            Console.WriteLine("Начало исполнения. Thread Id: {0}",
                Thread.CurrentThread.ManagedThreadId);

            //var task = GetWebResponseContentLength("http://rsdn.ru");
            var task = GetSummPageSize();
            // ожидаем завершения асинхронной операции
            task.Wait();
            Console.WriteLine("Execution time: {0}", sw.ElapsedMilliseconds);
            Console.WriteLine("ContentLength: {0}, Thread Id: {1}",
                task.Result, Thread.CurrentThread.ManagedThreadId);

            Console.ReadLine();

        }

        static async Task<long> GetWebResponseContentLength(string url)
        {
            var webRequest = WebRequest.Create(url);
            Console.WriteLine("Перед вызовом await-a. Thread Id: {0}",
                Thread.CurrentThread.ManagedThreadId);

            // Начинаем асинхронную операцию
            Task<WebResponse> responseTask = webRequest.GetResponseAsync();

            // Ожидаем получения ответа
            WebResponse webResponse = await responseTask;

            Console.WriteLine("После завершения await-а. Thread Id: {0}",
                Thread.CurrentThread.ManagedThreadId);

            // В этой строке мы уже получили ответ от веб-узла
            // можем обрабатывать результаты. Тип возвращаемого значения
            // должен соответствовать обобщенному параметру класса Task

            return webResponse.ContentLength;
        }
        private static async Task<long> GetSummPageSize()
        {
            var tasks = from url in urls
                        let webRequest = WebRequest.Create(url)
                        select webRequest.GetResponseAsync();
            Console.WriteLine("Before await. Thread Id: {0}", Thread.CurrentThread.ManagedThreadId);
            try
            {
                WebResponse[] data = await Task.WhenAll(tasks);
                // Обработка данных
                Console.WriteLine("After await. Thread Id: {0}", Thread.CurrentThread.ManagedThreadId);
                Console.WriteLine("data typeof: {0}", data.GetType());
                return data.Sum(s => s.ContentLength);
            }
            catch (WebException we)
            {
                //Обработка ошибки получения данных 
                Console.WriteLine(we);
                return 0;
            }

        }
 
    }
}