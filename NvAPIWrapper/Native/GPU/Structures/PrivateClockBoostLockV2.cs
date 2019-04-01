using System;
using System.Linq;
using System.Runtime.InteropServices;
using NvAPIWrapper.Native.Attributes;
using NvAPIWrapper.Native.General.Structures;
using NvAPIWrapper.Native.Helpers;
using NvAPIWrapper.Native.Interfaces;

namespace NvAPIWrapper.Native.GPU.Structures
{
    [StructLayout(LayoutKind.Sequential, Pack = 8)]
    [StructureVersion(2)]
    public struct PrivateClockBoostLockV2 : IInitializable
    {
        internal const int MaxNumberOfClocksPerGPU = ClockFrequenciesV1.MaxClocksPerGPU;

        internal StructureVersion _Version;
        internal uint _Unknown;
        internal uint _ClockBoostLocksCount;

        [MarshalAs(UnmanagedType.ByValArray, SizeConst = MaxNumberOfClocksPerGPU)]
        public ClockBoostLock[] _ClockBoostLocks;

        public ClockBoostLock[] ClockBoostLocks
        {
            get => _ClockBoostLocks.Take((int) _ClockBoostLocksCount).ToArray();
        }

        public PrivateClockBoostLockV2(ClockBoostLock[] clockBoostLocks)
        {
            if (clockBoostLocks?.Length > MaxNumberOfClocksPerGPU)
            {
                throw new ArgumentException($"Maximum of {MaxNumberOfClocksPerGPU} clocks are configurable.",
                    nameof(clockBoostLocks));
            }

            if (clockBoostLocks == null || clockBoostLocks.Length == 0)
            {
                throw new ArgumentException("Array is null or empty.", nameof(clockBoostLocks));
            }

            this = typeof(PrivateClockBoostLockV2).Instantiate<PrivateClockBoostLockV2>();
            _ClockBoostLocksCount = (uint) clockBoostLocks.Length;
            Array.Copy(clockBoostLocks, 0, _ClockBoostLocks, 0, clockBoostLocks.Length);
        }

        [StructLayout(LayoutKind.Sequential, Pack = 8)]
        public struct ClockBoostLock
        {
            internal PublicClockDomain _ClockDomain;
            internal uint _Unknown1;
            internal ClockLockMode _LockMode;
            internal uint _Unknown2;
            internal uint _VoltageInMicroV;
            internal uint _Unknown3;

            public PublicClockDomain ClockDomain
            {
                get => _ClockDomain;
            }

            public ClockLockMode LockMode
            {
                get => _LockMode;
            }

            public uint VoltageInMicroV
            {
                get => _VoltageInMicroV;
            }

            public ClockBoostLock(PublicClockDomain clockDomain, ClockLockMode lockMode, uint voltageInMicroV) : this()
            {
                _ClockDomain = clockDomain;
                _LockMode = lockMode;
                _VoltageInMicroV = voltageInMicroV;
            }
        }
    }
}