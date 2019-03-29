using System;

namespace NvAPIWrapper.DRS.SettingValues
{
    public enum SLIPredefinedMode : UInt32
    {
        AutoSelect = 0x0,

        ForceSingle = 0x1,

        ForceAFR = 0x2,

        ForceAFR2 = 0x3,

        ForceSFR = 0x4,

        ForceAFROfSFRFallback3AFR = 0x5,

        Default = 0x0
    }
}
