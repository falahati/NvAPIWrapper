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
    [StructureVersion(3)]
    public struct ColorDataV3 : IInitializable, IColorData
    {
        internal StructureVersion _Version;
        internal ushort _Size;
        internal ColorCommand _ColorCommand;
        internal ColorDataBagV3 _Data;

        public ColorDataV3(
            ColorCommand colorCommand,
            ColorDataBagV3 data = default)
        {
            this = typeof(ColorDataV3).Instantiate<ColorDataV3>();
            _Size = (ushort)_Version.StructureSize;
            _ColorCommand = colorCommand;
            _Data = data;
        }

        public ColorDataV3(ColorCommand colorCommand) : this(colorCommand, default)
        {
        }

        public ColorCommand ColorCommand
        {
            get => _ColorCommand;
            set => _ColorCommand = value;
        }

        public ColorDataBagV3 Data
        {
            get => _Data;
            set => _Data = value;
        }
    }
}
