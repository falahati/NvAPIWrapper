using System;
using System.Linq;
using NvAPIWrapper.Native.Display.Structures;
using NvAPIWrapper.Native.Exceptions;
using NvAPIWrapper.Native.General;
using NvAPIWrapper.Native.General.Structures;
using NvAPIWrapper.Native.GPU;
using NvAPIWrapper.Native.GPU.Structures;
using NvAPIWrapper.Native.Helpers;
using NvAPIWrapper.Native.Helpers.Structures;
using NvAPIWrapper.Native.Interfaces.GPU;

namespace NvAPIWrapper.Native
{
    /// <summary>
    ///     Contains GPU static functions
    /// </summary>
    public static class GPUApi
    {
        /// <summary>
        ///     This function returns an array of logical GPU handles.
        ///     Each handle represents one or more GPUs acting in concert as a single graphics device.
        ///     At least one GPU must be present in the system and running an NVIDIA display driver.
        ///     All logical GPUs handles get invalidated on a GPU topology change, so the calling application is required to reenum
        ///     the logical GPU handles to get latest physical handle mapping after every GPU topology change activated by a call
        ///     to SetGpuTopologies().
        ///     To detect if SLI rendering is enabled, use Direct3DApi.GetCurrentSLIState().
        /// </summary>
        /// <returns>Array of logical GPU handles.</returns>
        /// <exception cref="NVIDIAApiException">Status.NvidiaDeviceNotFound: No NVIDIA GPU driving a display was found</exception>
        public static LogicalGPUHandle[] EnumLogicalGPUs()
        {
            var gpuList =
                typeof(LogicalGPUHandle).Instantiate<LogicalGPUHandle>().Repeat(LogicalGPUHandle.MaxLogicalGPUs);
            uint count;
            var status = DelegateFactory.Get<Delegates.GPU.NvAPI_EnumLogicalGPUs>()(gpuList, out count);
            if (status != Status.Ok)
                throw new NVIDIAApiException(status);
            return gpuList.Take((int) count).ToArray();
        }

        /// <summary>
        ///     This function returns an array of physical GPU handles.
        ///     Each handle represents a physical GPU present in the system.
        ///     That GPU may be part of an SLI configuration, or may not be visible to the OS directly.
        ///     At least one GPU must be present in the system and running an NVIDIA display driver.
        ///     Note: In drivers older than 105.00, all physical GPU handles get invalidated on a modeset. So the calling
        ///     applications need to reenum the handles after every modeset. With drivers 105.00 and up, all physical GPU handles
        ///     are constant. Physical GPU handles are constant as long as the GPUs are not physically moved and the SBIOS VGA
        ///     order is unchanged.
        ///     For GPU handles in TCC MODE please use EnumTCCPhysicalGPUs()
        /// </summary>
        /// <returns></returns>
        /// <exception cref="NVIDIAApiException">Status.NvidiaDeviceNotFound: No NVIDIA GPU driving a display was found</exception>
        public static PhysicalGPUHandle[] EnumPhysicalGPUs()
        {
            var gpuList =
                typeof(PhysicalGPUHandle).Instantiate<PhysicalGPUHandle>().Repeat(PhysicalGPUHandle.PhysicalGPUs);
            uint count;
            var status = DelegateFactory.Get<Delegates.GPU.NvAPI_EnumPhysicalGPUs>()(gpuList, out count);
            if (status != Status.Ok)
                throw new NVIDIAApiException(status);
            return gpuList.Take((int) count).ToArray();
        }

        /// <summary>
        ///     This function returns an array of physical GPU handles that are in TCC Mode.
        ///     Each handle represents a physical GPU present in the system in TCC Mode.
        ///     That GPU may not be visible to the OS directly.
        ///     NOTE: Handles enumerated by this API are only valid for NvAPIs that are tagged as TCC_SUPPORTED If handle is passed
        ///     to any other API, it will fail with Status.InvalidHandle
        ///     For WDDM GPU handles please use EnumPhysicalGPUs()
        /// </summary>
        /// <returns>An array of physical GPU handles that are in TCC Mode.</returns>
        /// <exception cref="NVIDIAApiException">See NVIDIAApiException.Status for the reason of the exception.</exception>
        public static PhysicalGPUHandle[] EnumTCCPhysicalGPUs()
        {
            var gpuList =
                typeof(PhysicalGPUHandle).Instantiate<PhysicalGPUHandle>().Repeat(PhysicalGPUHandle.PhysicalGPUs);
            uint count;
            var status = DelegateFactory.Get<Delegates.GPU.NvAPI_EnumTCCPhysicalGPUs>()(gpuList, out count);
            if (status != Status.Ok)
                throw new NVIDIAApiException(status);
            return gpuList.Take((int) count).ToArray();
        }

        /// <summary>
        ///     This function is the same as GetAllOutputs() but returns only the set of GPU output identifiers that are actively
        ///     driving display devices.
        /// </summary>
        /// <param name="gpuHandle">Physical GPU handle to get information about</param>
        /// <returns>Active output identifications as a flag</returns>
        /// <exception cref="NVIDIAApiException">Status.InvalidArgument: display is not valid</exception>
        /// <exception cref="NVIDIAApiException">Status.NvidiaDeviceNotFound: No NVIDIA GPU driving a display was found</exception>
        /// <exception cref="NVIDIAApiException">Status.ExpectedPhysicalGPUHandle: gpuHandle was not a physical GPU handle.</exception>
        public static OutputId GetActiveOutputs(PhysicalGPUHandle gpuHandle)
        {
            OutputId outputMask;
            var status = DelegateFactory.Get<Delegates.GPU.NvAPI_GPU_GetActiveOutputs>()(gpuHandle, out outputMask);
            if (status != Status.Ok)
                throw new NVIDIAApiException(status);
            return outputMask;
        }

        /// <summary>
        ///     This function returns the AGP aperture in megabytes.
        /// </summary>
        /// <param name="gpuHandle">Physical GPU handle to get information about</param>
        /// <returns>AGP aperture in megabytes</returns>
        /// <exception cref="NVIDIAApiException">Status.InvalidArgument: display is not valid</exception>
        /// <exception cref="NVIDIAApiException">Status.NvidiaDeviceNotFound: No NVIDIA GPU driving a display was found</exception>
        /// <exception cref="NVIDIAApiException">Status.ExpectedPhysicalGPUHandle: gpuHandle was not a physical GPU handle.</exception>
        public static int GetAGPAperture(PhysicalGPUHandle gpuHandle)
        {
            uint agpAperture;
            var status = DelegateFactory.Get<Delegates.GPU.NvAPI_GPU_GetAGPAperture>()(gpuHandle, out agpAperture);
            if (status != Status.Ok)
                throw new NVIDIAApiException(status);
            return (int) agpAperture;
        }

