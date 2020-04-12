using NvAPIWrapper.Native.Display;
using NvAPIWrapper.Native.Display.Structures;

namespace NvAPIWrapper.Native.Interfaces.Display
{
    /// <summary>
    ///     Contains information regarding HDR color data
    /// </summary>
    public interface IHDRColorData
    {
        /// <summary>
        ///     Command get/set
        /// </summary>
        HDRCommand HDRCommand { get; set; }

        /// <summary>
        ///     HDR mode
        /// </summary>
        HDRMode HDRMode { get; set; }

        /// <summary>
        ///     Holds color coordinates data
        /// </summary>
        MasteringDisplayData MasteringDisplayData { get; set; }

        /// <summary>
        ///     Static Metadata Descriptor Id (0 for static metadata type 1)
        /// </summary>
        StaticMetadataDescriptorId StaticMetadataDescriptorId { get; }
    }
}