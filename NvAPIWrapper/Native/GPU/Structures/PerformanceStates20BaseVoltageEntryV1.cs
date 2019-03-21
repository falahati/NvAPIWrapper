using System.Runtime.InteropServices;
using NvAPIWrapper.Native.Helpers;
using NvAPIWrapper.Native.Interfaces.GPU;

namespace NvAPIWrapper.Native.GPU.Structures
{
    /// <inheritdoc cref="IPerformanceStates20VoltageEntry" />
    [StructLayout(LayoutKind.Sequential, Pack = 8)]
    public struct PerformanceStates20BaseVoltageEntryV1 : IPerformanceStates20VoltageEntry
    {
        internal PerformanceVoltageDomain _DomainId;
        internal uint _Flags;
        internal uint _Value;
        internal PerformanceStates20ParameterDelta _ValueDelta;

        /// <inheritdoc />
        public PerformanceVoltageDomain DomainId
        {
            get => _DomainId;
        }

        /// <inheritdoc />
        public bool IsEditable
        {
            get => _Flags.GetBit(0);
        }

        /// <inheritdoc />
        public uint ValueInMicroVolt
        {
            get => _Value;
        }

        /// <inheritdoc />
        public PerformanceStates20ParameterDelta ValueDeltaInMicroVolt
        {
            get => _ValueDelta;
        }
    }
}