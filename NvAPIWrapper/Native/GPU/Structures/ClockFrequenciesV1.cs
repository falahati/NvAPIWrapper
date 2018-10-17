using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;
using NvAPIWrapper.Native.Attributes;
using NvAPIWrapper.Native.General.Structures;
using NvAPIWrapper.Native.Interfaces;
using NvAPIWrapper.Native.Interfaces.GPU;
using static System.String;

namespace NvAPIWrapper.Native.GPU.Structures
{
    /// <summary>
    /// Holds information about all present gpu clock frequencies (in kHz)
    /// </summary>
    [StructLayout(LayoutKind.Sequential, Pack = 8)]
    [StructureVersion(1)]
    public struct ClockFrequenciesV1 : IInitializable, IClockFrequenciesInfo
    {
        internal const int MaxClocksPerGpu = 32;

        internal StructureVersion _Version;
        internal readonly uint _Reserved;

        [MarshalAs(UnmanagedType.ByValArray, SizeConst = MaxClocksPerGpu)] internal 
            ClockDomainInfo[] _Clocks;

        public ClockDomainInfo ClockGraphics => _Clocks[(int)PublicClockId.Graphics];

        public ClockDomainInfo ClockMemory => _Clocks[(int)PublicClockId.Memory];

        public ClockDomainInfo ClockVideo => _Clocks[(int)PublicClockId.Video];

        public ClockDomainInfo ClockProcessor => _Clocks[(int)PublicClockId.Processor];

        public ClockDomainInfo[] Domain => _Clocks;

        /// <inheritdoc />
        public override string ToString() {
            return "ClockFrequenciesV1: " + Join(", ", _Clocks.Select((x, index) => $"{index} = '{x}'").ToArray());
        }
    }
}
