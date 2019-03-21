using System;

namespace NvAPIWrapper.GPU
{
    /// <summary>
    ///     Represents an integer value range
    /// </summary>
    public class GPUPerformanceStateValueRange : IEquatable<GPUPerformanceStateValueRange>
    {
        /// <summary>
        ///     Creates a new instance of <see cref="GPUPerformanceStateValueRange" />.
        /// </summary>
        /// <param name="min">The lower bound of the range.</param>
        /// <param name="max">The upper bound of the range.</param>
        public GPUPerformanceStateValueRange(long min, long max)
        {
            Minimum = min;
            Maximum = max;
        }

        /// <summary>
        ///     Creates a new single value instance of <see cref="GPUPerformanceStateValueRange" />.
        /// </summary>
        /// <param name="value">The only value in the range</param>
        public GPUPerformanceStateValueRange(long value)
        {
            Minimum = value;
            Maximum = value;
        }

        /// <summary>
        ///     Gets the upper bound of the inclusive range
        /// </summary>
        public long Maximum { get; }

        /// <summary>
        ///     Gets the lower bound of the inclusive range
        /// </summary>
        public long Minimum { get; }

        /// <inheritdoc />
        public bool Equals(GPUPerformanceStateValueRange other)
        {
            if (other == null)
            {
                return false;
            }

            if (ReferenceEquals(this, other))
            {
                return true;
            }

            return Maximum == other.Maximum && Minimum == other.Minimum;
        }

        public static bool operator ==(GPUPerformanceStateValueRange left, GPUPerformanceStateValueRange right)
        {
            return Equals(left, right) || left?.Equals(right) == true;
        }

        public static bool operator !=(GPUPerformanceStateValueRange left, GPUPerformanceStateValueRange right)
        {
            return !(left == right);
        }

        /// <inheritdoc />
        public override bool Equals(object obj)
        {
            if (obj == null)
            {
                return false;
            }

            if (ReferenceEquals(this, obj))
            {
                return true;
            }

            return Equals(obj as GPUPerformanceStateValueRange);
        }

        /// <inheritdoc />
        public override int GetHashCode()
        {
            unchecked
            {
                return ((int) Maximum * 397) ^ (int) Minimum;
            }
        }

        /// <inheritdoc />
        public override string ToString()
        {
            if (Minimum == Maximum)
            {
                return $"({Minimum})";
            }

            return $"[({Minimum}) - ({Maximum})]";
        }
    }
}