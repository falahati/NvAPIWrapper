using System;

namespace NvAPIWrapper.DRS.SettingValues
{
    public enum ApplicationProfileNotificationTimeOut : UInt32
    {
        Disabled = 0x0,

        NineSeconds = 0x9,

        FifteenSeconds = 0xF,

        ThirtySeconds = 0x1E,

        OneMinute = 0x3C,

        TwoMinutes = 0x78,

        Default = 0x0
    }
}
