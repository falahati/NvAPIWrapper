using System;
using System.Runtime.InteropServices;
using NvAPIWrapper.Native.Attributes;
using NvAPIWrapper.Native.General.Structures;
using NvAPIWrapper.Native.Helpers;
using NvAPIWrapper.Native.Interfaces;
using NvAPIWrapper.Native.Interfaces.GPU;

namespace NvAPIWrapper.Native.GPU.Structures
{
    /// <summary>
    ///     Represents a display identification and its attributes
    /// </summary>
    [StructLayout(LayoutKind.Sequential, Pack = 8)]
    [StructureVersion(3)]
    public struct DisplayIdsV2 : IInitializable, IDisplayIds, IEquatable<DisplayIdsV2>
    {
        internal StructureVersion _Version;
        internal readonly MonitorConnectionType _ConnectionType;
        internal readonly uint _DisplayId;
        internal readonly uint _RawReserved;

        /// <inheritdoc />
        public uint DisplayId => _DisplayId;

        /// <inheritdoc />
        public bool Equals(DisplayIdsV2 other)
        {
            return _DisplayId == other._DisplayId;
        }

        /// <inheritdoc />
        public override bool Equals(object obj)
        {
            if (ReferenceEquals(null, obj)) return false;
            return obj is DisplayIdsV2 && Equals((DisplayIdsV2) obj);
        }

        /// <inheritdoc />
        public override int GetHashCode()
        {
            return (int) _DisplayId;
        }

        /// <inheritdoc />
        public MonitorConnectionType ConnectionType => _ConnectionType;

        /// <inheritdoc />
        public bool IsDynamic => _RawReserved.GetBit(0);

        /// <inheritdoc />
        public bool IsMultiStreamRootNode => _RawReserved.GetBit(1);

        /// <inheritdoc />
        public bool IsActive => _RawReserved.GetBit(2);

        /// <inheritdoc />
        public bool IsCluster => _RawReserved.GetBit(3);

        /// <inheritdoc />
        public bool IsOSVisible => _RawReserved.GetBit(4);

        /// <inheritdoc />
        public bool IsWFD => _RawReserved.GetBit(5);

        /// <inheritdoc />
        public bool IsConnected => _RawReserved.GetBit(6);

        /// <inheritdoc />
        public bool IsPhysicallyConnected => _RawReserved.GetBit(17);
    }
}