using System;
using System.Runtime.InteropServices;
using NvAPIWrapper.Native.Attributes;
using NvAPIWrapper.Native.General.Structures;
using NvAPIWrapper.Native.Helpers;
using NvAPIWrapper.Native.Interfaces;

namespace NvAPIWrapper.Native.GPU.Structures
{
    [StructLayout(LayoutKind.Sequential, Pack = 8)]
    [StructureVersion(1)]
    public struct PrivateClockBoostTableV1 : IInitializable
    {
        internal const int MaxNumberOfMasks = 4;
        internal const int MaxNumberOfUnknown1 = 12;
        internal const int MaxNumberOfGPUDeltas = 80;
        internal const int MaxNumberOfMemoryFilled = 23;
        internal const int MaxNumberOfMemoryDeltas = 23;
        internal const int MaxNumberOfUnknown2 = 1529;

        internal StructureVersion _Version;


        [MarshalAs(UnmanagedType.ByValArray, SizeConst = MaxNumberOfMasks)]
        internal uint[] _Masks;

        [MarshalAs(UnmanagedType.ByValArray, SizeConst = MaxNumberOfUnknown1)]
        internal uint[] _Unknown1;

        [MarshalAs(UnmanagedType.ByValArray, SizeConst = MaxNumberOfGPUDeltas)]
        internal GPUDelta[] _GPUDeltas;

        [MarshalAs(UnmanagedType.ByValArray, SizeConst = MaxNumberOfMemoryFilled)]
        internal uint[] _MemoryFilled;

        [MarshalAs(UnmanagedType.ByValArray, SizeConst = MaxNumberOfMemoryDeltas)]
        internal int[] _MemoryDeltas;

        [MarshalAs(UnmanagedType.ByValArray, SizeConst = MaxNumberOfUnknown2)]
        internal uint[] _Unknown2;

        public uint[] Masks
        {
            get => _Masks;
        }

        public uint[] MemoryFilled
        {
            get => _MemoryFilled;
        }

        public int[] MemoryDeltas
        {
            get => _MemoryDeltas;
        }

        public GPUDelta[] GPUDeltas
        {
            get => _GPUDeltas;
        }

        // ReSharper disable once TooManyDependencies
        public PrivateClockBoostTableV1(uint[] masks, GPUDelta[] gpuDeltas, uint[] memoryFilled, int[] memoryDeltas)
        {
            if (masks?.Length > MaxNumberOfMasks)
            {
                throw new ArgumentException($"Maximum of {MaxNumberOfMasks} masks are configurable.",
                    nameof(masks));
            }

            if (masks == null)
            {
                throw new ArgumentNullException(nameof(masks));
            }

            if (gpuDeltas?.Length > MaxNumberOfGPUDeltas)
            {
                throw new ArgumentException($"Maximum of {MaxNumberOfGPUDeltas} GPU delta values are configurable.",
                    nameof(gpuDeltas));
            }

            if (gpuDeltas == null)
            {
                throw new ArgumentNullException(nameof(gpuDeltas));
            }

            if (memoryFilled?.Length > MaxNumberOfMemoryFilled)
            {
                throw new ArgumentException($"Maximum of {MaxNumberOfMemoryFilled} memory filled values are configurable.",
                    nameof(memoryFilled));
            }

            if (memoryFilled == null)
            {
                throw new ArgumentNullException(nameof(memoryFilled));
            }

            if (memoryDeltas?.Length > MaxNumberOfMemoryDeltas)
            {
                throw new ArgumentException($"Maximum of {MaxNumberOfMemoryDeltas} memory delta values are configurable.",
                    nameof(memoryDeltas));
            }

            if (memoryDeltas == null)
            {
                throw new ArgumentNullException(nameof(memoryDeltas));
            }

            this = typeof(PrivateClockBoostTableV1).Instantiate<PrivateClockBoostTableV1>();

            Array.Copy(masks, 0, _Masks, 0, masks.Length);
            Array.Copy(gpuDeltas, 0, _GPUDeltas, 0, gpuDeltas.Length);
            Array.Copy(memoryFilled, 0, _MemoryFilled, 0, memoryFilled.Length);
            Array.Copy(memoryDeltas, 0, _MemoryDeltas, 0, memoryDeltas.Length);
        }

        [StructLayout(LayoutKind.Sequential, Pack = 8)]
        public struct GPUDelta
        {
            internal uint _Unknown1;
            internal uint _Unknown2;
            internal uint _Unknown3;
            internal uint _Unknown4;
            internal uint _Unknown5;
            internal int _FrequencyDeltaInkHz;
            internal uint _Unknown7;
            internal uint _Unknown8;
            internal uint _Unknown9;

            public int FrequencyDeltaInkHz
            {
                get => _FrequencyDeltaInkHz;
            }

            public GPUDelta(int frequencyDeltaInkHz) : this()
            {
                _FrequencyDeltaInkHz = frequencyDeltaInkHz;
            }
        }
    }
}