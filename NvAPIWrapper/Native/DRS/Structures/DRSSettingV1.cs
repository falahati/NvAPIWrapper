using System;
using System.Linq;
using System.Runtime.InteropServices;
using NvAPIWrapper.Native.Attributes;
using NvAPIWrapper.Native.General.Structures;
using NvAPIWrapper.Native.Helpers;
using NvAPIWrapper.Native.Interfaces;

namespace NvAPIWrapper.Native.DRS.Structures
{
    [StructLayout(LayoutKind.Sequential, Pack = 8)]
    [StructureVersion(1)]
    public struct DRSSettingV1 : IInitializable
    {
        internal StructureVersion _Version;
        internal UnicodeString _SettingName;
        internal uint _SettingId;
        internal DRSSettingType _SettingType;
        internal DRSSettingLocation _SettingLocation;
        internal uint _IsCurrentPredefined;
        internal uint _IsPredefinedValid;
        internal DRSSettingValue _PredefinedValue;
        internal DRSSettingValue _CurrentValue;

        public DRSSettingV1(uint id, DRSSettingType settingType, object value)
        {
            this = typeof(DRSSettingV1).Instantiate<DRSSettingV1>();
            Id = id;
            IsPredefinedValueValid = false;
            _SettingType = settingType;
            CurrentValue = value;
        }

        public DRSSettingV1(uint id, string value) : this(id, DRSSettingType.String, value)
        {
        }

        public DRSSettingV1(uint id, uint value) : this(id, DRSSettingType.Integer, value)
        {
        }

        public DRSSettingV1(uint id, byte[] value) : this(id, DRSSettingType.Binary, value)
        {
        }

        public string Name
        {
            get => _SettingName.Value;
        }

        public uint Id
        {
            get => _SettingId;
            private set => _SettingId = value;
        }

        public DRSSettingType SettingType
        {
            get => _SettingType;
            private set => _SettingType = value;
        }

        public DRSSettingLocation SettingLocation
        {
            get => _SettingLocation;
        }

        public bool IsCurrentValuePredefined
        {
            get => _IsCurrentPredefined > 0;
            private set => _IsCurrentPredefined = value ? 1u : 0u;
        }

        public bool IsPredefinedValueValid
        {
            get => _IsPredefinedValid > 0;
            private set => _IsPredefinedValid = value ? 1u : 0u;
        }

        public uint GetPredefinedValueAsInteger()
        {
            return _PredefinedValue.AsInteger();
        }

        public byte[] GetPredefinedValueAsBinary()
        {
            return _PredefinedValue.AsBinary();
        }

        public string GetPredefinedValueAsUnicodeString()
        {
            return _PredefinedValue.AsUnicodeString();
        }


        public object PredefinedValue
        {
            get
            {
                if (!IsPredefinedValueValid)
                {
                    return null;
                }

                switch (_SettingType)
                {
                    case DRSSettingType.Integer:

                        return GetPredefinedValueAsInteger();
                    case DRSSettingType.Binary:

                        return GetPredefinedValueAsBinary();
                    case DRSSettingType.String:
                    case DRSSettingType.UnicodeString:

                        return GetPredefinedValueAsUnicodeString();
                    default:

                        throw new ArgumentOutOfRangeException(nameof(SettingType));
                }
            }
        }

        public uint GetCurrentValueAsInteger()
        {
            return _CurrentValue.AsInteger();
        }

        public byte[] GetCurrentValueAsBinary()
        {
            return _CurrentValue.AsBinary();
        }

        public string GetCurrentValueAsUnicodeString()
        {
            return _CurrentValue.AsUnicodeString();
        }

        public void SetCurrentValueAsInteger(uint value)
        {
            if (SettingType != DRSSettingType.Integer)
            {
                throw new ArgumentOutOfRangeException(nameof(value), "Passed argument is invalid for this setting.");
            }

            _CurrentValue = new DRSSettingValue(value);
            IsCurrentValuePredefined = IsPredefinedValueValid && (uint) CurrentValue == (uint) PredefinedValue;
        }

        public void SetCurrentValueAsBinary(byte[] value)
        {
            if (SettingType != DRSSettingType.Binary)
            {
                throw new ArgumentOutOfRangeException(nameof(value), "Passed argument is invalid for this setting.");
            }

            _CurrentValue = new DRSSettingValue(value);
            IsCurrentValuePredefined =
                IsPredefinedValueValid &&
                ((byte[]) CurrentValue)?.SequenceEqual((byte[]) PredefinedValue ?? new byte[0]) == true;
        }

        public void SetCurrentValueAsUnicodeString(string value)
        {
            if (SettingType != DRSSettingType.UnicodeString)
            {
                throw new ArgumentOutOfRangeException(nameof(value), "Passed argument is invalid for this setting.");
            }

            _CurrentValue = new DRSSettingValue(value);
            IsCurrentValuePredefined =
                IsPredefinedValueValid &&
                string.Equals(
                    (string) CurrentValue,
                    (string) PredefinedValue,
                    StringComparison.InvariantCulture
                );
        }

        public object CurrentValue
        {
            get
            {
                switch (_SettingType)
                {
                    case DRSSettingType.Integer:

                        return GetCurrentValueAsInteger();
                    case DRSSettingType.Binary:

                        return GetCurrentValueAsBinary();
                    case DRSSettingType.String:
                    case DRSSettingType.UnicodeString:

                        return GetCurrentValueAsUnicodeString();
                    default:

                        throw new ArgumentOutOfRangeException(nameof(SettingType));
                }
            }
            private set
            {
                if (value is int intValue)
                {
                    SetCurrentValueAsInteger((uint) intValue);
                }
                else if (value is uint unsignedIntValue)
                {
                    SetCurrentValueAsInteger(unsignedIntValue);
                }
                else if (value is short shortValue)
                {
                    SetCurrentValueAsInteger((uint) shortValue);
                }
                else if (value is ushort unsignedShortValue)
                {
                    SetCurrentValueAsInteger(unsignedShortValue);
                }
                else if (value is long longValue)
                {
                    SetCurrentValueAsInteger((uint) longValue);
                }
                else if (value is ulong unsignedLongValue)
                {
                    SetCurrentValueAsInteger((uint) unsignedLongValue);
                }
                else if (value is byte byteValue)
                {
                    SetCurrentValueAsInteger(byteValue);
                }
                else if (value is string stringValue)
                {
                    SetCurrentValueAsUnicodeString(stringValue);
                }
                else if (value is byte[] binaryValue)
                {
                    SetCurrentValueAsBinary(binaryValue);
                }
                else
                {
                    throw new ArgumentException("Unacceptable argument type.", nameof(value));
                }
            }
        }
    }
}