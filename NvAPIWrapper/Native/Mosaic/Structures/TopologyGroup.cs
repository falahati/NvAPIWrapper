using System.Linq;
using System.Runtime.InteropServices;
using NvAPIWrapper.Native.Attributes;
using NvAPIWrapper.Native.General.Structures;
using NvAPIWrapper.Native.Interfaces;

namespace NvAPIWrapper.Native.Mosaic.Structures
{
    [StructLayout(LayoutKind.Sequential, Pack = 8)]
    [StructureVersion(1)]
    public struct TopologyGroup : IInitializable
    {
        public const int MaxTopologyPerGroup = 2;

        internal StructureVersion _Version;
        internal TopologyBrief _Brief;
        internal uint _TopologiesCount;

        [MarshalAs(UnmanagedType.ByValArray, SizeConst = MaxTopologyPerGroup)] internal TopologyDetails[]
            _TopologyDetails;

        public TopologyBrief Brief => _Brief;
        public TopologyDetails[] TopologyDetails => _TopologyDetails.Take((int) _TopologiesCount).ToArray();
    }
}