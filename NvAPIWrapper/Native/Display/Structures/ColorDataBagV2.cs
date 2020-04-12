using System.Runtime.InteropServices;

namespace NvAPIWrapper.Native.Display.Structures
{
    [StructLayout(LayoutKind.Sequential, Pack = 8)]
    public struct ColorDataBagV2
    {
        internal ColorFormat _ColorFormat;
        internal Colorimetry _Colorimetry;
        internal DynamicRange _DynamicRange;

        /// <summary>
        ///     One of ColorFormat enum values.
        /// </summary>
        public ColorFormat ColorFormat
        {
            get => _ColorFormat;
            set => _ColorFormat = value;
        }

        /// <summary>
        ///     One of Colorimetry enum values.
        /// </summary>
        public Colorimetry Colorimetry
        {
            get => _Colorimetry;
            set => _Colorimetry = value;
        }

        /// <summary>
        ///     One of DynamicRange enum values.
        /// </summary>
        public DynamicRange DynamicRange
        {
            get => _DynamicRange;
            set => _DynamicRange = value;
        }
    }
}
