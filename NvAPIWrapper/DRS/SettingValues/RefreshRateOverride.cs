namespace NvAPIWrapper.DRS.SettingValues
{
    public enum RefreshRateOverride : uint
    {
        ApplicationControlled = 0x0,

        HighestAvailable = 0x1,

        LowLatencyRefreshRateMask = 0xFF0,

        Default = 0x0
    }
}