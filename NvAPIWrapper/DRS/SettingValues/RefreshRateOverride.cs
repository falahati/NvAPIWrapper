using System;

namespace NvAPIWrapper.DRS.SettingValues
{
    public enum RefreshRateOverride : UInt32
    {
        ApplicationControlled = 0x0,

        HighestAvailable = 0x1,

        LowLatencyRefreshRateMask = 0xFF0,

        Default = 0x0
    }
}
