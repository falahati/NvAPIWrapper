using NvAPIWrapper.Native.General;

namespace NvAPIWrapper.Native.Interfaces.General
{
    public interface IChipsetInfo
    {
        int VendorId { get; }
        int DeviceId { get; }
        string VendorName { get; }
        string ChipsetName { get; }
        ChipsetInfoFlag Flags { get; }
    }
}