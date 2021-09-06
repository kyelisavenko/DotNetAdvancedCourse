using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace SyncContext
{
    class Program
    {
        static void Main(string[] args)
        {
            if (SynchronizationContext.Current == null)
                Console.WriteLine("No context");
            else
                Console.WriteLine("Context exists");
            Form frm = new Form();
            if (SynchronizationContext.Current == null)
                Console.WriteLine("No context");
            else
                Console.WriteLine($"Context exists: {SynchronizationContext.Current}");
        }
    }
}
