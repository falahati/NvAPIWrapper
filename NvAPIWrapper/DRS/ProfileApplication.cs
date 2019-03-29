using System;
using NvAPIWrapper.Native;
using NvAPIWrapper.Native.DRS.Structures;
using NvAPIWrapper.Native.Exceptions;
using NvAPIWrapper.Native.Interfaces.DRS;

namespace NvAPIWrapper.DRS
{
    public class ProfileApplication
    {
        private IDRSApplication _application;

        internal ProfileApplication(IDRSApplication application, DriverSettingsProfile profile)
        {
            Profile = profile;
            _application = application;
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
                return $"{ApplicationName} (Predefined)";
            }
            
            return ApplicationName;
        }

        public string ApplicationName
        {
            get
            {
                if (!IsValid)
                {
                    throw new InvalidOperationException(
                        "Can not perform this operation with an invalid application instance."
                    );
                }

                return _application.ApplicationName;
            }
        }

        public string CommandLine
        {
            get
            {
                if (!IsValid)
                {
                    throw new InvalidOperationException(
                        "Can not perform this operation with an invalid application instance."
                    );
                }

                if (_application is DRSApplicationV4 applicationV4)
                {
                    return applicationV4.ApplicationCommandLine;
                }

                return null;
            }
        }

        public string[] FilesInFolder
        {
            get
            {
                if (!IsValid)
                {
                    throw new InvalidOperationException(
                        "Can not perform this operation with an invalid application instance."
                    );
                }

                if (_application is DRSApplicationV2 applicationV2)
                {
                    return applicationV2.FilesInFolder;
                }

                if (_application is DRSApplicationV3 applicationV3)
                {
                    return applicationV3.FilesInFolder;
                }

                if (_application is DRSApplicationV4 applicationV4)
                {
                    return applicationV4.FilesInFolder;
                }

                return null;
            }
        }

        public string FriendlyName
        {
            get
            {
                if (!IsValid)
                {
                    throw new InvalidOperationException(
                        "Can not perform this operation with an invalid application instance."
                    );
                }

                return _application.FriendlyName;
            }
        }

        public bool? HasCommandLine
        {
            get
            {
                if (!IsValid)
                {
                    throw new InvalidOperationException(
                        "Can not perform this operation with an invalid application instance."
                    );
                }

                if (_application is DRSApplicationV3 applicationV3)
                {
                    return applicationV3.HasCommandLine;
                }

                if (_application is DRSApplicationV4 applicationV4)
                {
                    return applicationV4.HasCommandLine;
                }

                return null;
            }
        }

        public bool? IsMetroApplication
        {
            get
            {
                if (!IsValid)
                {
                    throw new InvalidOperationException(
                        "Can not perform this operation with an invalid application instance."
                    );
                }

                if (_application is DRSApplicationV3 applicationV3)
                {
                    return applicationV3.IsMetroApplication;
                }

                if (_application is DRSApplicationV4 applicationV4)
                {
                    return applicationV4.IsMetroApplication;
                }

                return null;
            }
        }

        public bool IsPredefined
        {
            get
            {
                if (!IsValid)
                {
                    throw new InvalidOperationException(
                        "Can not perform this operation with an invalid application instance."
                    );
                }

                return _application.IsPredefined;
            }
        }

        public bool IsValid
        {
            get => _application != null && Profile.IsValid;
        }

        public string LauncherName
        {
            get
            {
                if (!IsValid)
                {
                    throw new InvalidOperationException(
                        "Can not perform this operation with an invalid application instance."
                    );
                }

                return _application.LauncherName;
            }
        }

        public DriverSettingsProfile Profile { get; }

        // ReSharper disable once TooManyArguments
        // ReSharper disable once FunctionComplexityOverflow
        public static ProfileApplication CreateApplication(
            DriverSettingsProfile profile,
            string applicationName,
            string friendlyName = null,
            string launcherName = null,
            string[] fileInFolders = null,
            bool isMetro = false,
            string commandLine = null
        )
        {
            var createDelegates = new Func<string, string, string, string[], bool, string, IDRSApplication>[]
            {
                CreateApplicationInstanceV4,
                CreateApplicationInstanceV3,
                CreateApplicationInstanceV2,
                CreateApplicationInstanceV1
            };

            Exception lastException = null;
            IDRSApplication application = null;

            foreach (var func in createDelegates)
            {
                try
                {
                    application = func(
                        applicationName,
                        friendlyName,
                        launcherName,
                        fileInFolders,
                        isMetro,
                        commandLine
                    );

                    break;
                }
                catch (NVIDIANotSupportedException e)
                {
                    // ignore
                    lastException = e;
                }
            }

            if (application == null)
            {
                // ReSharper disable once ThrowingSystemException
                throw lastException;
            }

            application = DRSApi.CreateApplication(profile.Session.Handle, profile.Handle, application);

            return new ProfileApplication(application, profile);
        }

        // ReSharper disable once TooManyArguments
        private static IDRSApplication CreateApplicationInstanceV1(
            string applicationName,
            string friendlyName = null,
            string launcherName = null,
            string[] fileInFolders = null,
            bool isMetro = false,
            string commandLine = null
        )
        {
            if (!string.IsNullOrWhiteSpace(commandLine))
            {
                throw new NotSupportedException(
                    "CommandLine is not supported with the current execution environment."
                );
            }

            if (fileInFolders?.Length > 0)
            {
                throw new NotSupportedException(
                    "Same folder file presence check is not supported with the current execution environment."
                );
            }

            return new DRSApplicationV1(
                applicationName,
                friendlyName,
                launcherName
            );
        }

        // ReSharper disable once TooManyArguments
        private static IDRSApplication CreateApplicationInstanceV2(
            string applicationName,
            string friendlyName = null,
            string launcherName = null,
            string[] fileInFolders = null,
            bool isMetro = false,
            string commandLine = null
        )
        {
            if (!string.IsNullOrWhiteSpace(commandLine))
            {
                throw new NotSupportedException(
                    "CommandLine is not supported with the current execution environment."
                );
            }

            return new DRSApplicationV2(
                applicationName,
                friendlyName,
                launcherName,
                fileInFolders
            );
        }

        // ReSharper disable once TooManyArguments
        private static IDRSApplication CreateApplicationInstanceV3(
            string applicationName,
            string friendlyName = null,
            string launcherName = null,
            string[] fileInFolders = null,
            bool isMetro = false,
            string commandLine = null
        )
        {
            if (!string.IsNullOrWhiteSpace(commandLine))
            {
                throw new NotSupportedException(
                    "CommandLine is not supported with the current execution environment."
                );
            }

            return new DRSApplicationV3(
                applicationName,
                friendlyName,
                launcherName,
                fileInFolders,
                isMetro
            );
        }

        // ReSharper disable once TooManyArguments
        private static IDRSApplication CreateApplicationInstanceV4(
            string applicationName,
            string friendlyName = null,
            string launcherName = null,
            string[] fileInFolders = null,
            bool isMetro = false,
            string commandLine = null
        )
        {
            return new DRSApplicationV4(
                applicationName,
                friendlyName,
                launcherName,
                fileInFolders,
                isMetro,
                commandLine
            );
        }

        public void Delete()
        {
            if (!IsValid)
            {
                throw new InvalidOperationException(
                    "Can not perform this operation with an invalid application instance."
                );
            }

            DRSApi.DeleteApplication(Profile.Session.Handle, Profile.Handle, _application);
            _application = null;
        }
    }
}