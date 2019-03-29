using System;
using System.Linq;
using System.Runtime.InteropServices;
using NvAPIWrapper.Native.Attributes;
using NvAPIWrapper.Native.General.Structures;
using NvAPIWrapper.Native.Interfaces;

namespace NvAPIWrapper.Native.DRS.Structures
{
    [StructLayout(LayoutKind.Sequential, Pack = 8)]
    [StructureVersion(1)]
    public struct DRSSettingValues : IInitializable
    {
        public const int MaxValues = 100;
        internal StructureVersion _Version;
        internal uint _NumberOfValues;
        internal DRSSettingType _SettingType;
        internal DRSSettingValue _DefaultValue;

        [MarshalAs(UnmanagedType.ByValArray, SizeConst = MaxValues)]
        internal DRSSettingValue[] _Values;

        public DRSSettingType SettingType
        {
            get => _SettingType;
        }

        public object[] Values
        {
            get
            {
                switch (_SettingType)
                {
                    case DRSSettingType.Integer:

                        return ValuesAsInteger().Cast<object>().ToArray();
                    case DRSSettingType.Binary:

                        return ValuesAsBinary().Cast<object>().ToArray();
                    case DRSSettingType.String:
                    case DRSSettingType.UnicodeString:

                        return ValuesAsUnicodeString().Cast<object>().ToArray();
                    default:

                        throw new ArgumentOutOfRangeException(nameof(SettingType));
                }
            }
        }

        public object DefaultValue
        {
            get
            {
                switch (_SettingType)
                {
                    case DRSSettingType.Integer:

                        return DefaultValueAsInteger();
                    case DRSSettingType.Binary:

                        return DefaultValueAsBinary();
                    case DRSSettingType.String:
                    case DRSSettingType.UnicodeString:

                        return DefaultValueAsUnicodeString();
                    default:

                        throw new ArgumentOutOfRangeException(nameof(SettingType));
                }
            }
        }

        public uint DefaultValueAsInteger()
        {
            return _DefaultValue.AsInteger();
        }

        public byte[] DefaultValueAsBinary()
        {
            return _DefaultValue.AsBinary();
        }

        public string DefaultValueAsUnicodeString()
        {
            return _DefaultValue.AsUnicodeString();
        }

        public uint[] ValuesAsInteger()
        {
            return _Values.Take((int) _NumberOfValues).Select(value => value.AsInteger()).ToArray();
        }

        public byte[][] ValuesAsBinary()
        {
            return _Values.Take((int) _NumberOfValues).Select(value => value.AsBinary()).ToArray();
        }

        public string[] ValuesAsUnicodeString()
        {
            return _Values.Take((int) _NumberOfValues).Select(value => value.AsUnicodeString()).ToArray();
        }
    }
}