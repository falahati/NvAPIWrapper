using System.Runtime.InteropServices;
using NvAPIWrapper.Native.Attributes;
using NvAPIWrapper.Native.General.Structures;
using NvAPIWrapper.Native.Helpers;
using NvAPIWrapper.Native.Interfaces;

namespace NvAPIWrapper.Native.GPU.Structures
{
    [StructLayout(LayoutKind.Sequential, Pack = 8)]
    [StructureVersion(1)]
    public struct PrivateVoltageBoostPercentV1 : IInitializable
    {
        internal const int MaxNumberOfUnknown = 8;

        internal StructureVersion _Version;

        internal readonly uint _Percent;

        [MarshalAs(UnmanagedType.ByValArray, SizeConst = MaxNumberOfUnknown)]
        internal readonly uint[] _Unknown;

        public uint Percent
        {
            get => _Percent;
        }

        public PrivateVoltageBoostPercentV1(uint percent)
        {
            this = typeof(PrivateVoltageBoostPercentV1).Instantiate<PrivateVoltageBoostPercentV1>();
            _Percent = percent;
        }
    }
}