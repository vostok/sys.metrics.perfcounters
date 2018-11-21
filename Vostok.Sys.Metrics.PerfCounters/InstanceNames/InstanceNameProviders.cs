namespace Vostok.Sys.Metrics.PerfCounters.InstanceNames
{
    public static class InstanceNameProviders
    {
        public static readonly ICategoryProcessInstanceNameProviders ProcessCategory = new CategoryProcessInstanceNameProviders(false);
        public static readonly ICategoryProcessInstanceNameProviders DotNetCategories = new CategoryProcessInstanceNameProviders(true);
    }
}