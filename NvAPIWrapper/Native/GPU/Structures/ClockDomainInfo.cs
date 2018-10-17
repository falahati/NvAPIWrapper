using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;
using NvAPIWrapper.Native.Helpers;

namespace NvAPIWrapper.Native.GPU.Structures
{
    [StructLayout(LayoutKind.Sequential, Pack = 8)]
    public struct ClockDomainInfo
    {
        internal readonly uint _IsPresent;
        internal readonly uint _Frequency;

        public bool IsPresent => _IsPresent.GetBit(0);

        public uint Frequency => _Frequency;

        /// <inheritdoc />
        public override string ToString() {
            return IsPresent ? $"{_Frequency} kHz" : "Not Present";
        }
    }
}
