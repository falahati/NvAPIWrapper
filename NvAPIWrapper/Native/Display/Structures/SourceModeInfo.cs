using System;
using System.Runtime.InteropServices;
using NvAPIWrapper.Native.Helpers;

namespace NvAPIWrapper.Native.Display.Structures
{
    /// <summary>
    ///     Holds information about a source mode
    /// </summary>
    [StructLayout(LayoutKind.Sequential, Pack = 8)]
    public struct SourceModeInfo : IEquatable<SourceModeInfo>
    {
        internal Resolution _Resolution;
        internal readonly ColorFormat _ColorFormat;
        internal Position _Position;
        internal readonly SpanningOrientation _SpanningOrientation;
        internal uint _RawReserved;

        /// <summary>
        ///     Creates a new SourceModeInfo
        /// </summary>
        /// <param name="resolution">Source resolution</param>
        /// <param name="colorFormat">Must be Format.Unknown</param>
        /// <param name="position">Source position</param>
        /// <param name="spanningOrientation">Spanning orientation for XP</param>
        /// <param name="isGDIPrimary">true if this source represents the GDI primary display, otherwise false</param>
        /// <param name="isSLIFocus">true if this source represents the SLI focus display, otherwise false</param>
        public SourceModeInfo(Resolution resolution, ColorFormat colorFormat, Position position = default(Position),
            SpanningOrientation spanningOrientation = SpanningOrientation.None, bool isGDIPrimary = false,
            bool isSLIFocus = false) : this()
        {
            _Resolution = resolution;
            _ColorFormat = colorFormat;
            _Position = position;
            _SpanningOrientation = spanningOrientation;
            IsGDIPrimary = isGDIPrimary;
            IsSLIFocus = isSLIFocus;
        }

        /// <inheritdoc />
        public bool Equals(SourceModeInfo other)
        {
            return _Resolution.Equals(other._Resolution) && (_ColorFormat == other._ColorFormat) &&
                   _Position.Equals(other._Position) && (_SpanningOrientation == other._SpanningOrientation) &&
                   (_RawReserved == other._RawReserved);
        }

        /// <inheritdoc />
        public override bool Equals(object obj)
        {
            if (ReferenceEquals(null, obj)) return false;
            return obj is SourceModeInfo && Equals((SourceModeInfo) obj);
        }

        /// <inheritdoc />
        public override int GetHashCode()
        {
            unchecked
            {
                var hashCode = _Resolution.GetHashCode();
                hashCode = (hashCode*397) ^ (int) _ColorFormat;
                hashCode = (hashCode*397) ^ _Position.GetHashCode();
                hashCode = (hashCode*397) ^ (int) _SpanningOrientation;
                hashCode = (hashCode*397) ^ (int) _RawReserved;
                return hashCode;
            }
        }

        /// <inheritdoc />
        public override string ToString()
        {
            return $"{Resolution} @ {Position} - {ColorFormat}";
        }

        /// <summary>
        ///     Holds the source resolution
        /// </summary>
        public Resolution Resolution => _Resolution;

        /// <summary>
        ///     Ignored at present, must be Format.Unknown
        /// </summary>
        public ColorFormat ColorFormat => _ColorFormat;

        /// <summary>
        ///     Is all positions are 0 or invalid, displays will be automatically positioned from left to right with GDI Primary at
        ///     0,0, and all other displays in the order of the path array.
        /// </summary>
        public Position Position => _Position;

        /// <summary>
        ///     Spanning is only supported on XP
        /// </summary>
        public SpanningOrientation SpanningOrientation => _SpanningOrientation;

        /// <summary>
        ///     Indicates if the path is for the primary GDI display
        /// </summary>
        public bool IsGDIPrimary
        {
            get { return _RawReserved.GetBit(0); }
            private set { _RawReserved = _RawReserved.SetBit(0, value); }
        }

        /// <summary>
        ///     Indicates if the path is for the SLI focus display
        /// </summary>
        public bool IsSLIFocus
        {
            get { return _RawReserved.GetBit(1); }
            private set { _RawReserved = _RawReserved.SetBit(1, value); }
        }
    }
}