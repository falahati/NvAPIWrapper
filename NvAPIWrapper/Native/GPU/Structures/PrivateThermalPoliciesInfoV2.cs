using System.Linq;
using System.Runtime.InteropServices;
using NvAPIWrapper.Native.Attributes;
using NvAPIWrapper.Native.General.Structures;
using NvAPIWrapper.Native.Interfaces;

namespace NvAPIWrapper.Native.GPU.Structures
{
    [StructLayout(LayoutKind.Sequential, Pack = 8)]
    [StructureVersion(2)]
    public struct PrivateThermalPoliciesInfoV2 : IInitializable
    {
        internal const int MaxNumberOfThermalPoliciesInfoEntries = 4;

        internal StructureVersion _Version;
        internal readonly byte _ThermalPoliciesInfoCount;
        internal readonly byte _Unknown;

        [MarshalAs(UnmanagedType.ByValArray, SizeConst = MaxNumberOfThermalPoliciesInfoEntries,
            ArraySubType = UnmanagedType.Struct)]
        internal readonly ThermalPoliciesInfoEntry[] _ThermalPoliciesInfoEntries;

        public ThermalPoliciesInfoEntry[] ThermalPoliciesInfoEntries
        {
            get => _ThermalPoliciesInfoEntries.Take(_ThermalPoliciesInfoCount).ToArray();
        }

        [StructLayout(LayoutKind.Sequential, Pack = 8)]
        public struct ThermalPoliciesInfoEntry
        {
            internal ThermalSettingsController _Controller;
            internal uint _Unknown1;
            internal int _MinimumTemperature;
            internal int _DefaultTemperature;
            internal int _MaximumTemperature;
            internal uint _Unknown2;

            public ThermalSettingsController ThermalController
            {
                get => _Controller;
            }

            public int MinimumTemperature
            {
                get => _MinimumTemperature;
            }

            public int DefaultTemperature
            {
                get => _DefaultTemperature;
            }

            public int MaximumTemperature
            {
                get => _MaximumTemperature;
            }
        }
    }
}