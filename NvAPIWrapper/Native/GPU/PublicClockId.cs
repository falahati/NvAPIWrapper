using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NvAPIWrapper.Native.GPU
{
    /// <summary>
    ///     Clock id for determine right offset of clocks domain array
    /// </summary>
    public enum PublicClockId
    {
        /// <summary>
        ///     Undefined id (value of NVAPI_MAX_GPU_PUBLIC_CLOCKS = 32)
        /// </summary>
        Undefined = 32,

        /// <summary>
        ///     Graphics engine clocks id
        /// </summary>
        Graphics = 0,

        /// <summary>
        ///     Memory engine clocks id
        /// </summary>
        Memory = 4,

        /// <summary>
        ///     Video processor clock id
        /// </summary>
        Processor = 7,

        Video = 8
    }
}
