using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;
using NvAPIWrapper.Native.GPU;
using NvAPIWrapper.Native.GPU.Structures;

namespace NvAPIWrapper.Native.Interfaces.GPU
{
    public interface IClockFrequenciesInfo
    {
        /// <summary>
        ///     Representing all possible clocks in single domain array
        /// </summary>
        ClockDomainInfo[] Domain { get; }

        /// <summary>
        ///     Representing graphics engine clocks
        /// </summary>
        ClockDomainInfo ClockGraphics { get; }

        /// <summary>
        ///     Representing memory engine clocks
        /// </summary>
        ClockDomainInfo ClockMemory { get; }

        ClockDomainInfo ClockVideo { get; }

        ClockDomainInfo ClockProcessor { get; }
    }
}
