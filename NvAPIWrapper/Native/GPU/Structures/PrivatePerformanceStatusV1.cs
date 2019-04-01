using System.Runtime.InteropServices;
using NvAPIWrapper.Native.Attributes;
using NvAPIWrapper.Native.General.Structures;
using NvAPIWrapper.Native.Interfaces;

namespace NvAPIWrapper.Native.GPU.Structures
{
    [StructLayout(LayoutKind.Sequential, Pack = 8)]
    [StructureVersion(1)]
    public struct PrivatePerformanceStatusV1 : IInitializable
    {
        internal const int MaxNumberOfTimers = 3;
        internal const int MaxNumberOfUnknown5 = 326;

        internal StructureVersion _Version;
        internal uint _Unknown1;
        internal ulong _TimerInNanoSecond;
        internal PerformanceLimit _PerformanceLimit;
        internal uint _Unknown2;
        internal uint _Unknown3;
        internal uint _Unknown4;

        [MarshalAs(UnmanagedType.ByValArray, SizeConst = MaxNumberOfTimers)]
        internal ulong[] _TimersInNanoSecond;

        [MarshalAs(UnmanagedType.ByValArray, SizeConst = MaxNumberOfUnknown5)]
        internal uint[] _Unknown5;

        public ulong TimerInNanoSecond
        {
            get => _TimerInNanoSecond;
        }

        public ulong[] TimersInNanoSecond
        {
            get => _TimersInNanoSecond;
        }

        public PerformanceLimit PerformanceLimit
        {
            get => _PerformanceLimit;
        }
    }
}