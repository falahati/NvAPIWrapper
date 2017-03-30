using System.Runtime.InteropServices;
using NvAPIWrapper.Native.Display;
using NvAPIWrapper.Native.Interfaces.Mosaic;

namespace NvAPIWrapper.Native.Mosaic.Structures
{
    [StructLayout(LayoutKind.Sequential, Pack = 8)]
    public struct GridTopologyDisplayV1 : IGridTopologyDisplay
    {
        internal uint _DisplayId;
        internal int _OverlapX;
        internal int _OverlapY;
        internal Rotate _Rotation;
        internal uint _CloneGroup;

        public GridTopologyDisplayV1(uint displayId, int overlapX, int overlapY, Rotate rotation, uint cloneGroup = 0) : this()
        {
            _DisplayId = displayId;
            _OverlapX = overlapX;
            _OverlapY = overlapY;
            _Rotation = rotation;
            _CloneGroup = cloneGroup;
        }

        public uint DisplayId => _DisplayId;
        public int OverlapX => _OverlapX;
        public int OverlapY => _OverlapY;
        public Rotate Rotation => _Rotation;
        public uint CloneGroup =>  _CloneGroup;
    }
}