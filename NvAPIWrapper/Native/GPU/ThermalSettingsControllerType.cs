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
    public enum ThermalSettingsControllerType
    {
        NONE = 0,
        GPU_INTERNAL,
        ADM1032,
        MAX6649,
        MAX1617,
        LM99,
        LM89,
        LM64,
        ADT7473,
        SBMAX6649,
        VBIOSEVT,
        OS,
        UNKNOWN = -1,
    }
}
