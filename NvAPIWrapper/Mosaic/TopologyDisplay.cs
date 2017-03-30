using NvAPIWrapper.GPU;
using NvAPIWrapper.Native.GPU;

namespace NvAPIWrapper.Mosaic
{
    public class TopologyDisplay
    {
        internal TopologyDisplay(Native.Mosaic.Structures.TopologyDetails.LayoutCell layoutCell)
        {
            PhysicalGPU = !layoutCell.PhysicalGPUHandle.IsNull ? new PhysicalGPU(layoutCell.PhysicalGPUHandle) : null;
            OutputId = layoutCell.DisplayOutputId;
            OverlapX = layoutCell.OverlapX;
            OverlapY = layoutCell.OverlapY;
        }

        public PhysicalGPU PhysicalGPU { get; }
        public OutputId OutputId { get; }
        public int OverlapX { get; }
        public int OverlapY { get; }
    }
}