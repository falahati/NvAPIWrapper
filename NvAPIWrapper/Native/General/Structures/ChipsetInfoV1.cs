using System;
using System.Runtime.InteropServices;
using NvAPIWrapper.Native.Attributes;
using NvAPIWrapper.Native.Interfaces;
using NvAPIWrapper.Native.Interfaces.General;

namespace NvAPIWrapper.Native.General.Structures
{
    /// <summary>
    ///     Holds information about the system's chipset.
    /// </summary>
    [StructLayout(LayoutKind.Sequential, Pack = 8)]
    [StructureVersion(1)]
    public struct ChipsetInfoV1 : IInitializable, IChipsetInfo, IEquatable<ChipsetInfoV1>
    {
        internal StructureVersion _Version;
        internal readonly uint _VendorId;
        internal readonly uint _DeviceId;
        internal ShortString _VendorName;
        internal ShortString _ChipsetName;

        /// <inheritdoc />
        public bool Equals(ChipsetInfoV1 other)
        {
            return (_VendorId == other._VendorId) && (_DeviceId == other._DeviceId) &&
                   _VendorName.Equals(other._VendorName) && _ChipsetName.Equals(other._ChipsetName);
        }

        /// <inheritdoc />
        public override bool Equals(object obj)
        {
            if (ReferenceEquals(null, obj)) return false;
            return obj is ChipsetInfoV1 && Equals((ChipsetInfoV1) obj);
        }

        /// <inheritdoc />
        public override int GetHashCode()
        {
            unchecked
            {
                var hashCode = (int) _VendorId;
                hashCode = (hashCode*397) ^ (int) _DeviceId;
                hashCode = (hashCode*397) ^ _VendorName.GetHashCode();
                hashCode = (hashCode*397) ^ _ChipsetName.GetHashCode();
                return hashCode;
            }
        }

        /// <inheritdoc />
        public override string ToString()
        {
            return $"{VendorName} {ChipsetName}";
        }

        /// <inheritdoc />
        public int VendorId => (int) _VendorId;

        /// <inheritdoc />
        public int DeviceId => (int) _DeviceId;

        /// <inheritdoc />
        public string VendorName => _VendorName.Value;

        /// <inheritdoc />
        public string ChipsetName => _ChipsetName.Value;

        /// <inheritdoc />
        public ChipsetInfoFlag Flags => ChipsetInfoFlag.None;
    }
}