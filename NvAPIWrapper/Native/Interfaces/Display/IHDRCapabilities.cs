using NvAPIWrapper.Native.Display;
using NvAPIWrapper.Native.Display.Structures;

namespace NvAPIWrapper.Native.Interfaces.Display
{
    public interface IHDRCapabilities
    {
        /// <summary>
        ///     Static Metadata Descriptor Id (0 for static metadata type 1)
        /// </summary>
        StaticMetadataDescriptorId StaticMetadataDescriptorId { get; }

        /// <summary>
        ///     Holds color coordinates data
        /// </summary>
        DisplayData DisplayData { get; }

        /// <summary>
        ///     HDMI2.0a UHDA HDR with ST2084 EOTF (CEA861.3). Boolean: 0 = not supported, 1 = supported;
        /// </summary>
        bool IsST2084EOTFSupported { get; }

        /// <summary>
        ///     HDMI2.0a traditional HDR gamma (CEA861.3). Boolean: 0 = not supported, 1 = supported;
        /// </summary>
        bool IsTraditionalHDRGammaSupported { get; }

        /// <summary>
        ///     Extended Dynamic Range on SDR displays. Boolean: 0 = not supported, 1 = supported;
        /// </summary>
        bool IsEDRSupported { get; }

        /// <summary>
        ///     If set, driver will expand default (=zero) HDR capabilities parameters contained in display's EDID.
        ///     Boolean: 0 = report actual HDR parameters, 1 = expand default HDR parameters;
        /// </summary>
        bool DriverExpandDefaultHDRParameters { get; }

        /// <summary>
        ///     HDMI2.0a traditional SDR gamma (CEA861.3). Boolean: 0 = not supported, 1 = supported;
        /// </summary>
        bool IsTraditionalSDRGammaSupported { get; }
    }
}