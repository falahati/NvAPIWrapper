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
        public ushort DisplayPrimaryX0
        {
            get => _DisplayPrimaryX0;
        }

        /// <summary>
        /// y coordinate of color primary 0 (e.g. Red) of the display ([0x0000-0xC350] = [0.0 - 1.0])
        /// </summary>
        public ushort DisplayPrimaryY0
        {
            get => _DisplayPrimaryY0;
        }

        /// <summary>
        /// x coordinate of color primary 1 (e.g. Green) of the display ([0x0000-0xC350] = [0.0 - 1.0])
        /// </summary>
        public ushort DisplayPrimaryX1
        {
            get => _DisplayPrimaryX1;
        }

        /// <summary>
        /// y coordinate of color primary 1 (e.g. Green) of the display ([0x0000-0xC350] = [0.0 - 1.0])
        /// </summary>
        public ushort DisplayPrimaryY1
        {
            get => _DisplayPrimaryY1;
        }

        /// <summary>
        /// x coordinate of color primary 2 (e.g. Blue) of the display ([0x0000-0xC350] = [0.0 - 1.0])
        /// </summary>
        public ushort DisplayPrimaryX2
        {
            get => _DisplayPrimaryX2;
        }

        /// <summary>
        /// y coordinate of color primary 2 (e.g. Blue) of the display ([0x0000-0xC350] = [0.0 - 1.0])
        /// </summary>
        public ushort DisplayPrimaryY2
        {
            get => _DisplayPrimaryY2;
        }

        /// <summary>
        /// x coordinate of white point of the display ([0x0000-0xC350] = [0.0 - 1.0])
        /// </summary>
        public ushort DisplayWhitePointX
        {
            get => _DisplayWhitePointX;
        }

        /// <summary>
        /// y coordinate of white point of the display ([0x0000-0xC350] = [0.0 - 1.0])
        /// </summary>
        public ushort DisplayWhitePointY
        {
            get => _DisplayWhitePointY;
        }

        /// <summary>
        /// Maximum display luminance = desired max luminance of HDR content ([0x0001-0xFFFF] = [1.0 - 65535.0] cd/m^2)
        /// </summary>
        public ushort DesiredContentMaxLuminance
        {
            get => _DesiredContentMaxLuminance;
        }

        /// <summary>
        /// Minimum display luminance = desired min luminance of HDR content ([0x0001-0xFFFF] = [1.0 - 6.55350] cd/m^2)
        /// </summary>
        public ushort DesiredContentMinLuminance
        {
            get => _DesiredContentMinLuminance;
        }

        /// <summary>
        /// Desired maximum Frame-Average Light Level (MaxFALL) of HDR content ([0x0001-0xFFFF] = [1.0 - 65535.0] cd/m^2)
        /// </summary>
        public ushort DesiredContentMaxFrameAverageLuminance
        {
            get => _DesiredContentMaxFrameAverageLuminance;
        }
    }
}
