using System;

namespace NvAPIWrapper.DRS.SettingValues
{
    public enum VSyncMode : UInt32
    {
        Passive = 0x60925292,

        ForceOff = 0x8416747,

        ForceOn = 0x47814940,

        FlipInterval2 = 0x32610244,

        FlipInterval3 = 0x71271021,

        FlipInterval4 = 0x13245256,

        Virtual = 0x18888888,

        Default = 0x60925292
    }
}
