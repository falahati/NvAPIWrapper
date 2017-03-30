using NvAPIWrapper.Native.GPU;

namespace NvAPIWrapper.Native.Interfaces.GPU
{
    public interface IDisplayIds
    {
        uint DisplayId { get; }
        MonitorConnectionType ConnectionType { get; }
        bool IsDynamic { get; }
        bool IsMultiStreamRootNode { get; }
        bool IsActive { get; }
        bool IsCluster { get; }
        bool IsOSVisible { get; }
        bool IsWFD { get; }
        bool IsConnected { get; }
        bool IsPhysicallyConnected { get; }
    }
}