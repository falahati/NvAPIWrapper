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
        public const int MaxGpuUtilizations = 8;

        internal StructureVersion _Version;
        internal readonly uint _Flags;

        [MarshalAs(UnmanagedType.ByValArray, SizeConst = MaxGpuUtilizations)] internal
            DynamicPerformanceStateUtilizationDomainInfo[] UtilizationDomain;

        public bool IsDynamicPerformanceStateEnabled => _Flags.GetBit(0);

        /// Graphic engine (GPU) utilization
        public DynamicPerformanceStateUtilizationDomainInfo GPUDomain => UtilizationDomain[0];

        /// Frame buffer (FB) utilization
        public DynamicPerformanceStateUtilizationDomainInfo FrameBufferDomain => UtilizationDomain[1];

        /// Video engine (VID) utilization
        public DynamicPerformanceStateUtilizationDomainInfo VideoEngineDomain => UtilizationDomain[2];

        /// Bus interface (BUS) utilization
        public DynamicPerformanceStateUtilizationDomainInfo BusDomain => UtilizationDomain[3];

        /// <inheritdoc />
        public override string ToString()
        {
            return
                $"GPU = {GPUDomain} - FrameBuffer = {FrameBufferDomain} - VideoEngine = {VideoEngineDomain} - BusInterface = {BusDomain}";
        }
    }
}