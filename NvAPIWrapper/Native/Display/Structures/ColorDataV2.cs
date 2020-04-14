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
    [StructureVersion(2)]
    public struct ColorDataV2 : IInitializable, IColorData
    {
        internal StructureVersion _Version;
        internal ushort _Size;
        internal ColorCommand _ColorCommand;
        internal ColorDataBagV2 _Data;

        public ColorDataV2(
            ColorCommand colorCommand,
            ColorDataBagV2 data)
        {
            this = typeof(ColorDataV2).Instantiate<ColorDataV2>();
            _Size = (ushort)_Version.StructureSize;
            _ColorCommand = colorCommand;
            _Data = data;
        }

        public ColorDataV2(ColorCommand colorCommand) : this(colorCommand, default)
        {
        }

        public ColorCommand ColorCommand
        {
            get => _ColorCommand;
            set => _ColorCommand = value;
        }

        public ColorDataBagV2 Data
        {
            get => _Data;
            set => _Data = value;
        }
    }
}
