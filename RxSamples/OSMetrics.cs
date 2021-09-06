using System;
using System.Diagnostics;
using System.Text;

namespace RxSamples
{
    public class OSMetrics
    {
        public string Source { get; set; }
        public long TotalMemory { get; set; }
        public long WorkingSet { get; set; }
        public long LoadedAssemblies { get; set; }

        public override string ToString()
        {
            var sb = new StringBuilder();
            sb.AppendFormat("Source: {0}", Source).AppendLine()
                .AppendFormat("TotalMemory: {0}K", TotalMemory / 1024)
                .AppendLine()
                .AppendFormat("WorkingSet: {0}K", WorkingSet / 1024)
                .AppendLine()
                .AppendFormat("LoadedAssemblies: {0}", LoadedAssemblies)
                .AppendLine();
            return sb.ToString();
        }

        public static OSMetrics GetCurrentMetrics(string source)
        {
            return new OSMetrics()
            {
                LoadedAssemblies = AppDomain.CurrentDomain.GetAssemblies().Length,
                TotalMemory = GC.GetTotalMemory(false),
                WorkingSet = Process.GetCurrentProcess().WorkingSet64,
                Source = source
            };
            
        }

        public static OSMetrics Difference(OSMetrics lhs, OSMetrics rhs)
        {
            return new OSMetrics()
                       {
                           TotalMemory = lhs.TotalMemory - rhs.TotalMemory,
                           LoadedAssemblies = lhs.LoadedAssemblies - rhs.LoadedAssemblies,
                           WorkingSet = lhs.WorkingSet - rhs.WorkingSet,
                       };
        }
    }
}