using System;
using System.Runtime.InteropServices;
using NvAPIWrapper.Native.Attributes;
using NvAPIWrapper.Native.General.Structures;
using NvAPIWrapper.Native.Helpers;
using NvAPIWrapper.Native.Interfaces;
using NvAPIWrapper.Native.Interfaces.Display;

namespace NvAPIWrapper.Native.Display.Structures
{
    /// <inheritdoc cref="IColorData" />
    [StructLayout(LayoutKind.Sequential, Pack = 8)]
    [StructureVersion(4)]
    public struct ColorDataV4 : IInitializable, IColorData
    {
        internal StructureVersion _Version;
        private readonly ColorDataCommand _Command;
        private readonly ColorDataBag _Data;

        [StructLayout(LayoutKind.Sequential, Pack = 8)]
        private struct ColorDataBag
        {
            public readonly ColorDataFormat ColorFormat;
            public readonly ColorDataColorimetry Colorimetry;
            public readonly ColorDataDynamicRange ColorDynamicRange;
            public readonly ColorDataDepth ColorDepth;
            public readonly ColorDataSelectionPolicy ColorSelectionPolicy;

            public ColorDataBag(
                ColorDataFormat colorFormat,
                ColorDataColorimetry colorimetry,
                ColorDataDynamicRange colorDynamicRange,
                ColorDataDepth colorDepth,
                ColorDataSelectionPolicy colorSelectionPolicy
            )
            {
                ColorFormat = colorFormat;
                Colorimetry = colorimetry;
                ColorDynamicRange = colorDynamicRange;
                ColorDepth = colorDepth;
                ColorSelectionPolicy = colorSelectionPolicy;
            }
        }

        /// <summary>
        ///     Creates an instance of <see cref="ColorDataV4" /> to retrieve color data information
        /// </summary>
        /// <param name="command">The command to be executed.</param>
        public ColorDataV4(ColorDataCommand command)
        {
            this = typeof(ColorDataV4).Instantiate<ColorDataV4>();

            if (command != ColorDataCommand.Get && command != ColorDataCommand.GetDefault)
            {
                throw new ArgumentOutOfRangeException(nameof(command));
            }

            _Command = command;
        }

        /// <summary>
        ///     Creates an instance of <see cref="ColorDataV4" /> to modify the color data
        /// </summary>
        /// <param name="command">The command to be executed.</param>
        /// <param name="colorFormat">The color data color format.</param>
        /// <param name="colorimetry">The color data color space.</param>
        /// <param name="colorDynamicRange">The color data dynamic range.</param>
        /// <param name="colorDepth">The color data color depth.</param>
        /// <param name="colorSelectionPolicy">The color data selection policy.</param>
        public ColorDataV4(
            ColorDataCommand command,
            ColorDataFormat colorFormat,
            ColorDataColorimetry colorimetry,
            ColorDataDynamicRange colorDynamicRange,
            ColorDataDepth colorDepth,
            ColorDataSelectionPolicy colorSelectionPolicy
        )
        {
            this = typeof(ColorDataV4).Instantiate<ColorDataV4>();

            if (command != ColorDataCommand.Set && command != ColorDataCommand.IsSupportedColor)
            {
                throw new ArgumentOutOfRangeException(nameof(command));
            }

            _Command = command;
            _Data = new ColorDataBag(colorFormat, colorimetry, colorDynamicRange, colorDepth, colorSelectionPolicy);
        }

        /// <inheritdoc />
        public ColorDataFormat ColorFormat
        {
            get => _Data.ColorFormat;
        }

        /// <inheritdoc />
        public ColorDataColorimetry Colorimetry
        {
            get => _Data.Colorimetry;
        }

        /// <inheritdoc />
        public ColorDataDynamicRange? DynamicRange
        {
            get => _Data.ColorDynamicRange;
        }

        /// <inheritdoc />
        public ColorDataDepth? ColorDepth
        {
            get => _Data.ColorDepth;
        }

        /// <inheritdoc />
        public ColorDataSelectionPolicy? SelectionPolicy
        {
            get => _Data.ColorSelectionPolicy;
        }

        /// <inheritdoc />
        public ColorDataDesktopDepth? DesktopColorDepth
        {
            get => null;
        }

        /// <summary>
        ///     Gets the color data command
        /// </summary>
        // ReSharper disable once ConvertToAutoProperty
        public ColorDataCommand Command
        {
            get => _Command;
        }
    }
}