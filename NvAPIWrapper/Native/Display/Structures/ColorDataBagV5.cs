﻿using System.Runtime.InteropServices;

namespace NvAPIWrapper.Native.Display.Structures
{
    [StructLayout(LayoutKind.Sequential, Pack = 8)]
    public struct ColorDataBagV5
    {
        internal ColorFormat _ColorFormat;
        internal Colorimetry _Colorimetry;
        internal DynamicRange _DynamicRange;
        internal BPC _BPC;
        internal ColorSelectionPolicy _ColorSelectionPolicy;
        internal DesktopColorDepth _DesktopColorDepth;

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

        /// <summary>
        ///     One of BPC enum values.
        /// </summary>
        public BPC BPC
        {
            get => _BPC;
            set => _BPC = value;
        }

        /// <summary>
        ///     One of the color selection policy
        /// </summary>
        public ColorSelectionPolicy ColorSelectionPolicy
        {
            get => _ColorSelectionPolicy;
            set => _ColorSelectionPolicy = value;
        }

        /// <summary>
        ///     One of DesktopColorDepth enum values.
        /// </summary>
        public DesktopColorDepth DesktopColorDepth
        {
            get => _DesktopColorDepth;
            set => _DesktopColorDepth = value;
        }
    }
}