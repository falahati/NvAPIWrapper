using System.Runtime.InteropServices;
using NvAPIWrapper.Native.Attributes;
using NvAPIWrapper.Native.General.Structures;
using NvAPIWrapper.Native.Helpers;
using NvAPIWrapper.Native.Interfaces;
using NvAPIWrapper.Native.Interfaces.GPU;

namespace NvAPIWrapper.Native.GPU.Structures
{
    [StructLayout(LayoutKind.Sequential, Pack = 8)]
    [StructureVersion(3)]
    public struct DisplayIdsV2 : IInitializable, IDisplayIds
    {
        internal StructureVersion _Version;
        internal MonitorConnectionType _ConnectionType;
        internal uint _DisplayId;
        internal uint _RawReserved;

        public uint DisplayId => _DisplayId;
        public MonitorConnectionType ConnectionType => _ConnectionType;
        public bool IsDynamic => _RawReserved.GetBit(0);
        public bool IsMultiStreamRootNode => _RawReserved.GetBit(1);
        public bool IsActive => _RawReserved.GetBit(2);
        public bool IsCluster => _RawReserved.GetBit(3);
        public bool IsOSVisible => _RawReserved.GetBit(4);
        public bool IsWFD => _RawReserved.GetBit(5);
        public bool IsConnected => _RawReserved.GetBit(6);
        public bool IsPhysicallyConnected => _RawReserved.GetBit(17);
    }
}