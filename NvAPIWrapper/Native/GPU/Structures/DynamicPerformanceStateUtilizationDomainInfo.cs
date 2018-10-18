using System.Runtime.InteropServices;
using NvAPIWrapper.Native.Helpers;

namespace NvAPIWrapper.Native.GPU.Structures
{
    /// <summary>
    ///     Holds information about a dynamic performance state utilization domain
    /// </summary>
    [StructLayout(LayoutKind.Sequential)]
    public struct DynamicPerformanceStateUtilizationDomainInfo
    {
        internal readonly uint _IsPresent;
        internal readonly uint _Percentage;

        /// <summary>
        ///     Gets a boolean value that indicates if this utilization domain is present on this GPU.
        /// </summary>
        public bool IsPresent
        {
            get => _IsPresent.GetBit(0);
        }

        /// <summary>
        ///     Gets the percentage of time where the domain is considered busy in the last 1 second interval.
        /// </summary>
        public uint Percentage
        {
            get => _Percentage;
        }

        /// <inheritdoc />
        public override string ToString()
        {
            return IsPresent ? $"{Percentage}%" : "N/A";
        }
    }
}