using System;

namespace NvAPIWrapper.DRS.SettingValues
{
    public enum BatteryBoost : UInt32
    {
        Minimum = 0x1,

        Maximum = 0xFF,

        Enabled = 0x10000000,

        Disabled = 0x0,

        Default = 0x0
    }
}
