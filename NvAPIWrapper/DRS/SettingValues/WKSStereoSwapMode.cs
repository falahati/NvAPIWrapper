using System;

namespace NvAPIWrapper.DRS.SettingValues
{
    public enum WKSStereoSwapMode : UInt32
    {
        ApplicationControl = 0x0,

        PerEye = 0x1,

        PerEyePair = 0x2,

        LegacyBehavior = 0x3,

        Default = 0x0
    }
}
