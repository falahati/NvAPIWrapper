using System;

namespace NvAPIWrapper.DRS.SettingValues
{
    public enum PerformanceStateFrameRateLimiter2Control : UInt32
    {
        DelayCE = 0x0,

        Delay3D = 0x1,

        AvoidNoop = 0x2,

        DelayCEPresent3D = 0x8,

        AllowAllMaxwell = 0x10,

        AllowAll = 0x20,

        ForceOff = 0x40,

        EnableVCE = 0x80,

        DefaultForGM10X = 0x11,

        Default = 0x88
    }
}
