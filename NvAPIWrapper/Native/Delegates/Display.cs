using System.Runtime.InteropServices;
using NvAPIWrapper.Native.Attributes;
using NvAPIWrapper.Native.Display;
using NvAPIWrapper.Native.Display.Structures;
using NvAPIWrapper.Native.General;
using NvAPIWrapper.Native.General.Structures;
using NvAPIWrapper.Native.GPU;
using NvAPIWrapper.Native.Helpers;
using NvAPIWrapper.Native.Helpers.Structures;

// ReSharper disable InconsistentNaming

namespace NvAPIWrapper.Native.Delegates
{
    internal static class Display
    {
        [FunctionId(FunctionId.NvAPI_CreateDisplayFromUnAttachedDisplay)]
        public delegate Status NvAPI_CreateDisplayFromUnAttachedDisplay(
            [In] UnAttachedDisplayHandle display,
            [Out] out DisplayHandle newDisplay);

        [FunctionId(FunctionId.NvAPI_DISP_GetAssociatedUnAttachedNvidiaDisplayHandle)]
        public delegate Status NvAPI_DISP_GetAssociatedUnAttachedNvidiaDisplayHandle(
            [In] [MarshalAs(UnmanagedType.LPStr)] string displayName,
            [Out] out UnAttachedDisplayHandle display);

        [FunctionId(FunctionId.NvAPI_DISP_GetDisplayConfig)]
        public delegate Status NvAPI_DISP_GetDisplayConfig(
            [In] [Out] ref uint pathInfoCount,
            [In] [Accepts(typeof(PathInfoV2), typeof(PathInfoV1))]
            ValueTypeArray pathInfos);

        [FunctionId(FunctionId.NvAPI_DISP_GetDisplayIdByDisplayName)]
        public delegate Status NvAPI_DISP_GetDisplayIdByDisplayName([In] string displayName, [Out] out uint displayId);

        [FunctionId(FunctionId.NvAPI_DISP_SetDisplayConfig)]
        public delegate Status NvAPI_DISP_SetDisplayConfig(
            [In] uint pathInfoCount,
            [In] [Accepts(typeof(PathInfoV2), typeof(PathInfoV1))]
            ValueTypeArray pathInfos,
            [In] DisplayConfigFlags flags);

        [FunctionId(FunctionId.NvAPI_EnumNvidiaDisplayHandle)]
        public delegate Status NvAPI_EnumNvidiaDisplayHandle(
            [In] uint enumId,
            [Out] out DisplayHandle display);

        [FunctionId(FunctionId.NvAPI_EnumNvidiaUnAttachedDisplayHandle)]
        public delegate Status NvAPI_EnumNvidiaUnAttachedDisplayHandle(
            [In] uint enumId,
            [Out] out UnAttachedDisplayHandle display);

        [FunctionId(FunctionId.NvAPI_GetAssociatedDisplayOutputId)]
        public delegate Status NvAPI_GetAssociatedDisplayOutputId(
            [In] DisplayHandle display,
            [Out] out OutputId outputId);

        [FunctionId(FunctionId.NvAPI_GetAssociatedNvidiaDisplayHandle)]
        public delegate Status NvAPI_GetAssociatedNvidiaDisplayHandle(
            [In] [MarshalAs(UnmanagedType.LPStr)] string displayName,
            [Out] out DisplayHandle display);

        [FunctionId(FunctionId.NvAPI_GetAssociatedNvidiaDisplayName)]
        public delegate Status NvAPI_GetAssociatedNvidiaDisplayName(
            [In] DisplayHandle display,
            [Out] out ShortString displayName);

        [FunctionId(FunctionId.NvAPI_GetSupportedViews)]
        public delegate Status NvAPI_GetSupportedViews(
            [In] DisplayHandle display,
            [In] [Accepts(typeof(TargetViewMode))] ValueTypeArray viewModes,
            [Out] [In] ref uint viewCount);

        [FunctionId(FunctionId.NvAPI_GetUnAttachedAssociatedDisplayName)]
        public delegate Status NvAPI_GetUnAttachedAssociatedDisplayName(
            [In] UnAttachedDisplayHandle display,
            [Out] out ShortString displayName);
    }
}