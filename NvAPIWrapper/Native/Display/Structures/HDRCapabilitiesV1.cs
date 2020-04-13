using NvAPIWrapper.Native.Attributes;
using NvAPIWrapper.Native.General.Structures;
using NvAPIWrapper.Native.Helpers;
using NvAPIWrapper.Native.Interfaces;
using NvAPIWrapper.Native.Interfaces.Display;
using System.Runtime.InteropServices;

namespace NvAPIWrapper.Native.Display.Structures
{
    /// <inheritdoc cref="IHDRCapabilities" />
    [StructLayout(LayoutKind.Sequential, Pack = 8)]
    [StructureVersion(1)]
    public struct HDRCapabilitiesV1 : IInitializable, IHDRCapabilities
    {
        internal StructureVersion _Version;
        internal uint _RawReserved;
        internal StaticMetadataDescriptorId _StaticMetadataDescriptorId;
        internal DisplayData _DisplayData;

        public HDRCapabilitiesV1(
            bool driverExpandDefaultHDRParameters,
            StaticMetadataDescriptorId staticMetadataDescriptorId,
            DisplayData displayData)
        {
            this = typeof(HDRCapabilitiesV1).Instantiate<HDRCapabilitiesV1>();
            DriverExpandDefaultHDRParameters = driverExpandDefaultHDRParameters;
            _StaticMetadataDescriptorId = staticMetadataDescriptorId;
            _DisplayData = displayData;
        }

        public HDRCapabilitiesV1(bool driverExpandDefaultHDRParameters, StaticMetadataDescriptorId staticMetadataDescriptorId)
            : this(driverExpandDefaultHDRParameters, staticMetadataDescriptorId, default)
        {
        }

        public HDRCapabilitiesV1(bool driverExpandDefaultHDRParameters)
            : this(driverExpandDefaultHDRParameters, StaticMetadataDescriptorId.StaticMetadataType1, default)
        {
        }

        /// <inheritdoc />
        public StaticMetadataDescriptorId StaticMetadataDescriptorId
        {
            get => _StaticMetadataDescriptorId;
        }

        /// <inheritdoc />
        public DisplayData DisplayData
        {
            get => _DisplayData;
        }

        /// <inheritdoc />
        public bool IsST2084EOTFSupported
        {
            get => _RawReserved.GetBit(0);
        }

        /// <inheritdoc />
        public bool IsTraditionalHDRGammaSupported
        {
            get => _RawReserved.GetBit(1);
        }

        /// <inheritdoc />
        public bool IsEDRSupported
        {
            get => _RawReserved.GetBit(2);
        }

        /// <inheritdoc />
        public bool DriverExpandDefaultHDRParameters
        {
            get => _RawReserved.GetBit(3);
            private set => _RawReserved = _RawReserved.SetBit(3, value);
        }

        /// <inheritdoc />
        public bool IsTraditionalSDRGammaSupported
        {
            get => _RawReserved.GetBit(4);
        }
    }
}
