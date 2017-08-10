using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NvAPIWrapper.Native.GPU
{
    /// <summary>
    ///     All possible thermal controllers
    /// </summary>
    [SuppressMessage("ReSharper", "InconsistentNaming")]
    public enum ThermalSettingsTargetType
    {
        NVAPI_THERMAL_TARGET_NONE = 0,

        /// <summary>
        /// GPU core temperature requires NvPhysicalGpuHandle
        /// </summary>
        NVAPI_THERMAL_TARGET_GPU = 1,

        /// <summary>
        /// GPU memory temperature requires NvPhysicalGpuHandle
        /// </summary>
        NVAPI_THERMAL_TARGET_MEMORY = 2,

        /// <summary>
        /// GPU power supply temperature requires NvPhysicalGpuHandle
        /// </summary>
        NVAPI_THERMAL_TARGET_POWER_SUPPLY = 4,

        /// <summary>
        /// GPU board ambient temperature requires NvPhysicalGpuHandle
        /// </summary>
        NVAPI_THERMAL_TARGET_BOARD = 8,

        /// <summary>
        /// Visual Computing Device Board temperature requires NvVisualComputingDeviceHandle
        /// </summary>
        NVAPI_THERMAL_TARGET_VCD_BOARD = 9,

        /// <summary>
        /// Visual Computing Device Inlet temperature requires NvVisualComputingDeviceHandle
        /// </summary>
        NVAPI_THERMAL_TARGET_VCD_INLET = 10,

        /// <summary>
        /// Visual Computing Device Outlet temperature requires NvVisualComputingDeviceHandle
        /// </summary>
        NVAPI_THERMAL_TARGET_VCD_OUTLET = 11,

        NVAPI_THERMAL_TARGET_ALL = 15,
        NVAPI_THERMAL_TARGET_UNKNOWN = -1,
    }
}
