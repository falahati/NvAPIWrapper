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

        public bool IsDynamicPstateEnabled => _Flags == 1;

        public DynamicPStatesUtilizationInfo[] Utilization => _Utilization;
    }
}
