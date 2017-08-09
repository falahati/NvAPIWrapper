using System.Runtime.InteropServices;
using NvAPIWrapper.Native.Attributes;
using NvAPIWrapper.Native.General.Structures;
using NvAPIWrapper.Native.Interfaces;
using NvAPIWrapper.Native.Interfaces.GPU;

namespace NvAPIWrapper.Native.GPU.Structures
{
    /// <summary>
    ///     Holds information about the dynamic states (such as gpu utilization)
    /// </summary>
    [StructLayout(LayoutKind.Sequential, Pack = 8)]
    [StructureVersion(2)]
    public struct ThermalSettingsV2 : IInitializable, IThermalSettings
    {
        public const int MaxThermalSensorsPerGpu = 3;

        internal StructureVersion _Version;
        internal readonly uint _Count;
        [MarshalAs(UnmanagedType.ByValArray, SizeConst = MaxThermalSensorsPerGpu)] internal readonly ThermalSeonsorInfoV2[] _Sensors;

        public uint ThermalSensorsCount => _Count;

        public IThermalSeonsorInfo Sensor => _Sensors[0];
    }
}
