using System.Runtime.InteropServices;
using NvAPIWrapper.Native.Attributes;
using NvAPIWrapper.Native.Interfaces;
using NvAPIWrapper.Native.Interfaces.General;

namespace NvAPIWrapper.Native.General.Structures
{
    [StructLayout(LayoutKind.Sequential, Pack = 8)]
    [StructureVersion(3)]
    public struct ChipsetInfoV3 : IInitializable, IChipsetInfo
    {
        internal StructureVersion _Version;
        internal uint _VendorId;
        internal uint _DeviceId;
        internal ShortString _VendorName;
        internal ShortString _ChipsetName;
        internal ChipsetInfoFlag _Flags;
        internal uint _SubSystemVendorId;
        internal uint _SubSystemDeviceId;
        internal ShortString _SubSystemVendorName;

        public int VendorId => (int) _VendorId;
        public int DeviceId => (int) _DeviceId;
        public string VendorName => _VendorName.Value;
        public string ChipsetName => _ChipsetName.Value;
        public ChipsetInfoFlag Flags => _Flags;
        public int SubSystemVendorId => (int) _SubSystemVendorId;
        public int SubSystemDeviceId => (int) _SubSystemDeviceId;
        public string SubSystemVendorName => _SubSystemVendorName.Value;
    }
}