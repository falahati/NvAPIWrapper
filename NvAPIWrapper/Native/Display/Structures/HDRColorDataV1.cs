using NvAPIWrapper.Native.Attributes;
using NvAPIWrapper.Native.General.Structures;
using NvAPIWrapper.Native.Helpers;
using NvAPIWrapper.Native.Interfaces;
using NvAPIWrapper.Native.Interfaces.Display;
using System.Runtime.InteropServices;

namespace NvAPIWrapper.Native.Display.Structures
{
    /// <inheritdoc cref="IHDRColorData" />
    [StructLayout(LayoutKind.Sequential, Pack = 8)]
    [StructureVersion(1)]
    public struct HDRColorDataV1 : IInitializable, IHDRColorData
    {
        internal StructureVersion _Version;
        internal HDRCommand _HDRCommand;
        internal HDRMode _HDRMode;
        internal StaticMetadataDescriptorId _StaticMetadataDescriptorId;
        internal MasteringDisplayData _MasteringDisplayData;

        public HDRColorDataV1(
            HDRCommand hdrCommand,
            HDRMode hdrMode,
            StaticMetadataDescriptorId staticMetadataDescriptorId,
            MasteringDisplayData masteringDisplayData)
        {
            this = typeof(HDRColorDataV1).Instantiate<HDRColorDataV1>();
            _HDRCommand = hdrCommand;
            _HDRMode = hdrMode;
            _StaticMetadataDescriptorId = staticMetadataDescriptorId;
            _MasteringDisplayData = masteringDisplayData;
        }

        public HDRColorDataV1(HDRCommand hdrCommand, HDRMode hdrMode, StaticMetadataDescriptorId staticMetadataDescriptorId)
            : this(hdrCommand, hdrMode, staticMetadataDescriptorId, default)
        {
        }

        public HDRColorDataV1(HDRCommand hdrCommand, HDRMode hdrMode)
            : this(hdrCommand, hdrMode, StaticMetadataDescriptorId.StaticMetadataType1, default)
        {
        }

        public HDRColorDataV1(HDRCommand hdrCommand)
            : this(hdrCommand, HDRMode.Off, StaticMetadataDescriptorId.StaticMetadataType1, default)
        {
        }

        /// <inheritdoc />
        public HDRCommand HDRCommand
        {
            get => _HDRCommand;
            set => _HDRCommand = value;
        }

        /// <inheritdoc />
        public HDRMode HDRMode
        {
            get => _HDRMode;
            set => _HDRMode = value;
        }

        /// <inheritdoc />
        public StaticMetadataDescriptorId StaticMetadataDescriptorId
        {
            get => _StaticMetadataDescriptorId;
        }

        /// <inheritdoc />
        public MasteringDisplayData MasteringDisplayData
        {
            get => _MasteringDisplayData;
            set => _MasteringDisplayData = value;
        }
    }
}
