using NvAPIWrapper.Native.Display;

namespace NvAPIWrapper.Native.Interfaces.Mosaic
{
    public interface IGridTopologyDisplay
    {
        uint DisplayId { get; }
        int OverlapX { get; }
        int OverlapY { get; }
        Rotate Rotation { get; }
        uint CloneGroup { get; }
    }
}