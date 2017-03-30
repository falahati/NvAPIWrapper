using System.Runtime.InteropServices;

namespace NvAPIWrapper.Native.Display.Structures
{
    [StructLayout(LayoutKind.Sequential, Pack = 8)]
    public struct Position
    {
        internal int _X;
        internal int _Y;

        public Position(int x, int y)
        {
            _X = x;
            _Y = y;
        }

        public int X => _X;
        public int Y => _Y;
    }
}