using System;

namespace NvAPIWrapper.DRS.SettingValues
{
    public enum WKSMemoryAllocationPolicy : UInt32
    {
        AsNeeded = 0x0,

        ModeratePreAllocation = 0x1,

        AggressivePreAllocation = 0x2,

        Default = 0x0
    }
}
