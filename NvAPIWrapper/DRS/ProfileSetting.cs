using System;
using NvAPIWrapper.Native.DRS;
using NvAPIWrapper.Native.DRS.Structures;

namespace NvAPIWrapper.DRS
{
    public class ProfileSetting
    {
        private readonly DRSSettingV1 _setting;

        internal ProfileSetting(DRSSettingV1 setting)
        {
            _setting = setting;
        }

        public object CurrentValue
        {
            get
            {
                if (IsPredefinedValueValid && IsCurrentValuePredefined)
                {
                    return _setting.PredefinedValue;
                }

                return _setting.CurrentValue;
            }
        }

        public bool IsCurrentValuePredefined
        {
            get => _setting.IsCurrentValuePredefined;
        }

        public bool IsPredefinedValueValid
        {
            get => _setting.IsPredefinedValueValid;
        }

        public object PredefinedValue
        {
            get
            {
                if (!IsPredefinedValueValid)
                {
                    throw new InvalidOperationException("Predefined value is not valid.");
                }

                return _setting.PredefinedValue;
            }
        }

        public uint SettingId
        {
            get => _setting.Id;
        }

        public SettingInfo SettingInfo
        {
            get => SettingInfo.FromId(SettingId);
        }

        public DRSSettingLocation SettingLocation
        {
            get => _setting.SettingLocation;
        }

        public DRSSettingType SettingType
        {
            get => _setting.SettingType;
        }

        /// <inheritdoc />
        public override string ToString()
        {
            string settingName = null;

            try
            {
                settingName = SettingInfo.Name;
            }
            catch
            {
                // ignore;
            }

            if (string.IsNullOrWhiteSpace(settingName))
            {
                settingName = $"#{SettingId:X}";
            }

            if (IsCurrentValuePredefined)
            {
                return $"{settingName} = {CurrentValue ?? "[NULL]"} (Predefined)";
            }

            return $"{settingName} = {CurrentValue ?? "[NULL]"}";
        }
    }
}