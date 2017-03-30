using System.Linq;
using NvAPIWrapper.GPU;
using NvAPIWrapper.Native.Mosaic;

namespace NvAPIWrapper.Mosaic
{
    public class TopologyDetails
    {
        internal TopologyDetails(Native.Mosaic.Structures.TopologyDetails details)
        {
            Rows = details.Rows;
            Columns = details.Columns;
            LogicalGPU = !details.LogicalGPUHandle.IsNull ? new LogicalGPU(details.LogicalGPUHandle) : null;
            ValidityFlags = details.ValidityFlags;
            Displays =
                details.Layout.Select(cells => cells.Select(cell => new TopologyDisplay(cell)).ToArray()).ToArray();
        }

        public int Rows { get; }
        public int Columns { get; }
        public LogicalGPU LogicalGPU { get; }
        public TopologyValidity ValidityFlags { get; }
        public TopologyDisplay[][] Displays { get; }
    }
}