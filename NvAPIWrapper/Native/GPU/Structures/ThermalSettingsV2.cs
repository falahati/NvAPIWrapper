using System.Linq;
using System.Runtime.InteropServices;
using NvAPIWrapper.Native.Attributes;
using NvAPIWrapper.Native.General.Structures;
using NvAPIWrapper.Native.Interfaces;
using NvAPIWrapper.Native.Interfaces.GPU;

namespace NvAPIWrapper.Native.GPU.Structures
{
    /// <summary>
    ///     Holds a list of thermal sensor information settings (temperature values)
    /// </summary>
    [StructLayout(LayoutKind.Sequential, Pack = 8)]
    [StructureVersion(2)]
    public struct ThermalSettingsV2 : IInitializable, IThermalSettings
    {
        internal StructureVersion _Version;
        internal readonly uint _Count;

        [MarshalAs(UnmanagedType.ByValArray, SizeConst = ThermalSettingsV1.MaxThermalSensorsPerGPU)] internal readonly
            ThermalSensor[] _Sensors;

        /// <inheritdoc />
        public IThermalSensor[] Sensors
            => _Sensors.Take((int) _Count).Cast<IThermalSensor>().ToArray();

        /// <summary>
        ///     Holds information about a single thermal sensor
        /// </summary>
        [StructLayout(LayoutKind.Sequential)]
        public struct ThermalSensor : IThermalSensor
        {
            internal readonly ThermalSettingsController _Controller;
            internal readonly int _DefaultMinTemp;
            internal readonly int _DefaultMaxTemp;
            internal readonly int _CurrentTemp;
            internal readonly ThermalSettingsTarget _Target;

            /// <inheritdoc />
            public ThermalSettingsController Controller => _Controller;

            /// <inheritdoc />
            public int DefaultMinimumTemperature => _DefaultMinTemp;

            /// <inheritdoc />
            public int DefaultMaximumTemperature => _DefaultMaxTemp;

            /// <inheritdoc />
            public int CurrentTemperature => _CurrentTemp;

            /// <inheritdoc />
            public ThermalSettingsTarget Target => _Target;
        }
    }
}