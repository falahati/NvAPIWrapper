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
    [StructureVersion(2)]
    public struct HDRColorDataV2 : IInitializable, IHDRColorData
    {
        internal StructureVersion _Version;
        internal HDRCommand _HDRCommand;
        internal HDRMode _HDRMode;
        internal StaticMetadataDescriptorId _StaticMetadataDescriptorId;
        internal MasteringDisplayData _MasteringDisplayData;
        internal ColorFormat _ColorFormat;
        internal HDRDynamicRange _HDRDynamicRange;
        internal BPC _BPC;

        public HDRColorDataV2(
            HDRCommand hdrCommand,
            HDRMode hdrMode = HDRMode.Off,
            StaticMetadataDescriptorId staticMetadataDescriptorId = StaticMetadataDescriptorId.StaticMetadataType1,
            MasteringDisplayData masteringDisplayData = default,
            ColorFormat colorFormat = ColorFormat.Auto,
            HDRDynamicRange hdrDynamicRange = HDRDynamicRange.Auto,
            BPC bpc = BPC.Default)
        {
            this = typeof(HDRColorDataV2).Instantiate<HDRColorDataV2>();
            _HDRCommand = hdrCommand;
            _HDRMode = hdrMode;
            _StaticMetadataDescriptorId = staticMetadataDescriptorId;
            _MasteringDisplayData = masteringDisplayData;
            _ColorFormat = colorFormat;
            _HDRDynamicRange = hdrDynamicRange;
            _BPC = bpc;
        }

        public HDRColorDataV2(HDRCommand hdrCommand) : this(hdrCommand, HDRMode.Off)
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

        /// <inheritdoc />
        public ColorFormat ColorFormat
        {
            get => _ColorFormat;
            set => _ColorFormat = value;
        }

        /// <summary>
        ///     Optional, One of HDRDynamicRange enum values, if set it will apply requested dynamic range for HDR session
        /// </summary>
        public HDRDynamicRange HDRDynamicRange
        {
            get => _HDRDynamicRange;
            set => _HDRDynamicRange = value;
        }

        /// <summary>
        ///     Optional, One of BPC enum values, if set it will apply requested color depth
        /// </summary>
        public BPC BPC
        {
            get => _BPC;
            set => _BPC = value;
        }
    }
}
