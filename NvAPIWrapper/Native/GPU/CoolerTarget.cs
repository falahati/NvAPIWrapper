using System;

namespace NvAPIWrapper.Native.GPU
{
    [Flags]
    public enum CoolerTarget : uint
    {
        None = 0,
        GPU = 0b1,
        Memory = 0b10,
        PowerSupply = 0b100,
        All = GPU | Memory | PowerSupply
    }
}