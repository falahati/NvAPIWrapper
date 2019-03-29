using System.Collections.Generic;
using NvAPIWrapper.Native.DRS.Structures;
using NvAPIWrapper.Native.Exceptions;
using NvAPIWrapper.Native.General;
using NvAPIWrapper.Native.General.Structures;
using NvAPIWrapper.Native.Helpers;
using NvAPIWrapper.Native.Helpers.Structures;
using NvAPIWrapper.Native.Interfaces.DRS;

namespace NvAPIWrapper.Native
{
    // ReSharper disable once ClassTooBig
    public static class DRSApi
    {
        public static IDRSApplication CreateApplication(
            DRSSessionHandle sessionHandle,
            DRSProfileHandle profileHandle,
            IDRSApplication application)
        {
            using (var applicationReference = ValueTypeReference.FromValueType(application, application.GetType()))
            {
                var status = DelegateFactory.GetDelegate<Delegates.DRS.NvAPI_DRS_CreateApplication>()(
                    sessionHandle,
                    profileHandle,
                    applicationReference
                );

                if (status == Status.IncompatibleStructureVersion)
                {
                    throw new NVIDIANotSupportedException("This operation is not supported.");
                }

                if (status != Status.Ok)
                {
                    throw new NVIDIAApiException(status);
                }

                return applicationReference.ToValueType<IDRSApplication>(application.GetType());
            }
        }


        public static DRSProfileHandle CreateProfile(DRSSessionHandle sessionHandle, DRSProfileV1 profile)
        {
            using (var profileReference = ValueTypeReference.FromValueType(profile))
            {
                var status = DelegateFactory.GetDelegate<Delegates.DRS.NvAPI_DRS_CreateProfile>()(
                    sessionHandle,
                    profileReference,
                    out var profileHandle
                );

                if (status != Status.Ok)
                {
                    throw new NVIDIAApiException(status);
                }

                return profileHandle;
            }
        }

        public static DRSSessionHandle CreateSession()
        {
            var status = DelegateFactory.GetDelegate<Delegates.DRS.NvAPI_DRS_CreateSession>()(out var sessionHandle);

            if (status != Status.Ok)
            {
                throw new NVIDIAApiException(status);
            }

            return sessionHandle;
        }


        public static IDRSApplication DeleteApplication(
            DRSSessionHandle sessionHandle,
            DRSProfileHandle profileHandle,
            IDRSApplication application)
        {
            using (var applicationReference = ValueTypeReference.FromValueType(application, application.GetType()))
            {
                var status = DelegateFactory.GetDelegate<Delegates.DRS.NvAPI_DRS_DeleteApplicationEx>()(
                    sessionHandle,
                    profileHandle,
                    applicationReference
                );

                if (status == Status.IncompatibleStructureVersion)
                {
                    throw new NVIDIANotSupportedException("This operation is not supported.");
                }

                if (status != Status.Ok)
                {
                    throw new NVIDIAApiException(status);
                }

                return applicationReference.ToValueType<IDRSApplication>(application.GetType());
            }
        }

        public static void DeleteApplication(
            DRSSessionHandle sessionHandle,
            DRSProfileHandle profileHandle,
            string applicationName)
        {
            var status = DelegateFactory.GetDelegate<Delegates.DRS.NvAPI_DRS_DeleteApplication>()(
                sessionHandle,
                profileHandle,
                new UnicodeString(applicationName)
            );

            if (status == Status.IncompatibleStructureVersion)
            {
                throw new NVIDIANotSupportedException("This operation is not supported.");
            }

            if (status != Status.Ok)
            {
                throw new NVIDIAApiException(status);
            }
        }

        public static void DeleteProfile(DRSSessionHandle sessionHandle, DRSProfileHandle profileHandle)
        {
            var status = DelegateFactory.GetDelegate<Delegates.DRS.NvAPI_DRS_DeleteProfile>()(
                sessionHandle,
                profileHandle
            );

            if (status != Status.Ok)
            {
                throw new NVIDIAApiException(status);
            }
        }

