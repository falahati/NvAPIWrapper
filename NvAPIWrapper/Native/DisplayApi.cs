using System.Collections.Generic;
using System.Linq;
using NvAPIWrapper.Native.Display;
using NvAPIWrapper.Native.Display.Structures;
using NvAPIWrapper.Native.Exceptions;
using NvAPIWrapper.Native.General;
using NvAPIWrapper.Native.General.Structures;
using NvAPIWrapper.Native.GPU;
using NvAPIWrapper.Native.Helpers;
using NvAPIWrapper.Native.Helpers.Structures;
using NvAPIWrapper.Native.Interfaces.Display;

namespace NvAPIWrapper.Native
{
    public static class DisplayApi
    {
        public static void SetDisplayConfig(IPathInfo[] pathInfos, DisplayConfigFlags flags)
        {
            var setDisplayConfig = DelegateFactory.Get<Delegates.Display.NvAPI_DISP_SetDisplayConfig>();
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

        public static IPathInfo[] GetDisplayConfig()
        {
            var getDisplayConfig = DelegateFactory.Get<Delegates.Display.NvAPI_DISP_GetDisplayConfig>();
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

        public static DisplayHandle CreateDisplayFromUnAttachedDisplay(UnAttachedDisplayHandle display)
        {
            var createDisplayFromUnAttachedDisplay =
                DelegateFactory.Get<Delegates.Display.NvAPI_CreateDisplayFromUnAttachedDisplay>();
            DisplayHandle newDisplay;
            var status = createDisplayFromUnAttachedDisplay(display, out newDisplay);
            if (status != Status.Ok)
            {
                throw new NVIDIAApiException(status);
            }
            return newDisplay;
        }

        public static UnAttachedDisplayHandle GetAssociatedUnAttachedNvidiaDisplayHandle(string displayName)
        {
            var getAssociatedUnAttachedNvidiaDisplayHandle =
                DelegateFactory.Get<Delegates.Display.NvAPI_DISP_GetAssociatedUnAttachedNvidiaDisplayHandle>();
            UnAttachedDisplayHandle display;
            var status = getAssociatedUnAttachedNvidiaDisplayHandle(displayName, out display);
            if (status != Status.Ok)
            {
                throw new NVIDIAApiException(status);
            }
            return display;
        }

        public static DisplayHandle GetAssociatedNvidiaDisplayHandle(string displayName)
        {
            var getAssociatedNvidiaDisplayHandle =
                DelegateFactory.Get<Delegates.Display.NvAPI_GetAssociatedNvidiaDisplayHandle>();
            DisplayHandle display;
            var status = getAssociatedNvidiaDisplayHandle(displayName, out display);
            if (status != Status.Ok)
            {
                throw new NVIDIAApiException(status);
            }
            return display;
        }

        public static uint GetDisplayIdByDisplayName(string displayName)
        {
            var getDisplayIdByDisplayName =
                DelegateFactory.Get<Delegates.Display.NvAPI_DISP_GetDisplayIdByDisplayName>();
            uint display;
            var status = getDisplayIdByDisplayName(displayName, out display);
            if (status != Status.Ok)
            {
                throw new NVIDIAApiException(status);
            }
            return display;
        }

        public static string GetUnAttachedAssociatedDisplayName(UnAttachedDisplayHandle display)
        {
            var getUnAttachedAssociatedDisplayName =
                DelegateFactory.Get<Delegates.Display.NvAPI_GetUnAttachedAssociatedDisplayName>();
            ShortString displayName;
            var status = getUnAttachedAssociatedDisplayName(display, out displayName);
            if (status != Status.Ok)
            {
                throw new NVIDIAApiException(status);
            }
            return displayName.Value;
        }

        public static string GetAssociatedNvidiaDisplayName(DisplayHandle display)
        {
            var getAssociatedNvidiaDisplayName =
                DelegateFactory.Get<Delegates.Display.NvAPI_GetAssociatedNvidiaDisplayName>();
            ShortString displayName;
            var status = getAssociatedNvidiaDisplayName(display, out displayName);
            if (status != Status.Ok)
            {
                throw new NVIDIAApiException(status);
            }
            return displayName.Value;
        }

        public static OutputId GetAssociatedDisplayOutputId(DisplayHandle display)
        {
            var getAssociatedDisplayOutputId =
                DelegateFactory.Get<Delegates.Display.NvAPI_GetAssociatedDisplayOutputId>();
            OutputId outputId;
            var status = getAssociatedDisplayOutputId(display, out outputId);
            if (status != Status.Ok)
            {
                throw new NVIDIAApiException(status);
            }
            return outputId;
        }

        public static TargetViewMode[] GetSupportedViews(DisplayHandle display)
        {
            var getSupportedViews = DelegateFactory.Get<Delegates.Display.NvAPI_GetSupportedViews>();
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

        public static DisplayHandle[] EnumNvidiaDisplayHandle()
        {
            var enumNvidiaDisplayHandle = DelegateFactory.Get<Delegates.Display.NvAPI_EnumNvidiaDisplayHandle>();
            var results = new List<DisplayHandle>();
            uint i = 0;
            while (true)
            {
                DisplayHandle displayHandle;
                var status = enumNvidiaDisplayHandle(i, out displayHandle);
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

        public static UnAttachedDisplayHandle[] EnumNvidiaUnAttachedDisplayHandle()
        {
            var enumNvidiaUnAttachedDisplayHandle =
                DelegateFactory.Get<Delegates.Display.NvAPI_EnumNvidiaUnAttachedDisplayHandle>();
            var results = new List<UnAttachedDisplayHandle>();
            uint i = 0;
            while (true)
            {
                UnAttachedDisplayHandle displayHandle;
                var status = enumNvidiaUnAttachedDisplayHandle(i, out displayHandle);
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
    }
}