using System;

namespace NvAPIWrapper.Native.Mosaic
{
    [Flags]
    public enum DisplayTopologyWarning : uint
    {
        NoWarning = 0,
        DisplayPosition = 1,
        DriverReloadRequired = 2
    }
}