using NvAPIWrapper.Native.Attributes;
using NvAPIWrapper.Native.General.Structures;
using NvAPIWrapper.Native.Helpers;
using NvAPIWrapper.Native.Interfaces;
using NvAPIWrapper.Native.Interfaces.Display;
using System.Runtime.InteropServices;

namespace NvAPIWrapper.Native.Display.Structures
{
    /// <inheritdoc cref="IColorData" />
    [StructLayout(LayoutKind.Sequential, Pack = 8)]
    [StructureVersion(5)]
    public struct ColorDataV5 : IInitializable, IColorData
    {
        internal StructureVersion _Version;
        internal ushort _Size;
        internal ColorCommand _ColorCommand;
        internal ColorDataBagV5 _Data;

        public ColorDataV5(
            ColorCommand colorCommand,
            ColorDataBagV5 data = default)
        {
            this = typeof(ColorDataV5).Instantiate<ColorDataV5>();
            _Size = (ushort)_Version.StructureSize;
            _ColorCommand = colorCommand;
            _Data = data;
        }

        public ColorDataV5(ColorCommand colorCommand) : this(colorCommand, default)
        {
        }

        public ushort Size
        {
            get => _Size;
        }

        public ColorCommand ColorCommand
        {
            get => _ColorCommand;
            set => _ColorCommand = value;
        }

        public ColorDataBagV5 Data
        {
            get => _Data;
            set => _Data = value;
        }
    }
}
