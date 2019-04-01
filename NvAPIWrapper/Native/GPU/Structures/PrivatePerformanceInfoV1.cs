using System.Runtime.InteropServices;
using NvAPIWrapper.Native.Attributes;
using NvAPIWrapper.Native.General.Structures;
using NvAPIWrapper.Native.Helpers;
using NvAPIWrapper.Native.Interfaces;

namespace NvAPIWrapper.Native.GPU.Structures
{
    [StructLayout(LayoutKind.Sequential, Pack = 8)]
    [StructureVersion(1)]
    public struct PrivatePerformanceInfoV1 : IInitializable
    {
        internal const int MaxNumberOfUnknown2 = 16;

        internal StructureVersion _Version;
        internal uint _Unknown1;
        internal PerformanceLimit _SupportedLimits;

        [MarshalAs(UnmanagedType.ByValArray, SizeConst = MaxNumberOfUnknown2)]
        internal uint[] _Unknown2;

        public bool IsPowerLimitSupported
        {
            get => _SupportedLimits.GetBit(0);
        }

        public bool IsTemperatureLimitSupported
        {
            get => _SupportedLimits.GetBit(1);
        }

        public bool IsVoltageLimitSupported
        {
            get => _SupportedLimits.GetBit(2);
        }

        public bool IsNoLoadLimitSupported
        {
            get => _SupportedLimits.GetBit(4);
        }
    }
}