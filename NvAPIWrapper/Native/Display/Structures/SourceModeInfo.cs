using System.Runtime.InteropServices;
using NvAPIWrapper.Native.Helpers;

namespace NvAPIWrapper.Native.Display.Structures
{
    [StructLayout(LayoutKind.Sequential, Pack = 8)]
    public struct SourceModeInfo
    {
        internal Resolution _Resolution;
        internal Format _ColorFormat;
        internal Position _Position;
        internal SpanningOrientation _SpanningOrientation;
        internal uint _RawReserved;

        public SourceModeInfo(Resolution resolution, Format colorFormat, Position position = default(Position),
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

        public Resolution Resolution => _Resolution;
        public Format ColorFormat => _ColorFormat;
        public Position Position => _Position;
        public SpanningOrientation SpanningOrientation => _SpanningOrientation;

        public bool IsGDIPrimary
        {
            get { return _RawReserved.GetBit(0); }
            private set { _RawReserved = _RawReserved.SetBit(0, value); }
        }

        public bool IsSLIFocus
        {
            get { return _RawReserved.GetBit(1); }
            private set { _RawReserved = _RawReserved.SetBit(1, value); }
        }
    }
}