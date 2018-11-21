using System;

namespace Vostok.Sys.Metrics.PerfCounters.PDH
{
    internal static class PdhStatusExtensions
    {
        public static bool IsLargerBufferRequired(this PdhStatus status)
            => status == PdhStatus.PDH_MORE_DATA;

        public static void EnsureSuccess(this PdhStatus status, string method, string message = "")
        {
            if (status != 0)
                FailWithError(status, method, message);
        }

        public static void EnsureStatus(this PdhStatus status, PdhStatus successfulStatus, string method, string message = "")
        {
            if (status != successfulStatus)
                FailWithError(status, method, message);
        }

        public static void EnsureStatus(this PdhStatus status, PdhStatus successfulStatus1, PdhStatus successfulStatus2, string method, string message = "")
        {
            if (status != successfulStatus1 && status != successfulStatus2)
                FailWithError(status, method, message);
        }

        private static void FailWithError(PdhStatus error, string function, string message)
            => throw CreateException(error, function, message);

        private static InvalidOperationException CreateException(PdhStatus error, string function, string message)
        {
            var exceptionMessage = $"Pdh function {function} failed with code {(int) error:x8} ({Enum.GetName(typeof(PdhStatus), error)})";
            if (!string.IsNullOrEmpty(message))
                exceptionMessage = message + " " + exceptionMessage;

            return new InvalidOperationException(exceptionMessage);
        }
    }
}