using NvAPIWrapper.Native.Display;

namespace NvAPIWrapper.Native.Interfaces.Display
{
    /// <summary>
    ///     Contains data corresponding to color information
    /// </summary>
    public interface IColorData
    {
        ColorCommand ColorCommand { get; set; }
        ushort Size { get; }
    }
}