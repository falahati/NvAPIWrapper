using System;

namespace NvAPIWrapper.DRS.SettingValues
{
    public enum OpenGLEventLogSeverityThreshold : UInt32
    {
        Disable = 0x0,

        Critical = 0x1,

        Warning = 0x2,

        Information = 0x3,

        All = 0x4,

        Default = 0x4
    }
}
