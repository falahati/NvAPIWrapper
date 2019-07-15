using System.Linq;
using System.Runtime.InteropServices;
using NvAPIWrapper.Native.Attributes;
using NvAPIWrapper.Native.General.Structures;
using NvAPIWrapper.Native.Interfaces;

namespace NvAPIWrapper.Native.GPU.Structures
{
    [StructLayout(LayoutKind.Sequential, Pack = 8)]
    [StructureVersion(1)]
    public struct PrivateFanCoolersControlV1 : IInitializable
    {
        internal const int MaxNumberOfFanCoolerControlEntries = 3;

        internal StructureVersion _Version;
        internal readonly uint _FanCoolersControlCount;

        [MarshalAs(UnmanagedType.ByValArray, SizeConst = MaxNumberOfFanCoolerControlEntries)]
        internal readonly FanCoolersControlEntry[] _FanCoolersControlEntries;

        internal readonly uint _Unknown;

        public FanCoolersControlEntry[] FanCoolersControlEntries
        {
            get => _FanCoolersControlEntries.Take((int) _FanCoolersControlCount).ToArray();
        }

        [StructLayout(LayoutKind.Sequential, Pack = 8)]
        public struct FanCoolersControlEntry
        {
            [MarshalAs(UnmanagedType.ByValArray, SizeConst = 120, ArraySubType = UnmanagedType.U4)]
            internal readonly uint[] _Unknown;

            public uint[] RawData
            {
                get => _Unknown;
            }
        }
    }
}