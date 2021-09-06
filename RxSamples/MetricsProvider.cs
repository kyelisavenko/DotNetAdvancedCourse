using System;
using System.Reactive.Subjects;
using System.Runtime.CompilerServices;

namespace RxSamples
{
    public class MetricsProvider
    {
        private static readonly Subject<OSMetrics> _metricsSubject = 
            new Subject<OSMetrics>();

        public static void GatherMetrics([CallerMemberName]string source = "Undefined")
        {
            var metrics = OSMetrics.GetCurrentMetrics(source);
            _metricsSubject.OnNext(metrics);
        }

        public static IObservable<OSMetrics> Metrics
        {
            get { return _metricsSubject; }
        }
    }
}