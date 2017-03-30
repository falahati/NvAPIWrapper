using System.Linq;
using System.Runtime.InteropServices;
using NvAPIWrapper.Native.Attributes;
using NvAPIWrapper.Native.General.Structures;
using NvAPIWrapper.Native.Helpers;
using NvAPIWrapper.Native.Interfaces;

namespace NvAPIWrapper.Native.Display.Structures
{
    [StructLayout(LayoutKind.Sequential, Pack = 8)]
    [StructureVersion(2)]
    internal struct ViewTargetInfo : IInitializable
    {
        public const int MaxViewTargets = 2;

        internal StructureVersion _Version;
        internal uint _Count;
        [MarshalAs(UnmanagedType.ByValArray, SizeConst = MaxViewTargets)] internal ViewTarget[] _ViewTargets;

        public ViewTarget[] ViewTargets => _ViewTargets.Take((int) _Count).ToArray();

        [StructLayout(LayoutKind.Sequential, Pack = 8)]
        public struct ViewTarget
        {
            internal uint _DeviceMask;
            internal uint _SourceId;
            internal uint _RawBits;

            public uint DeviceMask => _DeviceMask;
            public uint SourceId => _SourceId;
            public bool IsPrimary => _RawBits.GetBit(0);
            public bool IsInterlaced => _RawBits.GetBit(1);
            public bool IsGDIPrimary => _RawBits.GetBit(2);
            public bool ForceToSetMode => _RawBits.GetBit(3);
        }
    }
}