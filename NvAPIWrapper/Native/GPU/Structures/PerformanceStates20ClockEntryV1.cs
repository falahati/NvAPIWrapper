using System.Runtime.InteropServices;
using NvAPIWrapper.Native.Helpers;
using NvAPIWrapper.Native.Interfaces.GPU;

namespace NvAPIWrapper.Native.GPU.Structures
{
    /// <inheritdoc cref="IPerformanceStates20ClockEntry" />
    [StructLayout(LayoutKind.Sequential, Pack = 8)]
    public struct PerformanceStates20ClockEntryV1 : IPerformanceStates20ClockEntry
    {
        internal PublicClockDomain _DomainId;
        internal PerformanceStates20ClockType _TypeId;
        internal uint _Flags;
        internal PerformanceStates20ParameterDelta _FrequencyDeltaInkHz;
        internal PerformanceStates20ClockDependentInfo _Data;

        /// <inheritdoc />
        public PublicClockDomain DomainId
        {
            get => _DomainId;
        }

        /// <inheritdoc />
        public PerformanceStates20ClockType ClockType
        {
            get => _TypeId;
        }

        /// <inheritdoc />
        public bool IsEditable
        {
            get => _Flags.GetBit(0);
        }

        /// <inheritdoc />
        public PerformanceStates20ParameterDelta FrequencyDeltaInkHz
        {
            get => _FrequencyDeltaInkHz;
        }

        /// <inheritdoc />
        public IPerformanceStates20ClockDependentSingleFrequency SingleFrequency
        {
            get => _Data._Single;
        }

        /// <inheritdoc />
        public IPerformanceStates20ClockDependentFrequencyRange FrequencyRange
        {
            get => _Data._Range;
        }

        [StructLayout(LayoutKind.Explicit, Pack = 8)]
        internal struct PerformanceStates20ClockDependentInfo
        {
            [FieldOffset(0)] internal PerformanceStates20ClockDependentSingleFrequency _Single;
            [FieldOffset(0)] internal PerformanceStates20ClockDependentFrequencyRange _Range;

            /// <inheritdoc cref="IPerformanceStates20ClockDependentSingleFrequency" />
            [StructLayout(LayoutKind.Sequential, Pack = 8)]
            public struct
                PerformanceStates20ClockDependentSingleFrequency : IPerformanceStates20ClockDependentSingleFrequency
            {
                internal uint _Frequency;

                /// <inheritdoc />
                public uint FrequencyInkHz
                {
                    get => _Frequency;
                }
            }

            /// <inheritdoc cref="IPerformanceStates20ClockDependentFrequencyRange" />
            [StructLayout(LayoutKind.Sequential, Pack = 8)]
            public struct
                PerformanceStates20ClockDependentFrequencyRange : IPerformanceStates20ClockDependentFrequencyRange
            {
                internal uint _MinimumFrequency;
                internal uint _MaximumFrequency;
                internal PerformanceVoltageDomain _VoltageDomainId;
                internal uint _MinimumVoltage;
                internal uint _MaximumVoltage;

                /// <inheritdoc />
                public uint MinimumFrequencyInkHz
                {
                    get => _MinimumFrequency;
                }

                /// <inheritdoc />
                public uint MaximumFrequencyInkHz
                {
                    get => _MaximumFrequency;
                }

                /// <inheritdoc />
                public PerformanceVoltageDomain VoltageDomainId
                {
                    get => _VoltageDomainId;
                }

                /// <inheritdoc />
                public uint MinimumVoltageInMicroVolt
                {
                    get => _MinimumVoltage;
                }

                /// <inheritdoc />
                public uint MaximumVoltageInMicroVolt
                {
                    get => _MaximumVoltage;
                }
            }
        }
    }
}