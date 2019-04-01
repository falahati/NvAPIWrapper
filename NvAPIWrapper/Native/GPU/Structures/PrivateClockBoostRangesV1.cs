using System.Linq;
using System.Runtime.InteropServices;
using NvAPIWrapper.Native.Attributes;
using NvAPIWrapper.Native.General.Structures;
using NvAPIWrapper.Native.Interfaces;

namespace NvAPIWrapper.Native.GPU.Structures
{
    [StructLayout(LayoutKind.Sequential, Pack = 8)]
    [StructureVersion(1)]
    public struct PrivateClockBoostRangesV1 : IInitializable
    {
        internal const int MaxNumberOfClocksPerGPU = ClockFrequenciesV1.MaxClocksPerGPU;
        internal const int MaxNumberOfUnknown = 8;

        internal StructureVersion _Version;
        internal uint _ClockBoostRangesCount;

        [MarshalAs(UnmanagedType.ByValArray, SizeConst = MaxNumberOfUnknown)]
        internal uint[] _Unknown;

        [MarshalAs(UnmanagedType.ByValArray, SizeConst = MaxNumberOfClocksPerGPU)]
        internal ClockBoostRange[] _ClockBoostRanges;

        public ClockBoostRange[] ClockBoostRanges
        {
            get => _ClockBoostRanges.Take((int) _ClockBoostRangesCount).ToArray();
        }

        [StructLayout(LayoutKind.Sequential, Pack = 8)]
        public struct ClockBoostRange
        {
            internal uint _Unknown1;
            internal ClockType _ClockType;
            internal uint _Unknown2;
            internal uint _Unknown3;
            internal uint _Unknown4;
            internal uint _Unknown5;
            internal uint _Unknown6;
            internal uint _Unknown7;
            internal uint _Unknown8;
            internal uint _Unknown9;
            internal int _RangeMaximum;
            internal int _RangeMinimum;
            internal int _MaximumTemperature;
            internal uint _Unknown10;
            internal uint _Unknown11;
            internal uint _Unknown12;
            internal uint _Unknown13;
            internal uint _Unknown14;

            public ClockType ClockType
            {
                get => _ClockType;
            }

            public int Maximum
            {
                get => _RangeMaximum;
            }

            public int Minimum
            {
                get => _RangeMinimum;
            }

            public int MaximumTemperature
            {
                get => _MaximumTemperature;
            }
        }
    }
}