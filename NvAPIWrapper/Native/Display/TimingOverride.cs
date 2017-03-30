namespace NvAPIWrapper.Native.Display
{
    public enum TimingOverride
    {
        Current = 0,

        Auto,

        EDID,

        // VESA DMT timing
        DMT,

        // VESA DMT timing with reduced blanking
        // ReSharper disable once InconsistentNaming
        DMT_RB,

        // VESA CVT timing
        CVT,

        // VESA CVT timing with reduced blanking
        // ReSharper disable once InconsistentNaming
        CVT_RB,

        // VESA GTF
        GTF,

        // EIA 861x PreDefined timing
        EIA861,

        AnalogTV,

        // NVIDIA Custom timing
        Custom,

        // NVIDIA PreDefined timing
        Predefined,
        PSF = Predefined,

        ASPR,

        // Override for SDI timing
        SDI,

        Max
    }
}