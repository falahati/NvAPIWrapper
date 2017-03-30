using System.Linq;
using System.Runtime.InteropServices;
using NvAPIWrapper.Native.Attributes;
using NvAPIWrapper.Native.General.Structures;
using NvAPIWrapper.Native.GPU.Structures;
using NvAPIWrapper.Native.Helpers;
using NvAPIWrapper.Native.Interfaces;

namespace NvAPIWrapper.Native.Mosaic.Structures
{
    [StructLayout(LayoutKind.Sequential, Pack = 8)]
    [StructureVersion(1)]
    public struct DisplayTopologyStatus : IInitializable
    {
        public const int MaxDisplays =
            PhysicalGPUHandle.PhysicalGPUs*Constants.Display.AdvancedDisplayHeads;

        internal StructureVersion _Version;
        internal DisplayCapacityProblem _Errors;
        internal DisplayTopologyWarning _Warnings;
        internal uint _DisplayCounts;
        [MarshalAs(UnmanagedType.ByValArray, SizeConst = MaxDisplays)] internal Display[] _Displays;

        public DisplayCapacityProblem Errors => _Errors;
        public DisplayTopologyWarning Warnings => _Warnings;
        public Display[] Displays => _Displays.Take((int) _DisplayCounts).ToArray();

        [StructLayout(LayoutKind.Sequential, Pack = 8)]
        public struct Display
        {
            internal uint _DisplayId;
            internal DisplayCapacityProblem _Errors;
            internal DisplayTopologyWarning _Warnings;
            internal uint _RawReserved;

            public uint DisplayId => _DisplayId;
            public DisplayCapacityProblem Errors => _Errors;
            public DisplayTopologyWarning Warnings => _Warnings;
            public bool SupportsRotation => _RawReserved.GetBit(0);
        }
    }
}