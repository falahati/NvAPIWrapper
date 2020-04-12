using System.Runtime.InteropServices;

namespace NvAPIWrapper.Native.Display.Structures
{
    [StructLayout(LayoutKind.Sequential, Pack = 8)]
    public struct MasteringDisplayData
    {
        internal readonly ushort _DisplayPrimaryX0;
        internal readonly ushort _DisplayPrimaryY0;
        internal readonly ushort _DisplayPrimaryX1;
        internal readonly ushort _DisplayPrimaryY1;
        internal readonly ushort _DisplayPrimaryX2;
        internal readonly ushort _DisplayPrimaryY2;
        internal readonly ushort _DisplayWhitePointX;
        internal readonly ushort _DisplayWhitePointY;
        internal readonly ushort _MaxDisplayMasteringLuminance;
        internal readonly ushort _MinDisplayMasteringLuminance;
        internal readonly ushort _MaxContentLightLevel;
        internal readonly ushort _MaxFrameAverageLightLevel;

        /// <summary>
        ///    coordinate of color primary 0 (e.g. Red) of mastering display ([0x0000-0xC350] = [0.0 - 1.0])
        ///</summary>
        public ushort DisplayPrimaryX0
        {
            get => _DisplayPrimaryX0;
        }

        /// <summary>
        ///     coordinate of color primary 0 (e.g. Red) of mastering display ([0x0000-0xC350] = [0.0 - 1.0])
        ///</summary>
        public ushort DisplayPrimaryY0
        {
            get => _DisplayPrimaryY0;
        }

        /// <summary>
        ///    coordinate of color primary 1 (e.g. Green) of mastering display ([0x0000-0xC350] = [0.0 - 1.0])
        ///</summary>
        public ushort DisplayPrimaryX1
        {
            get => _DisplayPrimaryX1;
        }

        /// <summary>
        ///     coordinate of color primary 1 (e.g. Green) of mastering display ([0x0000-0xC350] = [0.0 - 1.0])
        ///</summary>
        public ushort DisplayPrimaryY1
        {
            get => _DisplayPrimaryY1;
        }

        /// <summary>
        ///    coordinate of color primary 2 (e.g. Blue) of mastering display ([0x0000-0xC350] = [0.0 - 1.0])
        ///</summary>
        public ushort DisplayPrimaryX2
        {
            get => _DisplayPrimaryX2;
        }

        /// <summary>
        ///     coordinate of color primary 2 (e.g. Blue) of mastering display ([0x0000-0xC350] = [0.0 - 1.0])
        ///</summary>
        public ushort DisplayPrimaryY2
        {
            get => _DisplayPrimaryY2;
        }

        /// <summary>
        ///    coordinate of white point of mastering display ([0x0000-0xC350] = [0.0 - 1.0])
        ///</summary>
        public ushort DisplayWhitePointX
        {
            get => _DisplayWhitePointX;
        }

        /// <summary>
        ///     coordinate of white point of mastering display ([0x0000-0xC350] = [0.0 - 1.0])
        ///</summary>
        public ushort DisplayWhitePointY
        {
            get => _DisplayWhitePointY;
        }

        /// <summary>
        ///     Maximum display mastering luminance ([0x0001-0xFFFF] = [1.0 - 65535.0] cd/m^2)
        ///</summary>
        public ushort MaxDisplayMasteringLuminance
        {
            get => _MaxDisplayMasteringLuminance;
        }

        /// <summary>
        ///     Minimum display mastering luminance ([0x0001-0xFFFF] = [1.0 - 6.55350] cd/m^2)
        ///</summary>
        public ushort MinDisplayMasteringLuminance
        {
            get => _MinDisplayMasteringLuminance;
        }

        /// <summary>
        ///     Maximum Content Light level (MaxCLL) ([0x0001-0xFFFF] = [1.0 - 65535.0] cd/m^2)
        ///</summary>
        public ushort MaxContentLightLevel
        {
            get => _MaxContentLightLevel;
        }

        /// <summary>
        ///     Maximum Frame-Average Light Level (MaxFALL) ([0x0001-0xFFFF] = [1.0 - 65535.0] cd/m^2)
        ///</summary>
        public ushort MaxFrameAverageLightLevel
        {
            get => _MaxFrameAverageLightLevel;
        }
    }
}
