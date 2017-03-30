namespace NvAPIWrapper.Native.Display
{
    public enum Scaling
    {
        // No change
        Default = 0,

        // New Scaling Declarations

        // Balanced  - Full Screen
        ToClosest = 1,
        // Force GPU - Full Screen
        ToNative = 2,
        // Force GPU - Centered\No Scaling 
        GPUScanOutToNative = 3,
        // Force GPU - Aspect Ratio
        ToAspectScanOutToNative = 5,
        // Balanced  - Aspect Ratio
        ToAspectScanOutToClosest = 6,
        // Balanced  - Centered\No Scaling
        GPUScanOutToClosest = 7,

        // \New Scaling Declarations

        // Legacy Declarations

        MonitorScaling = ToClosest,
        AdapterScaling = ToNative,
        Centered = GPUScanOutToNative,
        AspectScaling = ToAspectScanOutToNative,

        // \Legacy Declarations

        // For future use
        Customized = 255
    }
}