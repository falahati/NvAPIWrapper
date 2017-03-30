using NvAPIWrapper.Native.Display.Structures;

namespace NvAPIWrapper.Native.Interfaces.Display
{
    public interface IPathTargetInfo
    {
        uint DisplayId { get; }
        PathAdvancedTargetInfo Details { get; }
    }
}