        public static void DeleteProfileSetting(
            DRSSessionHandle sessionHandle,
            DRSProfileHandle profileHandle,
            uint settingId)
        {
            var status = DelegateFactory.GetDelegate<Delegates.DRS.NvAPI_DRS_DeleteProfileSetting>()(
                sessionHandle,
                profileHandle,
                settingId
            );

            if (status != Status.Ok)
            {
                throw new NVIDIAApiException(status);
            }
        }

        public static void DestroySession(DRSSessionHandle sessionHandle)
        {
            var status = DelegateFactory.GetDelegate<Delegates.DRS.NvAPI_DRS_DestroySession>()(sessionHandle);

            if (status != Status.Ok)
            {
                throw new NVIDIAApiException(status);
            }
        }

        public static IEnumerable<IDRSApplication> EnumApplications(
            DRSSessionHandle sessionHandle,
            DRSProfileHandle profileHandle)
        {
            var maxApplicationsPerRequest = 8;
            var enumApplications = DelegateFactory.GetDelegate<Delegates.DRS.NvAPI_DRS_EnumApplications>();

            foreach (var acceptType in enumApplications.Accepts())
            {
                var i = 0u;

                while (true)
                {
                    var instances = acceptType.Instantiate<IDRSApplication>().Repeat(maxApplicationsPerRequest);

                    using (var applicationsReference = ValueTypeArray.FromArray(instances, acceptType))
                    {
                        var count = (uint) instances.Length;
                        var status = enumApplications(
                            sessionHandle,
                            profileHandle,
                            i,
                            ref count,
                            applicationsReference
                        );

                        if (status == Status.IncompatibleStructureVersion)
                        {
                            break;
                        }

                        if (status == Status.EndEnumeration)
                        {
                            yield break;
                        }

                        if (status != Status.Ok)
                        {
                            throw new NVIDIAApiException(status);
                        }

                        foreach (var application in applicationsReference.ToArray<IDRSApplication>(
                            (int) count,
                            acceptType))
                        {
                            yield return application;
                            i++;
                        }

                        if (count < maxApplicationsPerRequest)
                        {
                            yield break;
                        }
                    }
                }
            }

            throw new NVIDIANotSupportedException("This operation is not supported.");
        }

        public static uint[] EnumAvailableSettingIds()
        {
            var enumAvailableSettingIds =
                DelegateFactory.GetDelegate<Delegates.DRS.NvAPI_DRS_EnumAvailableSettingIds>();
            var settingIdsCount = (uint) ushort.MaxValue;
            var settingIds = 0u.Repeat((int) settingIdsCount);

            using (var settingIdsArray = ValueTypeArray.FromArray(settingIds))
            {
                var status = enumAvailableSettingIds(settingIdsArray, ref settingIdsCount);

                if (status != Status.Ok)
                {
                    throw new NVIDIAApiException(status);
                }

                return settingIdsArray.ToArray<uint>((int) settingIdsCount);
            }
        }


        public static DRSSettingValues EnumAvailableSettingValues(uint settingId)
        {
            var enumAvailableSettingValues =
                DelegateFactory.GetDelegate<Delegates.DRS.NvAPI_DRS_EnumAvailableSettingValues>();
            var settingValuesCount = (uint) DRSSettingValues.MaxValues;
            var settingValues = typeof(DRSSettingValues).Instantiate<DRSSettingValues>();

            using (var settingValuesReference = ValueTypeReference.FromValueType(settingValues))
            {
                var status = enumAvailableSettingValues(settingId, ref settingValuesCount, settingValuesReference);

                if (status == Status.IncompatibleStructureVersion)
                {
                    throw new NVIDIANotSupportedException("This operation is not supported.");
                }

                if (status != Status.Ok)
                {
                    throw new NVIDIAApiException(status);
                }

                return settingValuesReference.ToValueType<DRSSettingValues>(typeof(DRSSettingValues));
            }
        }


        public static IEnumerable<DRSProfileHandle> EnumProfiles(DRSSessionHandle sessionHandle)
        {
            var i = 0u;

            while (true)
            {
                var status = DelegateFactory.GetDelegate<Delegates.DRS.NvAPI_DRS_EnumProfiles>()(
                    sessionHandle,
                    i,
                    out var profileHandle
                );

                if (status == Status.EndEnumeration)
                {
                    yield break;
                }

                if (status != Status.Ok)
                {
                    throw new NVIDIAApiException(status);
                }

                yield return profileHandle;
                i++;
            }
        }

