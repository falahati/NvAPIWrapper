using System;
using NvAPIWrapper.Native.GPU;

namespace NvAPIWrapper.GPU
{
    /// <summary>
    ///     Contains information about the GPU bus
    /// </summary>
    public struct GPUBus : IEquatable<GPUBus>
    {
        internal GPUBus(int busId, int busSlot, GPUBusType busType)
        {
            BusId = busId;
            BusSlot = busSlot;
            BusType = busType;
        }

        /// <inheritdoc />
        public override bool Equals(object obj)
        {
            if (ReferenceEquals(null, obj))
            {
                return false;
            }

            return obj is GPUBus && Equals((GPUBus) obj);
        }

        /// <inheritdoc />
        public bool Equals(GPUBus other)
        {
            return BusId == other.BusId && BusSlot == other.BusSlot && BusType == other.BusType;
        }

        /// <inheritdoc />
        public override int GetHashCode()
        {
            unchecked
            {
                var hashCode = BusId;
                hashCode = (hashCode * 397) ^ BusSlot;
                hashCode = (hashCode * 397) ^ (int) BusType;

                return hashCode;
            }
        }

        /// <summary>
        ///     Checks for equality between two objects of same type
        /// </summary>
        /// <param name="left">The first object</param>
        /// <param name="right">The second object</param>
        /// <returns>true, if both objects are equal, otherwise false</returns>
        public static bool operator ==(GPUBus left, GPUBus right)
        {
            return left.Equals(right);
        }

        /// <summary>
        ///     Checks for inequality between two objects of same type
        /// </summary>
        /// <param name="left">The first object</param>
        /// <param name="right">The second object</param>
        /// <returns>true, if both objects are not equal, otherwise false</returns>
        public static bool operator !=(GPUBus left, GPUBus right)
        {
            return !left.Equals(right);
        }


        /// <inheritdoc />
        public override string ToString()
        {
            return $"{BusType} Bus #{BusId}, Slot #{BusSlot}";
        }

        /// <summary>
        ///     Gets the bus identification
        /// </summary>
        public int BusId { get; }

        /// <summary>
        ///     Gets the bus slot identification
        /// </summary>
        public int BusSlot { get; }

        /// <summary>
        ///     Gets the the bus type
        /// </summary>
        public GPUBusType BusType { get; }
    }
}