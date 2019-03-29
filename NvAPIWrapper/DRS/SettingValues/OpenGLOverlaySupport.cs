using System;

namespace NvAPIWrapper.DRS.SettingValues
{
    public enum OpenGLOverlaySupport : UInt32
    {
        Off = 0x0,

        On = 0x1,

        ForceSoftware = 0x2,

        Default = 0x0
    }
}
