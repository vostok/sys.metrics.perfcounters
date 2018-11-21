using System;

namespace Vostok.Sys.Metrics.PerfCounters.Implementations
{
    internal readonly struct CategoryCounter
    {
        public readonly string Category;
        public readonly string Counter;

        public CategoryCounter(string category, string counter)
        {
            Category = category;
            Counter = counter;
        }
    }

    internal readonly struct CategoryCounterInstance
    {
        public readonly string Category;
        public readonly string Counter;
        public readonly string Instance;

        public CategoryCounterInstance(string category, string counter, string instance)
        {
            Instance = instance;
            Category = category;
            Counter = counter;
        }
    }
}