using System.Linq;
using System.Runtime.InteropServices;
using NvAPIWrapper.Native.Attributes;
using NvAPIWrapper.Native.General.Structures;
using NvAPIWrapper.Native.Interfaces;

namespace NvAPIWrapper.Native.GPU.Structures
{
    [StructLayout(LayoutKind.Sequential, Pack = 8)]
    [StructureVersion(1)]
    public struct PrivateFanCoolersStatusV1 : IInitializable
    {
        internal const int MaxNumberOfFanCoolerStatusEntries = 3;

        internal StructureVersion _Version;
        internal readonly uint _FanCoolersStatusCount;

        [MarshalAs(UnmanagedType.ByValArray, SizeConst = MaxNumberOfFanCoolerStatusEntries)]
        internal readonly FanCoolersStatusEntry[] _FanCoolersStatusEntries;

        internal readonly uint _Unknown;

        public FanCoolersStatusEntry[] FanCoolersStatusEntries
        {
            get => _FanCoolersStatusEntries.Take((int) _FanCoolersStatusCount).ToArray();
        }

        [StructLayout(LayoutKind.Sequential, Pack = 8)]
        public struct FanCoolersStatusEntry
        {
            [MarshalAs(UnmanagedType.ByValArray, SizeConst = 141, ArraySubType = UnmanagedType.U4)]
            internal readonly uint[] _Unknown;

            public uint[] RawData
            {
                get => _Unknown;
            }
        }
    }
}