        /// <summary>
        ///     This API returns display IDs for all possible outputs on the GPU.
        ///     For DPMST connector, it will return display IDs for all the video sinks in the topology.
        /// </summary>
        /// <param name="gpuHandle">Physical GPU handle to get information about</param>
        /// <returns>An array of display identifications and their attributes</returns>
        /// <exception cref="NVIDIANotSupportedException">This operation is not supported.</exception>
        /// <exception cref="NVIDIAApiException">See NVIDIAApiException.Status for the reason of the exception.</exception>
        public static DisplayIdsV2[] GetAllDisplayIds(PhysicalGPUHandle gpuHandle)
        {
            var gpuGetConnectedDisplayIds = DelegateFactory.Get<Delegates.GPU.NvAPI_GPU_GetAllDisplayIds>();
            if (!gpuGetConnectedDisplayIds.Accepts().Contains(typeof(DisplayIdsV2)))
                throw new NVIDIANotSupportedException("This operation is not supported.");

            uint count = 0;
            var status = gpuGetConnectedDisplayIds(gpuHandle, ValueTypeArray.Null, ref count);

            if (status != Status.Ok)
                throw new NVIDIAApiException(status);

            if (count == 0)
                return new DisplayIdsV2[0];
            using (
                var displayIds =
                    ValueTypeArray.FromArray(typeof(DisplayIdsV2).Instantiate<DisplayIdsV2>().Repeat((int) count)))
            {
                status = gpuGetConnectedDisplayIds(gpuHandle, displayIds, ref count);
                if (status != Status.Ok)
                    throw new NVIDIAApiException(status);
                return displayIds.ToArray<DisplayIdsV2>((int) count);
            }
        }

        /// <summary>
        ///     This API Retrieves the Board information (a unique GPU Board Serial Number) stored in the InfoROM.
        /// </summary>
        /// <param name="gpuHandle">Physical GPU Handle</param>
        /// <returns>Board Information</returns>
        /// <exception cref="NVIDIAApiException">Status.Error: Miscellaneous error occurred</exception>
        /// <exception cref="NVIDIAApiException">Status.ExpectedPhysicalGPUHandle: Handle passed is not a physical GPU handle</exception>
        /// <exception cref="NVIDIAApiException">Status.ApiNotInitialized: NVAPI not initialized</exception>
        public static BoardInfo GetBoardInfo(PhysicalGPUHandle gpuHandle)
        {
            var boardInfo = typeof(BoardInfo).Instantiate<BoardInfo>();
            var status = DelegateFactory.Get<Delegates.GPU.NvAPI_GPU_GetBoardInfo>()(gpuHandle, ref boardInfo);
            if (status != Status.Ok)
                throw new NVIDIAApiException(status);
            return boardInfo;
        }

        /// <summary>
        ///     Returns the identification of the bus associated with this GPU.
        /// </summary>
        /// <param name="gpuHandle">GPU handle to get information about</param>
        /// <returns>Id of the bus</returns>
        /// <exception cref="NVIDIAApiException">Status.NvidiaDeviceNotFound: No NVIDIA GPU driving a display was found.</exception>
        /// <exception cref="NVIDIAApiException">Status.ExpectedPhysicalGPUHandle: gpuHandle was not a physical GPU handle.</exception>
        public static int GetBusId(PhysicalGPUHandle gpuHandle)
        {
            uint busId;
            var status = DelegateFactory.Get<Delegates.GPU.NvAPI_GPU_GetBusId>()(gpuHandle, out busId);
            if (status != Status.Ok)
                throw new NVIDIAApiException(status);
            return (int) busId;
        }

        /// <summary>
        ///     Returns the identification of the bus slot associated with this GPU.
        /// </summary>
        /// <param name="gpuHandle">GPU handle to get information about</param>
        /// <returns>Identification of the bus slot associated with this GPU</returns>
        /// <exception cref="NVIDIAApiException">Status.InvalidArgument: gpuHandle is NULL</exception>
        /// <exception cref="NVIDIAApiException">Status.NvidiaDeviceNotFound: No NVIDIA GPU driving a display was found</exception>
        /// <exception cref="NVIDIAApiException">Status.ExpectedPhysicalGPUHandle: gpuHandle was not a physical GPU handle</exception>
        public static int GetBusSlotId(PhysicalGPUHandle gpuHandle)
        {
            uint busId;
            var status = DelegateFactory.Get<Delegates.GPU.NvAPI_GPU_GetBusSlotId>()(gpuHandle, out busId);
            if (status != Status.Ok)
                throw new NVIDIAApiException(status);
            return (int) busId;
        }

        /// <summary>
        ///     This function returns the type of bus associated with this GPU.
        ///     TCC_SUPPORTED
        /// </summary>
        /// <param name="gpuHandle">GPU handle to get information about</param>
        /// <returns>Type of bus associated with this GPU</returns>
        /// <exception cref="NVIDIAApiException">Status.InvalidArgument: gpuHandle is NULL</exception>
        public static GPUBusType GetBusType(PhysicalGPUHandle gpuHandle)
        {
            GPUBusType busType;
            var status = DelegateFactory.Get<Delegates.GPU.NvAPI_GPU_GetBusType>()(gpuHandle, out busType);
            if (status != Status.Ok)
                throw new NVIDIAApiException(status);
            return busType;
        }

        /// <summary>
        ///     <para>
        ///         This function retreives the clock frequencies information from specific GPU\n
        ///         IClockFrequenciesInfo contains GPU clock frequencies domain array</para>
        ///     <i>From NVAPI.h:</i> 
        ///     <para>
        ///         This function retrieves the NV_GPU_CLOCK_FREQUENCIES structure for the specified physical GPU.\n
        ///         Each domain's info is indexed in the array</para>
        /// </summary>
        /// <param name="physicalGPUHandle">Handle of the physical GPU for which the memory information is to be extracted.</param>
        /// <returns>The device clock frequencies interface (structures domain array).</returns>
        /// <exception cref="NVIDIANotSupportedException">This operation is not supported.</exception>
        /// <exception cref="NVIDIAApiException">Status.NvidiaDeviceNotFound: No NVIDIA GPU driving a display was found.</exception>
        public static IClockFrequenciesInfo GetAllClockFrequencies(PhysicalGPUHandle physicalGPUHandle) 
        {
            var getClocksInfo = DelegateFactory.Get<Delegates.GPU.NvAPI_GPU_GetAllClockFrequencies>();
            foreach (var acceptType in getClocksInfo.Accepts()) 
            {
                var instance = acceptType.Instantiate<IClockFrequenciesInfo>();
                using (var clockFrequenciesInfo = ValueTypeReference.FromValueType(instance, acceptType)) 
                {
                    var status = getClocksInfo(physicalGPUHandle, clockFrequenciesInfo);
                    if (status == Status.IncompatibleStructureVersion)
                        continue;
                    if (status != Status.Ok)
                        throw new NVIDIAApiException(status);

                    return clockFrequenciesInfo.ToValueType<IClockFrequenciesInfo>(acceptType);
                }
            }
            throw new NVIDIANotSupportedException("This operation is not supported.");
        }


