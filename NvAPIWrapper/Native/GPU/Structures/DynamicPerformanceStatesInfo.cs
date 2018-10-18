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
    public struct DynamicPerformanceStatesInfo : IInitializable
    {
        internal const int MaxGpuUtilizations = 8;

        internal StructureVersion _Version;
        internal readonly uint _Flags;

        [MarshalAs(UnmanagedType.ByValArray, SizeConst = MaxGpuUtilizations)]
        internal
            DynamicPerformanceStateUtilizationDomainInfo[] UtilizationDomain;

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
        public Dictionary<UtilizationDomain, DynamicPerformanceStateUtilizationDomainInfo> Domains
        {
            get => UtilizationDomain
                .Select((value, index) => new {index, value})
                .Where(arg => Enum.IsDefined(typeof(UtilizationDomain), arg.index))
                .ToDictionary(arg => (UtilizationDomain) arg.index, arg => arg.value);
        }

        /// Graphic engine (GPU) utilization
        public DynamicPerformanceStateUtilizationDomainInfo GPU
        {
            get => UtilizationDomain[(int) Native.GPU.UtilizationDomain.GPU];
        }

        /// Frame buffer (FB) utilization
        public DynamicPerformanceStateUtilizationDomainInfo FrameBuffer
        {
            get => UtilizationDomain[(int) Native.GPU.UtilizationDomain.FrameBuffer];
        }

        /// Video engine (VID) utilization
        public DynamicPerformanceStateUtilizationDomainInfo VideoEngine
        {
            get => UtilizationDomain[(int) Native.GPU.UtilizationDomain.VideoEngine];
        }

        /// Bus interface (BUS) utilization
        public DynamicPerformanceStateUtilizationDomainInfo BusInterface
        {
            get => UtilizationDomain[(int) Native.GPU.UtilizationDomain.BusInterface];
        }

        /// <inheritdoc />
        public override string ToString()
        {
            return
                $"GPU = {GPU} - FrameBuffer = {FrameBuffer} - VideoEngine = {VideoEngine} - BusInterface = {BusInterface}";
        }
    }
}