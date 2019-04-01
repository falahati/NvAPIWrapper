using System;

namespace NvAPIWrapper.Native.GPU
{
    [Flags]
    public enum CoolerPolicy : uint
    {
        None = 0,
        Manual = 0b1,
        Performance = 0b10,
        TemperatureDiscrete = 0b100,
        TemperatureContinuous = 0b1000,
        Silent = 0b10000
    }
}