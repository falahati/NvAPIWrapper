using System;
using System.Collections.Generic;
using System.Linq;
using NvAPIWrapper.Native.Display;
using NvAPIWrapper.Native.Display.Structures;
using NvAPIWrapper.Native.Exceptions;
using NvAPIWrapper.Native.General;
using NvAPIWrapper.Native.GPU;
using NvAPIWrapper.Native.Helpers;
using NvAPIWrapper.Native.Helpers.Structures;
using NvAPIWrapper.Native.Interfaces.Display;

namespace NvAPIWrapper.Native
{
    /// <summary>
    ///     Contains display and display control static functions
    /// </summary>
    public static class DisplayApi
    {
        /// <summary>
        ///     This function converts the unattached display handle to an active attached display handle.
        ///     At least one GPU must be present in the system and running an NVIDIA display driver.
        /// </summary>
        /// <param name="display">An unattached display handle to convert.</param>
        /// <returns>Display handle of newly created display.</returns>
        /// <exception cref="NVIDIAApiException">Status.InvalidArgument: Invalid UnAttachedDisplayHandle handle.</exception>
        /// <exception cref="NVIDIAApiException">Status.NvidiaDeviceNotFound: No NVIDIA GPU driving a display was found</exception>
        /// <exception cref="Exception">A delegate callback throws an exception.</exception>
        public static DisplayHandle CreateDisplayFromUnAttachedDisplay(UnAttachedDisplayHandle display)
        {
            var createDisplayFromUnAttachedDisplay =
                DelegateFactory.GetDelegate<Delegates.Display.NvAPI_CreateDisplayFromUnAttachedDisplay>();
            var status = createDisplayFromUnAttachedDisplay(display, out var newDisplay);

            if (status != Status.Ok)
            {
                throw new NVIDIAApiException(status);
            }

            return newDisplay;
        }

        /// <summary>
        ///     This function returns the handle of all NVIDIA displays
        ///     Note: Display handles can get invalidated on a mode-set, so the calling applications need to re-enum the handles
        ///     after every mode-set.
        /// </summary>
        /// <returns>Array of display handles.</returns>
        /// <exception cref="NVIDIAApiException">Status.NvidiaDeviceNotFound: No NVIDIA device found in the system</exception>
        /// <exception cref="Exception">A delegate callback throws an exception.</exception>
        public static DisplayHandle[] EnumNvidiaDisplayHandle()
        {
            var enumNvidiaDisplayHandle =
                DelegateFactory.GetDelegate<Delegates.Display.NvAPI_EnumNvidiaDisplayHandle>();
            var results = new List<DisplayHandle>();
            uint i = 0;

            while (true)
            {
                var status = enumNvidiaDisplayHandle(i, out var displayHandle);

                if (status == Status.EndEnumeration)
                {
                    break;
                }

                if (status != Status.Ok)
                {
                    throw new NVIDIAApiException(status);
                }

                results.Add(displayHandle);
                i++;
            }

            return results.ToArray();
        }

        /// <summary>
        ///     This function returns the handle of all unattached NVIDIA displays
        ///     Note: Display handles can get invalidated on a mode-set, so the calling applications need to re-enum the handles
        ///     after every mode-set.
        /// </summary>
        /// <returns>Array of unattached display handles.</returns>
        /// <exception cref="NVIDIAApiException">Status.NvidiaDeviceNotFound: No NVIDIA device found in the system</exception>
        /// <exception cref="Exception">A delegate callback throws an exception.</exception>
        public static UnAttachedDisplayHandle[] EnumNvidiaUnAttachedDisplayHandle()
        {
            var enumNvidiaUnAttachedDisplayHandle =
                DelegateFactory.GetDelegate<Delegates.Display.NvAPI_EnumNvidiaUnAttachedDisplayHandle>();
            var results = new List<UnAttachedDisplayHandle>();
            uint i = 0;

            while (true)
            {
                var status = enumNvidiaUnAttachedDisplayHandle(i, out var displayHandle);

                if (status == Status.EndEnumeration)
                {
                    break;
                }

                if (status != Status.Ok)
                {
                    throw new NVIDIAApiException(status);
                }

                results.Add(displayHandle);
                i++;
            }

            return results.ToArray();
        }

