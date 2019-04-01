using System;
using System.Linq;
using System.Runtime.InteropServices;
using NvAPIWrapper.Native.Attributes;
using NvAPIWrapper.Native.General.Structures;
using NvAPIWrapper.Native.Helpers;
using NvAPIWrapper.Native.Interfaces;

namespace NvAPIWrapper.Native.GPU.Structures
{
    [StructLayout(LayoutKind.Sequential, Pack = 8)]
    [StructureVersion(1)]
    public struct PrivateCoolerLevelsV1 : IInitializable
    {
        internal const int MaxNumberOfCoolersPerGPU = PrivateCoolerSettingsV1.MaxNumberOfCoolersPerGPU;

        internal StructureVersion _Version;
        [MarshalAs(UnmanagedType.ByValArray, SizeConst = MaxNumberOfCoolersPerGPU)]
        internal CoolerLevel[] _CoolerLevels;

        public CoolerLevel[] GetCoolerLevels(int count)
        {
            return _CoolerLevels.Take(count).ToArray();
        }

        public PrivateCoolerLevelsV1(CoolerLevel[] levels)
        {
            if (levels?.Length > MaxNumberOfCoolersPerGPU)
            {
                throw new ArgumentException($"Maximum of {MaxNumberOfCoolersPerGPU} cooler levels are configurable.", nameof(levels));
            }

            if (levels == null || levels.Length == 0)
            {
                throw new ArgumentException("Array is null or empty.", nameof(levels));
            }

            this = typeof(PrivateCoolerLevelsV1).Instantiate<PrivateCoolerLevelsV1>();
            Array.Copy(levels, 0, _CoolerLevels, 0, levels.Length);
        }

        [StructLayout(LayoutKind.Sequential, Pack = 8)]
        public struct CoolerLevel
        {
            internal uint _CurrentLevel;
            internal CoolerPolicy _CurrentPolicy;

            public CoolerLevel(CoolerPolicy coolerPolicy, uint level)
            {
                _CurrentPolicy = coolerPolicy;
                _CurrentLevel = level;
            }

            public CoolerLevel(CoolerPolicy coolerPolicy) : this(coolerPolicy, 0)
            {
                if (coolerPolicy == CoolerPolicy.Manual)
                {
                    throw new ArgumentException(
                        "Manual policy is not valid when no level value is provided.",
                        nameof(coolerPolicy)
                    );
                }
            }

            public CoolerLevel(uint level) : this(CoolerPolicy.Manual, level)
            {
            }

            public uint CurrentLevel
            {
                get => _CurrentLevel;
            }

            public CoolerPolicy CoolerPolicy
            {
                get => _CurrentPolicy;
            }
        }
    }
}