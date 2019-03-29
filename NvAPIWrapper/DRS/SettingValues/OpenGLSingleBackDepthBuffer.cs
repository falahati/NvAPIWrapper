using System;

namespace NvAPIWrapper.DRS.SettingValues
{
    public enum OpenGLSingleBackDepthBuffer : UInt32
    {
        Disable = 0x0,

        Enable = 0x1,

        UseHardwareDefault = 0xFFFFFFFF,

        Default = 0x0
    }
}
