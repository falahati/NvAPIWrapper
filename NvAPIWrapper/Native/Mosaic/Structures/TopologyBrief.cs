using System.Runtime.InteropServices;
using NvAPIWrapper.Native.Attributes;
using NvAPIWrapper.Native.General.Structures;
using NvAPIWrapper.Native.Helpers;
using NvAPIWrapper.Native.Interfaces;

namespace NvAPIWrapper.Native.Mosaic.Structures
{
    [StructLayout(LayoutKind.Sequential, Pack = 8)]
    [StructureVersion(1)]
    public struct TopologyBrief : IInitializable
    {
        internal StructureVersion _Version;
        internal Topology _Topology;
        internal uint _IsEnable;
        internal uint _IsPossible;

        public TopologyBrief(Topology topology)
        {
            this = typeof(TopologyBrief).Instantiate<TopologyBrief>();
            _Topology = topology;
        }

        public Topology Topology => _Topology;
        public bool IsEnable => _IsEnable > 0;
        public bool IsPossible => _IsPossible > 0;
    }
}