using System.Runtime.InteropServices;

namespace NvAPIWrapper.Native.Display.Structures
{
    [StructLayout(LayoutKind.Sequential, Pack = 8)]
    public struct LUID
    {
        public uint LowPart;
        public int HighPart;
    }
}