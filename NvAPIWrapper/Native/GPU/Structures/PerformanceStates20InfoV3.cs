using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using NvAPIWrapper.Native.Attributes;
using NvAPIWrapper.Native.General.Structures;
using NvAPIWrapper.Native.Helpers;
using NvAPIWrapper.Native.Interfaces;
using NvAPIWrapper.Native.Interfaces.GPU;

namespace NvAPIWrapper.Native.GPU.Structures
{
    /// <inheritdoc cref="IPerformanceStates20Info" />
    [StructLayout(LayoutKind.Sequential, Pack = 8)]
    [StructureVersion(3)]
    public struct PerformanceStates20InfoV3 : IInitializable, IPerformanceStates20Info
    {
        internal const int MaxPerformanceStates20 = PerformanceStates20InfoV2.MaxPerformanceStates20;

        internal const int MaxPerformanceStates20BaseVoltages =
            PerformanceStates20InfoV2.MaxPerformanceStates20BaseVoltages;

        internal StructureVersion _Version;
        internal uint _Flags;
        internal uint _NumberOfPerformanceStates;
        internal uint _NumberOfClocks;
        internal uint _NumberOfBaseVoltages;

        [MarshalAs(UnmanagedType.ByValArray, SizeConst = MaxPerformanceStates20)]
        internal PerformanceStates20InfoV1.PerformanceState20[] _PerformanceStates;

        internal PerformanceStates20InfoV2.PerformanceStates20OverVoltingSetting _OverVoltingSettings;

        public PerformanceStates20BaseVoltageEntryV1[] GeneralVoltages
        {
            get => _OverVoltingSettings.Voltages.ToArray();
        }

        /// <inheritdoc />
        IPerformanceStates20VoltageEntry[] IPerformanceStates20Info.GeneralVoltages
        {
            get => GeneralVoltages.Cast<IPerformanceStates20VoltageEntry>().ToArray();
        }

        /// <inheritdoc />
        public bool IsEditable
        {
            get => _Flags.GetBit(0);
        }

        public PerformanceStates20InfoV1.PerformanceState20[] PerformanceStates
        {
            get => _PerformanceStates.Take((int) _NumberOfPerformanceStates).ToArray();
        }

        /// <inheritdoc />
        IPerformanceState20[] IPerformanceStates20Info.PerformanceStates
        {
            get => PerformanceStates.Cast<IPerformanceState20>().ToArray();
        }

        public IReadOnlyDictionary<PerformanceStateId, PerformanceStates20ClockEntryV1[]> Clocks
        {
            get
            {
                var clocks = (int) _NumberOfClocks;

                return PerformanceStates.ToDictionary(
                    state20 => state20.StateId,
                    state20 => state20._Clocks.Take(clocks).ToArray()
                );
            }
        }

        public IReadOnlyDictionary<PerformanceStateId, PerformanceStates20BaseVoltageEntryV1[]> Voltages
        {
            get
            {
                var baseVoltages = (int) _NumberOfBaseVoltages;

                return PerformanceStates.ToDictionary(
                    state20 => state20.StateId,
                    state20 => state20._BaseVoltages.Take(baseVoltages)
                        .ToArray()
                );
            }
        }

        /// <inheritdoc />
        IReadOnlyDictionary<PerformanceStateId, IPerformanceStates20ClockEntry[]> IPerformanceStates20Info.Clocks
        {
            get
            {
                return Clocks.ToDictionary(
                    pair => pair.Key,
                    pair => pair.Value.Cast<IPerformanceStates20ClockEntry>().ToArray()
                );
            }
        }

        /// <inheritdoc />
        IReadOnlyDictionary<PerformanceStateId, IPerformanceStates20VoltageEntry[]> IPerformanceStates20Info.Voltages
        {
            get
            {
                return Voltages.ToDictionary(
                    pair => pair.Key,
                    pair => pair.Value.Cast<IPerformanceStates20VoltageEntry>().ToArray()
                );
            }
        }
    }
}