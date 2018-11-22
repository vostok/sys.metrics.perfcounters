using System;
using System.Diagnostics;
using System.Linq;
using System.Threading;
using NUnit.Framework;
using Vostok.Sys.Metrics.PerfCounters.InstanceNames;

namespace Vostok.Sys.Metrics.PerfCounters.Tests
{
    public class Examples
    {
        [Test, Explicit]
        public void Create_simple_performance_counter()
        {
            Console.WriteLine(Process.GetCurrentProcess().Id);
            var counter = PerformanceCounterFactory
                .Default
                .CreateCounter(
                    "Process",
                    "Working Set - Private",
                    InstanceNameProviders.Process.ForCurrentProcess());

            for (var i = 0; i < 3; ++i)
            {
                Console.WriteLine(counter.Observe());
                Thread.Sleep(500);
            }
        }
    }
}