using System.Collections.Generic;
using NvAPIWrapper.Native.Mosaic.Structures;

namespace NvAPIWrapper.Native.Interfaces.Mosaic
{
    public interface ISupportedTopologiesInfo
    {
        IEnumerable<TopologyBrief> TopologyBriefs { get; }
        IEnumerable<IDisplaySettings> DisplaySettings { get; }
    }
}