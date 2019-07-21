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
        internal const int MaxNumberOfFanCoolerControlEntries = 32;
        internal StructureVersion _Version;
        internal readonly uint _UnknownUInt;
        internal readonly uint _FanCoolersControlCount;

        [MarshalAs(UnmanagedType.ByValArray, SizeConst = 8, ArraySubType = UnmanagedType.U4)]
        internal readonly uint[] _Reserved;

        [MarshalAs(UnmanagedType.ByValArray, SizeConst = MaxNumberOfFanCoolerControlEntries)]
        internal readonly FanCoolersControlEntry[] _FanCoolersControlEntries;

        public FanCoolersControlEntry[] FanCoolersControlEntries
        {
            get => _FanCoolersControlEntries.Take((int) _FanCoolersControlCount).ToArray();
        }

        [StructLayout(LayoutKind.Sequential, Pack = 8)]
        public struct FanCoolersControlEntry
        {
            internal uint _CoolerId;
            internal uint _Level;
            internal FanCoolersControlMode _ControlMode;

            [MarshalAs(UnmanagedType.ByValArray, SizeConst = 8, ArraySubType = UnmanagedType.U4)]
            internal readonly uint[] _Reserved;

            public uint CoolerId
            {
                get => _CoolerId;
                internal set => _CoolerId = value;
            }

            public uint Level
            {
                get => _Level;
                internal set => _Level = value;
            }

            public FanCoolersControlMode ControlMode
            {
                get => _ControlMode;
                internal set => _ControlMode = value;
            }
        }
    }
}