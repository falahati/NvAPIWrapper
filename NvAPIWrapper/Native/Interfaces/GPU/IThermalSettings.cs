using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using NvAPIWrapper.Native.GPU;

namespace NvAPIWrapper.Native.Interfaces.GPU
{
    public interface IThermalSettings
    {
        /// <summary>
        ///     Number of associated thermal sensors
        /// </summary>
        uint ThermalSensorsCount { get; }

        IThermalSeonsorInfo[] Sensor { get; }
    }
}
