using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using NvAPIWrapper.Native.Attributes;
using NvAPIWrapper.Native.General.Structures;
using NvAPIWrapper.Native.Helpers;
using NvAPIWrapper.Native.Interfaces;
using NvAPIWrapper.Native.Interfaces.Mosaic;

namespace NvAPIWrapper.Native.Mosaic.Structures
{
    [StructLayout(LayoutKind.Sequential, Pack = 8)]
    [StructureVersion(2)]
    public struct GridTopologyV2 : IGridTopology, IInitializable
    {
        public const int MaxDisplays = 64;

        internal StructureVersion _Version;
        internal uint _Rows;
        internal uint _Columns;
        internal uint _DisplayCount;
        internal uint _RawReserved;
        [MarshalAs(UnmanagedType.ByValArray, SizeConst = MaxDisplays)] internal GridTopologyDisplayV2[] _Displays;
        internal DisplaySettingsV1 _DisplaySettings;

        public GridTopologyV2(int rows, int columns, GridTopologyDisplayV2[] displays, DisplaySettingsV1 displaySettings,
            bool applyWithBezelCorrectedResolution, bool immersiveGaming, bool baseMosaicPanoramic,
            bool driverReloadAllowed,
            bool acceleratePrimaryDisplay, bool pixelShift)
        {
            if (rows*columns <= 0)
            {
                throw new ArgumentOutOfRangeException($"{nameof(rows)}, {nameof(columns)}", "Invalid display arrangement.");
            }
            if (displays.Length > MaxDisplays)
            {
                throw new ArgumentException("Too many displays.");
            }
            if (displays.Length != rows*columns)
            {
                throw new ArgumentException("Number of displays should match the arrangement.", nameof(displays));
            }
            this = typeof(GridTopologyV2).Instantiate<GridTopologyV2>();
            _Rows = (uint) rows;
            _Columns = (uint) columns;
            _DisplayCount = (uint) displays.Length;
            _Displays = displays;
            _DisplaySettings = displaySettings;
            ApplyWithBezelCorrectedResolution = applyWithBezelCorrectedResolution;
            ImmersiveGaming = immersiveGaming;
            BaseMosaicPanoramic = baseMosaicPanoramic;
            DriverReloadAllowed = driverReloadAllowed;
            AcceleratePrimaryDisplay = acceleratePrimaryDisplay;
            PixelShift = pixelShift;
            Array.Resize(ref _Displays, MaxDisplays);
        }

        public int Rows => (int) _Rows;
        public int Columns => (int) _Columns;

        public IEnumerable<IGridTopologyDisplay> Displays
            => _Displays.Take((int) _DisplayCount).Cast<IGridTopologyDisplay>();

        public DisplaySettingsV1 DisplaySettings => _DisplaySettings;

        public bool ApplyWithBezelCorrectedResolution
        {
            get { return _RawReserved.GetBit(0); }
            private set { _RawReserved = _RawReserved.SetBit(0, value); }
        }

        public bool ImmersiveGaming
        {
            get { return _RawReserved.GetBit(1); }
            private set { _RawReserved = _RawReserved.SetBit(1, value); }
        }

        public bool BaseMosaicPanoramic
        {
            get { return _RawReserved.GetBit(2); }
            private set { _RawReserved = _RawReserved.SetBit(2, value); }
        }

        public bool DriverReloadAllowed
        {
            get { return _RawReserved.GetBit(3); }
            private set { _RawReserved = _RawReserved.SetBit(3, value); }
        }

        public bool AcceleratePrimaryDisplay
        {
            get { return _RawReserved.GetBit(4); }
            private set { _RawReserved = _RawReserved.SetBit(4, value); }
        }

        public bool PixelShift
        {
            get { return _RawReserved.GetBit(5); }
            private set { _RawReserved = _RawReserved.SetBit(5, value); }
        }
    }
}