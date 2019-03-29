using System;

namespace NvAPIWrapper.DRS.SettingValues
{
    public enum OpenGLAntiAliasingLineGamma : UInt32
    {
        Disabled = 0x10,

        Enabled = 0x23,

        Minimum = 0x1,

        Maximum = 0x64,

        Default = 0x10
    }
}
