using System;
using System.Linq;
using System.Runtime.InteropServices;
using NvAPIWrapper.Native.Attributes;
using NvAPIWrapper.Native.General.Structures;
using NvAPIWrapper.Native.Helpers;
using NvAPIWrapper.Native.Interfaces;
using NvAPIWrapper.Native.Interfaces.GPU;

namespace NvAPIWrapper.Native.GPU.Structures
{
    [StructLayout(LayoutKind.Sequential, Pack = 8)]
    [StructureVersion(2)]
    public struct EDIDV2 : IEDID, IInitializable
    {
        public const int MaxDataSize = EDIDV1.MaxDataSize;

        internal StructureVersion _Version;
        [MarshalAs(UnmanagedType.ByValArray, SizeConst = MaxDataSize)] internal byte[] _Data;
        internal uint _TotalSize;

        internal static EDIDV2 CreateWithData(byte[] data, int totalSize)
        {
            if (data.Length > MaxDataSize)
            {
                throw new ArgumentException("Data is too big.", nameof(data));
            }
            var edid = typeof(EDIDV2).Instantiate<EDIDV2>();
            edid._TotalSize = (uint) totalSize;
            edid._Data = data;
            return edid;
        }


        public int TotalSize => (int) _TotalSize;
        public byte[] Data => _Data.Take((int) Math.Min(_TotalSize, MaxDataSize)).ToArray();
    }
}