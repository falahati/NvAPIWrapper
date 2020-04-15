using System;
using System.Collections.Generic;
using System.Linq;
using NvAPIWrapper.GPU;
using NvAPIWrapper.Native;
using NvAPIWrapper.Native.Display;
using NvAPIWrapper.Native.Display.Structures;
using NvAPIWrapper.Native.GPU;
using NvAPIWrapper.Native.GPU.Structures;
using NvAPIWrapper.Native.Interfaces.GPU;

namespace NvAPIWrapper.Display
{
    /// <summary>
    ///     Represents an NVIDIA display device
    /// </summary>
    public class DisplayDevice : IEquatable<DisplayDevice>
    {
        /// <summary>
        ///     Creates a new DisplayDevice
        /// </summary>
        /// <param name="displayId">Display identification of the device</param>
        public DisplayDevice(uint displayId)
        {
            DisplayId = displayId;
            var extraInformation = PhysicalGPU.GetDisplayDevices().FirstOrDefault(ids => ids.DisplayId == DisplayId);

            if (extraInformation != null)
            {
                IsAvailable = true;
                ScanOutInformation = new ScanOutInformation(this);
                ConnectionType = extraInformation.ConnectionType;
                IsDynamic = extraInformation.IsDynamic;
                IsMultiStreamRootNode = extraInformation.IsMultiStreamRootNode;
                IsActive = extraInformation.IsActive;
                IsCluster = extraInformation.IsCluster;
                IsOSVisible = extraInformation.IsOSVisible;
                IsWFD = extraInformation.IsWFD;
                IsConnected = extraInformation.IsConnected;
                IsPhysicallyConnected = extraInformation.IsPhysicallyConnected;
            }
        }

        /// <summary>
        ///     Creates a new DisplayDevice
        /// </summary>
        /// <param name="displayIds">Display identification and attributes of the display device</param>
        public DisplayDevice(IDisplayIds displayIds)
        {
            IsAvailable = true;
            DisplayId = displayIds.DisplayId;
            ScanOutInformation = new ScanOutInformation(this);
            ConnectionType = displayIds.ConnectionType;
            IsDynamic = displayIds.IsDynamic;
            IsMultiStreamRootNode = displayIds.IsMultiStreamRootNode;
            IsActive = displayIds.IsActive;
            IsCluster = displayIds.IsCluster;
            IsOSVisible = displayIds.IsOSVisible;
            IsWFD = displayIds.IsWFD;
            IsConnected = displayIds.IsConnected;
            IsPhysicallyConnected = displayIds.IsPhysicallyConnected;
        }


        /// <summary>
        ///     Creates a new DisplayDevice
        /// </summary>
        /// <param name="displayName">Display name of the display device</param>
        public DisplayDevice(string displayName) : this(DisplayApi.GetDisplayIdByDisplayName(displayName))
        {
        }

        /// <summary>
        ///     Gets the display device connection type
        /// </summary>
        public MonitorConnectionType ConnectionType { get; }

        /// <summary>
        ///     Gets the current display device timing
        /// </summary>
        public Timing CurrentTiming
        {
            get => DisplayApi.GetTiming(DisplayId, new TimingInput(TimingOverride.Current));
        }

        /// <summary>
        ///     Gets the NVIDIA display identification
        /// </summary>
        public uint DisplayId { get; }

        /// <summary>
        ///     Gets the monitor Display port capabilities
        /// </summary>
        public MonitorColorData[] DisplayPortColorCapabilities
        {
            get
            {
                if (ConnectionType != MonitorConnectionType.DisplayPort)
                {
                    return null;
                }

                return DisplayApi.GetMonitorColorCapabilities(DisplayId);
            }
        }

        /// <summary>
        ///     Indicates if the display is being actively driven
        /// </summary>
        public bool IsActive { get; }

        /// <summary>
        ///     Indicates if the display device is currently available
        /// </summary>
        public bool IsAvailable { get; }

        /// <summary>
        ///     Indicates if the display is the representative display
        /// </summary>
        public bool IsCluster { get; }

        /// <summary>
        ///     Indicates if the display is connected
        /// </summary>
        public bool IsConnected { get; }

        /// <summary>
        ///     Indicates if the display is part of MST topology and it's a dynamic
        /// </summary>
        public bool IsDynamic { get; }

