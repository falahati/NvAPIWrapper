using System.Runtime.InteropServices;
using NvAPIWrapper.Native.Helpers;
using NvAPIWrapper.Native.Interfaces;

namespace NvAPIWrapper.Native.GPU.Structures
{
    /// <summary>
    ///     Holds information regarding a fixed color control data
    /// </summary>
    [StructLayout(LayoutKind.Explicit, Pack = 8)]
    public struct IlluminationZoneControlDataFixedColor : IInitializable
    {
        private const int MaximumNumberOfDataBytes = 64;
        private const int MaximumNumberOfReservedBytes = 64;

        [FieldOffset(0)] internal IlluminationZoneControlDataManualFixedColor _ManualFixedColor;

        [FieldOffset(0)] internal IlluminationZoneControlDataPiecewiseLinearFixedColor _PiecewiseLinearFixedColor;

        [FieldOffset(0)] [MarshalAs(UnmanagedType.ByValArray, SizeConst = MaximumNumberOfDataBytes)]
        internal byte[] _Data;

        [FieldOffset(MaximumNumberOfDataBytes)]
        [MarshalAs(UnmanagedType.ByValArray, SizeConst = MaximumNumberOfReservedBytes)]
        internal byte[] _Reserved;

        /// <summary>
        ///     Creates a new instance of <see cref="IlluminationZoneControlDataFixedColor" />.
        /// </summary>
        /// <param name="manualFixedColor">The zone manual control data.</param>
        public IlluminationZoneControlDataFixedColor(IlluminationZoneControlDataManualFixedColor manualFixedColor)
        {
            this = typeof(IlluminationZoneControlDataFixedColor).Instantiate<IlluminationZoneControlDataFixedColor>();
            _ManualFixedColor = manualFixedColor;
        }

        /// <summary>
        ///     Creates a new instance of <see cref="IlluminationZoneControlDataFixedColor" />.
        /// </summary>
        /// <param name="piecewiseLinearFixedColor">The zone piecewise linear control data.</param>
        public IlluminationZoneControlDataFixedColor(
            IlluminationZoneControlDataPiecewiseLinearFixedColor piecewiseLinearFixedColor)
        {
            this = typeof(IlluminationZoneControlDataFixedColor).Instantiate<IlluminationZoneControlDataFixedColor>();
            _PiecewiseLinearFixedColor = piecewiseLinearFixedColor;
        }

        /// <summary>
        ///     Gets the control data as a manual control structure.
        /// </summary>
        /// <returns>An instance of <see cref="IlluminationZoneControlDataManualFixedColor" /> containing manual settings.</returns>
        public IlluminationZoneControlDataManualFixedColor AsManual()
        {
            return _ManualFixedColor;
        }

        /// <summary>
        ///     Gets the control data as a piecewise linear control structure.
        /// </summary>
        /// <returns>
        ///     An instance of <see cref="IlluminationZoneControlDataPiecewiseLinearFixedColor" /> containing piecewise
        ///     settings.
        /// </returns>
        public IlluminationZoneControlDataPiecewiseLinearFixedColor AsPiecewise()
        {
            return _PiecewiseLinearFixedColor;
        }
    }
}