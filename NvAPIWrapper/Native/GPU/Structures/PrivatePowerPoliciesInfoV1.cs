using System.Linq;
using System.Runtime.InteropServices;
using NvAPIWrapper.Native.Attributes;
using NvAPIWrapper.Native.General.Structures;
using NvAPIWrapper.Native.Interfaces;

namespace NvAPIWrapper.Native.GPU.Structures
{
    [StructLayout(LayoutKind.Sequential, Pack = 8)]
    [StructureVersion(1)]
    public struct PrivatePowerPoliciesInfoV1 : IInitializable
    {
        internal const int MaxNumberOfPowerPolicyInfoEntries = 4;

        internal StructureVersion _Version;
        internal readonly byte _Valid;
        internal readonly byte _PowerPolicyEntriesCount;

        [MarshalAs(UnmanagedType.ByValArray, SizeConst = MaxNumberOfPowerPolicyInfoEntries)]
        internal readonly PowerPolicyInfoEntry[] _PowerPolicyInfoEntries;

        public PowerPolicyInfoEntry[] PowerPolicyInfoEntries
        {
            get => _PowerPolicyInfoEntries.Take(_PowerPolicyEntriesCount).ToArray();
        }

        [StructLayout(LayoutKind.Sequential, Pack = 8)]
        public struct PowerPolicyInfoEntry
        {
            internal PerformanceStateId _StateId;
            internal uint _Unknown1;
            internal uint _Unknown2;
            internal uint _MinimumPower;
            internal uint _Unknown3;
            internal uint _Unknown4;
            internal uint _DefaultPower;
            internal uint _Unknown5;
            internal uint _Unknown6;
            internal uint _MaximumPower;
            internal uint _Unknown7;

            public PerformanceStateId PerformanceStateId
            {
                get => _StateId;
            }

            public uint MinimumPower
            {
                get => _MinimumPower;
            }

            public uint DefaultPower
            {
                get => _DefaultPower;
            }

            public uint MaximumPower
            {
                get => _MaximumPower;
            }
        }
    }
}