        public static IEnumerable<DRSSettingV1> EnumSettings(
            DRSSessionHandle sessionHandle,
            DRSProfileHandle profileHandle)
        {
            var maxSettingsPerRequest = 32;
            var enumSettings = DelegateFactory.GetDelegate<Delegates.DRS.NvAPI_DRS_EnumSettings>();

            var i = 0u;

            while (true)
            {
                var instances = typeof(DRSSettingV1).Instantiate<DRSSettingV1>().Repeat(maxSettingsPerRequest);

                using (var applicationsReference = ValueTypeArray.FromArray(instances))
                {
                    var count = (uint) instances.Length;
                    var status = enumSettings(
                        sessionHandle,
                        profileHandle,
                        i,
                        ref count,
                        applicationsReference
                    );

                    if (status == Status.IncompatibleStructureVersion)
                    {
                        throw new NVIDIANotSupportedException("This operation is not supported.");
                    }

                    if (status == Status.EndEnumeration)
                    {
                        yield break;
                    }

                    if (status != Status.Ok)
                    {
                        throw new NVIDIAApiException(status);
                    }

                    foreach (var application in applicationsReference.ToArray<DRSSettingV1>(
                        (int) count,
                        typeof(DRSSettingV1))
                    )
                    {
                        yield return application;
                        i++;
                    }

                    if (count < maxSettingsPerRequest)
                    {
                        yield break;
                    }
                }
            }
        }

        public static IDRSApplication FindApplicationByName(
            DRSSessionHandle sessionHandle,
            string applicationName,
            out DRSProfileHandle? profileHandle)
        {
            var findApplicationByName = DelegateFactory.GetDelegate<Delegates.DRS.NvAPI_DRS_FindApplicationByName>();

            foreach (var acceptType in findApplicationByName.Accepts())
            {
                var instance = acceptType.Instantiate<IDRSApplication>();

                using (var applicationReference = ValueTypeReference.FromValueType(instance, acceptType))
                {
                    var status = findApplicationByName(
                        sessionHandle,
                        new UnicodeString(applicationName),
                        out var applicationProfileHandle,
                        applicationReference
                    );

                    if (status == Status.IncompatibleStructureVersion)
                    {
                        continue;
                    }

                    if (status == Status.ExecutableNotFound)
                    {
                        profileHandle = null;

                        return null;
                    }

                    if (status != Status.Ok)
                    {
                        throw new NVIDIAApiException(status);
                    }

                    profileHandle = applicationProfileHandle;

                    return applicationReference.ToValueType<IDRSApplication>(acceptType);
                }
            }

            throw new NVIDIANotSupportedException("This operation is not supported.");
        }

        public static DRSProfileHandle FindProfileByName(DRSSessionHandle sessionHandle, string profileName)
        {
            var status = DelegateFactory.GetDelegate<Delegates.DRS.NvAPI_DRS_FindProfileByName>()(
                sessionHandle,
                new UnicodeString(profileName),
                out var profileHandle
            );

            if (status != Status.Ok)
            {
                throw new NVIDIAApiException(status);
            }

            return profileHandle;
        }

        public static IDRSApplication GetApplicationInfo(
            DRSSessionHandle sessionHandle,
            DRSProfileHandle profileHandle,
            string applicationName)
        {
            var getApplicationInfo = DelegateFactory.GetDelegate<Delegates.DRS.NvAPI_DRS_GetApplicationInfo>();

            foreach (var acceptType in getApplicationInfo.Accepts())
            {
                var instance = acceptType.Instantiate<IDRSApplication>();

                using (var applicationReference = ValueTypeReference.FromValueType(instance, acceptType))
                {
                    var status = getApplicationInfo(
                        sessionHandle,
                        profileHandle,
                        new UnicodeString(applicationName),
                        applicationReference
                    );

                    if (status == Status.IncompatibleStructureVersion)
                    {
                        continue;
                    }

                    if (status == Status.ExecutableNotFound)
                    {
                        return null;
                    }

                    if (status != Status.Ok)
                    {
                        throw new NVIDIAApiException(status);
                    }

                    return applicationReference.ToValueType<IDRSApplication>(acceptType);
                }
            }

            throw new NVIDIANotSupportedException("This operation is not supported.");
        }


