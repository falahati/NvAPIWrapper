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
    [StructureVersion(1)]
    public struct PrivatePowerPoliciesStatusV1 : IInitializable
    {
        internal const int MaxNumberOfPowerPoliciesStatusEntries = 4;

        internal StructureVersion _Version;
        internal readonly uint _PowerPoliciesStatusEntriesCount;

        [MarshalAs(UnmanagedType.ByValArray, SizeConst = MaxNumberOfPowerPoliciesStatusEntries,
            ArraySubType = UnmanagedType.Struct)]
        internal readonly PowerPolicyStatusEntry[] _PowerPoliciesStatusEntries;

        public PowerPolicyStatusEntry[] PowerPolicyStatusEntries
        {
            get => _PowerPoliciesStatusEntries.Take((int) _PowerPoliciesStatusEntriesCount).ToArray();
        }

        public PrivatePowerPoliciesStatusV1(PowerPolicyStatusEntry[] powerPoliciesStatusEntries)
        {
            if (powerPoliciesStatusEntries?.Length > MaxNumberOfPowerPoliciesStatusEntries)
            {
                throw new ArgumentException(
                    $"Maximum of {MaxNumberOfPowerPoliciesStatusEntries} power policies entries are configurable.",
                    nameof(powerPoliciesStatusEntries)
                );
            }

            if (powerPoliciesStatusEntries == null || powerPoliciesStatusEntries.Length == 0)
            {
                throw new ArgumentException("Array is null or empty.", nameof(powerPoliciesStatusEntries));
            }

            this = typeof(PrivatePowerPoliciesStatusV1).Instantiate<PrivatePowerPoliciesStatusV1>();
            _PowerPoliciesStatusEntriesCount = (uint) powerPoliciesStatusEntries.Length;
            Array.Copy(
                powerPoliciesStatusEntries,
                0,
                _PowerPoliciesStatusEntries,
                0,
                powerPoliciesStatusEntries.Length
            );
        }

        [StructLayout(LayoutKind.Sequential, Pack = 8)]
        public struct PowerPolicyStatusEntry
        {
            internal uint _Unknown;
            internal uint _Unknown2;
            internal uint _Power;
            internal uint _Unknown3;

            public PowerPolicyStatusEntry(uint power) : this()
            {
                _Power = power;
            }

            public uint Power
            {
                get => _Power;
            }
        }
    }
}