using System.Runtime.InteropServices;
using NvAPIWrapper.Native.Attributes;
using NvAPIWrapper.Native.Interfaces;
using NvAPIWrapper.Native.Interfaces.General;

namespace NvAPIWrapper.Native.General.Structures
{
    [StructLayout(LayoutKind.Sequential, Pack = 8)]
    [StructureVersion(1)]
    public struct ChipsetInfoV1 : IInitializable, IChipsetInfo
    {
        internal StructureVersion _Version;
        internal uint _VendorId;
        internal uint _DeviceId;
        internal ShortString _VendorName;
        internal ShortString _ChipsetName;

        public int VendorId => (int) _VendorId;
        public int DeviceId => (int) _DeviceId;
        public string VendorName => _VendorName.Value;
        public string ChipsetName => _ChipsetName.Value;
        public ChipsetInfoFlag Flags => ChipsetInfoFlag.None;
    }
}