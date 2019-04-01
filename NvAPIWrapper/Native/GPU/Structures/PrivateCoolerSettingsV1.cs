using System.Linq;
using System.Runtime.InteropServices;
using NvAPIWrapper.Native.Attributes;
using NvAPIWrapper.Native.General.Structures;
using NvAPIWrapper.Native.Interfaces;

namespace NvAPIWrapper.Native.GPU.Structures
{
    [StructLayout(LayoutKind.Sequential, Pack = 8)]
    [StructureVersion(1)]
    public struct PrivateCoolerSettingsV1 : IInitializable
    {
        internal const int MaxNumberOfCoolersPerGPU = 3;

        internal StructureVersion _Version;
        internal readonly uint _CoolerSettingsCount;

        [MarshalAs(UnmanagedType.ByValArray, SizeConst = MaxNumberOfCoolersPerGPU)]
        internal readonly CoolerSetting[] _CoolerSettings;

        public CoolerSetting[] CoolerSettings
        {
            get => _CoolerSettings.Take((int) _CoolerSettingsCount).ToArray();
        }

        [StructLayout(LayoutKind.Sequential, Pack = 8)]
        public struct CoolerSetting
        {
            internal CoolerType _CoolerType;
            internal CoolerController _CoolerController;
            internal uint _DefaultMinimumLevel;
            internal uint _DefaultMaximumLevel;
            internal uint _CurrentMinimumLevel;
            internal uint _CurrentMaximumLevel;
            internal uint _CurrentLevel;
            internal CoolerPolicy _DefaultPolicy;
            internal CoolerPolicy _CurrentPolicy;
            internal CoolerTarget _Target;
            internal CoolerControl _ControlType;
            internal uint _IsActive;

            public uint CurrentLevel
            {
                get => _CurrentLevel;
            }

            public uint DefaultMinimumLevel
            {
                get => _DefaultMinimumLevel;
            }

            public uint DefaultMaximumLevel
            {
                get => _DefaultMaximumLevel;
            }

            public uint CurrentMinimumLevel
            {
                get => _CurrentMinimumLevel;
            }

            public uint CurrentMaximumLevel
            {
                get => _CurrentMaximumLevel;
            }

            public CoolerType CoolerType
            {
                get => _CoolerType;
            }

            public CoolerController CoolerController
            {
                get => _CoolerController;
            }

            public CoolerPolicy DefaultPolicy
            {
                get => _DefaultPolicy;
            }

            public CoolerPolicy CurrentPolicy
            {
                get => _CurrentPolicy;
            }

            public CoolerTarget Target
            {
                get => _Target;
            }

            public CoolerControl ControlType
            {
                get => _ControlType;
            }

            public bool IsActive
            {
                get => _IsActive > 0;
            }
        }
    }
}