        /// <summary>
        ///     Due to space limitation GetConnectedOutputs() can return maximum 32 devices, but this is no longer true for DPMST.
        ///     GetConnectedDisplayIds() will return all the connected display devices in the form of displayIds for the associated
        ///     gpuHandle.
        ///     This function can accept set of flags to request cached, uncached, sli and lid to get the connected devices.
        ///     Default value for flags will be cached.
        /// </summary>
        /// <param name="gpuHandle">Physical GPU handle to get information about</param>
        /// <param name="flags">ConnectedIdsFlag flags</param>
        /// <returns>An array of display identifications and their attributes</returns>
        /// <exception cref="NVIDIANotSupportedException">This operation is not supported.</exception>
        /// <exception cref="NVIDIAApiException">Status.InvalidArgument: gpuHandle is invalid</exception>
        /// <exception cref="NVIDIAApiException">Status.NvidiaDeviceNotFound: No NVIDIA GPU driving a display was found</exception>
        /// <exception cref="NVIDIAApiException">Status.ExpectedPhysicalGPUHandle: gpuHandle was not a physical GPU handle</exception>
        public static DisplayIdsV2[] GetConnectedDisplayIds(PhysicalGPUHandle gpuHandle, ConnectedIdsFlag flags)
        {
            var gpuGetConnectedDisplayIds = DelegateFactory.Get<Delegates.GPU.NvAPI_GPU_GetConnectedDisplayIds>();
            if (!gpuGetConnectedDisplayIds.Accepts().Contains(typeof(DisplayIdsV2)))
                throw new NVIDIANotSupportedException("This operation is not supported.");

            uint count = 0;
            var status = gpuGetConnectedDisplayIds(gpuHandle, ValueTypeArray.Null, ref count, flags);

            if (status != Status.Ok)
                throw new NVIDIAApiException(status);

            if (count == 0)
                return new DisplayIdsV2[0];
            using (
                var displayIds =
                    ValueTypeArray.FromArray(typeof(DisplayIdsV2).Instantiate<DisplayIdsV2>().Repeat((int) count)))
            {
                status = gpuGetConnectedDisplayIds(gpuHandle, displayIds, ref count, flags);
                if (status != Status.Ok)
                    throw new NVIDIAApiException(status);
                return displayIds.ToArray<DisplayIdsV2>((int) count);
            }
        }

        /// <summary>
        ///     This function returns the current AGP Rate (0 = AGP not present, 1 = 1x, 2 = 2x, etc.).
        /// </summary>
        /// <param name="gpuHandle">Physical GPU handle to get information about</param>
        /// <returns>Current AGP rate</returns>
        /// <exception cref="NVIDIAApiException">Status.InvalidArgument: display is not valid</exception>
        /// <exception cref="NVIDIAApiException">Status.NvidiaDeviceNotFound: No NVIDIA GPU driving a display was found</exception>
        /// <exception cref="NVIDIAApiException">Status.ExpectedPhysicalGPUHandle: gpuHandle was not a physical GPU handle.</exception>
        public static int GetCurrentAGPRate(PhysicalGPUHandle gpuHandle)
        {
            uint agpRate;
            var status = DelegateFactory.Get<Delegates.GPU.NvAPI_GPU_GetCurrentAGPRate>()(gpuHandle, out agpRate);
            if (status != Status.Ok)
                throw new NVIDIAApiException(status);
            return (int) agpRate;
        }

        /// <summary>
        ///     This function returns the number of PCIE lanes being used for the PCIE interface downstream from the GPU.
        /// </summary>
        /// <param name="gpuHandle">Physical GPU handle to get information about</param>
        /// <returns>PCIE lanes being used for the PCIE interface downstream</returns>
        /// <exception cref="NVIDIAApiException">Status.InvalidArgument: display is not valid</exception>
        /// <exception cref="NVIDIAApiException">Status.NvidiaDeviceNotFound: No NVIDIA GPU driving a display was found</exception>
        /// <exception cref="NVIDIAApiException">Status.ExpectedPhysicalGPUHandle: gpuHandle was not a physical GPU handle.</exception>
        public static int GetCurrentPCIEDownStreamWidth(PhysicalGPUHandle gpuHandle)
        {
            uint width;
            var status = DelegateFactory.Get<Delegates.GPU.NvAPI_GPU_GetCurrentPCIEDownstreamWidth>()(gpuHandle,
                out width);
            if (status != Status.Ok)
                throw new NVIDIAApiException(status);
            return (int) width;
        }

        /// <summary>
        ///     This API converts a Physical GPU handle and output ID to a display ID.
        /// </summary>
        /// <param name="gpuHandle">Handle to the physical GPU</param>
        /// <param name="outputId">Connected display output identification on the target GPU - must only have one bit set</param>
        /// <returns>Display identification</returns>
        /// <exception cref="NVIDIAApiException">Status.ApiNotInitialized: NVAPI not initialized</exception>
        /// <exception cref="NVIDIAApiException">Status.Error: miscellaneous error occurred</exception>
        /// <exception cref="NVIDIAApiException">Status.InvalidArgument: Invalid input parameter.</exception>
        public static uint GetDisplayIdFromGPUAndOutputId(PhysicalGPUHandle gpuHandle, OutputId outputId)
        {
            uint display;
            var status = DelegateFactory.Get<Delegates.GPU.NvAPI_SYS_GetDisplayIdFromGpuAndOutputId>()(gpuHandle,
                outputId, out display);
            if (status != Status.Ok)
                throw new NVIDIAApiException(status);
            return display;
        }

        /// <summary>
        ///     This function retrieves the dynamic performance states information from specific GPU
        /// </summary>
        /// <param name="physicalGPUHandle">Handle of the physical GPU for which the memory information is to be extracted.</param>
        /// <returns>The device utilizations information array.</returns>
        /// <exception cref="NVIDIANotSupportedException">This operation is not supported.</exception>
        /// <exception cref="NVIDIAApiException">Status.NvidiaDeviceNotFound: No NVIDIA GPU driving a display was found.</exception>
        public static DynamicPerformanceStatesInfo GetDynamicPerformanceStatesInfoEx(PhysicalGPUHandle physicalGPUHandle)
        {
            var getDynamicPerformanceStatesInfoEx =
                DelegateFactory.Get<Delegates.GPU.NvAPI_GPU_GetDynamicPStatesInfoEx>();
            foreach (var acceptType in getDynamicPerformanceStatesInfoEx.Accepts())
            {
                var instance = acceptType.Instantiate<DynamicPerformanceStatesInfo>();
                using (var gpuDynamicPstateInfo = ValueTypeReference.FromValueType(instance, acceptType))
                {
                    var status = getDynamicPerformanceStatesInfoEx(physicalGPUHandle, gpuDynamicPstateInfo);
                    if (status == Status.IncompatibleStructureVersion)
                        continue;
                    if (status != Status.Ok)
                        throw new NVIDIAApiException(status);

                    return gpuDynamicPstateInfo.ToValueType<DynamicPerformanceStatesInfo>(acceptType);
                }
            }
            throw new NVIDIANotSupportedException("This operation is not supported.");
        }