        /// <summary>
        ///     This function gets the active outputId associated with the display handle.
        /// </summary>
        /// <param name="display">
        ///     NVIDIA Display selection. It can be DisplayHandle.DefaultHandle or a handle enumerated from
        ///     DisplayApi.EnumNVidiaDisplayHandle().
        /// </param>
        /// <returns>
        ///     The active display output ID associated with the selected display handle hNvDisplay. The output id will have
        ///     only one bit set. In the case of Clone or Span mode, this will indicate the display outputId of the primary display
        ///     that the GPU is driving.
        /// </returns>
        /// <exception cref="NVIDIAApiException">Status.NvidiaDeviceNotFound: No NVIDIA GPU driving a display was found.</exception>
        /// <exception cref="NVIDIAApiException">Status.ExpectedDisplayHandle: display is not a valid display handle.</exception>
        /// <exception cref="Exception">A delegate callback throws an exception.</exception>
        public static OutputId GetAssociatedDisplayOutputId(DisplayHandle display)
        {
            var getAssociatedDisplayOutputId =
                DelegateFactory.GetDelegate<Delegates.Display.NvAPI_GetAssociatedDisplayOutputId>();
            var status = getAssociatedDisplayOutputId(display, out var outputId);

            if (status != Status.Ok)
            {
                throw new NVIDIAApiException(status);
            }

            return outputId;
        }

        /// <summary>
        ///     This function returns the handle of the NVIDIA display that is associated with the given display "name" (such as
        ///     "\\.\DISPLAY1").
        /// </summary>
        /// <param name="name">Display name</param>
        /// <returns>Display handle of associated display</returns>
        /// <exception cref="NVIDIAApiException">Status.InvalidArgument: Display name is null.</exception>
        /// <exception cref="NVIDIAApiException">Status.NvidiaDeviceNotFound: No NVIDIA device maps to that display name.</exception>
        /// <exception cref="Exception">A delegate callback throws an exception.</exception>
        public static DisplayHandle GetAssociatedNvidiaDisplayHandle(string name)
        {
            var getAssociatedNvidiaDisplayHandle =
                DelegateFactory.GetDelegate<Delegates.Display.NvAPI_GetAssociatedNvidiaDisplayHandle>();
            var status = getAssociatedNvidiaDisplayHandle(name, out var display);

            if (status != Status.Ok)
            {
                throw new NVIDIAApiException(status);
            }

            return display;
        }

        /// <summary>
        ///     For a given NVIDIA display handle, this function returns a string (such as "\\.\DISPLAY1") to identify the display.
        /// </summary>
        /// <param name="display">Handle of the associated display</param>
        /// <returns>Name of the display</returns>
        /// <exception cref="NVIDIAApiException">Status.InvalidArgument: Display handle is null.</exception>
        /// <exception cref="NVIDIAApiException">Status.NvidiaDeviceNotFound: No NVIDIA device maps to that display name.</exception>
        /// <exception cref="Exception">A delegate callback throws an exception.</exception>
        public static string GetAssociatedNvidiaDisplayName(DisplayHandle display)
        {
            var getAssociatedNvidiaDisplayName =
                DelegateFactory.GetDelegate<Delegates.Display.NvAPI_GetAssociatedNvidiaDisplayName>();
            var status = getAssociatedNvidiaDisplayName(display, out var displayName);

            if (status != Status.Ok)
            {
                throw new NVIDIAApiException(status);
            }

            return displayName.Value;
        }

        /// <summary>
        ///     This function returns the handle of an unattached NVIDIA display that is associated with the given display "name"
        ///     (such as "\\DISPLAY1").
        /// </summary>
        /// <param name="name">Display name</param>
        /// <returns>Display handle of associated unattached display</returns>
        /// <exception cref="NVIDIAApiException">Status.InvalidArgument: Display name is null.</exception>
        /// <exception cref="NVIDIAApiException">Status.NvidiaDeviceNotFound: No NVIDIA device maps to that display name.</exception>
        /// <exception cref="Exception">A delegate callback throws an exception.</exception>
        public static UnAttachedDisplayHandle GetAssociatedUnAttachedNvidiaDisplayHandle(string name)
        {
            var getAssociatedUnAttachedNvidiaDisplayHandle =
                DelegateFactory.GetDelegate<Delegates.Display.NvAPI_DISP_GetAssociatedUnAttachedNvidiaDisplayHandle>();
            var status = getAssociatedUnAttachedNvidiaDisplayHandle(name, out var display);

            if (status != Status.Ok)
            {
                throw new NVIDIAApiException(status);
            }

            return display;
        }

