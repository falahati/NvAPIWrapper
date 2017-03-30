using System.Runtime.InteropServices;
using NvAPIWrapper.Native.Attributes;
using NvAPIWrapper.Native.Display;
using NvAPIWrapper.Native.General.Structures;
using NvAPIWrapper.Native.Helpers;
using NvAPIWrapper.Native.Interfaces;
using NvAPIWrapper.Native.Interfaces.Mosaic;

namespace NvAPIWrapper.Native.Mosaic.Structures
{
    [StructLayout(LayoutKind.Sequential, Pack = 8)]
    [StructureVersion]
    public struct GridTopologyDisplayV2 : IGridTopologyDisplay, IInitializable
    {
        internal StructureVersion _Version;
        internal uint _DisplayId;
        internal int _OverlapX;
        internal int _OverlapY;
        internal Rotate _Rotation;
        internal uint _CloneGroup;
        internal PixelShiftType _PixelShiftType;

        public GridTopologyDisplayV2(uint displayId, int overlapX, int overlapY, Rotate rotation, uint cloneGroup = 0,
            PixelShiftType pixelShiftType = PixelShiftType.NoPixelShift) : this()
        {
            this = typeof(GridTopologyDisplayV2).Instantiate<GridTopologyDisplayV2>();
            _DisplayId = displayId;
            _OverlapX = overlapX;
            _OverlapY = overlapY;
            _Rotation = rotation;
            _CloneGroup = cloneGroup;
            _PixelShiftType = pixelShiftType;
        }

        public uint DisplayId => _DisplayId;
        public int OverlapX => _OverlapX;
        public int OverlapY => _OverlapY;
        public Rotate Rotation => _Rotation;
        public uint CloneGroup => _CloneGroup;
        public PixelShiftType PixelShiftType => _PixelShiftType;
    }
}