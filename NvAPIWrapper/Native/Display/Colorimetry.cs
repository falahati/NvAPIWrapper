namespace NvAPIWrapper.Native.Display
{
    public enum Colorimetry : byte
    {
        RGB = 0,
        YCC601,
        YCC709,
        XVYCC601,
        XVYCC709,
        SYCC601,
        ADOBEYCC601,
        ADOBERGB,
        BT2020RGB,
        BT2020YCC,
        BT2020cYCC,
        Default = 0xFE,
        Auto = 0xFF
    }
}
