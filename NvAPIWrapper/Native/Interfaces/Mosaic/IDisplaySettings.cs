namespace NvAPIWrapper.Native.Interfaces.Mosaic
{
    public interface IDisplaySettings
    {
        int Width { get; }
        int Height { get; }
        int BitsPerPixel { get; }
        int Frequency { get; }
        uint FrequencyInMillihertz { get; }
    }
}