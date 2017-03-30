using System.Runtime.InteropServices;
using NvAPIWrapper.Native.Attributes;
using NvAPIWrapper.Native.General.Structures;
using NvAPIWrapper.Native.Helpers;
using NvAPIWrapper.Native.Interfaces;
using NvAPIWrapper.Native.Interfaces.Mosaic;

namespace NvAPIWrapper.Native.Mosaic.Structures
{
    [StructLayout(LayoutKind.Sequential, Pack = 8)]
    [StructureVersion(2)]
    public struct DisplaySettingsV2 : IDisplaySettings, IInitializable
    {
        internal StructureVersion _Version;
        internal uint _Width;
        internal uint _Height;
        internal uint _BitsPerPixel;
        internal uint _Frequency;
        internal uint _FrequencyInMillihertz;

        public DisplaySettingsV2(int width, int height, int bitsPerPixel, int frequency, uint frequencyInMillihertz)
        {
            this = typeof(DisplaySettingsV2).Instantiate<DisplaySettingsV2>();
            _Width = (uint) width;
            _Height = (uint) height;
            _BitsPerPixel = (uint) bitsPerPixel;
            _Frequency = (uint) frequency;
            _FrequencyInMillihertz = frequencyInMillihertz;
        }

        public DisplaySettingsV2(int width, int height, int bitsPerPixel, int frequency)
        {
            this = typeof(DisplaySettingsV2).Instantiate<DisplaySettingsV2>();
            _Width = (uint) width;
            _Height = (uint) height;
            _BitsPerPixel = (uint) bitsPerPixel;
            _Frequency = (uint) frequency;
            _FrequencyInMillihertz = _Frequency*1000;
        }

        public int Width => (int) _Width;
        public int Height => (int) _Height;
        public int BitsPerPixel => (int) _BitsPerPixel;
        public int Frequency => (int) _Frequency;
        public uint FrequencyInMillihertz => _FrequencyInMillihertz;
    }
}