        /// <summary>
        ///     This API lets caller retrieve the current global display configuration.
        ///     Note: User should dispose all returned PathInfo objects
        /// </summary>
        /// <returns>Array of path information</returns>
        /// <exception cref="NVIDIANotSupportedException">This operation is not supported.</exception>
        /// <exception cref="NVIDIAApiException">Status.InvalidArgument: Invalid input parameter.</exception>
        /// <exception cref="NVIDIAApiException">Status.DeviceBusy: ModeSet has not yet completed. Please wait and call it again.</exception>
        /// <exception cref="Exception">A delegate callback throws an exception.</exception>
        public static IPathInfo[] GetDisplayConfig()
        {
            var getDisplayConfig = DelegateFactory.GetDelegate<Delegates.Display.NvAPI_DISP_GetDisplayConfig>();
            uint allAvailable = 0;
            var status = getDisplayConfig(ref allAvailable, ValueTypeArray.Null);

            if (status != Status.Ok)
            {
                throw new NVIDIAApiException(status);
            }

            if (allAvailable == 0)
            {
                return new IPathInfo[0];
            }

            foreach (var acceptType in getDisplayConfig.Accepts())
            {
                var count = allAvailable;
                var instances = acceptType.Instantiate<IPathInfo>().Repeat((int) allAvailable);

                using (var pathInfos = ValueTypeArray.FromArray(instances, acceptType))
                {
                    status = getDisplayConfig(ref count, pathInfos);

                    if (status != Status.Ok)
                    {
                        throw new NVIDIAApiException(status);
                    }

                    instances = pathInfos.ToArray<IPathInfo>((int) count, acceptType);
                }

                if (instances.Length <= 0)
                {
                    return new IPathInfo[0];
                }

                // After allocation, we should make sure to dispose objects
                // In this case however, the responsibility is on the user shoulders
                instances = instances.AllocateAll().ToArray();

                using (var pathInfos = ValueTypeArray.FromArray(instances, acceptType))
                {
                    status = getDisplayConfig(ref count, pathInfos);

                    if (status != Status.Ok)
                    {
                        throw new NVIDIAApiException(status);
                    }

                    return pathInfos.ToArray<IPathInfo>((int) count, acceptType);
                }
            }

            throw new NVIDIANotSupportedException("This operation is not supported.");
        }

        /// <summary>
        ///     This API retrieves the Display Id of a given display by display name. The display must be active to retrieve the
        ///     displayId. In the case of clone mode or Surround gaming, the primary or top-left display will be returned.
        /// </summary>
        /// <param name="displayName">Name of display (Eg: "\\DISPLAY1" to retrieve the displayId for.</param>
        /// <returns>Display ID of the requested display.</returns>
        /// <exception cref="NVIDIAApiException">Status.InvalidArgument: One or more args passed in are invalid.</exception>
        /// <exception cref="NVIDIAApiException">Status.ApiNotInitialized: The NvAPI API needs to be initialized first</exception>
        /// <exception cref="NVIDIAApiException">Status.NoImplementation: This entry-point not available</exception>
        /// <exception cref="NVIDIAApiException">Status.Error: Miscellaneous error occurred</exception>
        /// <exception cref="Exception">A delegate callback throws an exception.</exception>
        public static uint GetDisplayIdByDisplayName(string displayName)
        {
            var getDisplayIdByDisplayName =
                DelegateFactory.GetDelegate<Delegates.Display.NvAPI_DISP_GetDisplayIdByDisplayName>();
            var status = getDisplayIdByDisplayName(displayName, out var display);

            if (status != Status.Ok)
            {
                throw new NVIDIAApiException(status);
            }

            return display;
        }