        /// <summary>
        ///     This function returns the EDID data for the specified GPU handle and connection bit mask.
        ///     outputId should have exactly 1 bit set to indicate a single display.
        /// </summary>
        /// <param name="gpuHandle">Physical GPU handle to check outputs</param>
        /// <param name="outputId">Output identification</param>
        /// <param name="offset">EDID offset</param>
        /// <param name="readIdentification">EDID read identification for multi part read, or zero for first run</param>
        /// <returns>Whole or a part of the EDID data</returns>
        /// <exception cref="NVIDIANotSupportedException">This operation is not supported.</exception>
        /// <exception cref="NVIDIAApiException">
        ///     Status.InvalidArgument: gpuHandle or edid is invalid, outputId has 0 or > 1 bits
        ///     set
        /// </exception>
        /// <exception cref="NVIDIAApiException">Status.NvidiaDeviceNotFound: No NVIDIA GPU driving a display was found.</exception>
        /// <exception cref="NVIDIAApiException">Status.ExpectedPhysicalGPUHandle: gpuHandle was not a physical GPU handle.</exception>
        /// <exception cref="NVIDIAApiException">Status.DataNotFound: The requested display does not contain an EDID.</exception>
        public static EDIDV3 GetEDID(PhysicalGPUHandle gpuHandle, OutputId outputId, int offset,
            int readIdentification = 0)
        {
            var gpuGetEDID = DelegateFactory.Get<Delegates.GPU.NvAPI_GPU_GetEDID>();
            if (!gpuGetEDID.Accepts().Contains(typeof(EDIDV3)))
                throw new NVIDIANotSupportedException("This operation is not supported.");
            var instance = EDIDV3.CreateWithOffset((uint) readIdentification, (uint) offset);
            using (var edidReference = ValueTypeReference.FromValueType(instance))
            {
                var status = gpuGetEDID(gpuHandle, outputId, edidReference);
                if (status != Status.Ok)
                    throw new NVIDIAApiException(status);
                return edidReference.ToValueType<EDIDV3>() ?? default(EDIDV3);
            }
        }

        /// <summary>
        ///     This function returns the EDID data for the specified GPU handle and connection bit mask.
        ///     outputId should have exactly 1 bit set to indicate a single display.
        /// </summary>
        /// <param name="gpuHandle">Physical GPU handle to check outputs</param>
        /// <param name="outputId">Output identification</param>
        /// <returns>Whole or a part of the EDID data</returns>
        /// <exception cref="NVIDIANotSupportedException">This operation is not supported.</exception>
        /// <exception cref="NVIDIAApiException">
        ///     Status.InvalidArgument: gpuHandle or edid is invalid, outputId has 0 or > 1 bits
        ///     set
        /// </exception>
        /// <exception cref="NVIDIAApiException">Status.NvidiaDeviceNotFound: No NVIDIA GPU driving a display was found.</exception>
        /// <exception cref="NVIDIAApiException">Status.ExpectedPhysicalGPUHandle: gpuHandle was not a physical GPU handle.</exception>
        /// <exception cref="NVIDIAApiException">Status.DataNotFound: The requested display does not contain an EDID.</exception>
        public static IEDID GetEDID(PhysicalGPUHandle gpuHandle, OutputId outputId)
        {
            var gpuGetEDID = DelegateFactory.Get<Delegates.GPU.NvAPI_GPU_GetEDID>();
            foreach (var acceptType in gpuGetEDID.Accepts())
                using (var edidReference = ValueTypeReference.FromValueType(acceptType.Instantiate<IEDID>(), acceptType)
                )
                {
                    var status = gpuGetEDID(gpuHandle, outputId, edidReference);
                    if (status != Status.IncompatibleStructureVersion)
                        continue;
                    if (status != Status.Ok)
                        throw new NVIDIAApiException(status);
                    return edidReference.ToValueType<IEDID>(acceptType);
                }
            throw new NVIDIANotSupportedException("This operation is not supported.");
        }

        /// <summary>
        ///     This function retrieves the full GPU name as an ASCII string - for example, "Quadro FX 1400".
        /// </summary>
        /// <param name="gpuHandle">Physical GPU handle to get information about</param>
        /// <returns>Full GPU name as an ASCII string</returns>
        /// <exception cref="NVIDIAApiException">See NVIDIAApiException.Status for the reason of the exception.</exception>
        public static string GetFullName(PhysicalGPUHandle gpuHandle)
        {
            ShortString name;
            var status = DelegateFactory.Get<Delegates.GPU.NvAPI_GPU_GetFullName>()(gpuHandle, out name);
            if (status != Status.Ok)
                throw new NVIDIAApiException(status);
            return name.Value;
        }

        /// <summary>
        ///     This API converts a display ID to a Physical GPU handle and output ID.
        /// </summary>
        /// <param name="displayId">Display identification of display to retrieve GPU and outputId for</param>
        /// <param name="gpuHandle">Handle to the physical GPU</param>
        /// <returns>Connected display output identification on the target GPU will only have one bit set.</returns>
        /// <exception cref="NVIDIAApiException">Status.ApiNotInitialized: NVAPI not initialized</exception>
        /// <exception cref="NVIDIAApiException">Status.Error: Miscellaneous error occurred</exception>
        /// <exception cref="NVIDIAApiException">Status.InvalidArgument: Invalid input parameter</exception>
        /// <exception cref="NVIDIAApiException">
        ///     Status.IdOutOfRange: The DisplayId corresponds to a display which is not within
        ///     the normal outputId range.
        /// </exception>
        public static OutputId GetGPUAndOutputIdFromDisplayId(uint displayId, out PhysicalGPUHandle gpuHandle)
        {
            OutputId outputId;
            var status = DelegateFactory.Get<Delegates.GPU.NvAPI_SYS_GetGpuAndOutputIdFromDisplayId>()(displayId,
                out gpuHandle, out outputId);
            if (status != Status.Ok)
                throw new NVIDIAApiException(status);
            return outputId;
        }

        /// <summary>
        ///     Retrieves the total number of cores defined for a GPU.
        ///     Returns 0 on architectures that don't define GPU cores.
        /// </summary>
        /// <param name="gpuHandle">Physical GPU handle to get information about</param>
        /// <returns>Total number of cores</returns>
        /// <exception cref="NVIDIAApiException">Status.InvalidArgument: display is not valid</exception>
        /// <exception cref="NVIDIAApiException">Status.NvidiaDeviceNotFound: No NVIDIA GPU driving a display was found</exception>
        /// <exception cref="NVIDIAApiException">Status.ExpectedPhysicalGPUHandle: gpuHandle was not a physical GPU handle.</exception>
        /// <exception cref="NVIDIAApiException">Status.NotSupported: API call is not supported on current architecture</exception>
        public static int GetGPUCoreCount(PhysicalGPUHandle gpuHandle)
        {
            uint cores;
            var status = DelegateFactory.Get<Delegates.GPU.NvAPI_GPU_GetGpuCoreCount>()(gpuHandle, out cores);
            if (status != Status.Ok)
                throw new NVIDIAApiException(status);
            return (int) cores;
        }