        public static DRSProfileHandle GetBaseProfile(DRSSessionHandle sessionHandle)
        {
            var status = DelegateFactory.GetDelegate<Delegates.DRS.NvAPI_DRS_GetBaseProfile>()(
                sessionHandle,
                out var profileHandle
            );

            if (status != Status.Ok)
            {
                throw new NVIDIAApiException(status);
            }

            return profileHandle;
        }


        public static DRSProfileHandle GetCurrentGlobalProfile(DRSSessionHandle sessionHandle)
        {
            var status = DelegateFactory.GetDelegate<Delegates.DRS.NvAPI_DRS_GetCurrentGlobalProfile>()(
                sessionHandle,
                out var profileHandle
            );

            if (status != Status.Ok)
            {
                throw new NVIDIAApiException(status);
            }

            return profileHandle;
        }

        public static int GetNumberOfProfiles(DRSSessionHandle sessionHandle)
        {
            var status = DelegateFactory.GetDelegate<Delegates.DRS.NvAPI_DRS_GetNumProfiles>()(
                sessionHandle,
                out var profileCount
            );

            if (status != Status.Ok)
            {
                throw new NVIDIAApiException(status);
            }

            return (int) profileCount;
        }

        public static DRSProfileV1 GetProfileInfo(DRSSessionHandle sessionHandle, DRSProfileHandle profileHandle)
        {
            var profile = typeof(DRSProfileV1).Instantiate<DRSProfileV1>();

            using (var profileReference = ValueTypeReference.FromValueType(profile))
            {
                var status = DelegateFactory.GetDelegate<Delegates.DRS.NvAPI_DRS_GetProfileInfo>()(
                    sessionHandle,
                    profileHandle,
                    profileReference
                );

                if (status != Status.Ok)
                {
                    throw new NVIDIAApiException(status);
                }

                return profileReference.ToValueType<DRSProfileV1>().GetValueOrDefault();
            }
        }


        public static DRSSettingV1? GetSetting(
            DRSSessionHandle sessionHandle,
            DRSProfileHandle profileHandle,
            uint settingId)
        {
            var getSetting = DelegateFactory.GetDelegate<Delegates.DRS.NvAPI_DRS_GetSetting>();

            var instance = typeof(DRSSettingV1).Instantiate<DRSSettingV1>();

            using (var settingReference = ValueTypeReference.FromValueType(instance, typeof(DRSSettingV1)))
            {
                var status = getSetting(
                    sessionHandle,
                    profileHandle,
                    settingId,
                    settingReference
                );

                if (status == Status.IncompatibleStructureVersion)
                {
                    throw new NVIDIANotSupportedException("This operation is not supported.");
                }

                if (status == Status.SettingNotFound)
                {
                    return null;
                }

                if (status != Status.Ok)
                {
                    throw new NVIDIAApiException(status);
                }

                return settingReference.ToValueType<DRSSettingV1>(typeof(DRSSettingV1));
            }
        }

        public static uint GetSettingIdFromName(string settingName)
        {
            var status = DelegateFactory.GetDelegate<Delegates.DRS.NvAPI_DRS_GetSettingIdFromName>()(
                new UnicodeString(settingName),
                out var settingId
            );

            if (status != Status.Ok)
            {
                throw new NVIDIAApiException(status);
            }

            return settingId;
        }


        public static string GetSettingNameFromId(uint settingId)
        {
            var status = DelegateFactory.GetDelegate<Delegates.DRS.NvAPI_DRS_GetSettingNameFromId>()(
                settingId,
                out var settingName
            );

            if (status != Status.Ok)
            {
                throw new NVIDIAApiException(status);
            }

            return settingName.Value;
        }

        public static void LoadSettings(DRSSessionHandle sessionHandle)
        {
            var status = DelegateFactory.GetDelegate<Delegates.DRS.NvAPI_DRS_LoadSettings>()(sessionHandle);

            if (status != Status.Ok)
            {
                throw new NVIDIAApiException(status);
            }
        }

