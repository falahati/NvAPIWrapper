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
    [StructureVersion(4)]
    public struct ColorDataV4 : IInitializable, IColorData
    {
        internal StructureVersion _Version;
        internal ushort _Size;
        internal ColorCommand _ColorCommand;
        internal ColorDataBagV4 _Data;

        public ColorDataV4(
            ColorCommand colorCommand,
            ColorDataBagV4 data = default)
        {
            this = typeof(ColorDataV4).Instantiate<ColorDataV4>();
            _Size = (ushort)_Version.StructureSize;
            _ColorCommand = colorCommand;
            _Data = data;
        }

        public ColorDataV4(ColorCommand colorCommand) : this(colorCommand, default)
        {
        }

        public ColorCommand ColorCommand
        {
            get => _ColorCommand;
            set => _ColorCommand = value;
        }

        public ColorDataBagV4 Data
        {
            get => _Data;
            set => _Data = value;
        }
    }
}
