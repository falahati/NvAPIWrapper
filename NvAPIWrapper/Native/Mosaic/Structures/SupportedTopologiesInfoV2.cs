using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using NvAPIWrapper.Native.Attributes;
using NvAPIWrapper.Native.General.Structures;
using NvAPIWrapper.Native.Interfaces;
using NvAPIWrapper.Native.Interfaces.Mosaic;

namespace NvAPIWrapper.Native.Mosaic.Structures
{
    [StructLayout(LayoutKind.Sequential, Pack = 8)]
    [StructureVersion(2)]
    public struct SupportedTopologiesInfoV2 : ISupportedTopologiesInfo, IInitializable
    {
        public const int MaxSettings = 40;

        internal StructureVersion _Version;
        internal uint _TopologyBriefsCount;

        [MarshalAs(UnmanagedType.ByValArray, SizeConst = (int) Topology.Max)] internal TopologyBrief[]
            _TopologyBriefs;

        internal uint _DisplaySettingsCount;

        [MarshalAs(UnmanagedType.ByValArray, SizeConst = MaxSettings)] internal DisplaySettingsV2[]
            _DisplaySettings;

        public IEnumerable<TopologyBrief> TopologyBriefs => _TopologyBriefs.Take((int) _TopologyBriefsCount);

        public IEnumerable<IDisplaySettings> DisplaySettings
            => _DisplaySettings.Take((int) _DisplaySettingsCount).Cast<IDisplaySettings>();
    }
}