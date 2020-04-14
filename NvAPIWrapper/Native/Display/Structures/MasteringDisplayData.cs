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
        public float DisplayPrimaryX0
        {
            get => (float)_DisplayPrimaryX0 / 0xC350;
        }

        /// <summary>
        ///     coordinate of color primary 0 (e.g. Red) of mastering display ([0x0000-0xC350] = [0.0 - 1.0])
        ///</summary>
        public float DisplayPrimaryY0
        {
            get => (float)_DisplayPrimaryY0 / 0xC350;
        }

        /// <summary>
        ///    coordinate of color primary 1 (e.g. Green) of mastering display ([0x0000-0xC350] = [0.0 - 1.0])
        ///</summary>
        public float DisplayPrimaryX1
        {
            get => (float)_DisplayPrimaryX1 / 0xC350;
        }

        /// <summary>
        ///     coordinate of color primary 1 (e.g. Green) of mastering display ([0x0000-0xC350] = [0.0 - 1.0])
        ///</summary>
        public float DisplayPrimaryY1
        {
            get => (float)_DisplayPrimaryY1 / 0xC350;
        }

        /// <summary>
        ///    coordinate of color primary 2 (e.g. Blue) of mastering display ([0x0000-0xC350] = [0.0 - 1.0])
        ///</summary>
        public float DisplayPrimaryX2
        {
            get => (float)_DisplayPrimaryX2 / 0xC350;
        }

        /// <summary>
        ///     coordinate of color primary 2 (e.g. Blue) of mastering display ([0x0000-0xC350] = [0.0 - 1.0])
        ///</summary>
        public float DisplayPrimaryY2
        {
            get => (float)_DisplayPrimaryY2 / 0xC350;
        }

        /// <summary>
        ///    coordinate of white point of mastering display ([0x0000-0xC350] = [0.0 - 1.0])
        ///</summary>
        public float DisplayWhitePointX
        {
            get => (float)_DisplayWhitePointX / 0xC350;
        }

        /// <summary>
        ///     coordinate of white point of mastering display ([0x0000-0xC350] = [0.0 - 1.0])
        ///</summary>
        public float DisplayWhitePointY
        {
            get => (float)_DisplayWhitePointY / 0xC350;
        }

        /// <summary>
        ///     Maximum display mastering luminance ([0x0001-0xFFFF] = [1.0 - 65535.0] cd/m^2)
        ///</summary>
        public float MaxDisplayMasteringLuminance
        {
            get => (float)_MaxDisplayMasteringLuminance / 0xC350;
        }

        /// <summary>
        ///     Minimum display mastering luminance ([0x0001-0xFFFF] = [1.0 - 6.55350] cd/m^2)
        ///</summary>
        public float MinDisplayMasteringLuminance
        {
            get => (float)_MinDisplayMasteringLuminance / 0xC350;
        }

        /// <summary>
        ///     Maximum Content Light level (MaxCLL) ([0x0001-0xFFFF] = [1.0 - 65535.0] cd/m^2)
        ///</summary>
        public float MaxContentLightLevel
        {
            get => (float)_MaxContentLightLevel / 0xC350;
        }

        /// <summary>
        ///     Maximum Frame-Average Light Level (MaxFALL) ([0x0001-0xFFFF] = [1.0 - 65535.0] cd/m^2)
        ///</summary>
        public float MaxFrameAverageLightLevel
        {
            get => (float)_MaxFrameAverageLightLevel / 0xC350;
        }
    }
}
