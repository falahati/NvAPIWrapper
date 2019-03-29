using System;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using NvAPIWrapper.Native.General.Structures;
using NvAPIWrapper.Native.Interfaces;

namespace NvAPIWrapper.Native.DRS.Structures
{
    [StructLayout(LayoutKind.Sequential, Pack = 8)]
    public struct DRSSettingValue : IInitializable
    {
        private const int UnicodeStringLength = UnicodeString.UnicodeStringLength;
        private const int BinaryDataMax = 4096;

        // Math.Max(BinaryDataMax + sizeof(uint), UnicodeStringLength * sizeof(ushort))
        private const int FullStructureSize = 4100;

        [MarshalAs(UnmanagedType.ByValArray, SizeConst = FullStructureSize, ArraySubType = UnmanagedType.U1)]
        internal byte[] _BinaryValue;

        public DRSSettingValue(string value)
        {
            if (value?.Length > UnicodeStringLength)
            {
                value = value.Substring(0, UnicodeStringLength);
            }

            _BinaryValue = new byte[FullStructureSize];

            var stringBytes = Encoding.Unicode.GetBytes(value ?? string.Empty);
            Array.Copy(stringBytes, 0, _BinaryValue, 0, Math.Min(stringBytes.Length, _BinaryValue.Length));
        }

        public DRSSettingValue(byte[] value)
        {
            _BinaryValue = new byte[FullStructureSize];

            if (value?.Length > 0)
            {
                var arrayLength = Math.Min(value.Length, BinaryDataMax);
                var arrayLengthBytes = BitConverter.GetBytes((uint) arrayLength);
                Array.Copy(arrayLengthBytes, 0, _BinaryValue, 0, arrayLengthBytes.Length);
                Array.Copy(value, 0, _BinaryValue, arrayLengthBytes.Length, arrayLength);
            }
        }

        public DRSSettingValue(uint value)
        {
            _BinaryValue = new byte[FullStructureSize];
            var arrayLengthBytes = BitConverter.GetBytes(value);
            Array.Copy(arrayLengthBytes, 0, _BinaryValue, 0, arrayLengthBytes.Length);
        }

        public uint AsInteger()
        {
            return BitConverter.ToUInt32(_BinaryValue, 0);
        }

        public byte[] AsBinary()
        {
            return _BinaryValue.Skip(sizeof(uint)).Take((int) AsInteger()).ToArray();
        }

        public string AsUnicodeString()
        {
            return Encoding.Unicode.GetString(_BinaryValue).TrimEnd('\0');
        }
    }
}