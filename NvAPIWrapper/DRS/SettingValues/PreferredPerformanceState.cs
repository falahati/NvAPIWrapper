using System;

namespace NvAPIWrapper.DRS.SettingValues
{
    public enum PreferredPerformanceState : UInt32
    {
        Adaptive = 0x0,

        PreferMaximum = 0x1,

        DriverControlled = 0x2,

        PreferConsistentPerformance = 0x3,

        PreferMinimum = 0x4,

        OptimalPower = 0x5,

        Minimum = 0x0,

        Maximum = 0x5,

        Default = 0x5
    }
}
