using System.Runtime.InteropServices;
using NvAPIWrapper.Native.Interfaces;

namespace NvAPIWrapper.Native.Display.Structures
{
    [StructLayout(LayoutKind.Sequential, Pack = 8)]
    public struct TimingExtra : IInitializable
    {
        internal uint _HardwareFlags;
        internal ushort _RefreshRate;
        internal uint _FrequencyInMillihertz;
        internal ushort _VerticalAspect;
        internal ushort _HorizontalAspect;
        internal ushort _PixelRepetition;
        internal uint _Status;
        [MarshalAs(UnmanagedType.ByValTStr, SizeConst = 40)] internal string _Name;

        public uint HardwareFlags => _HardwareFlags;
        public int RefreshRate => _RefreshRate;
        public int FrequencyInMillihertz => (int) _FrequencyInMillihertz;
        public int VerticalAspect => _VerticalAspect;
        public int HorizontalAspect => _HorizontalAspect;
        public int PixelRepetition => _PixelRepetition;
        public uint Status => _Status;
        public string Name => _Name;
    }
}