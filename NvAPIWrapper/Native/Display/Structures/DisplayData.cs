using System.Runtime.InteropServices;

namespace NvAPIWrapper.Native.Display.Structures
{
    [StructLayout(LayoutKind.Sequential, Pack = 8)]
    public struct DisplayData
    {
        internal readonly ushort _DisplayPrimaryX0;
        internal readonly ushort _DisplayPrimaryY0;
        internal readonly ushort _DisplayPrimaryX1;
        internal readonly ushort _DisplayPrimaryY1;
        internal readonly ushort _DisplayPrimaryX2;
        internal readonly ushort _DisplayPrimaryY2;
        internal readonly ushort _DisplayWhitePointX;
        internal readonly ushort _DisplayWhitePointY;
        internal readonly ushort _DesiredContentMaxLuminance;
        internal readonly ushort _DesiredContentMinLuminance;
        internal readonly ushort _DesiredContentMaxFrameAverageLuminance;


        /// <summary>
        /// x coordinate of color primary 0 (e.g. Red) of the display ([0x0000-0xC350] = [0.0 - 1.0])
        /// </summary>
        public float DisplayPrimaryX0
        {
            get => (float)_DisplayPrimaryX0 / 0xC350;
        }

        /// <summary>
        /// y coordinate of color primary 0 (e.g. Red) of the display ([0x0000-0xC350] = [0.0 - 1.0])
        /// </summary>
        public float DisplayPrimaryY0
        {
            get => (float)_DisplayPrimaryY0 / 0xC350;
        }

        /// <summary>
        /// x coordinate of color primary 1 (e.g. Green) of the display ([0x0000-0xC350] = [0.0 - 1.0])
        /// </summary>
        public float DisplayPrimaryX1
        {
            get => (float)_DisplayPrimaryX1 / 0xC350;
        }

        /// <summary>
        /// y coordinate of color primary 1 (e.g. Green) of the display ([0x0000-0xC350] = [0.0 - 1.0])
        /// </summary>
        public float DisplayPrimaryY1
        {
            get => (float)_DisplayPrimaryY1 / 0xC350;
        }

        /// <summary>
        /// x coordinate of color primary 2 (e.g. Blue) of the display ([0x0000-0xC350] = [0.0 - 1.0])
        /// </summary>
        public float DisplayPrimaryX2
        {
            get => (float)_DisplayPrimaryX2 / 0xC350;
        }

        /// <summary>
        /// y coordinate of color primary 2 (e.g. Blue) of the display ([0x0000-0xC350] = [0.0 - 1.0])
        /// </summary>
        public float DisplayPrimaryY2
        {
            get => (float)_DisplayPrimaryY2 / 0xC350;
        }

        /// <summary>
        /// x coordinate of white point of the display ([0x0000-0xC350] = [0.0 - 1.0])
        /// </summary>
        public float DisplayWhitePointX
        {
            get => (float)_DisplayWhitePointX / 0xC350;
        }

        /// <summary>
        /// y coordinate of white point of the display ([0x0000-0xC350] = [0.0 - 1.0])
        /// </summary>
        public float DisplayWhitePointY
        {
            get => (float)_DisplayWhitePointY / 0xC350;
        }

        /// <summary>
        /// Maximum display luminance = desired max luminance of HDR content ([0x0001-0xFFFF] = [1.0 - 65535.0] cd/m^2)
        /// </summary>
        public float DesiredContentMaxLuminance
        {
            get => (float)_DesiredContentMaxLuminance / 0xC350;
        }

        /// <summary>
        /// Minimum display luminance = desired min luminance of HDR content ([0x0001-0xFFFF] = [1.0 - 6.55350] cd/m^2)
        /// </summary>
        public float DesiredContentMinLuminance
        {
            get => (float)_DesiredContentMinLuminance / 0xC350;
        }

        /// <summary>
        /// Desired maximum Frame-Average Light Level (MaxFALL) of HDR content ([0x0001-0xFFFF] = [1.0 - 65535.0] cd/m^2)
        /// </summary>
        public float DesiredContentMaxFrameAverageLuminance
        {
            get => (float)_DesiredContentMaxFrameAverageLuminance / 0xC350;
        }
    }
}
