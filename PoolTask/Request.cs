using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PoolTask
{
    class Request
    {
        public string Data { get; set; }
        public void TaskHandler()
        {
            for (int i = 0; i < 1000; i++)
                Console.Write(Data);
        }
    }
}
