using System.Runtime.InteropServices;
using NvAPIWrapper.Native.Attributes;
using NvAPIWrapper.Native.General.Structures;
using NvAPIWrapper.Native.Interfaces;

namespace NvAPIWrapper.Native.GPU.Structures
{
    /// <summary>
    ///     Contains information regarding GPU thermal status
    /// </summary>
    [StructLayout(LayoutKind.Sequential, Pack = 1)]
    [StructureVersion(2)]
    public struct PrivateThermalExV2 : IInitializable
    {
        internal StructureVersion _Version;

        public uint Mask;

        [MarshalAs(UnmanagedType.ByValArray, SizeConst = 33)]
        public byte[] Unknown1;

        [MarshalAs(UnmanagedType.ByValArray, SizeConst = 10)]
        public int[] Temperatures;

        [MarshalAs(UnmanagedType.ByValArray, SizeConst = 87)]
        public byte[] Unknown2;
    }
}