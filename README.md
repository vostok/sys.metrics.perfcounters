# Vostok.Sys.Metrics.PerfCounters

[![Build status](https://ci.appveyor.com/api/projects/status/github/vostok/sys.metrics.perfcounters?svg=true&branch=master)](https://ci.appveyor.com/project/vostok/sys.metrics.perfcounters/branch/master)
[![NuGet](https://img.shields.io/nuget/v/Vostok.Sys.Metrics.PerfCounters.svg)](https://www.nuget.org/packages/Vostok.Sys.Metrics.PerfCounters)

The Vostok.Sys.Metrics.PerfCounters library provides interface for efficient consuming Windows Performace Counters data. It uses Performance Data Helpers (PDH) API under the hood.

## Examples
##### Performance Counter without instance
```cs
var counter = PerformanceCounterFactory
    .Default
    .CreateCounter("Memory", "Committed Bytes");
```
##### Performance Counter with instance
```cs
var counter = PerformanceCounterFactory
    .Default
    .CreateCounter("LogicalDisk", "% Idle Time", "C:");
```
##### Performance Counter for process
```cs
var counter = PerformanceCounterFactory
    .Default
    .CreateCounter("LogicalDisk", "% Idle Time", InstanceNameProviders.DotNet.ForPid(1234));
```