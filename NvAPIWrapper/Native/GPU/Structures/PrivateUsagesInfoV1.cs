using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using NvAPIWrapper.Native.Attributes;
using NvAPIWrapper.Native.General.Structures;
using NvAPIWrapper.Native.Interfaces;

namespace NvAPIWrapper.Native.GPU.Structures
{
    /// <summary>
    ///     Holds information about the GPU usage statistics
    /// </summary>
    [StructLayout(LayoutKind.Sequential, Pack = 8)]
    [StructureVersion(1)]
    public struct PrivateUsagesInfoV1 : IInitializable
    {
        internal const int MaxNumberOfUsageEntries = DynamicPerformanceStatesInfoV1.MaxGpuUtilizations;

        internal StructureVersion _Version;
        internal uint _Unknown;

        [MarshalAs(UnmanagedType.ByValArray, SizeConst = MaxNumberOfUsageEntries, ArraySubType = UnmanagedType.Struct)]
        internal UsagesInfoEntry[] _UsagesInfoEntries;

        /// <summary>
        ///     Gets all valid <see cref="UsagesInfoEntry" />s
        /// </summary>
        public Dictionary<UtilizationDomain, UsagesInfoEntry> Usages
        {
            get => _UsagesInfoEntries
                .Select((value, index) => new {index, value})
                .Where(arg => Enum.IsDefined(typeof(UtilizationDomain), arg.index))
                .ToDictionary(arg => (UtilizationDomain) arg.index, arg => arg.value);
        }

        /// <summary>
        ///     Gets the graphic engine (GPU) usage information
        /// </summary>
        public UsagesInfoEntry GPU
        {
            get => _UsagesInfoEntries[(int) UtilizationDomain.GPU];
        }

        /// <summary>
        ///     Gets the frame buffer (FB) usage information
        /// </summary>
        public UsagesInfoEntry FrameBuffer
        {
            get => _UsagesInfoEntries[(int) UtilizationDomain.FrameBuffer];
        }

        /// <summary>
        ///     Gets the video engine (VID) usage information
        /// </summary>
        public UsagesInfoEntry VideoEngine
        {
            get => _UsagesInfoEntries[(int) UtilizationDomain.VideoEngine];
        }

        /// <summary>
        ///     Gets the bus interface (BUS) usage information
        /// </summary>
        public UsagesInfoEntry BusInterface
        {
            get => _UsagesInfoEntries[(int) UtilizationDomain.BusInterface];
        }

        /// <inheritdoc />
        public override string ToString()
        {
            return $"GPU = {GPU} - " +
                   $"FrameBuffer = {FrameBuffer} - " +
                   $"VideoEngine = {VideoEngine} - " +
                   $"BusInterface = {BusInterface}";
        }

        /// <summary>
        ///     Holds information about the usage statistics for a domain
        /// </summary>

        [StructLayout(LayoutKind.Sequential, Pack = 8)]
        public struct UsagesInfoEntry
        {
            internal uint _IsPresent;
            internal uint _Percentage;
            internal uint _Unknown1;
            internal uint _Unknown2;

            /// <summary>
            ///     Gets a boolean value that indicates if this utilization domain is present on this GPU.
            /// </summary>
            public bool IsPresent
            {
                get => _IsPresent > 0;
            }

            /// <summary>
            ///     Gets the percentage of time where the domain is considered busy in the last 1 second interval.
            /// </summary>
            public uint Percentage
            {
                get => _Percentage;
            }

            /// <inheritdoc />
            public override string ToString()
            {
                return IsPresent ? $"{Percentage}%" : "N/A";
            }
        }
    }
}