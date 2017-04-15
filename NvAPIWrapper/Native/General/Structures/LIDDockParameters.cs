using System;
using System.Runtime.InteropServices;
using NvAPIWrapper.Native.Attributes;
using NvAPIWrapper.Native.Interfaces;

namespace NvAPIWrapper.Native.General.Structures
{
    /// <summary>
    ///     Holds information about the lid and dock
    /// </summary>
    [StructLayout(LayoutKind.Sequential, Pack = 8)]
    [StructureVersion(1)]
    public struct LidDockParameters : IInitializable, IEquatable<LidDockParameters>
    {
        internal StructureVersion _Version;
        internal readonly uint _CurrentLIDState;
        internal readonly uint _CurrentDockState;
        internal readonly uint _CurrentLIDPolicy;
        internal readonly uint _CurrentDockPolicy;
        internal readonly uint _ForcedLIDMechanismPresent;
        internal readonly uint _ForcedDockMechanismPresent;

        /// <inheritdoc />
        public bool Equals(LidDockParameters other)
        {
            return (_CurrentLIDState == other._CurrentLIDState) && (_CurrentDockState == other._CurrentDockState) &&
                   (_CurrentLIDPolicy == other._CurrentLIDPolicy) && (_CurrentDockPolicy == other._CurrentDockPolicy) &&
                   (_ForcedLIDMechanismPresent == other._ForcedLIDMechanismPresent) &&
                   (_ForcedDockMechanismPresent == other._ForcedDockMechanismPresent);
        }

        /// <inheritdoc />
        public override bool Equals(object obj)
        {
            if (ReferenceEquals(null, obj)) return false;
            return obj is LidDockParameters && Equals((LidDockParameters) obj);
        }

        /// <inheritdoc />
        public override int GetHashCode()
        {
            unchecked
            {
                var hashCode = (int) _CurrentLIDState;
                hashCode = (hashCode*397) ^ (int) _CurrentDockState;
                hashCode = (hashCode*397) ^ (int) _CurrentLIDPolicy;
                hashCode = (hashCode*397) ^ (int) _CurrentDockPolicy;
                hashCode = (hashCode*397) ^ (int) _ForcedLIDMechanismPresent;
                hashCode = (hashCode*397) ^ (int) _ForcedDockMechanismPresent;
                return hashCode;
            }
        }

        /// <summary>
        ///     Gets current lid state
        /// </summary>
        public uint CurrentLidState => _CurrentLIDState;

        /// <summary>
        ///     Gets current dock state
        /// </summary>
        public uint CurrentDockState => _CurrentDockState;

        /// <summary>
        ///     Gets current lid policy
        /// </summary>
        public uint CurrentLidPolicy => _CurrentLIDPolicy;

        /// <summary>
        ///     Gets current dock policy
        /// </summary>
        public uint CurrentDockPolicy => _CurrentDockPolicy;

        /// <summary>
        ///     Gets forced lid mechanism present
        /// </summary>
        public uint ForcedLidMechanismPresent => _ForcedLIDMechanismPresent;

        /// <summary>
        ///     Gets forced dock mechanism present
        /// </summary>
        public uint ForcedDockMechanismPresent => _ForcedDockMechanismPresent;
    }
}