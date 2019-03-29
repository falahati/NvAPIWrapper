using System;
using System.Runtime.InteropServices;
using NvAPIWrapper.Native.Attributes;
using NvAPIWrapper.Native.General.Structures;
using NvAPIWrapper.Native.Helpers;
using NvAPIWrapper.Native.Interfaces;
using NvAPIWrapper.Native.Interfaces.DRS;

namespace NvAPIWrapper.Native.DRS.Structures
{
    [StructLayout(LayoutKind.Sequential, Pack = 8)]
    [StructureVersion(4)]
    public struct DRSApplicationV4 : IInitializable, IDRSApplication
    {
        public const char FileInFolderSeparator = DRSApplicationV3.FileInFolderSeparator;
        internal StructureVersion _Version;
        internal uint _IsPredefined;
        internal UnicodeString _ApplicationName;
        internal UnicodeString _FriendlyName;
        internal UnicodeString _LauncherName;
        internal UnicodeString _FileInFolder;
        internal uint _Flags;
        internal UnicodeString _CommandLine;

        // ReSharper disable once TooManyDependencies
        public DRSApplicationV4(
            string applicationName,
            string friendlyName = null,
            string launcherName = null,
            string[] fileInFolders = null,
            bool isMetro = false,
            string commandLine = null
        )
        {
            this = typeof(DRSApplicationV4).Instantiate<DRSApplicationV4>();
            IsPredefined = false;
            ApplicationName = applicationName;
            FriendlyName = friendlyName ?? string.Empty;
            LauncherName = launcherName ?? string.Empty;
            FilesInFolder = fileInFolders ?? new string[0];
            IsMetroApplication = isMetro;
            ApplicationCommandLine = commandLine ?? string.Empty;
        }


        public bool IsPredefined
        {
            get => _IsPredefined > 0;
            private set => _IsPredefined = value ? 1u : 0u;
        }

        public bool IsMetroApplication
        {
            get => _Flags.GetBit(0);
            private set => _Flags = _Flags.SetBit(0, value);
        }

        public bool HasCommandLine
        {
            get => _Flags.GetBit(1);
            private set => _Flags = _Flags.SetBit(1, value);
        }

        public string ApplicationName
        {
            get => _ApplicationName.Value;
            private set
            {
                if (string.IsNullOrEmpty(value))
                {
                    throw new ArgumentException("Name can not be empty or null.");
                }

                _ApplicationName = new UnicodeString(value);
            }
        }

        public string ApplicationCommandLine
        {
            get => (HasCommandLine ? _CommandLine.Value : null) ?? string.Empty;
            private set
            {
                if (string.IsNullOrEmpty(value))
                {
                    _CommandLine = new UnicodeString(null);

                    if (HasCommandLine)
                    {
                        HasCommandLine = false;
                    }
                }
                else
                {
                    _CommandLine = new UnicodeString(value);

                    if (!HasCommandLine)
                    {
                        HasCommandLine = true;
                    }
                }
            }
        }

        public string FriendlyName
        {
            get => _FriendlyName.Value;
            private set => _FriendlyName = new UnicodeString(value);
        }

        public string LauncherName
        {
            get => _LauncherName.Value;
            private set => _LauncherName = new UnicodeString(value);
        }

        public string[] FilesInFolder
        {
            get => _FileInFolder.Value?.Split(new[] {FileInFolderSeparator}, StringSplitOptions.RemoveEmptyEntries) ??
                   new string[0];
            private set => _FileInFolder = new UnicodeString(string.Join(FileInFolderSeparator.ToString(), value));
        }
    }
}