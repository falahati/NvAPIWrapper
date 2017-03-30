using System.Runtime.InteropServices;

namespace NvAPIWrapper.Native.Display.Structures
{
    [StructLayout(LayoutKind.Sequential, Pack = 8)]
    public struct Timing
    {
        internal ushort _HorizontalVisible;
        internal ushort _HorizontalBorder;
        internal ushort _HorizontalFrontPorch;
        internal ushort _HorizontalSyncWidth;
        internal ushort _HorizontalTotal;
        internal TimingHorizontalSyncPolarity _HorizontalSyncPolarity;
        internal ushort _VerticalVisible;
        internal ushort _VerticalBorder;
        internal ushort _VerticalFrontPorch;
        internal ushort _VerticalSyncWidth;
        internal ushort _VerticalTotal;
        internal TimingVerticalSyncPolarity _VerticalSyncPolarity;
        internal TimingScanMode _ScanMode;
        internal uint _PixelClockIn10KHertz;
        internal TimingExtra _Extra;

        public int HorizontalVisible => _HorizontalVisible;
        public int HorizontalBorder => _HorizontalBorder;
        public int HorizontalFrontPorch => _HorizontalFrontPorch;
        public int HorizontalSyncWidth => _HorizontalSyncWidth;
        public int HorizontalTotal => _HorizontalTotal;
        public TimingHorizontalSyncPolarity HorizontalSyncPolarity => _HorizontalSyncPolarity;
        public int VerticalVisible => _VerticalVisible;
        public int VerticalBorder => _VerticalBorder;
        public int VerticalFrontPorch => _VerticalFrontPorch;
        public int VerticalSyncWidth => _VerticalSyncWidth;
        public int VerticalTotal => _VerticalTotal;
        public TimingVerticalSyncPolarity VerticalSyncPolarity => _VerticalSyncPolarity;
        public TimingScanMode ScanMode => _ScanMode;
        public int PixelClockIn10KHertz => (int) _PixelClockIn10KHertz;
        public TimingExtra Extra => _Extra;
    }
}