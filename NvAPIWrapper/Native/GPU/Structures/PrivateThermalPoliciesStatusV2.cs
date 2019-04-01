using System;
using System.Runtime.InteropServices;
using NvAPIWrapper.Native.Attributes;
using NvAPIWrapper.Native.General.Structures;
using NvAPIWrapper.Native.Helpers;
using NvAPIWrapper.Native.Interfaces;

namespace NvAPIWrapper.Native.GPU.Structures
{
    [StructLayout(LayoutKind.Sequential, Pack = 8)]
    [StructureVersion(2)]
    public struct PrivateThermalPoliciesStatusV2 : IInitializable
    {
        internal const int MaxNumberOfThermalPoliciesStatusEntries = 4;

        internal StructureVersion _Version;
        internal readonly uint _Unknown;

        [MarshalAs(UnmanagedType.ByValArray, SizeConst = MaxNumberOfThermalPoliciesStatusEntries)]
        internal readonly ThermalPoliciesStatusEntry[] _ThermalPoliciesStatusEntries;

        public ThermalPoliciesStatusEntry[] ThermalPoliciesStatusEntries
        {
            get => _ThermalPoliciesStatusEntries;
        }

        public PrivateThermalPoliciesStatusV2(ThermalPoliciesStatusEntry[] policiesStatusEntries)
        {
            if (policiesStatusEntries?.Length > MaxNumberOfThermalPoliciesStatusEntries)
            {
                throw new ArgumentException(
                    $"Maximum of {MaxNumberOfThermalPoliciesStatusEntries} thermal policies entries are configurable.",
                    nameof(policiesStatusEntries)
                );
            }

            if (policiesStatusEntries == null || policiesStatusEntries.Length == 0)
            {
                throw new ArgumentException("Array is null or empty.", nameof(policiesStatusEntries));
            }

            this = typeof(PrivateThermalPoliciesStatusV2).Instantiate<PrivateThermalPoliciesStatusV2>();
            Array.Copy(
                policiesStatusEntries,
                0,
                _ThermalPoliciesStatusEntries,
                0,
                policiesStatusEntries.Length
            );
        }

        [StructLayout(LayoutKind.Sequential, Pack = 8)]
        public struct ThermalPoliciesStatusEntry
        {
            internal ThermalSettingsController _Controller;
            internal int _Value;
            internal uint _Unknown;

            public ThermalPoliciesStatusEntry(ThermalSettingsController controller, int value) : this()
            {
                _Controller = controller;
                _Value = value;
            }
        }
    }
}