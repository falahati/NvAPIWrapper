using System;

namespace NvAPIWrapper.DRS.SettingValues
{
    public enum VSyncVRRControl : UInt32
    {
        Disable = 0x0,

        Enable = 0x1,

        NotSupported = 0x9F95128E,

        Default = 0x1
    }
}
