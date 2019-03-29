using System;
using System.Collections.Generic;
using System.Linq;
using NvAPIWrapper.Native;
using NvAPIWrapper.Native.DRS;
using NvAPIWrapper.Native.DRS.Structures;

namespace NvAPIWrapper.DRS
{
    public class DriverSettingsProfile
    {
        internal DriverSettingsProfile(DRSProfileHandle handle, DriverSettingsSession parentSession)
        {
            Handle = handle;
            Session = parentSession;
        }

        public IEnumerable<ProfileApplication> Applications
        {
            get
            {
                if (!IsValid)
                {
                    throw new InvalidOperationException(
                        "Can not perform this operation with an invalid profile instance."
                    );
                }

                return DRSApi.EnumApplications(Session.Handle, Handle)
                    .Select(application => new ProfileApplication(application, this));
            }
        }

        public DRSGPUSupport GPUSupport
        {
            get
            {
                if (!IsValid)
                {
                    throw new InvalidOperationException(
                        "Can not perform this operation with an invalid profile instance."
                    );
                }

                var profileInfo = DRSApi.GetProfileInfo(Session.Handle, Handle);

                return profileInfo.GPUSupport;
            }
            set
            {
                if (!IsValid)
                {
                    throw new InvalidOperationException(
                        "Can not perform this operation with an invalid profile instance."
                    );
                }

                var profileInfo = DRSApi.GetProfileInfo(Session.Handle, Handle);
                profileInfo.GPUSupport = value;
                DRSApi.SetProfileInfo(Session.Handle, Handle, profileInfo);
            }
        }

        public DRSProfileHandle Handle { get; private set; }

        public bool IsPredefined
        {
            get
            {
                if (!IsValid)
                {
                    throw new InvalidOperationException(
                        "Can not perform this operation with an invalid profile instance."
                    );
                }

                var profileInfo = DRSApi.GetProfileInfo(Session.Handle, Handle);

                return profileInfo.IsPredefined;
            }
        }

        public bool IsValid
        {
            get => !Handle.IsNull;
        }

        public string Name
        {
            get
            {
                if (!IsValid)
                {
                    throw new InvalidOperationException(
                        "Can not perform this operation with an invalid profile instance."
                    );
                }

                var profileInfo = DRSApi.GetProfileInfo(Session.Handle, Handle);

                return profileInfo.Name;
            }
        }

        public int NumberOfApplications
        {
            get
            {
                if (!IsValid)
                {
                    throw new InvalidOperationException(
                        "Can not perform this operation with an invalid profile instance."
                    );
                }

                var profileInfo = DRSApi.GetProfileInfo(Session.Handle, Handle);

                return profileInfo.NumberOfApplications;
            }
        }

        public int NumberOfSettings
        {
            get
            {
                if (!IsValid)
                {
                    throw new InvalidOperationException(
                        "Can not perform this operation with an invalid profile instance."
                    );
                }

                var profileInfo = DRSApi.GetProfileInfo(Session.Handle, Handle);

                return profileInfo.NumberOfSettings;
            }
        }

        public DriverSettingsSession Session { get; }

        public IEnumerable<ProfileSetting> Settings
        {
            get
            {
                if (!IsValid)
                {
                    throw new InvalidOperationException(
                        "Can not perform this operation with an invalid profile instance."
                    );
                }

                return DRSApi.EnumSettings(Session.Handle, Handle).Select(setting => new ProfileSetting(setting));
            }
        }

        public static DriverSettingsProfile CreateProfile(
            DriverSettingsSession session,
            string profileName,
            DRSGPUSupport? gpuSupport = null)
        {
            gpuSupport = gpuSupport ?? new DRSGPUSupport();
            var profileInfo = new DRSProfileV1(profileName, gpuSupport.Value);
            var profileHandle = DRSApi.CreateProfile(session.Handle, profileInfo);

            return new DriverSettingsProfile(profileHandle, session);
        }

        /// <inheritdoc />
        public override string ToString()
        {
            if (!IsValid)
            {
                return "[Invalid]";
            }

            if (IsPredefined)
            {
                return $"{Name} (Predefined)";
            }

            return Name;
        }

        public void Delete()
        {
            if (!IsValid)
            {
                throw new InvalidOperationException(
                    "Can not perform this operation with an invalid profile instance."
                );
            }

            DRSApi.DeleteProfile(Session.Handle, Handle);
            Handle = DRSProfileHandle.DefaultHandle;
        }

        public void DeleteApplicationByName(string applicationName)
        {
            if (!IsValid)
            {
                throw new InvalidOperationException(
                    "Can not perform this operation with an invalid profile instance."
                );
            }

            DRSApi.DeleteApplication(Session.Handle, Handle, applicationName);
        }

        public void DeleteSetting(uint settingId)
        {
            if (!IsValid)
            {
                throw new InvalidOperationException(
                    "Can not perform this operation with an invalid profile instance."
                );
            }

            DRSApi.DeleteProfileSetting(Session.Handle, Handle, settingId);
        }

        public void DeleteSetting(KnownSettingId settingId)
        {
            DeleteSetting(SettingInfo.GetSettingId(settingId));
        }

        public ProfileApplication GetApplicationByName(string applicationName)
        {
            if (!IsValid)
            {
                throw new InvalidOperationException(
                    "Can not perform this operation with an invalid profile instance."
                );
            }

            var application = DRSApi.GetApplicationInfo(Session.Handle, Handle, applicationName);

            if (application == null)
            {
                return null;
            }

            return new ProfileApplication(application, this);
        }

        public ProfileSetting GetSetting(uint settingId)
        {
            if (!IsValid)
            {
                throw new InvalidOperationException(
                    "Can not perform this operation with an invalid profile instance."
                );
            }

            var setting = DRSApi.GetSetting(Session.Handle, Handle, settingId);

            if (setting == null)
            {
                return null;
            }

            return new ProfileSetting(setting.Value);
        }

        public ProfileSetting GetSetting(KnownSettingId settingId)
        {
            return GetSetting(SettingInfo.GetSettingId(settingId));
        }

        public void RestoreDefaults()
        {
            if (!IsValid)
            {
                throw new InvalidOperationException(
                    "Can not perform this operation with an invalid profile instance."
                );
            }

            DRSApi.RestoreDefaults(Session.Handle, Handle);
        }

        public void RestoreSettingToDefault(uint settingId)
        {
            if (!IsValid)
            {
                throw new InvalidOperationException(
                    "Can not perform this operation with an invalid profile instance."
                );
            }

            DRSApi.RestoreDefaults(Session.Handle, Handle, settingId);
        }

        public void RestoreSettingToDefault(KnownSettingId settingId)
        {
            RestoreSettingToDefault(SettingInfo.GetSettingId(settingId));
        }


        public void SetSetting(KnownSettingId settingId, DRSSettingType settingType, object value)
        {
            SetSetting(SettingInfo.GetSettingId(settingId), settingType, value);
        }

        public void SetSetting(uint settingId, DRSSettingType settingType, object value)
        {
            if (!IsValid)
            {
                throw new InvalidOperationException(
                    "Can not perform this operation with an invalid profile instance."
                );
            }

            var setting = new DRSSettingV1(settingId, settingType, value);

            DRSApi.SetSetting(Session.Handle, Handle, setting);
        }
    }
}