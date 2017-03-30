namespace NvAPIWrapper.Native.Display
{
    public enum TimingScanMode : ushort
    {
        Progressive = 0,
        Interlaced = 1,
        InterlacedWithExtraVerticalBlank = 1,
        InterlacedWithNoExtraVerticalBlank = 2
    }
}