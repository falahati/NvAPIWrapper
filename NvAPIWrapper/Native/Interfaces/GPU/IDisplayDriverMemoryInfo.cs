namespace NvAPIWrapper.Native.Interfaces.GPU
{
    /// <summary>
    ///     Interface for all DisplayDriverMemoryInfo structures
    /// </summary>
    public interface IDisplayDriverMemoryInfo
    {
        /// <summary>
        ///     Size(in kb) of the available physical frame buffer for allocating video memory surfaces.
        /// </summary>
        uint AvailableDedicatedVideoMemory { get; }

        /// <summary>
        ///     Size(in kb) of the current available physical frame buffer for allocating video memory surfaces.
        /// </summary>
        uint CurrentAvailableDedicatedVideoMemory { get; }

        /// <summary>
        ///     Size(in kb) of the physical frame buffer.
        /// </summary>
        uint DedicatedVideoMemory { get; }

        /// <summary>
        ///     Size(in kb) of shared system memory that driver is allowed to commit for surfaces across all allocations.
        /// </summary>
        uint SharedSystemMemory { get; }

        /// <summary>
        ///     Size(in kb) of system memory the driver allocates at load time.
        /// </summary>
        uint SystemVideoMemory { get; }
    }
}