        /// <summary>
        ///     This function returns the GPU type (integrated or discrete).
        ///     TCC_SUPPORTED
        /// </summary>
        /// <param name="gpuHandle">GPU handle to get information about</param>
        /// <returns>GPU type</returns>
        /// <exception cref="NVIDIAApiException">Status.InvalidArgument: gpuHandle is NULL</exception>
        /// <exception cref="NVIDIAApiException">Status.NvidiaDeviceNotFound: No NVIDIA GPU driving a display was found</exception>
        /// <exception cref="NVIDIAApiException">Status.ExpectedPhysicalGPUHandle: gpuHandle was not a physical GPU handle</exception>
        public static GPUType GetGPUType(PhysicalGPUHandle gpuHandle)
        {
            GPUType type;
            var status = DelegateFactory.Get<Delegates.GPU.NvAPI_GPU_GetGPUType>()(gpuHandle, out type);
            if (status != Status.Ok)
                throw new NVIDIAApiException(status);
            return type;
        }

        /// <summary>
        ///     This function returns the interrupt number associated with this GPU.
        /// </summary>
        /// <param name="gpuHandle">GPU handle to get information about</param>
        /// <returns>Interrupt number associated with this GPU</returns>
        /// <exception cref="NVIDIAApiException">Status.InvalidArgument: gpuHandle is NULL</exception>
        /// <exception cref="NVIDIAApiException">Status.NvidiaDeviceNotFound: No NVIDIA GPU driving a display was found</exception>
        /// <exception cref="NVIDIAApiException">Status.ExpectedPhysicalGPUHandle: gpuHandle was not a physical GPU handle</exception>
        public static int GetIRQ(PhysicalGPUHandle gpuHandle)
        {
            uint irq;
            var status = DelegateFactory.Get<Delegates.GPU.NvAPI_GPU_GetIRQ>()(gpuHandle, out irq);
            if (status != Status.Ok)
                throw new NVIDIAApiException(status);
            return (int) irq;
        }

        /// <summary>
        ///     This function returns the logical GPU handle associated with the specified display.
        ///     At least one GPU must be present in the system and running an NVIDIA display driver.
        ///     display can be DisplayHandle.DefaultHandle or a handle enumerated from EnumNVidiaDisplayHandle().
        /// </summary>
        /// <param name="display">Display handle to get information about</param>
        /// <returns>Logical GPU handle associated with the specified display</returns>
        /// <exception cref="NVIDIAApiException">Status.InvalidArgument: display is not valid</exception>
        /// <exception cref="NVIDIAApiException">Status.NvidiaDeviceNotFound: No NVIDIA GPU driving a display was found</exception>
        public static LogicalGPUHandle GetLogicalGPUFromDisplay(DisplayHandle display)
        {
            LogicalGPUHandle gpu;
            var status = DelegateFactory.Get<Delegates.GPU.NvAPI_GetLogicalGPUFromDisplay>()(display, out gpu);
            if (status != Status.Ok)
                throw new NVIDIAApiException(status);
            return gpu;
        }


        /// <summary>
        ///     This function returns the logical GPU handle associated with specified physical GPU handle.
        ///     At least one GPU must be present in the system and running an NVIDIA display driver.
        /// </summary>
        /// <param name="gpuHandle">GPU handle to get information about</param>
        /// <returns>Logical GPU handle associated with specified physical GPU handle</returns>
        /// <exception cref="NVIDIAApiException">Status.InvalidArgument: gpuHandle is NULL</exception>
        /// <exception cref="NVIDIAApiException">Status.NvidiaDeviceNotFound: No NVIDIA GPU driving a display was found</exception>
        public static LogicalGPUHandle GetLogicalGPUFromPhysicalGPU(PhysicalGPUHandle gpuHandle)
        {
            LogicalGPUHandle gpu;
            var status = DelegateFactory.Get<Delegates.GPU.NvAPI_GetLogicalGPUFromPhysicalGPU>()(gpuHandle, out gpu);
            if (status != Status.Ok)
                throw new NVIDIAApiException(status);
            return gpu;
        }

        /// <summary>
        ///     This function retrieves the available driver memory footprint for the specified GPU.
        ///     If the GPU is in TCC Mode, only dedicatedVideoMemory will be returned.
        /// </summary>
        /// <param name="physicalGPUHandle">Handle of the physical GPU for which the memory information is to be extracted.</param>
        /// <returns>The memory footprint available in the driver.</returns>
        /// <exception cref="NVIDIANotSupportedException">This operation is not supported.</exception>
        /// <exception cref="NVIDIAApiException">Status.NvidiaDeviceNotFound: No NVIDIA GPU driving a display was found.</exception>
        public static IDisplayDriverMemoryInfo GetMemoryInfo(PhysicalGPUHandle physicalGPUHandle)
        {
            var getMemoryInfo = DelegateFactory.Get<Delegates.GPU.NvAPI_GPU_GetMemoryInfo>();
            foreach (var acceptType in getMemoryInfo.Accepts())
            {
                var instance = acceptType.Instantiate<IDisplayDriverMemoryInfo>();
                using (var displayDriverMemoryInfo = ValueTypeReference.FromValueType(instance, acceptType))
                {
                    var status = getMemoryInfo(physicalGPUHandle, displayDriverMemoryInfo);
                    if (status == Status.IncompatibleStructureVersion)
                        continue;
                    if (status != Status.Ok)
                        throw new NVIDIAApiException(status);
                    return displayDriverMemoryInfo.ToValueType<IDisplayDriverMemoryInfo>(acceptType);
                }
            }
            throw new NVIDIANotSupportedException("This operation is not supported.");
        }

        /// <summary>
        ///     This function returns the output type. User can either specify both 'physical GPU handle and outputId (exactly 1
        ///     bit set)' or a valid displayId in the outputId parameter.
        /// </summary>
        /// <param name="gpuHandle">GPU handle to get information about</param>
        /// <param name="outputId">Output identification of the output to get information about</param>
        /// <returns>Type of the output</returns>
        /// <exception cref="NVIDIAApiException">Status.InvalidArgument: gpuHandle is NULL</exception>
        /// <exception cref="NVIDIAApiException">Status.NvidiaDeviceNotFound: No NVIDIA GPU driving a display was found</exception>
        /// <exception cref="NVIDIAApiException">Status.ExpectedPhysicalGPUHandle: gpuHandle was not a physical GPU handle</exception>
        public static OutputType GetOutputType(PhysicalGPUHandle gpuHandle, OutputId outputId)
        {
            return GetOutputType(gpuHandle, (uint) outputId);
        }

        /// <summary>
        ///     This function returns the output type. User can either specify both 'physical GPU handle and outputId (exactly 1
        ///     bit set)' or a valid displayId in the outputId parameter.
        /// </summary>
        /// <param name="gpuHandle">GPU handle to get information about</param>
        /// <param name="displayId">Display identification of the devide to get information about</param>
        /// <returns>Type of the output</returns>
        /// <exception cref="NVIDIAApiException">Status.InvalidArgument: gpuHandle is NULL</exception>
        /// <exception cref="NVIDIAApiException">Status.NvidiaDeviceNotFound: No NVIDIA GPU driving a display was found</exception>
        /// <exception cref="NVIDIAApiException">Status.ExpectedPhysicalGPUHandle: gpuHandle was not a physical GPU handle</exception>
        public static OutputType GetOutputType(PhysicalGPUHandle gpuHandle, uint displayId)
        {
            OutputType type;
            var status = DelegateFactory.Get<Delegates.GPU.NvAPI_GPU_GetOutputType>()(gpuHandle, displayId, out type);
            if (status != Status.Ok)
                throw new NVIDIAApiException(status);
            return type;
        }

