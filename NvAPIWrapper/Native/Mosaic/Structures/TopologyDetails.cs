using System.Linq;
using System.Runtime.InteropServices;
using NvAPIWrapper.Native.Attributes;
using NvAPIWrapper.Native.General.Structures;
using NvAPIWrapper.Native.GPU;
using NvAPIWrapper.Native.GPU.Structures;
using NvAPIWrapper.Native.Interfaces;

namespace NvAPIWrapper.Native.Mosaic.Structures
{
    [StructLayout(LayoutKind.Sequential, Pack = 8)]
    [StructureVersion(1)]
    public struct TopologyDetails : IInitializable
    {
        public const int MaxLayoutRows = 8;
        public const int MaxLayoutColumns = 8;

        internal StructureVersion _Version;
        internal LogicalGPUHandle _LogicalGPUHandle;
        internal TopologyValidity _ValidityFlags;
        internal uint _Rows;
        internal uint _Columns;
        [MarshalAs(UnmanagedType.ByValArray, SizeConst = MaxLayoutRows)] internal LayoutRow[] _LayoutRows;

        public LogicalGPUHandle LogicalGPUHandle => _LogicalGPUHandle;
        public TopologyValidity ValidityFlags => _ValidityFlags;
        public int Rows => (int) _Rows;
        public int Columns => (int) _Columns;

        public LayoutCell[][] Layout
        {
            get
            {
                var columns = (int) _Columns;
                return _LayoutRows.Take((int) _Rows).Select(row => row.LayoutCells.Take(columns).ToArray()).ToArray();
            }
        }

        [StructLayout(LayoutKind.Sequential)]
        internal struct LayoutRow : IInitializable
        {
            [MarshalAs(UnmanagedType.ByValArray, SizeConst = MaxLayoutColumns)] internal LayoutCell[] LayoutCells;
        }

        [StructLayout(LayoutKind.Sequential)]
        public struct LayoutCell
        {
            internal PhysicalGPUHandle _PhysicalGPUHandle;
            internal OutputId _DisplayOutputId;
            internal int _OverlapX;
            internal int _OverlapY;

            public PhysicalGPUHandle PhysicalGPUHandle => _PhysicalGPUHandle;
            public OutputId DisplayOutputId => _DisplayOutputId;
            public int OverlapX => _OverlapX;
            public int OverlapY => _OverlapY;
        }
    }
}