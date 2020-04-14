using System.Runtime.InteropServices;

namespace NvAPIWrapper.Native.Display.Structures
{
    [StructLayout(LayoutKind.Sequential, Pack = 8)]
    public struct ColorDataBagV1
    {
        internal HDRColorFormat _HDRColorFormat;
        internal Colorimetry _Colorimetry;

        /// <summary>
        ///     One of HDRColorFormat enum values.
        /// </summary>
        public HDRColorFormat HDRColorFormat
        {
            get => _HDRColorFormat;
            set => _HDRColorFormat = value;
        }

        /// <summary>
        ///     One of Colorimetry enum values.
        /// </summary>
        public Colorimetry Colorimetry
        {
            get => _Colorimetry;
            set => _Colorimetry = value;
        }
    }
}
