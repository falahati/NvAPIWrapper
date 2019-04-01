using System;

namespace NvAPIWrapper.Native.GPU
{
    [Flags]
    public enum PerformanceLimit : uint
    {
        None = 0,
        PowerLimit = 0b1,
        TemperatureLimit = 0b10,
        VoltageLimit = 0b100,
        Unknown8 = 0b1000,
        NoLoadLimit = 0b10000
    }
}