using System;
using System.Collections.Generic;
using System.Linq;
using NvAPIWrapper.Native;
using NvAPIWrapper.Native.DRS.Structures;

namespace NvAPIWrapper.DRS
{
    public class DriverSettingsSession : IDisposable
    {
        internal DriverSettingsSession(DRSSessionHandle handle)
        {
            Handle = handle;
        }

        private DriverSettingsSession() : this(DRSApi.CreateSession())
        {
        }

        /// <inheritdoc />
        public override string ToString()
        {
            return $"{Handle} ({NumberOfProfiles} Profiles)";
        }

        public DRSSessionHandle Handle { get; }

        public IEnumerable<DriverSettingsProfile> Profiles
        {
            get { return DRSApi.EnumProfiles(Handle).Select(handle => new DriverSettingsProfile(handle, this)); }
        }

        public void RestoreDefaults()
        {
            DRSApi.RestoreDefaults(Handle);
        }

        public int NumberOfProfiles
        {
            get => DRSApi.GetNumberOfProfiles(Handle);
        }

        public DriverSettingsProfile FindApplicationProfile(string applicationName)
        {
            var application = DRSApi.FindApplicationByName(Handle, applicationName, out var profileHandle);

            if (application == null || !profileHandle.HasValue || profileHandle.Value.IsNull)
            {
                return null;
            }

            return new DriverSettingsProfile(profileHandle.Value, this);
        }

        public ProfileApplication FindApplication(string applicationName)
        {
            var application = DRSApi.FindApplicationByName(Handle, applicationName, out var profileHandle);

            if (application == null || !profileHandle.HasValue || profileHandle.Value.IsNull)
            {
                return null;
            }

            var profile = new DriverSettingsProfile(profileHandle.Value, this);
            return new ProfileApplication(application, profile);
        }

        public DriverSettingsProfile BaseProfile
        {
            get
            {
                var profileHandle = DRSApi.GetBaseProfile(Handle);

                if (profileHandle.IsNull)
                {
                    return null;
                }

                return new DriverSettingsProfile(profileHandle, this);
            }
        }

        public DriverSettingsProfile CurrentGlobalProfile
        {
            get
            {
                var profileHandle = DRSApi.GetBaseProfile(Handle);

                if (profileHandle.IsNull)
                {
                    return null;
                }

                return new DriverSettingsProfile(profileHandle, this);
            }
            set
            {
                if (value == null)
                {
                    throw new ArgumentNullException(nameof(value));
                }

                if (string.IsNullOrEmpty(value.Name))
                {
                    throw new ArgumentException("Profile name can not be empty.", nameof(value));
                }

                DRSApi.SetCurrentGlobalProfile(Handle, value.Name);
            }
        }

        public DriverSettingsProfile FindProfileByName(string profileName)
        {
            var profileHandle = DRSApi.FindProfileByName(Handle, profileName);

            if (profileHandle.IsNull)
            {
                return null;
            }

            return new DriverSettingsProfile(profileHandle, this);
        }

        /// <inheritdoc />
        public void Dispose()
        {
            ReleaseUnmanagedResources();
            GC.SuppressFinalize(this);
        }

        public static DriverSettingsSession CreateAndLoad()
        {
            var session = new DriverSettingsSession();
            session.Load();

            return session;
        }

        public static DriverSettingsSession CreateAndLoad(string fileName)
        {
            var session = new DriverSettingsSession();
            session.Load(fileName);

            return session;
        }

        public void Save()
        {
            DRSApi.SaveSettings(Handle);
        }

        public void Save(string fileName)
        {
            DRSApi.SaveSettings(Handle, fileName);
        }

        private void Load()
        {
            DRSApi.LoadSettings(Handle);
        }

        private void Load(string fileName)
        {
            DRSApi.LoadSettings(Handle, fileName);
        }

        private void ReleaseUnmanagedResources()
        {
            DRSApi.DestroySession(Handle);
        }

        /// <inheritdoc />
        ~DriverSettingsSession()
        {
            ReleaseUnmanagedResources();
        }
    }
}