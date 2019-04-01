using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using NvAPIWrapper.Native.Attributes;
using NvAPIWrapper.Native.General.Structures;
using NvAPIWrapper.Native.Helpers;
using NvAPIWrapper.Native.Interfaces;

namespace NvAPIWrapper.Native.GPU.Structures
{
    /// <summary>
    ///     Holds information about the dynamic performance states (such as GPU utilization domain)
    /// </summary>
    [StructLayout(LayoutKind.Sequential, Pack = 8)]
    [StructureVersion(1)]
    public struct DynamicPerformanceStatesInfoV1 : IInitializable
    {
        internal const int MaxGpuUtilizations = 8;

        internal StructureVersion _Version;
        internal readonly uint _Flags;

        [MarshalAs(UnmanagedType.ByValArray, SizeConst = MaxGpuUtilizations)]
        internal UtilizationDomainInfo[] _UtilizationDomain;

        /// <summary>
        ///     Gets a boolean value indicating if the dynamic performance state is enabled
        /// </summary>
        public bool IsDynamicPerformanceStateEnabled
        {
            get => _Flags.GetBit(0);
        }

        /// <summary>
        ///     Gets all valid DynamicPerformanceStateUtilizationDomainInfo
        /// </summary>
        public Dictionary<UtilizationDomain, UtilizationDomainInfo> Domains
        {
            get => _UtilizationDomain
                .Select((value, index) => new {index, value})
                .Where(arg => Enum.IsDefined(typeof(UtilizationDomain), arg.index))
                .ToDictionary(arg => (UtilizationDomain) arg.index, arg => arg.value);
        }

        /// <summary>
        ///     Gets the graphic engine (GPU) utilization
        /// </summary>
        public UtilizationDomainInfo GPU
        {
            get => _UtilizationDomain[(int) UtilizationDomain.GPU];
        }

        /// <summary>
        ///     Gets the frame buffer (FB) utilization
        /// </summary>
        public UtilizationDomainInfo FrameBuffer
        {
            get => _UtilizationDomain[(int) UtilizationDomain.FrameBuffer];
        }

        /// <summary>
        ///     Gets the Video engine (VID) utilization
        /// </summary>
        public UtilizationDomainInfo VideoEngine
        {
            get => _UtilizationDomain[(int) UtilizationDomain.VideoEngine];
        }

        /// <summary>
        ///     Gets the Bus interface (BUS) utilization
        /// </summary>
        public UtilizationDomainInfo BusInterface
        {
            get => _UtilizationDomain[(int) UtilizationDomain.BusInterface];
        }

        /// <inheritdoc />
        public override string ToString()
        {
            return $"GPU = {GPU} - " +
                   $"FrameBuffer = {FrameBuffer} - " +
                   $"VideoEngine = {VideoEngine} - " +
                   $"BusInterface = {BusInterface}";
        }

        /// <summary>
        ///     Holds information about a dynamic performance state utilization domain
        /// </summary>
        [StructLayout(LayoutKind.Sequential)]
        public struct UtilizationDomainInfo
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
}