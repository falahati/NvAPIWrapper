namespace NvAPIWrapper.Native.Display
{
    public enum Format
    {
        // Driver will choose one as following value.
        Unknown = 0,

        // 8bpp mode
        P8 = 41,

        // 16bpp mode
        R5G6B5 = 23,

        // 32bpp mode
        A8R8G8B8 = 21,

        // 64bpp (floating point)
        A16B16G16R16F = 113
    }
}