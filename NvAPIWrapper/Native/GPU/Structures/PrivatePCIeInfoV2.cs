using System.Runtime.InteropServices;
using NvAPIWrapper.Native.Attributes;
using NvAPIWrapper.Native.General.Structures;
using NvAPIWrapper.Native.Interfaces;

namespace NvAPIWrapper.Native.GPU.Structures
{
    [StructLayout(LayoutKind.Sequential, Pack = 8)]
    [StructureVersion(2)]
    public struct PrivatePCIeInfoV2 : IInitializable
    {
        internal StructureVersion _Version;

        [MarshalAs(UnmanagedType.ByValArray, SizeConst = 10)]
        internal readonly PCIePerformanceStateInfo[] _PCIePerformanceStateInfos;

        public PCIePerformanceStateInfo[] PCIePerformanceStateInfos
        {
            get => _PCIePerformanceStateInfos;
        }

        [StructLayout(LayoutKind.Sequential, Pack = 8)]
        public struct PCIePerformanceStateInfo
        {
            internal readonly uint _TransferRate;
            internal readonly PCIeGeneration _Version;
            internal readonly uint _LanesNumber;
            internal readonly PCIeGeneration _Generation;

            /// <summary>
            ///     Gets the PCIe transfer rate in Mega Transfers per Second
            /// </summary>
            public uint TransferRateInMTps
            {
                get => _TransferRate;
            }

            public PCIeGeneration Generation
            {
                get => _Generation;
            }

            public uint Lanes
            {
                get => _LanesNumber;
            }

            public PCIeGeneration Version
            {
                get => _Version;
            }
        }
    }
}