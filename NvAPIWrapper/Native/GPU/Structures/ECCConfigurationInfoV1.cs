using System.Runtime.InteropServices;
using NvAPIWrapper.Native.Attributes;
using NvAPIWrapper.Native.General.Structures;
using NvAPIWrapper.Native.Helpers;
using NvAPIWrapper.Native.Interfaces;

namespace NvAPIWrapper.Native.GPU.Structures
{
    [StructLayout(LayoutKind.Sequential, Pack = 8)]
    [StructureVersion(1)]
    public struct ECCConfigurationInfoV1 : IInitializable
    {
        internal StructureVersion _Version;
        internal uint _Flags;

        public bool IsEnabled
        {
            get => _Flags.GetBit(0);
        }

        public bool IsEnabledByDefault
        {
            get => _Flags.GetBit(1);
        }
    }
}