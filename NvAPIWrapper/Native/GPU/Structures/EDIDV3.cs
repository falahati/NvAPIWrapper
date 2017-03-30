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
    [StructureVersion(3)]
    public struct EDIDV3 : IEDID, IInitializable
    {
        public const int MaxDataSize = EDIDV1.MaxDataSize;

        internal StructureVersion _Version;
        [MarshalAs(UnmanagedType.ByValArray, SizeConst = MaxDataSize)] internal byte[] _Data;
        internal uint _TotalSize;
        internal uint _Identification;
        internal uint _DataOffset;

        internal static EDIDV3 CreateWithOffset(uint id, uint offset)
        {
            var edid = typeof(EDIDV3).Instantiate<EDIDV3>();
            edid._Identification = id;
            edid._DataOffset = offset;
            return edid;
        }

        internal static EDIDV3 CreateWithData(uint id, uint offset, byte[] data, int totalSize)
        {
            if (data.Length > MaxDataSize)
            {
                throw new ArgumentException("Data is too big.", nameof(data));
            }
            var edid = typeof(EDIDV3).Instantiate<EDIDV3>();
            edid._Identification = id;
            edid._DataOffset = offset;
            edid._TotalSize = (uint) totalSize;
            edid._Data = data;
            return edid;
        }

        public int Identification => (int) _DataOffset;
        public int DataOffset => (int) _DataOffset;
        public int TotalSize => (int) _TotalSize;
        public byte[] Data => _Data.Take((int) Math.Min(_TotalSize - DataOffset, MaxDataSize)).ToArray();
    }
}