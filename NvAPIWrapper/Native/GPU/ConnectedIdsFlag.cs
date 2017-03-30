using System;

namespace NvAPIWrapper.Native.GPU
{
    [Flags]
    public enum ConnectedIdsFlag : uint
    {
        None = 0,
        Uncached = 1,
        SLI = 2,
        LIDState = 4,
        Fake = 8,
        ExcludeList = 16
    }
}