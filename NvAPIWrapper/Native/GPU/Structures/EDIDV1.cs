using System;
using System.Runtime.InteropServices;
using NvAPIWrapper.Native.Attributes;
using NvAPIWrapper.Native.General.Structures;
using NvAPIWrapper.Native.Helpers;
using NvAPIWrapper.Native.Interfaces;
using NvAPIWrapper.Native.Interfaces.GPU;

namespace NvAPIWrapper.Native.GPU.Structures
{
    [StructLayout(LayoutKind.Sequential, Pack = 8)]
    [StructureVersion(1)]
    public struct EDIDV1 : IEDID, IInitializable
    {
        public const int MaxDataSize = 256;

        internal StructureVersion _Version;
        [MarshalAs(UnmanagedType.ByValArray, SizeConst = MaxDataSize)] internal byte[] _Data;

        internal static EDIDV1 CreateWithData(byte[] data)
        {
            if (data.Length > MaxDataSize)
            {
                throw new ArgumentException("Data is too big.", nameof(data));
            }
            var edid = typeof(EDIDV1).Instantiate<EDIDV1>();
            edid._Data = data;
            return edid;
        }

        public byte[] Data => _Data;
    }
}