        /// <summary>
        ///     This function returns the PCI identifiers associated with this GPU.
        ///     TCC_SUPPORTED
        /// </summary>
        /// <param name="gpuHandle">GPU handle to get information about</param>
        /// <param name="deviceId">The internal PCI device identifier for the GPU.</param>
        /// <param name="subSystemId">The internal PCI subsystem identifier for the GPU.</param>
        /// <param name="revisionId">The internal PCI device-specific revision identifier for the GPU.</param>
        /// <param name="extDeviceId">The external PCI device identifier for the GPU.</param>
        /// <exception cref="NVIDIAApiException">Status.InvalidArgument: gpuHandle or an argument is NULL</exception>
        /// <exception cref="NVIDIAApiException">Status.NvidiaDeviceNotFound: No NVIDIA GPU driving a display was found</exception>
        /// <exception cref="NVIDIAApiException">Status.ExpectedPhysicalGPUHandle: gpuHandle was not a physical GPU handle</exception>
        public static void GetPCIIdentifiers(PhysicalGPUHandle gpuHandle, out uint deviceId, out uint subSystemId,
            out uint revisionId, out uint extDeviceId)
        {
            var status = DelegateFactory.Get<Delegates.GPU.NvAPI_GPU_GetPCIIdentifiers>()(gpuHandle, out deviceId,
                out subSystemId, out revisionId, out extDeviceId);
            if (status != Status.Ok)
                throw new NVIDIAApiException(status);
        }

        /// <summary>
        ///     This function returns the physical size of framebuffer in KB.  This does NOT include any system RAM that may be
        ///     dedicated for use by the GPU.
        ///     TCC_SUPPORTED
        /// </summary>
        /// <param name="gpuHandle">GPU handle to get information about</param>
        /// <returns>Physical size of framebuffer in KB</returns>
        /// <exception cref="NVIDIAApiException">Status.NvidiaDeviceNotFound: No NVIDIA GPU driving a display was found</exception>
        /// <exception cref="NVIDIAApiException">Status.ExpectedPhysicalGPUHandle: gpuHandle was not a physical GPU handle</exception>
        public static int GetPhysicalFrameBufferSize(PhysicalGPUHandle gpuHandle)
        {
            uint size;
            var status = DelegateFactory.Get<Delegates.GPU.NvAPI_GPU_GetPhysicalFrameBufferSize>()(gpuHandle, out size);
            if (status != Status.Ok)
                throw new NVIDIAApiException(status);
            return (int) size;
        }

        /// <summary>
        ///     This API retrieves the Physical GPU handle of the connected display
        /// </summary>
        /// <param name="displayId">Display identification of display to retrieve GPU handle</param>
        /// <returns>Handle to the physical GPU</returns>
        /// <exception cref="NVIDIAApiException">Status.ApiNotInitialized: NVAPI not initialized</exception>
        /// <exception cref="NVIDIAApiException">Status.Error: Miscellaneous error occurred</exception>
        /// <exception cref="NVIDIAApiException">Status.InvalidArgument: Invalid input parameter</exception>
        public static PhysicalGPUHandle GetPhysicalGPUFromDisplayId(uint displayId)
        {
            PhysicalGPUHandle gpu;
            var status = DelegateFactory.Get<Delegates.GPU.NvAPI_SYS_GetPhysicalGpuFromDisplayId>()(displayId, out gpu);
            if (status != Status.Ok)
                throw new NVIDIAApiException(status);
            return gpu;
        }

        /// <summary>
        ///     This function returns a physical GPU handle associated with the specified unattached display.
        ///     The source GPU is a physical render GPU which renders the frame buffer but may or may not drive the scan out.
        ///     At least one GPU must be present in the system and running an NVIDIA display driver.
        /// </summary>
        /// <param name="display">Display handle to get information about</param>
        /// <returns>Physical GPU handle associated with the specified unattached display.</returns>
        /// <exception cref="NVIDIAApiException">Status.InvalidArgument: display is not valid</exception>
        /// <exception cref="NVIDIAApiException">Status.NvidiaDeviceNotFound: No NVIDIA GPU driving a display was found</exception>
        public static PhysicalGPUHandle GetPhysicalGPUFromUnAttachedDisplay(UnAttachedDisplayHandle display)
        {
            PhysicalGPUHandle gpu;
            var status = DelegateFactory.Get<Delegates.GPU.NvAPI_GetPhysicalGPUFromUnAttachedDisplay>()(display, out gpu);
            if (status != Status.Ok)
                throw new NVIDIAApiException(status);
            return gpu;
        }

        /// <summary>
        ///     This function returns an array of physical GPU handles associated with the specified display.
        ///     At least one GPU must be present in the system and running an NVIDIA display driver.
        ///     If the display corresponds to more than one physical GPU, the first GPU returned is the one with the attached
        ///     active output.
        /// </summary>
        /// <param name="display">Display handle to get information about</param>
        /// <returns>An array of physical GPU handles</returns>
        /// <exception cref="NVIDIAApiException">Status.InvalidArgument: display is not valid</exception>
        /// <exception cref="NVIDIAApiException">Status.NvidiaDeviceNotFound: No NVIDIA GPU driving a display was found</exception>
        public static PhysicalGPUHandle[] GetPhysicalGPUsFromDisplay(DisplayHandle display)
        {
            var gpuList =
                typeof(PhysicalGPUHandle).Instantiate<PhysicalGPUHandle>().Repeat(PhysicalGPUHandle.MaxPhysicalGPUs);
            uint count;
            var status = DelegateFactory.Get<Delegates.GPU.NvAPI_GetPhysicalGPUsFromDisplay>()(display, gpuList,
                out count);
            if (status != Status.Ok)
                throw new NVIDIAApiException(status);
            return gpuList.Take((int) count).ToArray();
        }

        /// <summary>
        ///     This function returns the physical GPU handles associated with the specified logical GPU handle.
        ///     At least one GPU must be present in the system and running an NVIDIA display driver.
        /// </summary>
        /// <param name="gpuHandle">Logical GPU handle to get information about</param>
        /// <returns>An array of physical GPU handles</returns>
        /// <exception cref="NVIDIAApiException">Status.InvalidArgument: gpuHandle is not valid</exception>
        /// <exception cref="NVIDIAApiException">Status.NvidiaDeviceNotFound: No NVIDIA GPU driving a display was found</exception>
        /// <exception cref="NVIDIAApiException">Status.ExpectedLogicalGPUHandle: gpuHandle was not a logical GPU handle</exception>
        public static PhysicalGPUHandle[] GetPhysicalGPUsFromLogicalGPU(LogicalGPUHandle gpuHandle)
        {
            var gpuList =
                typeof(PhysicalGPUHandle).Instantiate<PhysicalGPUHandle>().Repeat(PhysicalGPUHandle.MaxPhysicalGPUs);
            uint count;
            var status = DelegateFactory.Get<Delegates.GPU.NvAPI_GetPhysicalGPUsFromLogicalGPU>()(gpuHandle, gpuList,
                out count);
            if (status != Status.Ok)
                throw new NVIDIAApiException(status);
            return gpuList.Take((int) count).ToArray();
        }

