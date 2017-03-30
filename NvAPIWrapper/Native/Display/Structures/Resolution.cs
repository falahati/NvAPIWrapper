using System.Runtime.InteropServices;

namespace NvAPIWrapper.Native.Display.Structures
{
    [StructLayout(LayoutKind.Sequential, Pack = 8)]
    public struct Resolution
    {
        internal uint _Width;
        internal uint _Height;
        internal uint _ColorDepth;

        public Resolution(int width, int height, int colorDepth)
        {
            _Width = (uint) width;
            _Height = (uint) height;
            _ColorDepth = (uint) colorDepth;
        }

        public int Width => (int) _Width;
        public int Height => (int) _Height;
        public int ColorDepth => (int) _ColorDepth;
    }
}