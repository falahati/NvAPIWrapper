using System.Linq;
using System.Runtime.InteropServices;
using NvAPIWrapper.Native.Attributes;
using NvAPIWrapper.Native.General.Structures;
using NvAPIWrapper.Native.Interfaces;

namespace NvAPIWrapper.Native.GPU.Structures
{
    [StructLayout(LayoutKind.Sequential, Pack = 8)]
    [StructureVersion(1)]
    public struct PrivateFanCoolersInfoV1 : IInitializable
    {
        internal const int MaxNumberOfFanCoolerInfoEntries = 3;

        internal StructureVersion _Version;
        internal readonly uint _FanCoolersInfoCount;

        [MarshalAs(UnmanagedType.ByValArray, SizeConst = MaxNumberOfFanCoolerInfoEntries)]
        internal readonly FanCoolersInfoEntry[] _FanCoolersInfoEntries;

        public FanCoolersInfoEntry[] FanCoolersInfoEntries
        {
            get => _FanCoolersInfoEntries.Take((int) _FanCoolersInfoCount).ToArray();
        }

        [StructLayout(LayoutKind.Sequential, Pack = 8)]
        public struct FanCoolersInfoEntry
        {
            [MarshalAs(UnmanagedType.ByValArray, SizeConst = 131, ArraySubType = UnmanagedType.U4)]
            internal readonly uint[] _Unknown;

            public uint[] RawData
            {
                get => _Unknown;
            }
        }
    }
}