        /// <summary>
        ///     This function retrieves the Quadro status for the GPU (true if Quadro, false if GeForce)
        /// </summary>
        /// <param name="gpuHandle">GPU handle to get information about</param>
        /// <returns>true if Quadro, false if GeForce</returns>
        /// <exception cref="NVIDIAApiException">Status.Error: Miscellaneous error occurred</exception>
        public static bool GetQuadroStatus(PhysicalGPUHandle gpuHandle)
        {
            uint isQuadro;
            var status = DelegateFactory.Get<Delegates.GPU.NvAPI_GPU_GetQuadroStatus>()(gpuHandle, out isQuadro);
            if (status != Status.Ok)
                throw new NVIDIAApiException(status);
            return isQuadro > 0;
        }

        /// <summary>
        ///     This function retrieves the number of Shader SubPipes on the GPU
        ///     On newer architectures, this corresponds to the number of SM units
        /// </summary>
        /// <param name="gpuHandle">GPU handle to get information about</param>
        /// <returns>Number of Shader SubPipes on the GPU</returns>
        /// <exception cref="NVIDIAApiException">Status.NvidiaDeviceNotFound: No NVIDIA GPU driving a display was found</exception>
        /// <exception cref="NVIDIAApiException">Status.ExpectedPhysicalGPUHandle: gpuHandle was not a physical GPU handle</exception>
        public static int GetShaderSubPipeCount(PhysicalGPUHandle gpuHandle)
        {
            uint count;
            var status = DelegateFactory.Get<Delegates.GPU.NvAPI_GPU_GetShaderSubPipeCount>()(gpuHandle, out count);
            if (status != Status.Ok)
                throw new NVIDIAApiException(status);
            return (int) count;
        }

        /// <summary>
        ///     This function identifies whether the GPU is a notebook GPU or a desktop GPU.
        /// </summary>
        /// <param name="gpuHandle">GPU handle to get information about</param>
        /// <returns>GPU system type</returns>
        /// <exception cref="NVIDIAApiException">Status.InvalidArgument: gpuHandle is NULL</exception>
        /// <exception cref="NVIDIAApiException">Status.NvidiaDeviceNotFound: No NVIDIA GPU driving a display was found</exception>
        /// <exception cref="NVIDIAApiException">Status.ExpectedPhysicalGPUHandle: gpuHandle was not a physical GPU handle</exception>
        public static SystemType GetSystemType(PhysicalGPUHandle gpuHandle)
        {
            SystemType systemType;
            var status = DelegateFactory.Get<Delegates.GPU.NvAPI_GPU_GetSystemType>()(gpuHandle, out systemType);
            if (status != Status.Ok)
                throw new NVIDIAApiException(status);
            return systemType;
        }

        /// <summary>
        ///     This function retrieves the thermal information of all thermal sensors or specific thermal sensor associated with
        ///     the selected GPU. To retrieve info for all sensors, set sensorTarget to ThermalSettingsTarget.All.
        /// </summary>
        /// <param name="physicalGPUHandle">Handle of the physical GPU for which the memory information is to be extracted.</param>
        /// <param name="sensorTarget">Specifies the requested thermal sensor target.</param>
        /// <returns>The device thermal sensors information array.</returns>
        /// <exception cref="NVIDIANotSupportedException">This operation is not supported.</exception>
        /// <exception cref="NVIDIAApiException">Status.NvidiaDeviceNotFound: No NVIDIA GPU driving a display was found.</exception>
        public static IThermalSensor[] GetThermalSettings(PhysicalGPUHandle physicalGPUHandle,
            ThermalSettingsTarget sensorTarget = ThermalSettingsTarget.All)
        {
            var getThermalSettings = DelegateFactory.Get<Delegates.GPU.NvAPI_GPU_GetThermalSettings>();
            foreach (var acceptType in getThermalSettings.Accepts())
            {
                var instance = acceptType.Instantiate<IThermalSettings>();
                using (var gpuThermalSettings = ValueTypeReference.FromValueType(instance, acceptType))
                {
                    var status = getThermalSettings(physicalGPUHandle, sensorTarget, gpuThermalSettings);
                    if (status == Status.IncompatibleStructureVersion)
                        continue;
                    if (status != Status.Ok)
                        throw new NVIDIAApiException(status);

                    return gpuThermalSettings.ToValueType<IThermalSettings>(acceptType).Sensors;
                }
            }
            throw new NVIDIANotSupportedException("This operation is not supported.");
        }

        /// <summary>
        ///     This function returns the OEM revision of the video BIOS associated with this GPU.
        /// </summary>
        /// <param name="gpuHandle">GPU handle to get information about</param>
        /// <returns>OEM revision of the video BIOS</returns>
        /// <exception cref="NVIDIAApiException">Status.NvidiaDeviceNotFound: No NVIDIA GPU driving a display was found.</exception>
        /// <exception cref="NVIDIAApiException">Status.ExpectedPhysicalGPUHandle: gpuHandle was not a physical GPU handle.</exception>
        public static uint GetVBIOSOEMRevision(PhysicalGPUHandle gpuHandle)
        {
            uint revision;
            var status = DelegateFactory.Get<Delegates.GPU.NvAPI_GPU_GetVbiosOEMRevision>()(gpuHandle, out revision);
            if (status != Status.Ok)
                throw new NVIDIAApiException(status);
            return revision;
        }

        /// <summary>
        ///     This function returns the revision of the video BIOS associated with this GPU.
        ///     TCC_SUPPORTED
        /// </summary>
        /// <param name="gpuHandle">GPU handle to get information about</param>
        /// <returns>Revision of the video BIOS</returns>
        /// <exception cref="NVIDIAApiException">Status.NvidiaDeviceNotFound: No NVIDIA GPU driving a display was found.</exception>
        /// <exception cref="NVIDIAApiException">Status.ExpectedPhysicalGPUHandle: gpuHandle was not a physical GPU handle.</exception>
        public static uint GetVBIOSRevision(PhysicalGPUHandle gpuHandle)
        {
            uint revision;
            var status = DelegateFactory.Get<Delegates.GPU.NvAPI_GPU_GetVbiosRevision>()(gpuHandle, out revision);
            if (status != Status.Ok)
                throw new NVIDIAApiException(status);
            return revision;
        }

