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

        internal readonly uint _Unknown;

        internal readonly uint _FanCoolersControlCount;

        [MarshalAs(UnmanagedType.ByValArray, SizeConst = MaxNumberOfFanCoolerControlEntries)]
        internal readonly FanCoolersControlEntry[] _FanCoolersControlEntries;


        public FanCoolersControlEntry[] FanCoolersControlEntries
        {
            get => _FanCoolersControlEntries.Take((int) _FanCoolersControlCount).ToArray();
        }

        [StructLayout(LayoutKind.Sequential, Pack = 8)]
        public struct FanCoolersControlEntry
        {
            [MarshalAs(UnmanagedType.ByValArray, SizeConst = 8, ArraySubType = UnmanagedType.U4)]
            internal readonly uint[] _UnknownBinary1;

            internal readonly uint _UnknownUInt1;
            internal uint _Level;
            internal CoolerPolicy _Policy;

            [MarshalAs(UnmanagedType.ByValArray, SizeConst = 109, ArraySubType = UnmanagedType.U4)]
            internal readonly uint[] _UnknownBinary2;

            public uint Level
            {
                get => _Level;
                internal set => _Level = value;
            }

            public CoolerPolicy Policy
            {
                get => _Policy;
                internal set => _Policy = value;
            }
        }
    }
}