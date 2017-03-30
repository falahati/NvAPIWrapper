using NvAPIWrapper.Display;
using NvAPIWrapper.Native.Display;
using NvAPIWrapper.Native.Interfaces.Mosaic;
using NvAPIWrapper.Native.Mosaic;
using NvAPIWrapper.Native.Mosaic.Structures;

namespace NvAPIWrapper.Mosaic
{
    public class GridTopologyDisplay
    {
        public GridTopologyDisplay(uint displayId, int overlapX = 0, int overlapY = 0, Rotate rotation = Rotate.Degree0,
            uint cloneGroup = 0,
            PixelShiftType pixelShiftType = PixelShiftType.NoPixelShift)
        {
            DisplayDevice = new DisplayDevice(displayId);
            OverlapX = overlapX;
            OverlapY = overlapY;
            Rotation = rotation;
            CloneGroup = cloneGroup;
            PixelShiftType = pixelShiftType;
        }

        public GridTopologyDisplay(DisplayDevice display, int overlapX = 0, int overlapY = 0, Rotate rotation = Rotate.Degree0,
            uint cloneGroup = 0,
            PixelShiftType pixelShiftType = PixelShiftType.NoPixelShift)
            : this(display.DisplayId, overlapX, overlapY, rotation, cloneGroup, pixelShiftType)
        {
        }

        public GridTopologyDisplay(IGridTopologyDisplay gridTopologyDisplay)
        {
            DisplayDevice = new DisplayDevice(gridTopologyDisplay.DisplayId);
            OverlapX = gridTopologyDisplay.OverlapX;
            OverlapY = gridTopologyDisplay.OverlapY;
            Rotation = gridTopologyDisplay.Rotation;
            CloneGroup = gridTopologyDisplay.CloneGroup;
            if (gridTopologyDisplay is GridTopologyDisplayV2)
            {
                PixelShiftType = ((GridTopologyDisplayV2) gridTopologyDisplay).PixelShiftType;
            }
        }

        public DisplayDevice DisplayDevice { get; }
        public int OverlapX { get; set; }
        public int OverlapY { get; set; }
        public Rotate Rotation { get; set; }
        public uint CloneGroup { get; set; }
        public PixelShiftType PixelShiftType { get; set; }

        public GridTopologyDisplayV1 GetGridTopologyDisplayV1()
        {
            return new GridTopologyDisplayV1(DisplayDevice.DisplayId, OverlapX, OverlapY, Rotation, CloneGroup);
        }

        public GridTopologyDisplayV2 GetGridTopologyDisplayV2()
        {
            return new GridTopologyDisplayV2(DisplayDevice.DisplayId, OverlapX, OverlapY, Rotation, CloneGroup, PixelShiftType);
        }
    }
}