        /// <summary>
        ///     This function returns the full video BIOS version string in the form of xx.xx.xx.xx.yy where xx numbers come from
        ///     GetVbiosRevision() and yy comes from GetVbiosOEMRevision().
        /// </summary>
        /// <param name="gpuHandle">Physical GPU handle to get information about</param>
        /// <returns>Full video BIOS version string</returns>
        /// <exception cref="NVIDIAApiException">Status.InvalidArgument: display is not valid</exception>
        /// <exception cref="NVIDIAApiException">Status.NvidiaDeviceNotFound: No NVIDIA GPU driving a display was found</exception>
        /// <exception cref="NVIDIAApiException">Status.ExpectedPhysicalGPUHandle: gpuHandle was not a physical GPU handle.</exception>
        public static string GetVBIOSVersionString(PhysicalGPUHandle gpuHandle)
        {
            ShortString version;
            var status = DelegateFactory.Get<Delegates.GPU.NvAPI_GPU_GetVbiosVersionString>()(gpuHandle, out version);
            if (status != Status.Ok)
                throw new NVIDIAApiException(status);
            return version.Value;
        }

        /// <summary>
        ///     This function returns the virtual size of framebuffer in KB. This includes the physical RAM plus any system RAM
        ///     that has been dedicated for use by the GPU.
        /// </summary>
        /// <param name="gpuHandle">Physical GPU handle to get information about</param>
        /// <returns>Virtual size of framebuffer in KB</returns>
        /// <exception cref="NVIDIAApiException">Status.InvalidArgument: display is not valid</exception>
        /// <exception cref="NVIDIAApiException">Status.NvidiaDeviceNotFound: No NVIDIA GPU driving a display was found</exception>
        /// <exception cref="NVIDIAApiException">Status.ExpectedPhysicalGPUHandle: gpuHandle was not a physical GPU handle.</exception>
        public static int GetVirtualFrameBufferSize(PhysicalGPUHandle gpuHandle)
        {
            uint bufferSize;
            var status = DelegateFactory.Get<Delegates.GPU.NvAPI_GPU_GetVirtualFrameBufferSize>()(gpuHandle,
                out bufferSize);
            if (status != Status.Ok)
                throw new NVIDIAApiException(status);
            return (int) bufferSize;
        }

        /// <summary>
        ///     Thus function sets the EDID data for the specified GPU handle and connection bit mask.
        ///     User can either send (Gpu handle and output id) or only display Id in variable outputId parameter and gpuHandle
        ///     parameter can be default handle.
        ///     Note: The EDID will be cached across the boot session and will be enumerated to the OS in this call. To remove the
        ///     EDID set sizeofEDID to zero. OS and NVAPI connection status APIs will reflect the newly set or removed EDID
        ///     dynamically.
        ///     This feature will NOT be supported on the following boards: GeForce, Quadro VX, Tesla
        /// </summary>
        /// <param name="gpuHandle">Physical GPU handle to check outputs</param>
        /// <param name="outputId">Output identification</param>
        /// <param name="edid">EDID information</param>
        /// <exception cref="NVIDIANotSupportedException">This operation is not supported.</exception>
        /// <exception cref="NVIDIAApiException">
        ///     Status.InvalidArgument: gpuHandle or edid is invalid, outputId has 0 or > 1 bits
        ///     set
        /// </exception>
        /// <exception cref="NVIDIAApiException">Status.NvidiaDeviceNotFound: No NVIDIA GPU driving a display was found.</exception>
        /// <exception cref="NVIDIAApiException">Status.ExpectedPhysicalGPUHandle: gpuHandle was not a physical GPU handle.</exception>
        /// <exception cref="NVIDIAApiException">Status.NotSupported: For the above mentioned GPUs</exception>
        public static void SetEDID(PhysicalGPUHandle gpuHandle, OutputId outputId, IEDID edid)
        {
            SetEDID(gpuHandle, (uint) outputId, edid);
        }

        /// <summary>
        ///     Thus function sets the EDID data for the specified GPU handle and connection bit mask.
        ///     User can either send (Gpu handle and output id) or only display Id in variable outputId parameter and gpuHandle
        ///     parameter can be default handle.
        ///     Note: The EDID will be cached across the boot session and will be enumerated to the OS in this call. To remove the
        ///     EDID set sizeofEDID to zero. OS and NVAPI connection status APIs will reflect the newly set or removed EDID
        ///     dynamically.
        ///     This feature will NOT be supported on the following boards: GeForce, Quadro VX, Tesla
        /// </summary>
        /// <param name="gpuHandle">Physical GPU handle to check outputs</param>
        /// <param name="displayId">Output identification</param>
        /// <param name="edid">EDID information</param>
        /// <exception cref="NVIDIANotSupportedException">This operation is not supported.</exception>
        /// <exception cref="NVIDIAApiException">
        ///     Status.InvalidArgument: gpuHandle or edid is invalid, outputId has 0 or > 1 bits
        ///     set
        /// </exception>
        /// <exception cref="NVIDIAApiException">Status.NvidiaDeviceNotFound: No NVIDIA GPU driving a display was found.</exception>
        /// <exception cref="NVIDIAApiException">Status.ExpectedPhysicalGPUHandle: gpuHandle was not a physical GPU handle.</exception>
        /// <exception cref="NVIDIAApiException">Status.NotSupported: For the above mentioned GPUs</exception>
        public static void SetEDID(PhysicalGPUHandle gpuHandle, uint displayId, IEDID edid)
        {
            var gpuSetEDID = DelegateFactory.Get<Delegates.GPU.NvAPI_GPU_SetEDID>();
            if (!gpuSetEDID.Accepts().Contains(edid.GetType()))
                throw new NVIDIANotSupportedException("This operation is not supported.");
            using (var edidReference = ValueTypeReference.FromValueType(edid, edid.GetType()))
            {
                var status = gpuSetEDID(gpuHandle, displayId, edidReference);
                if (status != Status.Ok)
                    throw new NVIDIAApiException(status);
            }
        }


        /// <summary>
        ///     This function determines if a set of GPU outputs can be active simultaneously.  While a GPU may have 'n' outputs,
        ///     typically they cannot all be active at the same time due to internal resource sharing.
        ///     Given a physical GPU handle and a mask of candidate outputs, this call will return true if all of the specified
        ///     outputs can be driven simultaneously. It will return false if they cannot.
        /// </summary>
        /// <param name="gpuHandle">Physical GPU handle to check outputs</param>
        /// <param name="outputIds">Output identification combination</param>
        /// <returns>true if all of the specified outputs can be driven simultaneously. It will return false if they cannot.</returns>
        /// <exception cref="NVIDIAApiException">Status.InvalidArgument: display is not valid</exception>
        /// <exception cref="NVIDIAApiException">Status.NvidiaDeviceNotFound: No NVIDIA GPU driving a display was found</exception>
        /// <exception cref="NVIDIAApiException">Status.ExpectedPhysicalGPUHandle: gpuHandle was not a physical GPU handle.</exception>
        public static bool ValidateOutputCombination(PhysicalGPUHandle gpuHandle, OutputId outputIds)
        {
            var status = DelegateFactory.Get<Delegates.GPU.NvAPI_GPU_ValidateOutputCombination>()(gpuHandle, outputIds);
            if (status == Status.InvalidCombination)
                return false;
            if (status != Status.Ok)
                throw new NVIDIAApiException(status);
            return true;
        }
    }
}