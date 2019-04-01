using System.Runtime.InteropServices;
using NvAPIWrapper.Native.Attributes;
using NvAPIWrapper.Native.General.Structures;
using NvAPIWrapper.Native.Interfaces;

namespace NvAPIWrapper.Native.GPU.Structures
{
    [StructLayout(LayoutKind.Sequential, Pack = 8)]
    [StructureVersion(1)]
    public struct ECCErrorInfoV1 : IInitializable
    {
        internal StructureVersion _Version;
        internal ErrorInfo _CurrentErrors;
        internal ErrorInfo _AggregatedErrors;

        public ErrorInfo CurrentErrors
        {
            get => _CurrentErrors;
        }

        public ErrorInfo AggregatedErrors
        {
            get => _AggregatedErrors;
        }

        [StructLayout(LayoutKind.Sequential, Pack = 8)]
        public struct ErrorInfo
        {
            internal ulong _SingleBitErrors;
            internal ulong _DoubleBitErrors;

            public ulong SingleBitErrors
            {
                get => _SingleBitErrors;
            }

            public ulong DoubleBitErrors
            {
                get => _DoubleBitErrors;
            }
        }
    }
}