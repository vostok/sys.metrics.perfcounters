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
                    InstanceNameProviders.ProcessCategory.ForCurrentProcess());

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

        [Test]
        public void Should_fails_on_invalid_counter_name()
        {
            var pc = new SingleValuePerformanceCounter<double>(new []{new CounterDescription<double>
            {
                Mode = CounterReadMode.FormattedValue,
                Category = "Memory",
                Counter = "bad bad bad",
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
        public void Should_fails_on_invalid_category()
        {
            var pc = new SingleValuePerformanceCounter<double>(new []{new CounterDescription<double>
            {
                Mode = CounterReadMode.FormattedValue,
                Category = "bad bad bad",
                Counter = "bad bad bad",
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

    public class SequenceCounter_Tests
    {
        [Test]
        public void Should_fails_on_invalid_category()
        {
            var pc = new SequenceCounter(new[] {new CounterDescription<None>("Process", "% Processor Time", "*"), });

            using (pc)
            {
                pc.Observe().ToList();
                Thread.Sleep(500);
                Console.WriteLine(string.Join(", ", pc.Observe().Select(x => x.Value)));
                
            }
        }
        
        [Test]
        public void Should_fails_on_invalid_category2()
        {
            var pc1 = new SequenceCounter(new[] {new CounterDescription<None>("Process", "Working Set - Private", "dotnet#")});
            var pc2 = new SequenceCounter(new[] {new CounterDescription<None>("Process", "Working Set - Private", "dotnet*"), });
            var pc3 = new SequenceCounter(new[] {new CounterDescription<None>("Process", "Working Set - Private", "dotnet"), });

            using (pc1)
            {
                Console.WriteLine(pc1.Observe().Count());
            }
            using (pc2)
            {
                Console.WriteLine(pc2.Observe().Count());
            }
            using (pc3)
            {
                Console.WriteLine(pc3.Observe().Count());
            }
        }
    }
}