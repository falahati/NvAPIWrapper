using System;

namespace NvAPIWrapper.DRS.SettingValues
{
    public enum OpenGLTMONLevel : UInt32
    {
        Disable = 0x0,

        Critical = 0x1,

        Warning = 0x2,

        Information = 0x3,

        Most = 0x4,

        Verbose = 0x5,

        Default = 0x4
    }
}
