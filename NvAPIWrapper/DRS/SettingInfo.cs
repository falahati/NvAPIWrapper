using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Reflection;
using NvAPIWrapper.Native;
using NvAPIWrapper.Native.DRS;

namespace NvAPIWrapper.DRS
{
    public class SettingInfo
    {
        private static uint[] _availableSettingIds;

        private SettingInfo(uint settingId)
        {
            SettingId = settingId;
        }

        public object[] AvailableValues
        {
            get
            {
                if (!IsAvailable)
                {
                    return null;
                }

                return DRSApi.EnumAvailableSettingValues(SettingId).Values;
            }
        }

        public object DefaultValue
        {
            get
            {
                if (!IsAvailable)
                {
                    return null;
                }

                var values = DRSApi.EnumAvailableSettingValues(SettingId);

                return values.DefaultValue;
            }
        }

        public bool IsAvailable
        {
            get => GetAvailableSetting().Any(info => info.SettingId == SettingId);
        }

        public bool IsKnown
        {
            get => IsSettingKnown(SettingId);
        }

        public string KnownDescription
        {
            get
            {
                if (!IsKnown || KnownSettingId == null)
                {
                    return null;
                }

                return GetSettingDescription(KnownSettingId.Value);
            }
        }

        public KnownSettingId? KnownSettingId
        {
            get
            {
                if (!IsKnown)
                {
                    return null;
                }

                return GetKnownSettingId(SettingId);
            }
        }

        public Type KnownValueType
        {
            get
            {
                if (!IsKnown || !IsAvailable)
                {
                    return null;
                }

                var name = KnownSettingId.ToString();
                var nameSpace = typeof(SettingInfo).Namespace + ".SettingValues";

                if (SettingType == DRSSettingType.Integer)
                {
                    return Assembly.GetExecutingAssembly().GetTypes().FirstOrDefault(type =>
                        type.IsEnum &&
                        type.Namespace?.Equals(nameSpace, StringComparison.InvariantCultureIgnoreCase) == true &&
                        type.Name.Equals(name, StringComparison.InvariantCultureIgnoreCase));
                }

                if (SettingType == DRSSettingType.String || SettingType == DRSSettingType.UnicodeString)
                {
                    return Assembly.GetExecutingAssembly().GetTypes().FirstOrDefault(type =>
                        type.IsClass &&
                        type.Namespace?.Equals(nameSpace, StringComparison.InvariantCultureIgnoreCase) == true &&
                        type.Name.Equals(name, StringComparison.InvariantCultureIgnoreCase));
                }

                return null;
            }
        }

        public string Name
        {
            get
            {
                if (!IsAvailable)
                {
                    return null;
                }

                return DRSApi.GetSettingNameFromId(SettingId);
            }
        }

        public uint SettingId { get; }

        public DRSSettingType? SettingType
        {
            get
            {
                if (!IsAvailable)
                {
                    return null;
                }

                var values = DRSApi.EnumAvailableSettingValues(SettingId);

                return values.SettingType;
            }
        }

        public static SettingInfo FromId(uint settingId)
        {
            return new SettingInfo(settingId);
        }

        public static SettingInfo FromKnownSettingId(KnownSettingId settingId)
        {
            return FromId(GetSettingId(settingId));
        }

        public static SettingInfo FromName(string settingName)
        {
            var settingId = DRSApi.GetSettingIdFromName(settingName);

            return FromId(settingId);
        }

        public static SettingInfo[] GetAvailableSetting()
        {
            if (_availableSettingIds == null)
            {
                _availableSettingIds = DRSApi.EnumAvailableSettingIds();
            }

            return _availableSettingIds.Select(FromId).ToArray();
        }

        public static KnownSettingId? GetKnownSettingId(uint settingId)
        {
            if (!IsSettingKnown(settingId))
            {
                return null;
            }

            return (KnownSettingId) settingId;
        }

        public static string GetSettingDescription(KnownSettingId knownSettingId)
        {
            var enumName = Enum.GetName(typeof(KnownSettingId), knownSettingId);

            if (enumName == null)
            {
                return null;
            }

            var enumField = typeof(KnownSettingId).GetField(enumName);

            if (enumField == null)
            {
                return null;
            }

            var descriptionAttribute = enumField
                .GetCustomAttributes(typeof(DescriptionAttribute), false)
                .OfType<DescriptionAttribute>()
                .FirstOrDefault();

            if (string.IsNullOrWhiteSpace(descriptionAttribute?.Description))
            {
                return null;
            }

            return descriptionAttribute.Description;
        }

        public static uint GetSettingId(KnownSettingId knownSettingId)
        {
            return (uint) knownSettingId;
        }

        public static bool IsSettingKnown(uint settingId)
        {
            return Enum.IsDefined(typeof(KnownSettingId), settingId);
        }

        /// <inheritdoc />
        public override string ToString()
        {
            try
            {
                var settingName = Name;

                if (!string.IsNullOrWhiteSpace(settingName))
                {
                    return settingName;
                }
            }
            catch
            {
                // ignore;
            }

            return $"#{SettingId:X}";
        }

        public string ResolveKnownValueName(object value)
        {
            if (!IsKnown)
            {
                return null;
            }

            var valueType = KnownValueType;

            if (valueType == null)
            {
                return null;
            }

            if (valueType.IsEnum)
            {
                return Enum.GetName(valueType, value);
            }

            var comparerType = typeof(EqualityComparer<>).MakeGenericType(value.GetType());
            var comparer = comparerType.GetProperty(nameof(EqualityComparer<object>.Default))?.GetValue(null);

            if (!(comparer is IEqualityComparer equalityComparer))
            {
                return null;
            }

            return valueType.GetFields()
                .FirstOrDefault(info =>
                    info.IsStatic &&
                    equalityComparer.Equals(info.GetValue(null), value)
                )?.Name;
        }
    }
}