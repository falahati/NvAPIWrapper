using System.Runtime.InteropServices;
using NvAPIWrapper.Native.Attributes;
using NvAPIWrapper.Native.Interfaces;

namespace NvAPIWrapper.Native.General.Structures
{
    [StructLayout(LayoutKind.Sequential, Pack = 8)]
    [StructureVersion(1)]
    public struct LIDDockParameters : IInitializable
    {
        internal StructureVersion _Version;
        internal uint _CurrentLIDState;
        internal uint _CurrentDockState;
        internal uint _CurrentLIDPolicy;
        internal uint _CurrentDockPolicy;
        internal uint _ForcedLIDMechanismPresent;
        internal uint _ForcedDockMechanismPresent;

        public uint CurrentLIDState => _CurrentLIDState;
        public uint CurrentDockState => _CurrentDockState;
        public uint CurrentLIDPolicy => _CurrentLIDPolicy;
        public uint CurrentDockPolicy => _CurrentDockPolicy;
        public uint ForcedLIDMechanismPresent => _ForcedLIDMechanismPresent;
        public uint ForcedDockMechanismPresent => _ForcedDockMechanismPresent;
    }
}