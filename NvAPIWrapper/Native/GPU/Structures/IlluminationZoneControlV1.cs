using System.Runtime.InteropServices;
using NvAPIWrapper.Native.Helpers;
using NvAPIWrapper.Native.Interfaces;

namespace NvAPIWrapper.Native.GPU.Structures
{
    /// <summary>
    ///     Holds information regarding a zone control status
    /// </summary>
    [StructLayout(LayoutKind.Explicit, Pack = 8)]
    public struct IlluminationZoneControlV1 : IInitializable
    {
        private const int MaximumNumberOfDataBytes = 64;
        private const int MaximumNumberOfReservedBytes = 64;

        [FieldOffset(0)] internal IlluminationZoneType _ZoneType;

        [FieldOffset(4)] internal IlluminationZoneControlMode _ControlMode;

        [FieldOffset(8)] internal IlluminationZoneControlDataRGB _RGBData;

        [FieldOffset(8)] internal IlluminationZoneControlDataFixedColor _FixedColorData;

        [FieldOffset(8)] [MarshalAs(UnmanagedType.ByValArray, SizeConst = MaximumNumberOfDataBytes)]
        internal byte[] _Data;

        [FieldOffset(MaximumNumberOfDataBytes + 8)]
        [MarshalAs(UnmanagedType.ByValArray, SizeConst = MaximumNumberOfReservedBytes)]
        internal byte[] _Reserved;

        /// <summary>
        ///     Creates a new instance of <see cref="IlluminationZoneControlV1" />.
        /// </summary>
        /// <param name="controlMode">The zone control mode.</param>
        /// <param name="rgbData">The zone control RGB data.</param>
        public IlluminationZoneControlV1(
            IlluminationZoneControlMode controlMode,
            IlluminationZoneControlDataRGB rgbData)
        {
            this = typeof(IlluminationZoneControlV1).Instantiate<IlluminationZoneControlV1>();
            _ControlMode = controlMode;
            _ZoneType = IlluminationZoneType.RGB;
            _RGBData = rgbData;
        }

        /// <summary>
        ///     Creates a new instance of <see cref="IlluminationZoneControlV1" />.
        /// </summary>
        /// <param name="controlMode">The zone control mode.</param>
        /// <param name="fixedColorData">The zone control fixed color data.</param>
        public IlluminationZoneControlV1(
            IlluminationZoneControlMode controlMode,
            IlluminationZoneControlDataFixedColor fixedColorData)
        {
            this = typeof(IlluminationZoneControlV1).Instantiate<IlluminationZoneControlV1>();
            _ControlMode = controlMode;
            _ZoneType = IlluminationZoneType.FixedColor;
            _FixedColorData = fixedColorData;
        }

        /// <summary>
        ///     Gets the type of zone and the type of data needed to control this zone
        /// </summary>
        internal IlluminationZoneType ZoneType
        {
            get => _ZoneType;
        }

        /// <summary>
        ///     Gets the zone control mode
        /// </summary>
        internal IlluminationZoneControlMode ControlMode
        {
            get => _ControlMode;
        }

        /// <summary>
        ///     Gets the control data as a RGB data structure.
        /// </summary>
        /// <returns>An instance of <see cref="IlluminationZoneControlDataRGB" /> containing RGB settings.</returns>
        public IlluminationZoneControlDataRGB AsRGBData()
        {
            return _RGBData;
        }

        /// <summary>
        ///     Gets the control data as a fixed color data structure.
        /// </summary>
        /// <returns>An instance of <see cref="IlluminationZoneControlDataFixedColor" /> containing fixed color settings.</returns>
        public IlluminationZoneControlDataFixedColor AsFixedColorData()
        {
            return _FixedColorData;
        }
    }
}