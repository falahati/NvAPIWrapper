using System;

namespace NvAPIWrapper.DRS.SettingValues
{
    public enum VRRMode : UInt32
    {
        Disabled = 0x0,

        FullScreenOnly = 0x1,

        FullScreenAndWindowed = 0x2,

        Default = 0x1
    }
}
