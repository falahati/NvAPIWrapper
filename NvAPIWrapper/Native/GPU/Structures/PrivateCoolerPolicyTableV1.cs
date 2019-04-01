using System;
using System.Runtime.InteropServices;
using NvAPIWrapper.Native.Attributes;
using NvAPIWrapper.Native.General.Structures;
using NvAPIWrapper.Native.Helpers;
using NvAPIWrapper.Native.Interfaces;

namespace NvAPIWrapper.Native.GPU.Structures
{
    [StructLayout(LayoutKind.Sequential, Pack = 8)]
    [StructureVersion(1)]
    public struct PrivateCoolerPolicyTableV1 : IInitializable
    {
        internal const int MaxNumberOfPolicyLevels = 24;

        internal StructureVersion _Version;
        internal CoolerPolicy _Policy;

        [MarshalAs(UnmanagedType.ByValArray, SizeConst = MaxNumberOfPolicyLevels)]
        internal readonly PolicyLevel[] _PolicyLevels;

        public PolicyLevel[] PolicyLevels
        {
            get => _PolicyLevels;
        }

        public CoolerPolicy Policy
        {
            get => _Policy;
        }

        public PrivateCoolerPolicyTableV1(CoolerPolicy policy, PolicyLevel[] policyLevels)
        {
            if (policyLevels?.Length > MaxNumberOfPolicyLevels)
            {
                throw new ArgumentException($"Maximum of {MaxNumberOfPolicyLevels} policy levels are configurable.",
                    nameof(policyLevels));
            }

            if (policyLevels == null || policyLevels.Length == 0)
            {
                throw new ArgumentException("Array is null or empty.", nameof(policyLevels));
            }

            this = typeof(PrivateCoolerPolicyTableV1).Instantiate<PrivateCoolerPolicyTableV1>();
            _Policy = policy;
            Array.Copy(policyLevels, 0, _PolicyLevels, 0, policyLevels.Length);
        }

        [StructLayout(LayoutKind.Sequential, Pack = 8)]
        public struct PolicyLevel
        {
            internal uint _LevelId;
            internal uint _CurrentLevel;
            internal uint _DefaultLevel;

            public uint LevelId
            {
                get => _LevelId;
            }

            public uint CurrentLevel
            {
                get => _CurrentLevel;
            }

            public uint DefaultLevel
            {
                get => _DefaultLevel;
            }

            public PolicyLevel(uint levelId, uint currentLevel, uint defaultLevel)
            {
                _LevelId = levelId;
                _CurrentLevel = currentLevel;
                _DefaultLevel = defaultLevel;
            }
        }
    }
}