        /// <summary>
        ///     Indicates if the display identification belongs to a multi stream enabled connector (root node). Note that when
        ///     multi stream is enabled and a single multi stream capable monitor is connected to it, the monitor will share the
        ///     display id with the RootNode.
        ///     When there is more than one monitor connected in a multi stream topology, then the root node will have a separate
        ///     displayId.
        /// </summary>
        public bool IsMultiStreamRootNode { get; }

        /// <summary>
        ///     Indicates if the display is reported to the OS
        /// </summary>
        public bool IsOSVisible { get; }

        /// <summary>
        ///     Indicates if the display is a physically connected display; Valid only when IsConnected is true
        /// </summary>
        public bool IsPhysicallyConnected { get; }

        /// <summary>
        ///     Indicates if the display is wireless
        /// </summary>
        public bool IsWFD { get; }

        /// <summary>
        ///     Gets the connected GPU output
        /// </summary>
        public GPUOutput Output
        {
            get
            {
                PhysicalGPUHandle handle;
                var outputId = GPUApi.GetGPUAndOutputIdFromDisplayId(DisplayId, out handle);

                return new GPUOutput(outputId, new PhysicalGPU(handle));
            }
        }

        /// <summary>
        ///     Gets the connected physical GPU
        /// </summary>
        public PhysicalGPU PhysicalGPU
        {
            get
            {
                try
                {
                    var gpuHandle = GPUApi.GetPhysicalGPUFromDisplayId(DisplayId);

                    return new PhysicalGPU(gpuHandle);
                }
                catch
                {
                    // ignored
                }

                return Output.PhysicalGPU;
            }
        }

        /// <summary>
        ///     Gets information regarding the scan-out settings of this display device
        /// </summary>
        public ScanOutInformation ScanOutInformation { get; }

        /// <summary>
        ///     Gets monitor capabilities from the Video Capability Data Block if available, otherwise null
        /// </summary>
        public MonitorCapabilities? VCDBMonitorCapabilities
        {
            get => DisplayApi.GetMonitorCapabilities(DisplayId, MonitorCapabilitiesType.VCDB);
        }

        /// <summary>
        ///     Gets monitor capabilities from the Vendor Specific Data Block if available, otherwise null
        /// </summary>
        public MonitorCapabilities? VSDBMonitorCapabilities
        {
            get => DisplayApi.GetMonitorCapabilities(DisplayId, MonitorCapabilitiesType.VSDB);
        }

        /// <inheritdoc />
        public bool Equals(DisplayDevice other)
        {
            if (ReferenceEquals(null, other))
            {
                return false;
            }

            if (ReferenceEquals(this, other))
            {
                return true;
            }

            return DisplayId == other.DisplayId;
        }

        /// <summary>
        ///     Deletes a custom resolution.
        /// </summary>
        /// <param name="customResolution">The custom resolution to delete.</param>
        /// <param name="displayIds">A list of display ids to remove the custom resolution from.</param>
        public static void DeleteCustomResolution(CustomResolution customResolution, uint[] displayIds)
        {
            var customDisplay = customResolution.AsCustomDisplay(false);
            DisplayApi.DeleteCustomDisplay(displayIds, customDisplay);
        }

        /// <summary>
        ///     Checks for equality between two objects of same type
        /// </summary>
        /// <param name="left">The first object</param>
        /// <param name="right">The second object</param>
        /// <returns>true, if both objects are equal, otherwise false</returns>
        public static bool operator ==(DisplayDevice left, DisplayDevice right)
        {
            return right?.Equals(left) ?? ReferenceEquals(left, null);
        }

        /// <summary>
        ///     Checks for inequality between two objects of same type
        /// </summary>
        /// <param name="left">The first object</param>
        /// <param name="right">The second object</param>
        /// <returns>true, if both objects are not equal, otherwise false</returns>
        public static bool operator !=(DisplayDevice left, DisplayDevice right)
        {
            return !(right == left);
        }

        /// <summary>
        ///     Reverts the custom resolution currently on trial.
        /// </summary>
        /// <param name="displayIds">A list of display ids to revert the custom resolution from.</param>
        public static void RevertCustomResolution(uint[] displayIds)
        {
            DisplayApi.RevertCustomDisplayTrial(displayIds);
        }

        /// <summary>
        ///     Saves the custom resolution currently on trial.
        /// </summary>
        /// <param name="displayIds">A list of display ids to save the custom resolution for.</param>
        /// <param name="isThisOutputIdOnly">
        ///     If set, the saved custom display will only be applied on the monitor with the same
        ///     outputId.
        /// </param>
        /// <param name="isThisMonitorOnly">
        ///     If set, the saved custom display will only be applied on the monitor with the same EDID
        ///     ID or the same TV connector in case of analog TV.
        /// </param>
        public static void SaveCustomResolution(uint[] displayIds, bool isThisOutputIdOnly, bool isThisMonitorOnly)
        {
            DisplayApi.SaveCustomDisplay(displayIds, isThisOutputIdOnly, isThisMonitorOnly);
        }

