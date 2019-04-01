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
        internal PerformanceStates20ClockType _ClockType;
        internal uint _Flags;
        internal PerformanceStates20ParameterDelta _FrequencyDeltaInkHz;
        internal PerformanceStates20ClockDependentInfo _ClockDependentInfo;

        public PerformanceStates20ClockEntryV1(
            PublicClockDomain domain,
            PerformanceStates20ParameterDelta valueDelta) : this()
        {
            _DomainId = domain;
            _FrequencyDeltaInkHz = valueDelta;
        }

        public PerformanceStates20ClockEntryV1(
            PublicClockDomain domain,
            PerformanceStates20ClockType clockType,
            PerformanceStates20ParameterDelta valueDelta) : this(domain, valueDelta)
        {
            _ClockType = clockType;
        }

        // ReSharper disable once TooManyDependencies
        public PerformanceStates20ClockEntryV1(
            PublicClockDomain domain,
            PerformanceStates20ClockType clockType,
            PerformanceStates20ParameterDelta valueDelta,
            PerformanceStates20ClockDependentSingleFrequency singleFrequency) :
            this(domain, clockType, valueDelta)
        {
            _ClockDependentInfo = new PerformanceStates20ClockDependentInfo(singleFrequency);
        }

        // ReSharper disable once TooManyDependencies
        public PerformanceStates20ClockEntryV1(
            PublicClockDomain domain,
            PerformanceStates20ClockType clockType,
            PerformanceStates20ParameterDelta valueDelta,
            PerformanceStates20ClockDependentFrequencyRange frequencyRange) :
            this(domain, clockType, valueDelta)
        {
            _ClockDependentInfo = new PerformanceStates20ClockDependentInfo(frequencyRange);
        }

        /// <inheritdoc />
        public PublicClockDomain DomainId
        {
            get => _DomainId;
        }

        /// <inheritdoc />
        public PerformanceStates20ClockType ClockType
        {
            get => _ClockType;
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
        IPerformanceStates20ClockDependentSingleFrequency IPerformanceStates20ClockEntry.SingleFrequency
        {
            get => _ClockDependentInfo._Single;
        }

        /// <inheritdoc />
        IPerformanceStates20ClockDependentFrequencyRange IPerformanceStates20ClockEntry.FrequencyRange
        {
            get => _ClockDependentInfo._Range;
        }

        public PerformanceStates20ClockDependentSingleFrequency SingleFrequency
        {
            get => _ClockDependentInfo._Single;
        }

        public PerformanceStates20ClockDependentFrequencyRange FrequencyRange
        {
            get => _ClockDependentInfo._Range;
        }


        /// <inheritdoc cref="IPerformanceStates20ClockDependentSingleFrequency" />
        [StructLayout(LayoutKind.Sequential, Pack = 8)]
        public struct PerformanceStates20ClockDependentSingleFrequency :
            IPerformanceStates20ClockDependentSingleFrequency
        {
            internal uint _Frequency;

            /// <inheritdoc />
            public uint FrequencyInkHz
            {
                get => _Frequency;
            }

            public PerformanceStates20ClockDependentSingleFrequency(uint frequency)
            {
                _Frequency = frequency;
            }
        }

        /// <inheritdoc cref="IPerformanceStates20ClockDependentFrequencyRange" />
        [StructLayout(LayoutKind.Sequential, Pack = 8)]
        public struct PerformanceStates20ClockDependentFrequencyRange :
            IPerformanceStates20ClockDependentFrequencyRange
        {
            internal uint _MinimumFrequency;
            internal uint _MaximumFrequency;
            internal PerformanceVoltageDomain _VoltageDomainId;
            internal uint _MinimumVoltage;
            internal uint _MaximumVoltage;

            public PerformanceStates20ClockDependentFrequencyRange(
                uint minimumFrequency,
                uint maximumFrequency,
                PerformanceVoltageDomain voltageDomainId,
                uint minimumVoltage,
                uint maximumVoltage) : this()
            {
                _MinimumFrequency = minimumFrequency;
                _MaximumFrequency = maximumFrequency;
                _VoltageDomainId = voltageDomainId;
                _MinimumVoltage = minimumVoltage;
                _MaximumVoltage = maximumVoltage;
            }

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

        [StructLayout(LayoutKind.Explicit, Pack = 8)]
        internal struct PerformanceStates20ClockDependentInfo
        {
            [FieldOffset(0)] internal PerformanceStates20ClockDependentSingleFrequency _Single;
            [FieldOffset(0)] internal PerformanceStates20ClockDependentFrequencyRange _Range;

            public PerformanceStates20ClockDependentInfo(
                PerformanceStates20ClockDependentSingleFrequency singleFrequency
            ) : this()
            {
                _Single = singleFrequency;
            }

            public PerformanceStates20ClockDependentInfo(
                PerformanceStates20ClockDependentFrequencyRange frequencyRange
            ) : this()
            {
                _Range = frequencyRange;
            }
        }
    }
}