        /// <summary>
        ///     This API lets caller enumerate all the supported NVIDIA display views - nView and DualView modes.
        /// </summary>
        /// <param name="display">
        ///     NVIDIA Display selection. It can be DisplayHandle.DefaultHandle or a handle enumerated from
        ///     DisplayApi.EnumNVidiaDisplayHandle().
        /// </param>
        /// <returns>Array of supported views.</returns>
        /// <exception cref="NVIDIANotSupportedException">This operation is not supported.</exception>
        /// <exception cref="NVIDIAApiException">Status.Error: Miscellaneous error occurred</exception>
        /// <exception cref="NVIDIAApiException">Status.InvalidArgument: Invalid input parameter.</exception>
        /// <exception cref="Exception">A delegate callback throws an exception.</exception>
        public static TargetViewMode[] GetSupportedViews(DisplayHandle display)
        {
            var getSupportedViews = DelegateFactory.GetDelegate<Delegates.Display.NvAPI_GetSupportedViews>();
            uint allAvailable = 0;
            var status = getSupportedViews(display, ValueTypeArray.Null, ref allAvailable);

            if (status != Status.Ok)
            {
                throw new NVIDIAApiException(status);
            }

            if (allAvailable == 0)
            {
                return new TargetViewMode[0];
            }

            if (!getSupportedViews.Accepts().Contains(typeof(TargetViewMode)))
            {
                throw new NVIDIANotSupportedException("This operation is not supported.");
            }

            using (
                var viewModes =
                    ValueTypeArray.FromArray(TargetViewMode.Standard.Repeat((int) allAvailable).Cast<object>(),
                        typeof(TargetViewMode).GetEnumUnderlyingType()))
            {
                status = getSupportedViews(display, viewModes, ref allAvailable);

                if (status != Status.Ok)
                {
                    throw new NVIDIAApiException(status);
                }

                return viewModes.ToArray<TargetViewMode>((int) allAvailable,
                    typeof(TargetViewMode).GetEnumUnderlyingType());
            }
        }

        /// <summary>
        ///     This function returns the display name given, for example, "\\DISPLAY1", using the unattached NVIDIA display handle
        /// </summary>
        /// <param name="display">Handle of the associated unattached display</param>
        /// <returns>Name of the display</returns>
        /// <exception cref="NVIDIAApiException">Status.InvalidArgument: Display handle is null.</exception>
        /// <exception cref="NVIDIAApiException">Status.NvidiaDeviceNotFound: No NVIDIA device maps to that display name.</exception>
        /// <exception cref="Exception">A delegate callback throws an exception.</exception>
        public static string GetUnAttachedAssociatedDisplayName(UnAttachedDisplayHandle display)
        {
            var getUnAttachedAssociatedDisplayName =
                DelegateFactory.GetDelegate<Delegates.Display.NvAPI_GetUnAttachedAssociatedDisplayName>();
            var status = getUnAttachedAssociatedDisplayName(display, out var displayName);

            if (status != Status.Ok)
            {
                throw new NVIDIAApiException(status);
            }

            return displayName.Value;
        }

        /// <summary>
        ///     This API lets caller apply a global display configuration across multiple GPUs.
        ///     If all sourceIds are zero, then NvAPI will pick up sourceId's based on the following criteria :
        ///     - If user provides SourceModeInfo then we are trying to assign 0th SourceId always to GDIPrimary.
        ///     This is needed since active windows always moves along with 0th sourceId.
        ///     - For rest of the paths, we are incrementally assigning the SourceId per adapter basis.
        ///     - If user doesn't provide SourceModeInfo then NVAPI just picks up some default SourceId's in incremental order.
        ///     Note : NVAPI will not intelligently choose the SourceIDs for any configs that does not need a mode-set.
        /// </summary>
        /// <param name="pathInfos">Array of path information</param>
        /// <param name="flags">Flags for applying settings</param>
        /// <exception cref="NVIDIANotSupportedException">This operation is not supported.</exception>
        /// <exception cref="NVIDIAApiException">Status.ApiNotInitialized: NVAPI not initialized</exception>
        /// <exception cref="NVIDIAApiException">Status.Error: Miscellaneous error occurred</exception>
        /// <exception cref="NVIDIAApiException">Status.InvalidArgument: Invalid input parameter.</exception>
        /// <exception cref="Exception">A delegate callback throws an exception.</exception>
        public static void SetDisplayConfig(IPathInfo[] pathInfos, DisplayConfigFlags flags)
        {
            var setDisplayConfig = DelegateFactory.GetDelegate<Delegates.Display.NvAPI_DISP_SetDisplayConfig>();

            if (pathInfos.Length > 0 && !setDisplayConfig.Accepts().Contains(pathInfos[0].GetType()))
            {
                throw new NVIDIANotSupportedException("This operation is not supported.");
            }

            using (var arrayReference = ValueTypeArray.FromArray(pathInfos.Cast<object>()))
            {
                var status = setDisplayConfig((uint) pathInfos.Length, arrayReference, flags);

                if (status != Status.Ok)
                {
                    throw new NVIDIAApiException(status);
                }
            }
        }
    }
}