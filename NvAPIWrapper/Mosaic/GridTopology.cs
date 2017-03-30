using System;
using System.Linq;
using NvAPIWrapper.Native;
using NvAPIWrapper.Native.Exceptions;
using NvAPIWrapper.Native.General;
using NvAPIWrapper.Native.Interfaces.Mosaic;
using NvAPIWrapper.Native.Mosaic;
using NvAPIWrapper.Native.Mosaic.Structures;

namespace NvAPIWrapper.Mosaic
{
    public class GridTopology
    {
        public GridTopology(int rows, int columns, GridTopologyDisplay[] displays)
        {
            SetDisplays(rows, columns, displays);
            var possibleDisplaySettings = GetPossibleDisplaySettings();
            if (possibleDisplaySettings.Any())
            {
                SetDisplaySettings(
                    possibleDisplaySettings.OrderByDescending(
                        settings => (long)settings.Width*settings.Height*settings.BitsPerPixel*settings.Frequency).First());
            }
        }

        public GridTopology(IGridTopology gridTopology)
        {
            Rows = gridTopology.Rows;
            Columns = gridTopology.Columns;
            Displays = gridTopology.Displays.Select(display => new GridTopologyDisplay(display)).ToArray();
            SetDisplaySettings(gridTopology.DisplaySettings);
            ApplyWithBezelCorrectedResolution = gridTopology.ApplyWithBezelCorrectedResolution;
            ImmersiveGaming = gridTopology.ImmersiveGaming;
            BaseMosaicPanoramic = gridTopology.BaseMosaicPanoramic;
            DriverReloadAllowed = gridTopology.DriverReloadAllowed;
            AcceleratePrimaryDisplay = gridTopology.AcceleratePrimaryDisplay;
        }

        public int Width { get; private set; }
        public int Height { get; private set; }
        public int BitsPerPixel { get; private set; }
        public int Frequency { get; private set; }
        public int Rows { get; private set; }
        public int Columns { get; private set; }
        public GridTopologyDisplay[] Displays { get; private set; }
        public bool ApplyWithBezelCorrectedResolution { get; set; }
        public bool ImmersiveGaming { get; set; }
        public bool BaseMosaicPanoramic { get; set; }
        public bool DriverReloadAllowed { get; set; }
        public bool AcceleratePrimaryDisplay { get; set; }

        public void SetDisplays(int rows, int columns, GridTopologyDisplay[] displays)
        {
            if (rows*columns <= 0)
            {
                throw new ArgumentOutOfRangeException($"{nameof(rows)}, {nameof(columns)}", "Invalid display arrangement.");
            }
            if (displays.Length != rows*columns)
            {
                throw new ArgumentException("Number of displays should match the arrangement.", nameof(displays));
            }
            Rows = rows;
            Columns = columns;
            Displays = displays;
        }

        public DisplaySettingsV1 GetDisplaySettingsV1()
        {
            return new DisplaySettingsV1(Width, Height, BitsPerPixel, Frequency);
        }

        public GridTopologyV1 GetGridTopologyV1()
        {
            var displaySettings = GetDisplaySettingsV1();
            return new GridTopologyV1(Rows, Columns,
                Displays.Select(display => display.GetGridTopologyDisplayV1()).ToArray(), displaySettings,
                ApplyWithBezelCorrectedResolution, ImmersiveGaming, BaseMosaicPanoramic, DriverReloadAllowed,
                AcceleratePrimaryDisplay);
        }

        public GridTopologyV2 GetGridTopologyV2()
        {
            var displaySettings = GetDisplaySettingsV1();
            return new GridTopologyV2(Rows, Columns,
                Displays.Select(display => display.GetGridTopologyDisplayV2()).ToArray(), displaySettings,
                ApplyWithBezelCorrectedResolution, ImmersiveGaming, BaseMosaicPanoramic, DriverReloadAllowed,
                AcceleratePrimaryDisplay, Displays.Any(display => display.PixelShiftType != PixelShiftType.NoPixelShift));
        }

        public void SetDisplaySettings(IDisplaySettings displaySettings)
        {
            Width = displaySettings.Width;
            Height = displaySettings.Height;
            BitsPerPixel = displaySettings.BitsPerPixel;
            Frequency = displaySettings.Frequency;
        }

        public IDisplaySettings[] GetPossibleDisplaySettings()
        {
            var gridTopologyV2 = GetGridTopologyV2();
            try
            {
                return MosaicApi.EnumDisplayModes(gridTopologyV2);
            }
            catch (NVIDIAApiException ex)
            {
                if (ex.Status != Status.IncompatibleStructVersion)
                {
                    throw;
                }
            }
            catch (NVIDIANotSupportedException)
            {
                // ignore
            }
            var gridTopologyV1 = GetGridTopologyV1();
            return MosaicApi.EnumDisplayModes(gridTopologyV1);
        }

        public static GridTopology[] GetGridTopologies()
        {
            return MosaicApi.EnumDisplayGrids().Select(topology => new GridTopology(topology)).ToArray();
        }

        public static DisplayTopologyStatus[] ValidateGridTopologies(GridTopology[] grids,
            SetDisplayTopologyFlag flags = SetDisplayTopologyFlag.AllowInvalid)
        {
            var gridTopologyV2 = grids.Select(grid => grid.GetGridTopologyV2()).Cast<IGridTopology>().ToArray();
            try
            {
                return MosaicApi.ValidateDisplayGrids(gridTopologyV2, flags);
            }
            catch (NVIDIAApiException ex)
            {
                if (ex.Status != Status.IncompatibleStructVersion)
                {
                    throw;
                }
            }
            catch (NVIDIANotSupportedException)
            {
                // ignore
            }
            var gridTopologyV1 = grids.Select(grid => grid.GetGridTopologyV1()).Cast<IGridTopology>().ToArray();
            return MosaicApi.ValidateDisplayGrids(gridTopologyV1, flags);
        }

        public static void SetGridTopologies(GridTopology[] grids,
            SetDisplayTopologyFlag flags = SetDisplayTopologyFlag.NoFlag)
        {
            var gridTopologyV2 = grids.Select(grid => grid.GetGridTopologyV2()).Cast<IGridTopology>().ToArray();
            try
            {
                MosaicApi.SetDisplayGrids(gridTopologyV2, flags);
            }
            catch (NVIDIAApiException ex)
            {
                if (ex.Status != Status.IncompatibleStructVersion)
                {
                    throw;
                }
            }
            catch (NVIDIANotSupportedException)
            {
                // ignore
            }
            var gridTopologyV1 = grids.Select(grid => grid.GetGridTopologyV1()).Cast<IGridTopology>().ToArray();
            MosaicApi.SetDisplayGrids(gridTopologyV1, flags);
        }
    }
}