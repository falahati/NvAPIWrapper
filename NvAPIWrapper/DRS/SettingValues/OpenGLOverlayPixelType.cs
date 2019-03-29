using System;

namespace NvAPIWrapper.DRS.SettingValues
{
    public enum OpenGLOverlayPixelType : UInt32
    {
        None = 0x0,

        CI = 0x1,

        RGBA = 0x2,

        CIAndRGBA = 0x3,

        Default = 0x1
    }
}