        public static void LoadSettings(DRSSessionHandle sessionHandle, string fileName)
        {
            var unicodeFileName = new UnicodeString(fileName);
            var status =
                DelegateFactory.GetDelegate<Delegates.DRS.NvAPI_DRS_LoadSettingsFromFile>()(sessionHandle,
                    unicodeFileName);

            if (status != Status.Ok)
            {
                throw new NVIDIAApiException(status);
            }
        }

        public static void RestoreDefaults(DRSSessionHandle sessionHandle)
        {
            var status = DelegateFactory.GetDelegate<Delegates.DRS.NvAPI_DRS_RestoreAllDefaults>()(
                sessionHandle
            );

            if (status != Status.Ok)
            {
                throw new NVIDIAApiException(status);
            }
        }

        public static void RestoreDefaults(
            DRSSessionHandle sessionHandle,
            DRSProfileHandle profileHandle)
        {
            var status = DelegateFactory.GetDelegate<Delegates.DRS.NvAPI_DRS_RestoreProfileDefault>()(
                sessionHandle,
                profileHandle
            );

            if (status != Status.Ok)
            {
                throw new NVIDIAApiException(status);
            }
        }

        public static void RestoreDefaults(
            DRSSessionHandle sessionHandle,
            DRSProfileHandle profileHandle,
            uint settingId)
        {
            var status = DelegateFactory.GetDelegate<Delegates.DRS.NvAPI_DRS_RestoreProfileDefaultSetting>()(
                sessionHandle,
                profileHandle,
                settingId
            );

            if (status != Status.Ok)
            {
                throw new NVIDIAApiException(status);
            }
        }


        public static void SaveSettings(DRSSessionHandle sessionHandle)
        {
            var status = DelegateFactory.GetDelegate<Delegates.DRS.NvAPI_DRS_SaveSettings>()(sessionHandle);

            if (status != Status.Ok)
            {
                throw new NVIDIAApiException(status);
            }
        }

        public static void SaveSettings(DRSSessionHandle sessionHandle, string fileName)
        {
            var status = DelegateFactory.GetDelegate<Delegates.DRS.NvAPI_DRS_SaveSettingsToFile>()(
                sessionHandle,
                new UnicodeString(fileName)
            );

            if (status != Status.Ok)
            {
                throw new NVIDIAApiException(status);
            }
        }

        public static void SetCurrentGlobalProfile(DRSSessionHandle sessionHandle, string profileName)
        {
            var status = DelegateFactory.GetDelegate<Delegates.DRS.NvAPI_DRS_SetCurrentGlobalProfile>()(
                sessionHandle,
                new UnicodeString(profileName)
            );

            if (status != Status.Ok)
            {
                throw new NVIDIAApiException(status);
            }
        }

        public static void SetProfileInfo(
            DRSSessionHandle sessionHandle,
            DRSProfileHandle profileHandle,
            DRSProfileV1 profile)
        {
            using (var profileReference = ValueTypeReference.FromValueType(profile))
            {
                var status = DelegateFactory.GetDelegate<Delegates.DRS.NvAPI_DRS_SetProfileInfo>()(
                    sessionHandle,
                    profileHandle,
                    profileReference
                );

                if (status != Status.Ok)
                {
                    throw new NVIDIAApiException(status);
                }
            }
        }


        public static void SetSetting(
            DRSSessionHandle sessionHandle,
            DRSProfileHandle profileHandle,
            DRSSettingV1 setting)
        {
            var setSetting = DelegateFactory.GetDelegate<Delegates.DRS.NvAPI_DRS_SetSetting>();

            using (var settingReference = ValueTypeReference.FromValueType(setting, setting.GetType()))
            {
                var status = setSetting(
                    sessionHandle,
                    profileHandle,
                    settingReference
                );

                if (status == Status.IncompatibleStructureVersion)
                {
                    throw new NVIDIANotSupportedException("This operation is not supported.");
                }

                if (status != Status.Ok)
                {
                    throw new NVIDIAApiException(status);
                }
            }
        }
    }
}