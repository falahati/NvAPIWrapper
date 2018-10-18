using System.Runtime.InteropServices;
using NvAPIWrapper.Native.Attributes;
using NvAPIWrapper.Native.General.Structures;
using NvAPIWrapper.Native.Interfaces;
using NvAPIWrapper.Native.Interfaces.GPU;

namespace NvAPIWrapper.Native.GPU.Structures
{
    /// <summary>
    ///     Holds information about the system's display driver memory.
    /// </summary>
    [StructLayout(LayoutKind.Sequential, Pack = 8)]
    [StructureVersion(1)]
    public struct DisplayDriverMemoryInfoV1 : IInitializable, IDisplayDriverMemoryInfo
    {
        internal StructureVersion _Version;
        internal readonly uint _DedicatedVideoMemory;
        internal readonly uint _AvailableDedicatedVideoMemory;
        internal readonly uint _SystemVideoMemory;
        internal readonly uint _SharedSystemMemory;

        /// <inheritdoc />
        public uint DedicatedVideoMemory
        {
            get => _DedicatedVideoMemory;
        }

        /// <inheritdoc />
        public uint AvailableDedicatedVideoMemory
        {
            get => _AvailableDedicatedVideoMemory;
        }

        /// <inheritdoc />
        public uint SystemVideoMemory
        {
            get => _SystemVideoMemory;
        }

        /// <inheritdoc />
        public uint SharedSystemMemory
        {
            get => _SharedSystemMemory;
        }

        /// <inheritdoc />
        public uint CurrentAvailableDedicatedVideoMemory
        {
            get => _AvailableDedicatedVideoMemory;
        }

        /// <inheritdoc />
        public override string ToString()
        {
            return $"{AvailableDedicatedVideoMemory / 1024} MB / {DedicatedVideoMemory / 1024} MB";
        }
    }
}