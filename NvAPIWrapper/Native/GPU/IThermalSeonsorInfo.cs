using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NvAPIWrapper.Native.GPU
{
    public interface IThermalSeonsorInfo
    {
        /// <summary>
        ///     internal, ADM1032, MAX6649...
        /// </summary>
        ThermalControllerType Controller { get; }

        /// <summary>
        ///     Minimum default temperature value of the thermal sensor in degree Celsius
        /// </summary>
        int DefaultMinTemp { get; }

        /// <summary>
        ///     Maximum default temperature value of the thermal sensor in degree Celsius
        /// </summary>
        int DefaultMaxTemp { get; }

        /// <summary>
        ///     Current temperature value of the thermal sensor in degree Celsius
        /// </summary>
        int CurrentTemp { get; }

        /// <summary>
        ///     Thermal sensor targeted - GPU, memory, chipset, powersupply, Visual Computing Device, etc
        /// </summary>
        ThermalControllerTargetType Target { get; }
    }
}
