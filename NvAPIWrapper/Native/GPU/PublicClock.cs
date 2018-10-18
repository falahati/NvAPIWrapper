using NvAPIWrapper.Native.GPU.Structures;

namespace NvAPIWrapper.Native.GPU
{
    /// <summary>
    ///     Clocks available to public
    /// </summary>
    public enum PublicClock
    {
        /// <summary>
        ///     Undefined
        /// </summary>
        Undefined = ClockFrequenciesV1.MaxClocksPerGpu,

        /// <summary>
        ///     3D graphics clock
        /// </summary>
        Graphics = 0,

        /// <summary>
        ///     Memory clock
        /// </summary>
        Memory = 4,

        /// <summary>
        ///     Processor clock
        /// </summary>
        Processor = 7,

        /// <summary>
        ///     Video decoding clock
        /// </summary>
        Video = 8
    }
}