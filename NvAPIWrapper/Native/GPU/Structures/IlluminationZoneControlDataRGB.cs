using System.Runtime.InteropServices;
using NvAPIWrapper.Native.Helpers;
using NvAPIWrapper.Native.Interfaces;

namespace NvAPIWrapper.Native.GPU.Structures
{
    /// <summary>
    ///     Holds information regarding a RGB control data
    /// </summary>
    [StructLayout(LayoutKind.Explicit, Pack = 8)]
    public struct IlluminationZoneControlDataRGB : IInitializable
    {
        private const int MaximumNumberOfDataBytes = 64;
        private const int MaximumNumberOfReservedBytes = 64;

        [FieldOffset(0)] internal IlluminationZoneControlDataManualRGB _ManualRGB;

        [FieldOffset(0)] internal IlluminationZoneControlDataPiecewiseLinearRGB _PiecewiseLinearRGB;

        [FieldOffset(0)] [MarshalAs(UnmanagedType.ByValArray, SizeConst = MaximumNumberOfDataBytes)]
        internal byte[] _Data;

        [FieldOffset(MaximumNumberOfDataBytes)]
        [MarshalAs(UnmanagedType.ByValArray, SizeConst = MaximumNumberOfReservedBytes)]
        internal byte[] _Reserved;

        /// <summary>
        ///     Creates a new instance of <see cref="IlluminationZoneControlDataRGB" />.
        /// </summary>
        /// <param name="manualRGB">The zone manual control data.</param>
        public IlluminationZoneControlDataRGB(IlluminationZoneControlDataManualRGB manualRGB)
        {
            this = typeof(IlluminationZoneControlDataRGB).Instantiate<IlluminationZoneControlDataRGB>();
            _ManualRGB = manualRGB;
        }

        /// <summary>
        ///     Creates a new instance of <see cref="IlluminationZoneControlDataRGB" />.
        /// </summary>
        /// <param name="piecewiseLinearRGB">The zone piecewise linear control data.</param>
        public IlluminationZoneControlDataRGB(IlluminationZoneControlDataPiecewiseLinearRGB piecewiseLinearRGB)
        {
            this = typeof(IlluminationZoneControlDataRGB).Instantiate<IlluminationZoneControlDataRGB>();
            _PiecewiseLinearRGB = piecewiseLinearRGB;
        }

        /// <summary>
        ///     Gets the control data as a manual control structure.
        /// </summary>
        /// <returns>An instance of <see cref="IlluminationZoneControlDataManualRGB" /> containing manual settings.</returns>
        public IlluminationZoneControlDataManualRGB AsManual()
        {
            return _ManualRGB;
        }

        /// <summary>
        ///     Gets the control data as a piecewise linear control structure.
        /// </summary>
        /// <returns>
        ///     An instance of <see cref="IlluminationZoneControlDataPiecewiseLinearRGB" /> containing piecewise linear
        ///     settings.
        /// </returns>
        public IlluminationZoneControlDataPiecewiseLinearRGB AsPiecewise()
        {
            return _PiecewiseLinearRGB;
        }
    }
}