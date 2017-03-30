using System;

namespace NvAPIWrapper.Native.Mosaic
{
    [Flags]
    public enum TopologyValidity : uint
    {
        Valid = 0,
        MissingGPU = 1,
        MissingDisplay = 2,
        MixedDisplayTypes = 4
    }
}