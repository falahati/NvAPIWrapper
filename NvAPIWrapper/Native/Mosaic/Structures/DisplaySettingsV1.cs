using System.Runtime.InteropServices;
using NvAPIWrapper.Native.Attributes;
using NvAPIWrapper.Native.General.Structures;
using NvAPIWrapper.Native.Helpers;
using NvAPIWrapper.Native.Interfaces;
using NvAPIWrapper.Native.Interfaces.Mosaic;

namespace NvAPIWrapper.Native.Mosaic.Structures
{
    [StructLayout(LayoutKind.Sequential, Pack = 8)]
    [StructureVersion(1)]
    public struct DisplaySettingsV1 : IDisplaySettings, IInitializable
    {
        internal StructureVersion _Version;
        internal uint _Width;
        internal uint _Height;
        internal uint _BitsPerPixel;
        internal uint _Frequency;

        public DisplaySettingsV1(int width, int height, int bitsPerPixel, int frequency)
        {
            this = typeof(DisplaySettingsV1).Instantiate<DisplaySettingsV1>();
            _Width = (uint) width;
            _Height = (uint) height;
            _BitsPerPixel = (uint) bitsPerPixel;
            _Frequency = (uint) frequency;
        }

        public int Width => (int) _Width;
        public int Height => (int) _Height;
        public int BitsPerPixel => (int) _BitsPerPixel;
        public int Frequency => (int) _Frequency;
        public uint FrequencyInMillihertz => _Frequency*1000;
    }
}