using System.Runtime.InteropServices;

namespace NvAPIWrapper.Native.GPU.Structures
{
    /// <summary>
    ///     Hold information regarding delta values and delta ranges for voltages or clock frequencies in their respective unit
    ///     (uV or kHz)
    /// </summary>
    [StructLayout(LayoutKind.Sequential, Pack = 8)]
    public struct PerformanceStates20ParameterDelta
    {
        internal int _DeltaValue;
        internal PerformanceState20ParameterDeltaValueRange _DeltaRange;

        /// <summary>
        ///     Gets the delta value in the respective unit (uV or kHz)
        /// </summary>
        public int DeltaValue
        {
            get => _DeltaValue;
        }

        /// <summary>
        ///     Gets the range of the valid delta values in the respective unit (uV or kHz)
        /// </summary>
        public PerformanceState20ParameterDeltaValueRange DeltaRange
        {
            get => _DeltaRange;
        }

        /// <summary>
        ///     Holds information regarding a range of values
        /// </summary>
        [StructLayout(LayoutKind.Sequential, Pack = 8)]
        public struct PerformanceState20ParameterDeltaValueRange
        {
            internal int _Minimum;
            internal int _Maximum;

            /// <summary>
            ///     Gets the minimum value
            /// </summary>
            public int Minimum
            {
                get => _Minimum;
            }

            /// <summary>
            ///     Gets the maximum value
            /// </summary>
            public int Maximum
            {
                get => _Maximum;
            }
        }
    }
}