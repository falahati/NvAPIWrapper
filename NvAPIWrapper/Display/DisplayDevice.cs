using System.Linq;
using NvAPIWrapper.GPU;
using NvAPIWrapper.Native;
using NvAPIWrapper.Native.GPU;
using NvAPIWrapper.Native.GPU.Structures;
using NvAPIWrapper.Native.Interfaces.GPU;

namespace NvAPIWrapper.Display
{
    public class DisplayDevice
    {
        public DisplayDevice(uint displayId)
        {
            DisplayId = displayId;
            var extraInformation = PhysicalGPU.GetDisplayDevices().FirstOrDefault(ids => ids.DisplayId == DisplayId);
            if (extraInformation != null)
            {
                IsAvailable = true;
                ConnectionType = extraInformation.ConnectionType;
                IsDynamic = extraInformation.IsDynamic;
                IsMultiStreamRootNode = extraInformation.IsMultiStreamRootNode;
                IsActive = extraInformation.IsActive;
                IsCluster = extraInformation.IsCluster;
                IsOSVisible = extraInformation.IsOSVisible;
                IsWFD = extraInformation.IsWFD;
                IsConnected = extraInformation.IsConnected;
                IsPhysicallyConnected = extraInformation.IsPhysicallyConnected;
            }
        }

        public DisplayDevice(IDisplayIds displayIds)
        {
            IsAvailable = true;
            DisplayId = displayIds.DisplayId;
            ConnectionType = displayIds.ConnectionType;
            IsDynamic = displayIds.IsDynamic;
            IsMultiStreamRootNode = displayIds.IsMultiStreamRootNode;
            IsActive = displayIds.IsActive;
            IsCluster = displayIds.IsCluster;
            IsOSVisible = displayIds.IsOSVisible;
            IsWFD = displayIds.IsWFD;
            IsConnected = displayIds.IsConnected;
            IsPhysicallyConnected = displayIds.IsPhysicallyConnected;
        }

        public DisplayDevice(string displayName) : this(DisplayApi.GetDisplayIdByDisplayName(displayName))
        {
        }

        public MonitorConnectionType ConnectionType { get; }
        public bool IsDynamic { get; }
        public bool IsMultiStreamRootNode { get; }
        public bool IsActive { get; }
        public bool IsCluster { get; }
        public bool IsOSVisible { get; }
        public bool IsWFD { get; }
        public bool IsConnected { get; }
        public bool IsPhysicallyConnected { get; }
        public bool IsAvailable { get; }

        public uint DisplayId { get; }

        public PhysicalGPU PhysicalGPU => new PhysicalGPU(GPUApi.GetPhysicalGpuFromDisplayId(DisplayId));

        public OutputId OutputId
        {
            get
            {
                PhysicalGPUHandle handle;
                return GPUApi.GetGpuAndOutputIdFromDisplayId(DisplayId, out handle);
            }
        }
    }
}