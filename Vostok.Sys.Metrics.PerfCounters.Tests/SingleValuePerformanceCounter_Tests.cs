using System;
using FluentAssertions;
using NUnit.Framework;
using Vostok.Sys.Metrics.PerfCounters.Implementations;

namespace Vostok.Sys.Metrics.PerfCounters.Tests
{
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