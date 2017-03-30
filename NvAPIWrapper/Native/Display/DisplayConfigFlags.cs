using System;

namespace NvAPIWrapper.Native.Display
{
    [Flags]
    public enum DisplayConfigFlags
    {
        None = 0,
        ValidateOnly = 0x00000001,
        SaveToPersistence = 0x00000002,
        DriverReloadAllowed = 0x00000004,
        ForceModeEnumeration = 0x00000008,
        ForceCommitVideoPresentNetwork = 0x00000010
    }
}