        /// <summary>
        ///     Applies a custom resolution into trial
        /// </summary>
        /// <param name="customResolution">The custom resolution to apply.</param>
        /// <param name="displayIds">A list of display ids to apply the custom resolution on.</param>
        /// <param name="hardwareModeSetOnly">
        ///     A boolean value indicating that a hardware mode-set without OS update should be
        ///     performed.
        /// </param>
        public static void TrialCustomResolution(
            CustomResolution customResolution,
            uint[] displayIds,
            bool hardwareModeSetOnly = true)
        {
            var customDisplay = customResolution.AsCustomDisplay(hardwareModeSetOnly);
            DisplayApi.TryCustomDisplay(displayIds.ToDictionary(u => u, u => customDisplay));
        }

        /// <inheritdoc />
        public override bool Equals(object obj)
        {
            if (ReferenceEquals(null, obj))
            {
                return false;
            }

            if (ReferenceEquals(this, obj))
            {
                return true;
            }

            if (obj.GetType() != GetType())
            {
                return false;
            }

            return Equals((DisplayDevice) obj);
        }

        /// <inheritdoc />
        public override int GetHashCode()
        {
            return (int) DisplayId;
        }


        /// <inheritdoc />
        public override string ToString()
        {
            return $"Display #{DisplayId}";
        }

        /// <summary>
        ///     Calculates a valid timing based on the argument passed
        /// </summary>
        /// <param name="width">The preferred width.</param>
        /// <param name="height">The preferred height.</param>
        /// <param name="refreshRate">The preferred refresh rate.</param>
        /// <param name="isInterlaced">The boolean value indicating if the preferred resolution is an interlaced resolution.</param>
        /// <returns>Returns a valid instance of <see cref="Timing" />.</returns>
        public Timing CalculateTiming(uint width, uint height, float refreshRate, bool isInterlaced)
        {
            return DisplayApi.GetTiming(
                DisplayId,
                new TimingInput(width, height, refreshRate, TimingOverride.Auto, isInterlaced)
            );
        }

        /// <summary>
        ///     Deletes a custom resolution.
        /// </summary>
        /// <param name="customResolution">The custom resolution to delete.</param>
        public void DeleteCustomResolution(CustomResolution customResolution)
        {
            DeleteCustomResolution(customResolution, new[] {DisplayId});
        }

        /// <summary>
        ///     Retrieves the list of custom resolutions saved for this display device
        /// </summary>
        /// <returns>A list of <see cref="CustomResolution" /> instances.</returns>
        public IEnumerable<CustomResolution> GetCustomResolutions()
        {
            return DisplayApi.EnumCustomDisplays(DisplayId).Select(custom => new CustomResolution(custom));
        }

        /// <summary>
        ///     Reverts the custom resolution currently on trial.
        /// </summary>
        public void RevertCustomResolution()
        {
            RevertCustomResolution(new[] {DisplayId});
        }

        /// <summary>
        ///     Saves the custom resolution currently on trial.
        /// </summary>
        /// <param name="isThisOutputIdOnly">
        ///     If set, the saved custom display will only be applied on the monitor with the same
        ///     outputId.
        /// </param>
        /// <param name="isThisMonitorOnly">
        ///     If set, the saved custom display will only be applied on the monitor with the same EDID
        ///     ID or the same TV connector in case of analog TV.
        /// </param>
        public void SaveCustomResolution(bool isThisOutputIdOnly = true, bool isThisMonitorOnly = true)
        {
            SaveCustomResolution(new[] {DisplayId}, isThisOutputIdOnly, isThisMonitorOnly);
        }

        /// <summary>
        ///     Applies a custom resolution into trial.
        /// </summary>
        /// <param name="customResolution">The custom resolution to apply.</param>
        /// <param name="hardwareModeSetOnly">
        ///     A boolean value indicating that a hardware mode-set without OS update should be
        ///     performed.
        /// </param>
        public void TrialCustomResolution(CustomResolution customResolution, bool hardwareModeSetOnly = true)
        {
            TrialCustomResolution(customResolution, new[] {DisplayId}, hardwareModeSetOnly);
        }
    }
}