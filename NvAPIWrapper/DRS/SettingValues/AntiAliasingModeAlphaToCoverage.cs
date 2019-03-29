using System;

namespace NvAPIWrapper.DRS.SettingValues
{
    public enum AntiAliasingModeAlphaToCoverage : UInt32
    {
        ModeMask = 0x4,

        ModeOff = 0x0,

        ModeOn = 0x4,

        ModeMaximum = 0x4,

        Default = 0x0
    }
}
