using System.Runtime.InteropServices;
using NvAPIWrapper.Native.Attributes;
using NvAPIWrapper.Native.General.Structures;
using NvAPIWrapper.Native.Interfaces;

namespace NvAPIWrapper.Native.GPU.Structures
{
    [StructLayout(LayoutKind.Sequential, Pack = 8)]
    [StructureVersion(1)]
    public struct PrivateVFPCurveV1 : IInitializable
    {
        internal const int MaxNumberOfMasks = 4;
        internal const int MaxNumberOfUnknown1 = 12;
        internal const int MaxNumberOfGPUCurveEntries = 80;
        internal const int MaxNumberOfMemoryCurveEntries = 23;
        internal const int MaxNumberOfUnknown2 = 1064;

        internal StructureVersion _Version;

        [MarshalAs(UnmanagedType.ByValArray, SizeConst = MaxNumberOfMasks)]
        internal readonly uint[] _Masks;

        [MarshalAs(UnmanagedType.ByValArray, SizeConst = MaxNumberOfUnknown1)]
        internal readonly uint[] _Unknown1;

        [MarshalAs(UnmanagedType.ByValArray, SizeConst = MaxNumberOfGPUCurveEntries)]
        internal readonly GPUCurveEntry[] _GPUCurveEntries;

        [MarshalAs(UnmanagedType.ByValArray, SizeConst = MaxNumberOfMemoryCurveEntries)]
        internal readonly MemoryCurveEntry[] _MemoryCurveEntries;

        [MarshalAs(UnmanagedType.ByValArray, SizeConst = MaxNumberOfUnknown2)]
        internal readonly uint[] _Unknown2;

        public uint[] Masks
        {
            get => _Masks;
        }

        public GPUCurveEntry[] GPUCurveEntries
        {
            get => _GPUCurveEntries;
        }

        public MemoryCurveEntry[] MemoryCurveEntries
        {
            get => _MemoryCurveEntries;
        }

        [StructLayout(LayoutKind.Sequential, Pack = 8)]
        public struct GPUCurveEntry
        {
            internal uint _Unknown1;
            internal uint _FrequencyInkHz;
            internal uint _Unknown2;
            internal uint _Unknown3;
            internal uint _Unknown4;
            internal uint _Unknown5;
            internal uint _Unknown6;

            public uint FrequencyInkHz
            {
                get => _FrequencyInkHz;
            }
        }

        [StructLayout(LayoutKind.Sequential, Pack = 8)]
        public struct MemoryCurveEntry
        {
            internal uint _Unknown1;
            internal uint _Unknown2;
            internal uint _VoltageInMicroV;
            internal uint _Unknown3;
            internal uint _Unknown4;
            internal uint _Unknown5;
            internal uint _Unknown6;

            public uint VoltageInMicroV
            {
                get => _VoltageInMicroV;
            }
        }
    }
}