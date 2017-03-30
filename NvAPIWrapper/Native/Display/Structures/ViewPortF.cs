using System.Runtime.InteropServices;

namespace NvAPIWrapper.Native.Display.Structures
{
    [StructLayout(LayoutKind.Sequential, Pack = 8)]
    internal struct ViewPortF
    {
        internal float _X;
        internal float _Y;
        internal float _Width;
        internal float _Height;

        public float X => _X;
        public float Y => _Y;
        public float Width => _Width;
        public float Height => _Height;
    }
}