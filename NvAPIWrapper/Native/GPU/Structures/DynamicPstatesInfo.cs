using System.Runtime.InteropServices;
using NvAPIWrapper.Native.Attributes;
using NvAPIWrapper.Native.General.Structures;
using NvAPIWrapper.Native.Interfaces;
using NvAPIWrapper.Native.Interfaces.GPU;

namespace NvAPIWrapper.Native.GPU.Structures
{
    /// <summary>
    ///     Holds information about the dynamic states (such as gpu utilization)
    /// </summary>
    [StructLayout(LayoutKind.Sequential, Pack = 8)]
    [StructureVersion(1)]
    public struct DynamicPStatesInfo : IInitializable
    {
        public const int MaxGpuUtilizations = 8;

        internal StructureVersion _Version;
        internal readonly uint _Flags;
        [MarshalAs(UnmanagedType.ByValArray, SizeConst = MaxGpuUtilizations)] internal DynamicPStatesUtilizationInfo[] _Utilization;

        public bool IsDynamicPStatesEnabled => _Flags == 0;

        /// Graphic engine (GPU) utilization
        public DynamicPStatesUtilizationInfo GpuUtilization => _Utilization[0];

        /// Frame buffer (FB) utilization
        public DynamicPStatesUtilizationInfo FbUtilization => _Utilization[1];

        /// Video engine (VID) utilization
        public DynamicPStatesUtilizationInfo VidUtilization => _Utilization[2];

        /// Bus interface (BUS) utilization
        public DynamicPStatesUtilizationInfo BusUtilization => _Utilization[3];
    }
}
