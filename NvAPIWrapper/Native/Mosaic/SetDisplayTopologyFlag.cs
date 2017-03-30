using System;

namespace NvAPIWrapper.Native.Mosaic
{
    [Flags]
    public enum SetDisplayTopologyFlag : uint
    {
        NoFlag = 0,
        CurrentGPUTopology = 1,
        NoDriverReload = 2,
        MaximizePerformance = 4,
        AllowInvalid = 8
    }
}