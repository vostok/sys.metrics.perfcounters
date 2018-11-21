using System;
using System.Diagnostics;
using System.Linq;
using System.Threading;
using FluentAssertions;
using NUnit.Framework;
using Vostok.Sys.Metrics.PerfCounters.Implementations;
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

    public class SingleValuePerformanceCounter_Tests
    {
        private IPerformanceCounterFactory factory = PerformanceCounterFactory.Default;
        
        [Test]
        public void Should_return_value_for_counter_without_instance()
        {
            var pc = new SingleValuePerformanceCounter<double>(new []{new CounterDescription<double>
            {
                Mode = CounterReadMode.FormattedValue,
                Category = "Memory",
                Counter = "% Committed Bytes In Use",
                SetValue = (context, value) => context.Result = value
            }});
            
            using (pc)
            {
                var value = pc.Observe();
                Console.WriteLine(value);
                value.Should().BeGreaterThan(0);
            }
        }
        
        [Test]
        public void Should_return_value_for_raw_counter_without_instance()
        {
            var pc = new SingleValuePerformanceCounter<double>(new []{new CounterDescription<double>
            {
                Mode = CounterReadMode.RawValue,
                Category = "Memory",
                Counter = "Committed Bytes",
                SetValue = (context, value) => context.Result = value
            }});
            
            using (pc)
            {
                var value = pc.Observe();
                Console.WriteLine(value);
                value.Should().BeGreaterThan(0);
            }
        }
    }
}