using System.Runtime.InteropServices;
using NvAPIWrapper.Native.Attributes;
using NvAPIWrapper.Native.General.Structures;
using NvAPIWrapper.Native.Helpers;
using NvAPIWrapper.Native.Interfaces;

namespace NvAPIWrapper.Native.GPU.Structures
{
    [StructLayout(LayoutKind.Sequential, Pack = 8)]
    [StructureVersion(1)]
    public struct ECCStatusInfoV1 : IInitializable
    {
        internal StructureVersion _Version;
        internal uint _IsSupported;
        internal ECCConfiguration _ConfigurationOptions;
        internal uint _IsEnabled;

        public bool IsSupported
        {
            get => _IsSupported.GetBit(0);
        }

        public ECCConfiguration ConfigurationOptions
        {
            get => _ConfigurationOptions;
        }

        public bool IsEnabled
        {
            get => _IsEnabled.GetBit(0);
        }
    }
}