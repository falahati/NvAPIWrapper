using System.Linq;
using System.Runtime.InteropServices;
using NvAPIWrapper.Native.Attributes;
using NvAPIWrapper.Native.General.Structures;
using NvAPIWrapper.Native.Interfaces;

namespace NvAPIWrapper.Native.GPU.Structures
{
    [StructLayout(LayoutKind.Sequential, Pack = 8)]
    [StructureVersion(1)]
    public struct PrivatePowerTopologiesStatusV1 : IInitializable
    {
        internal const int MaxNumberOfPowerTopologiesStatusEntries = 4;

        internal StructureVersion _Version;
        internal readonly uint _PowerTopologiesStatusEntriesCount;

        [MarshalAs(UnmanagedType.ByValArray, SizeConst = MaxNumberOfPowerTopologiesStatusEntries,
            ArraySubType = UnmanagedType.Struct)]
        internal readonly PowerTopologiesStatusEntry[] _PowerTopologiesStatusEntries;

        public PowerTopologiesStatusEntry[] PowerPolicyStatusEntries
        {
            get => _PowerTopologiesStatusEntries.Take((int)_PowerTopologiesStatusEntriesCount).ToArray();
        }
        
        [StructLayout(LayoutKind.Sequential, Pack = 8)]
        public struct PowerTopologiesStatusEntry
        {
            internal uint _Unknown;
            internal uint _Unknown2;
            internal uint _Power;
            internal uint _Unknown3;
            
            public uint Power
            {
                get => _Power;
            }
        }
    }
}