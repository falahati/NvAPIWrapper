using System.Dynamic;
using System.Runtime.InteropServices;
using NvAPIWrapper.Native.Attributes;
using NvAPIWrapper.Native.General.Structures;
using NvAPIWrapper.Native.Interfaces;
using NvAPIWrapper.Native.Interfaces.GPU;

namespace NvAPIWrapper.Native.GPU.Structures
{
    /// <summary>
    ///     Holds information of thermal sensors settings (temperature values)
    /// </summary>
    [StructLayout(LayoutKind.Sequential, Pack = 8)]
    [StructureVersion(1)]
    public struct ThermalSettingsV1 : IInitializable, IThermalSettings
    {
        public const int MaxThermalSensorsPerGpu = 3;

        internal StructureVersion _Version;
        internal readonly uint _Count;
        [MarshalAs(UnmanagedType.ByValArray, SizeConst = MaxThermalSensorsPerGpu)] internal readonly ThermalSeonsorInfoV2[] _Sensors;

        public uint ThermalSensorsCount => _Count;

        public IThermalSeonsorInfo[] Sensor {
            get {
                var ret = new IThermalSeonsorInfo[MaxThermalSensorsPerGpu];
                for (var i = 0; i < ret.Length; i++) {
                    ret[i] = _Sensors[i];
                }

                return ret;
            }
        }
    }
}
