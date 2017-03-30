using System.Collections.Generic;
using NvAPIWrapper.Native.Mosaic.Structures;

namespace NvAPIWrapper.Native.Interfaces.Mosaic
{
    public interface IGridTopology
    {
        int Rows { get; }
        int Columns { get; }
        IEnumerable<IGridTopologyDisplay> Displays { get; }
        DisplaySettingsV1 DisplaySettings { get; }
        bool ApplyWithBezelCorrectedResolution { get; }
        bool ImmersiveGaming { get; }
        bool BaseMosaicPanoramic { get; }
        bool DriverReloadAllowed { get; }
        bool AcceleratePrimaryDisplay { get; }
    }
}