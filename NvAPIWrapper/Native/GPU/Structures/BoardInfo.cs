using System.Runtime.InteropServices;
using NvAPIWrapper.Native.Attributes;
using NvAPIWrapper.Native.General.Structures;
using NvAPIWrapper.Native.Interfaces;

namespace NvAPIWrapper.Native.GPU.Structures
{
    [StructLayout(LayoutKind.Sequential, Pack = 8)]
    [StructureVersion(1)]
    public struct BoardInfo : IInitializable
    {
        internal StructureVersion _Version;
        [MarshalAs(UnmanagedType.ByValArray, SizeConst = 16)] internal byte[] _SerialNumber;

        public byte[] SerialNumber => _SerialNumber;
    }
}