using System;
using System.Collections.Generic;
using System.Net;

namespace AsyncProgramming
{
    /// <summary>
    /// Classic assync programming (a.k.a. APM) samples
    /// </summary>
    public class APMSample
    {
        private static readonly List<string> Urls = new List<string>{
            "https://www.lipsum.com/", "https://www.lipsum.com/", "https://en.wikipedia.org/wiki/Lorem_ipsum"};

        public static void RunSample()
        {
            foreach (var url in Urls)
            {
                var webRequest = WebRequest.Create(url);
                var iac = webRequest.BeginGetResponse(EndGetResponse, webRequest);
            }

            Console.ReadKey();
        }

        private static void EndGetResponse(IAsyncResult ar)
        {
            var webRequest = (WebRequest)ar.AsyncState;

            try
            {
                var result = webRequest.EndGetResponse(ar);
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
            }
        }

    }
}