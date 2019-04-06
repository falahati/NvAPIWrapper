using System;
using System.Diagnostics.CodeAnalysis;
using System.Linq;
using NvAPIWrapper.Native.Display.Structures;
using NvAPIWrapper.Native.Exceptions;
using NvAPIWrapper.Native.General;
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
    // ReSharper disable once ClassTooBig
    public static class GPUApi
    {
        /// <summary>
        ///     [PRIVATE]
        ///     Gets the current power policies information for the passed GPU handle.
        /// </summary>
        /// <param name="gpuHandle">The handle of the GPU to perform the operation on.</param>
        /// <returns>The current power policies information.</returns>
        public static PrivatePowerPoliciesInfoV1 ClientPowerPoliciesGetInfo(PhysicalGPUHandle gpuHandle)
        {
            var instance = typeof(PrivatePowerPoliciesInfoV1).Instantiate<PrivatePowerPoliciesInfoV1>();

            using (var policiesInfoReference = ValueTypeReference.FromValueType(instance))
            {
                var status =
                    DelegateFactory.GetDelegate<Delegates.GPU.NvAPI_GPU_ClientPowerPoliciesGetInfo>()(gpuHandle,
                        policiesInfoReference);

                if (status != Status.Ok)
                {
                    throw new NVIDIAApiException(status);
                }

                return policiesInfoReference.ToValueType<PrivatePowerPoliciesInfoV1>(
                    typeof(PrivatePowerPoliciesInfoV1));
            }
        }

        /// <summary>
        ///     [PRIVATE]
        ///     Gets the power policies status for the passed GPU handle.
        /// </summary>
        /// <param name="gpuHandle">The handle of the GPU to perform the operation on.</param>
        /// <returns>The power policies status.</returns>
        public static PrivatePowerPoliciesStatusV1 ClientPowerPoliciesGetStatus(PhysicalGPUHandle gpuHandle)
        {
            var instance = typeof(PrivatePowerPoliciesStatusV1).Instantiate<PrivatePowerPoliciesStatusV1>();

            using (var policiesStatusReference = ValueTypeReference.FromValueType(instance))
            {
                var status = DelegateFactory.GetDelegate<Delegates.GPU.NvAPI_GPU_ClientPowerPoliciesGetStatus>()(
                    gpuHandle,
                    policiesStatusReference
                );

                if (status != Status.Ok)
                {
                    throw new NVIDIAApiException(status);
                }

                return policiesStatusReference.ToValueType<PrivatePowerPoliciesStatusV1>(
                    typeof(PrivatePowerPoliciesStatusV1));
            }
        }

        /// <summary>
        ///     [PRIVATE]
        ///     Sets the power policies status for the passed GPU handle.
        /// </summary>
        /// <param name="gpuHandle">The handle of the GPU to perform the operation on.</param>
        /// <param name="policiesStatus">The new power limiter policy.</param>
        public static void ClientPowerPoliciesSetStatus(
            PhysicalGPUHandle gpuHandle,
            PrivatePowerPoliciesStatusV1 policiesStatus)
        {
            using (var policiesStatusReference = ValueTypeReference.FromValueType(policiesStatus))
            {
                var status = DelegateFactory.GetDelegate<Delegates.GPU.NvAPI_GPU_ClientPowerPoliciesSetStatus>()(
                    gpuHandle,
                    policiesStatusReference
                );

                if (status != Status.Ok)
                {
                    throw new NVIDIAApiException(status);
                }
            }
        }

        /// <summary>
        ///     [PRIVATE]
        ///     Gets the power topology status for the passed GPU handle.
        /// </summary>
        /// <param name="gpuHandle">The handle of the GPU to perform the operation on.</param>
        /// <returns>The power topology status.</returns>
        public static PrivatePowerTopologiesStatusV1 ClientPowerTopologyGetStatus(PhysicalGPUHandle gpuHandle)
        {
            var instance = typeof(PrivatePowerTopologiesStatusV1).Instantiate<PrivatePowerTopologiesStatusV1>();

            using (var topologiesStatusReference = ValueTypeReference.FromValueType(instance))
            {
                var status = DelegateFactory.GetDelegate<Delegates.GPU.NvAPI_GPU_ClientPowerTopologyGetStatus>()(
                    gpuHandle,
                    topologiesStatusReference
                );

                if (status != Status.Ok)
                {
                    throw new NVIDIAApiException(status);
                }

                return topologiesStatusReference.ToValueType<PrivatePowerTopologiesStatusV1>(
                    typeof(PrivatePowerTopologiesStatusV1));
            }
        }

        /// <summary>
        ///     [PRIVATE]
        ///     Enables the dynamic performance states
        /// </summary>
        /// <param name="gpuHandle">The handle of the GPU to perform the operation on.</param>
        public static void EnableDynamicPStates(PhysicalGPUHandle gpuHandle)
        {
            var status = DelegateFactory.GetDelegate<Delegates.GPU.NvAPI_GPU_EnableDynamicPStates>()(
                gpuHandle
            );

            if (status != Status.Ok)
            {
                throw new NVIDIAApiException(status);
            }
        }


        /// <summary>
        ///     [PRIVATE]
        ///     Enables the overclocked performance states
        /// </summary>
        /// <param name="gpuHandle">The handle of the GPU to perform the operation on.</param>
        public static void EnableOverclockedPStates(PhysicalGPUHandle gpuHandle)
        {
            var status = DelegateFactory.GetDelegate<Delegates.GPU.NvAPI_GPU_EnableOverclockedPStates>()(
                gpuHandle
            );

            if (status != Status.Ok)
            {
                throw new NVIDIAApiException(status);
            }
        }

        /// <summary>
        ///     This function returns an array of logical GPU handles.
        ///     Each handle represents one or more GPUs acting in concert as a single graphics device.
        ///     At least one GPU must be present in the system and running an NVIDIA display driver.
        ///     All logical GPUs handles get invalidated on a GPU topology change, so the calling application is required to
        ///     re-enum
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
            var status = DelegateFactory.GetDelegate<Delegates.GPU.NvAPI_EnumLogicalGPUs>()(gpuList, out var count);

            if (status != Status.Ok)
            {
                throw new NVIDIAApiException(status);
            }

            return gpuList.Take((int) count).ToArray();
        }

        /// <summary>
        ///     This function returns an array of physical GPU handles.
        ///     Each handle represents a physical GPU present in the system.
        ///     That GPU may be part of an SLI configuration, or may not be visible to the OS directly.
        ///     At least one GPU must be present in the system and running an NVIDIA display driver.
        ///     Note: In drivers older than 105.00, all physical GPU handles get invalidated on a mode-set. So the calling
        ///     applications need to re-enum the handles after every mode-set. With drivers 105.00 and up, all physical GPU handles
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
            var status = DelegateFactory.GetDelegate<Delegates.GPU.NvAPI_EnumPhysicalGPUs>()(gpuList, out var count);

            if (status != Status.Ok)
            {
                throw new NVIDIAApiException(status);
            }

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
            var status = DelegateFactory.GetDelegate<Delegates.GPU.NvAPI_EnumTCCPhysicalGPUs>()(gpuList, out var count);

            if (status != Status.Ok)
            {
                throw new NVIDIAApiException(status);
            }

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
            var status =
                DelegateFactory.GetDelegate<Delegates.GPU.NvAPI_GPU_GetActiveOutputs>()(gpuHandle, out var outputMask);

            if (status != Status.Ok)
            {
                throw new NVIDIAApiException(status);
            }

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
            var status =
                DelegateFactory.GetDelegate<Delegates.GPU.NvAPI_GPU_GetAGPAperture>()(gpuHandle, out var agpAperture);

            if (status != Status.Ok)
            {
                throw new NVIDIAApiException(status);
            }

            return (int) agpAperture;
        }

        /// <summary>
        ///     This function retrieves the clock frequencies information from an specific physical GPU and fills the structure
        /// </summary>
        /// <param name="physicalGPUHandle">
        ///     Handle of the physical GPU for which the clock frequency information is to be
        ///     retrieved.
        /// </param>
        /// <param name="clockFrequencyOptions">
        ///     The structure that holds options for the operations and should be filled with the
        ///     results, use null to return current clock frequencies
        /// </param>
        /// <returns>The device clock frequencies information.</returns>
        /// <exception cref="NVIDIANotSupportedException">This operation is not supported.</exception>
        /// <exception cref="NVIDIAApiException">Status.NvidiaDeviceNotFound: No NVIDIA GPU driving a display was found.</exception>
        /// <exception cref="Exception">A delegate callback throws an exception.</exception>
        public static IClockFrequencies GetAllClockFrequencies(
            PhysicalGPUHandle physicalGPUHandle,
            IClockFrequencies clockFrequencyOptions = null)
        {
            var getClocksInfo = DelegateFactory.GetDelegate<Delegates.GPU.NvAPI_GPU_GetAllClockFrequencies>();

            if (clockFrequencyOptions == null)
            {
                foreach (var acceptType in getClocksInfo.Accepts())
                {
                    var instance = acceptType.Instantiate<IClockFrequencies>();

                    using (var clockFrequenciesInfo = ValueTypeReference.FromValueType(instance, acceptType))
                    {
                        var status = getClocksInfo(physicalGPUHandle, clockFrequenciesInfo);

                        if (status == Status.IncompatibleStructureVersion)
                        {
                            continue;
                        }

                        if (status != Status.Ok)
                        {
                            throw new NVIDIAApiException(status);
                        }

                        return clockFrequenciesInfo.ToValueType<IClockFrequencies>(acceptType);
                    }
                }
            }
            else
            {
                using (var clockFrequenciesInfo =
                    ValueTypeReference.FromValueType(clockFrequencyOptions, clockFrequencyOptions.GetType()))
                {
                    var status = getClocksInfo(physicalGPUHandle, clockFrequenciesInfo);

                    if (status != Status.Ok)
                    {
                        throw new NVIDIAApiException(status);
                    }

                    return clockFrequenciesInfo.ToValueType<IClockFrequencies>(clockFrequencyOptions.GetType());
                }
            }

            throw new NVIDIANotSupportedException("This operation is not supported.");
        }

        /// <summary>
        ///     This API returns display IDs for all possible outputs on the GPU.
        ///     For DPMST connector, it will return display IDs for all the video sinks in the topology.
        /// </summary>
        /// <param name="gpuHandle">Physical GPU handle to get information about</param>
        /// <returns>An array of display identifications and their attributes</returns>
        /// <exception cref="NVIDIANotSupportedException">This operation is not supported.</exception>
        /// <exception cref="NVIDIAApiException">See NVIDIAApiException.Status for the reason of the exception.</exception>
        /// <exception cref="Exception">A delegate callback throws an exception.</exception>
        public static DisplayIdsV2[] GetAllDisplayIds(PhysicalGPUHandle gpuHandle)
        {
            var gpuGetConnectedDisplayIds = DelegateFactory.GetDelegate<Delegates.GPU.NvAPI_GPU_GetAllDisplayIds>();

            if (!gpuGetConnectedDisplayIds.Accepts().Contains(typeof(DisplayIdsV2)))
            {
                throw new NVIDIANotSupportedException("This operation is not supported.");
            }

            uint count = 0;
            var status = gpuGetConnectedDisplayIds(gpuHandle, ValueTypeArray.Null, ref count);

            if (status != Status.Ok)
            {
                throw new NVIDIAApiException(status);
            }

            if (count == 0)
            {
                return new DisplayIdsV2[0];
            }

            using (
                var displayIds =
                    ValueTypeArray.FromArray(typeof(DisplayIdsV2).Instantiate<DisplayIdsV2>().Repeat((int) count)))
            {
                status = gpuGetConnectedDisplayIds(gpuHandle, displayIds, ref count);

                if (status != Status.Ok)
                {
                    throw new NVIDIAApiException(status);
                }

                return displayIds.ToArray<DisplayIdsV2>((int) count);
            }
        }

        /// <summary>
        ///     [PRIVATE]
        ///     Gets the architect information for the passed physical GPU handle.
        /// </summary>
        /// <param name="gpuHandle">The GPU handle to retrieve information for.</param>
        /// <returns>The GPU architect information.</returns>
        public static PrivateArchitectInfoV2 GetArchitectInfo(PhysicalGPUHandle gpuHandle)
        {
            var instance = typeof(PrivateArchitectInfoV2).Instantiate<PrivateArchitectInfoV2>();

            using (var architectInfoReference = ValueTypeReference.FromValueType(instance))
            {
                var status = DelegateFactory.GetDelegate<Delegates.GPU.NvAPI_GPU_GetArchInfo>()(
                    gpuHandle,
                    architectInfoReference
                );

                if (status != Status.Ok)
                {
                    throw new NVIDIAApiException(status);
                }

                return architectInfoReference.ToValueType<PrivateArchitectInfoV2>(
                    typeof(PrivateArchitectInfoV2));
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
            var status = DelegateFactory.GetDelegate<Delegates.GPU.NvAPI_GPU_GetBoardInfo>()(gpuHandle, ref boardInfo);

            if (status != Status.Ok)
            {
                throw new NVIDIAApiException(status);
            }

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
            var status = DelegateFactory.GetDelegate<Delegates.GPU.NvAPI_GPU_GetBusId>()(gpuHandle, out var busId);

            if (status != Status.Ok)
            {
                throw new NVIDIAApiException(status);
            }

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
            var status = DelegateFactory.GetDelegate<Delegates.GPU.NvAPI_GPU_GetBusSlotId>()(gpuHandle, out var busId);

            if (status != Status.Ok)
            {
                throw new NVIDIAApiException(status);
            }

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
            var status = DelegateFactory.GetDelegate<Delegates.GPU.NvAPI_GPU_GetBusType>()(gpuHandle, out var busType);

            if (status != Status.Ok)
            {
                throw new NVIDIAApiException(status);
            }

            return busType;
        }

        /// <summary>
        ///     [PRIVATE] - [Pascal Only]
        ///     Gets the clock boost lock for the passed GPU handle.
        /// </summary>
        /// <param name="gpuHandle">The handle of the GPU to perform the operation on.</param>
        /// <returns>The GPU clock boost lock.</returns>
        public static PrivateClockBoostLockV2 GetClockBoostLock(PhysicalGPUHandle gpuHandle)
        {
            var instance = typeof(PrivateClockBoostLockV2).Instantiate<PrivateClockBoostLockV2>();

            using (var clockLockReference = ValueTypeReference.FromValueType(instance))
            {
                var status = DelegateFactory.GetDelegate<Delegates.GPU.NvAPI_GPU_GetClockBoostLock>()(
                    gpuHandle,
                    clockLockReference
                );

                if (status != Status.Ok)
                {
                    throw new NVIDIAApiException(status);
                }

                return clockLockReference.ToValueType<PrivateClockBoostLockV2>(typeof(PrivateClockBoostLockV2));
            }
        }

        /// <summary>
        ///     [PRIVATE] - [Pascal Only]
        ///     Gets the clock boost mask for passed GPU handle.
        /// </summary>
        /// <param name="gpuHandle">The handle of the GPU to perform the operation on.</param>
        /// <returns>The GPI clock boost mask.</returns>
        public static PrivateClockBoostMasksV1 GetClockBoostMask(PhysicalGPUHandle gpuHandle)
        {
            var instance = typeof(PrivateClockBoostMasksV1).Instantiate<PrivateClockBoostMasksV1>();

            using (var clockBoostReference = ValueTypeReference.FromValueType(instance))
            {
                var status = DelegateFactory.GetDelegate<Delegates.GPU.NvAPI_GPU_GetClockBoostMask>()(
                    gpuHandle,
                    clockBoostReference
                );

                if (status != Status.Ok)
                {
                    throw new NVIDIAApiException(status);
                }

                return clockBoostReference.ToValueType<PrivateClockBoostMasksV1>(typeof(PrivateClockBoostMasksV1));
            }
        }

        /// <summary>
        ///     [PRIVATE] - [Pascal Only]
        ///     Gets the clock boost ranges for the passed GPU handle.
        /// </summary>
        /// <param name="gpuHandle">The handle of the GPU to perform the operation on.</param>
        /// <returns>The GPU clock boost ranges.</returns>
        public static PrivateClockBoostRangesV1 GetClockBoostRanges(PhysicalGPUHandle gpuHandle)
        {
            var instance = typeof(PrivateClockBoostRangesV1).Instantiate<PrivateClockBoostRangesV1>();

            using (var clockRangesReference = ValueTypeReference.FromValueType(instance))
            {
                var status = DelegateFactory.GetDelegate<Delegates.GPU.NvAPI_GPU_GetClockBoostRanges>()(
                    gpuHandle,
                    clockRangesReference
                );

                if (status != Status.Ok)
                {
                    throw new NVIDIAApiException(status);
                }

                return clockRangesReference.ToValueType<PrivateClockBoostRangesV1>(typeof(PrivateClockBoostRangesV1));
            }
        }

        /// <summary>
        ///     [PRIVATE] - [Pascal Only]
        ///     Gets the clock boost table for the passed GPU handle.
        /// </summary>
        /// <param name="gpuHandle">The handle of the GPU to perform the operation on.</param>
        /// <returns>The GPU clock boost table.</returns>
        public static PrivateClockBoostTableV1 GetClockBoostTable(PhysicalGPUHandle gpuHandle)
        {
            var instance = typeof(PrivateClockBoostTableV1).Instantiate<PrivateClockBoostTableV1>();

            using (var clockTableReference = ValueTypeReference.FromValueType(instance))
            {
                var status = DelegateFactory.GetDelegate<Delegates.GPU.NvAPI_GPU_GetClockBoostTable>()(
                    gpuHandle,
                    clockTableReference
                );

                if (status != Status.Ok)
                {
                    throw new NVIDIAApiException(status);
                }

                return clockTableReference.ToValueType<PrivateClockBoostTableV1>(typeof(PrivateClockBoostTableV1));
            }
        }


        /// <summary>
        ///     Due to space limitation GetConnectedOutputs() can return maximum 32 devices, but this is no longer true for DPMST.
        ///     GetConnectedDisplayIds() will return all the connected display devices in the form of displayIds for the associated
        ///     gpuHandle.
        ///     This function can accept set of flags to request cached, un-cached, sli and lid to get the connected devices.
        ///     Default value for flags will be cached.
        /// </summary>
        /// <param name="gpuHandle">Physical GPU handle to get information about</param>
        /// <param name="flags">ConnectedIdsFlag flags</param>
        /// <returns>An array of display identifications and their attributes</returns>
        /// <exception cref="NVIDIANotSupportedException">This operation is not supported.</exception>
        /// <exception cref="NVIDIAApiException">Status.InvalidArgument: gpuHandle is invalid</exception>
        /// <exception cref="NVIDIAApiException">Status.NvidiaDeviceNotFound: No NVIDIA GPU driving a display was found</exception>
        /// <exception cref="NVIDIAApiException">Status.ExpectedPhysicalGPUHandle: gpuHandle was not a physical GPU handle</exception>
        /// <exception cref="Exception">A delegate callback throws an exception.</exception>
        public static DisplayIdsV2[] GetConnectedDisplayIds(PhysicalGPUHandle gpuHandle, ConnectedIdsFlag flags)
        {
            var gpuGetConnectedDisplayIds =
                DelegateFactory.GetDelegate<Delegates.GPU.NvAPI_GPU_GetConnectedDisplayIds>();

            if (!gpuGetConnectedDisplayIds.Accepts().Contains(typeof(DisplayIdsV2)))
            {
                throw new NVIDIANotSupportedException("This operation is not supported.");
            }

            uint count = 0;
            var status = gpuGetConnectedDisplayIds(gpuHandle, ValueTypeArray.Null, ref count, flags);

            if (status != Status.Ok)
            {
                throw new NVIDIAApiException(status);
            }

            if (count == 0)
            {
                return new DisplayIdsV2[0];
            }

            using (
                var displayIds =
                    ValueTypeArray.FromArray(typeof(DisplayIdsV2).Instantiate<DisplayIdsV2>().Repeat((int) count)))
            {
                status = gpuGetConnectedDisplayIds(gpuHandle, displayIds, ref count, flags);

                if (status != Status.Ok)
                {
                    throw new NVIDIAApiException(status);
                }

                return displayIds.ToArray<DisplayIdsV2>((int) count);
            }
        }


        /// <summary>
        ///     [PRIVATE]
        ///     Gets the cooler policy table for the passed GPU handle.
        /// </summary>
        /// <param name="gpuHandle">The handle of the GPU to perform the operation on.</param>
        /// <param name="policy">The cooler policy to get the table for.</param>
        /// <param name="index">The cooler index.</param>
        /// <param name="count">Number of policy table entries retrieved.</param>
        /// <returns>The cooler policy table for the GPU.</returns>
        // ReSharper disable once TooManyArguments
        public static PrivateCoolerPolicyTableV1 GetCoolerPolicyTable(
            PhysicalGPUHandle gpuHandle,
            CoolerPolicy policy,
            uint index,
            out uint count)
        {
            var instance = typeof(PrivateCoolerPolicyTableV1).Instantiate<PrivateCoolerPolicyTableV1>();
            instance._Policy = policy;

            using (var policyTableReference = ValueTypeReference.FromValueType(instance))
            {
                var status = DelegateFactory.GetDelegate<Delegates.GPU.NvAPI_GPU_GetCoolerPolicyTable>()(
                    gpuHandle,
                    index,
                    policyTableReference,
                    out count
                );

                if (status != Status.Ok)
                {
                    throw new NVIDIAApiException(status);
                }

                return policyTableReference.ToValueType<PrivateCoolerPolicyTableV1>(typeof(PrivateCoolerPolicyTableV1));
            }
        }

        /// <summary>
        ///     [PRIVATE]
        ///     Gets the cooler settings for the passed GPU handle.
        /// </summary>
        /// <param name="gpuHandle">The handle of the GPU to perform the operation on.</param>
        /// <param name="coolerTarget">The cooler targets to get settings.</param>
        /// <returns>The cooler settings.</returns>
        public static PrivateCoolerSettingsV1 GetCoolerSettings(
            PhysicalGPUHandle gpuHandle,
            CoolerTarget coolerTarget = CoolerTarget.All)
        {
            var instance = typeof(PrivateCoolerSettingsV1).Instantiate<PrivateCoolerSettingsV1>();

            using (var coolerSettingsReference = ValueTypeReference.FromValueType(instance))
            {
                var status = DelegateFactory.GetDelegate<Delegates.GPU.NvAPI_GPU_GetCoolerSettings>()(
                    gpuHandle,
                    coolerTarget,
                    coolerSettingsReference
                );

                if (status != Status.Ok)
                {
                    throw new NVIDIAApiException(status);
                }

                return coolerSettingsReference.ToValueType<PrivateCoolerSettingsV1>(typeof(PrivateCoolerSettingsV1));
            }
        }

        /// <summary>
        ///     [PRIVATE] - [Pascal Only]
        ///     Gets the core voltage boost percentage for the passed GPU handle.
        /// </summary>
        /// <param name="gpuHandle">The handle of the GPU to perform the operation on.</param>
        /// <returns>The voltage boost percentage.</returns>
        public static PrivateVoltageBoostPercentV1 GetCoreVoltageBoostPercent(PhysicalGPUHandle gpuHandle)
        {
            var instance = typeof(PrivateVoltageBoostPercentV1).Instantiate<PrivateVoltageBoostPercentV1>();

            using (var voltageBoostReference = ValueTypeReference.FromValueType(instance))
            {
                var status = DelegateFactory.GetDelegate<Delegates.GPU.NvAPI_GPU_GetCoreVoltageBoostPercent>()(
                    gpuHandle,
                    voltageBoostReference
                );

                if (status != Status.Ok)
                {
                    throw new NVIDIAApiException(status);
                }

                return voltageBoostReference.ToValueType<PrivateVoltageBoostPercentV1>(
                    typeof(PrivateVoltageBoostPercentV1));
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
            var status =
                DelegateFactory.GetDelegate<Delegates.GPU.NvAPI_GPU_GetCurrentAGPRate>()(gpuHandle, out var agpRate);

            if (status != Status.Ok)
            {
                throw new NVIDIAApiException(status);
            }

            return (int) agpRate;
        }

        /// <summary>
        ///     [PRIVATE]
        ///     Gets the current fan speed level for the passed GPU handle.
        /// </summary>
        /// <param name="gpuHandle">The handle of the GPU to perform the operation on.</param>
        /// <returns>The current fan speed level.</returns>
        public static uint GetCurrentFanSpeedLevel(PhysicalGPUHandle gpuHandle)
        {
            var status =
                DelegateFactory
                    .GetDelegate<Delegates.GPU.NvAPI_GPU_GetCurrentFanSpeedLevel>()(gpuHandle, out var count);

            if (status != Status.Ok)
            {
                throw new NVIDIAApiException(status);
            }

            return count;
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
            var status = DelegateFactory.GetDelegate<Delegates.GPU.NvAPI_GPU_GetCurrentPCIEDownstreamWidth>()(gpuHandle,
                out var width);

            if (status != Status.Ok)
            {
                throw new NVIDIAApiException(status);
            }

            return (int) width;
        }


        /// <summary>
        ///     This function returns the current performance state (P-State).
        /// </summary>
        /// <param name="gpuHandle">GPU handle to get information about</param>
        /// <returns>The current performance state.</returns>
        /// <exception cref="NVIDIAApiException">Status.InvalidArgument: gpuHandle is NULL</exception>
        public static PerformanceStateId GetCurrentPerformanceState(PhysicalGPUHandle gpuHandle)
        {
            var status =
                DelegateFactory.GetDelegate<Delegates.GPU.NvAPI_GPU_GetCurrentPState>()(gpuHandle,
                    out var performanceState);

            if (status != Status.Ok)
            {
                throw new NVIDIAApiException(status);
            }

            return performanceState;
        }

        /// <summary>
        ///     [PRIVATE]
        ///     Gets the current thermal level for the passed GPU handle.
        /// </summary>
        /// <param name="gpuHandle">The handle of the GPU to perform the operation on.</param>
        /// <returns>The current thermal level.</returns>
        public static uint GetCurrentThermalLevel(PhysicalGPUHandle gpuHandle)
        {
            var status =
                DelegateFactory.GetDelegate<Delegates.GPU.NvAPI_GPU_GetCurrentThermalLevel>()(gpuHandle, out var count);

            if (status != Status.Ok)
            {
                throw new NVIDIAApiException(status);
            }

            return count;
        }

        /// <summary>
        ///     [PRIVATE] - [Pascal Only]
        ///     Gets the current voltage status for the passed GPU handle.
        /// </summary>
        /// <param name="gpuHandle">The handle of the GPU to perform the operation on.</param>
        /// <returns>The voltage status of the GPU.</returns>
        public static PrivateVoltageStatusV1 GetCurrentVoltage(PhysicalGPUHandle gpuHandle)
        {
            var instance = typeof(PrivateVoltageStatusV1).Instantiate<PrivateVoltageStatusV1>();

            using (var voltageStatusReference = ValueTypeReference.FromValueType(instance))
            {
                var status = DelegateFactory.GetDelegate<Delegates.GPU.NvAPI_GPU_GetCurrentVoltage>()(
                    gpuHandle,
                    voltageStatusReference
                );

                if (status != Status.Ok)
                {
                    throw new NVIDIAApiException(status);
                }

                return voltageStatusReference.ToValueType<PrivateVoltageStatusV1>(typeof(PrivateVoltageStatusV1));
            }
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
            var status = DelegateFactory.GetDelegate<Delegates.GPU.NvAPI_SYS_GetDisplayIdFromGpuAndOutputId>()(
                gpuHandle,
                outputId, out var display);

            if (status != Status.Ok)
            {
                throw new NVIDIAApiException(status);
            }

            return display;
        }


        /// <summary>
        ///     [PRIVATE]
        ///     Gets the driver model for the passed GPU handle.
        /// </summary>
        /// <param name="gpuHandle">The handle of the GPU to perform the operation on.</param>
        /// <returns>The driver model of the GPU.</returns>
        public static uint GetDriverModel(PhysicalGPUHandle gpuHandle)
        {
            var status = DelegateFactory.GetDelegate<Delegates.GPU.NvAPI_GetDriverModel>()(gpuHandle, out var count);

            if (status != Status.Ok)
            {
                throw new NVIDIAApiException(status);
            }

            return count;
        }

        /// <summary>
        ///     This function retrieves the dynamic performance states information from specific GPU
        /// </summary>
        /// <param name="physicalGPUHandle">Handle of the physical GPU for which the memory information is to be extracted.</param>
        /// <returns>The device utilizations information array.</returns>
        /// <exception cref="NVIDIANotSupportedException">This operation is not supported.</exception>
        /// <exception cref="NVIDIAApiException">Status.NvidiaDeviceNotFound: No NVIDIA GPU driving a display was found.</exception>
        /// <exception cref="Exception">A delegate callback throws an exception.</exception>
        public static DynamicPerformanceStatesInfoV1 GetDynamicPerformanceStatesInfoEx(
            PhysicalGPUHandle physicalGPUHandle)
        {
            var getDynamicPerformanceStatesInfoEx =
                DelegateFactory.GetDelegate<Delegates.GPU.NvAPI_GPU_GetDynamicPStatesInfoEx>();

            foreach (var acceptType in getDynamicPerformanceStatesInfoEx.Accepts())
            {
                var instance = acceptType.Instantiate<DynamicPerformanceStatesInfoV1>();

                using (var gpuDynamicPStateInfo = ValueTypeReference.FromValueType(instance, acceptType))
                {
                    var status = getDynamicPerformanceStatesInfoEx(physicalGPUHandle, gpuDynamicPStateInfo);

                    if (status == Status.IncompatibleStructureVersion)
                    {
                        continue;
                    }

                    if (status != Status.Ok)
                    {
                        throw new NVIDIAApiException(status);
                    }

                    return gpuDynamicPStateInfo.ToValueType<DynamicPerformanceStatesInfoV1>(acceptType);
                }
            }

            throw new NVIDIANotSupportedException("This operation is not supported.");
        }

        /// <summary>
        ///     This function returns ECC memory configuration information.
        /// </summary>
        /// <param name="gpuHandle">
        ///     handle identifying the physical GPU for which ECC configuration information is to be
        ///     retrieved.
        /// </param>
        /// <returns>An instance of <see cref="ECCConfigurationInfoV1" /></returns>
        public static ECCConfigurationInfoV1 GetECCConfigurationInfo(PhysicalGPUHandle gpuHandle)
        {
            var instance = typeof(ECCConfigurationInfoV1).Instantiate<ECCConfigurationInfoV1>();

            using (var configurationInfoReference = ValueTypeReference.FromValueType(instance))
            {
                var status = DelegateFactory.GetDelegate<Delegates.GPU.NvAPI_GPU_GetECCConfigurationInfo>()(
                    gpuHandle,
                    configurationInfoReference
                );

                if (status != Status.Ok)
                {
                    throw new NVIDIAApiException(status);
                }

                return configurationInfoReference.ToValueType<ECCConfigurationInfoV1>(typeof(ECCConfigurationInfoV1));
            }
        }

        /// <summary>
        ///     This function returns ECC memory error information.
        /// </summary>
        /// <param name="gpuHandle">A handle identifying the physical GPU for which ECC error information is to be retrieved.</param>
        /// <returns>An instance of <see cref="ECCErrorInfoV1" /></returns>
        public static ECCErrorInfoV1 GetECCErrorInfo(PhysicalGPUHandle gpuHandle)
        {
            var instance = typeof(ECCErrorInfoV1).Instantiate<ECCErrorInfoV1>();

            using (var errorInfoReference = ValueTypeReference.FromValueType(instance))
            {
                var status = DelegateFactory.GetDelegate<Delegates.GPU.NvAPI_GPU_GetECCErrorInfo>()(
                    gpuHandle,
                    errorInfoReference
                );

                if (status != Status.Ok)
                {
                    throw new NVIDIAApiException(status);
                }

                return errorInfoReference.ToValueType<ECCErrorInfoV1>(typeof(ECCErrorInfoV1));
            }
        }

        /// <summary>
        ///     This function returns ECC memory status information.
        /// </summary>
        /// <param name="gpuHandle">A handle identifying the physical GPU for which ECC status information is to be retrieved.</param>
        /// <returns>An instance of <see cref="ECCStatusInfoV1" /></returns>
        public static ECCStatusInfoV1 GetECCStatusInfo(PhysicalGPUHandle gpuHandle)
        {
            var instance = typeof(ECCStatusInfoV1).Instantiate<ECCStatusInfoV1>();

            using (var statusInfoReference = ValueTypeReference.FromValueType(instance))
            {
                var status = DelegateFactory.GetDelegate<Delegates.GPU.NvAPI_GPU_GetECCStatusInfo>()(
                    gpuHandle,
                    statusInfoReference
                );

                if (status != Status.Ok)
                {
                    throw new NVIDIAApiException(status);
                }

                return statusInfoReference.ToValueType<ECCStatusInfoV1>(typeof(ECCStatusInfoV1));
            }
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
        /// <exception cref="Exception">A delegate callback throws an exception.</exception>
        // ReSharper disable once TooManyArguments
        public static EDIDV3 GetEDID(
            PhysicalGPUHandle gpuHandle,
            OutputId outputId,
            int offset,
            int readIdentification = 0)
        {
            var gpuGetEDID = DelegateFactory.GetDelegate<Delegates.GPU.NvAPI_GPU_GetEDID>();

            if (!gpuGetEDID.Accepts().Contains(typeof(EDIDV3)))
            {
                throw new NVIDIANotSupportedException("This operation is not supported.");
            }

            var instance = EDIDV3.CreateWithOffset((uint) readIdentification, (uint) offset);

            using (var edidReference = ValueTypeReference.FromValueType(instance))
            {
                var status = gpuGetEDID(gpuHandle, outputId, edidReference);

                if (status != Status.Ok)
                {
                    throw new NVIDIAApiException(status);
                }

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
        /// <exception cref="Exception">A delegate callback throws an exception.</exception>
        public static IEDID GetEDID(PhysicalGPUHandle gpuHandle, OutputId outputId)
        {
            var gpuGetEDID = DelegateFactory.GetDelegate<Delegates.GPU.NvAPI_GPU_GetEDID>();

            foreach (var acceptType in gpuGetEDID.Accepts())
            {
                using (var edidReference = ValueTypeReference.FromValueType(acceptType.Instantiate<IEDID>(), acceptType)
                )
                {
                    var status = gpuGetEDID(gpuHandle, outputId, edidReference);

                    if (status != Status.IncompatibleStructureVersion)
                    {
                        continue;
                    }

                    if (status != Status.Ok)
                    {
                        throw new NVIDIAApiException(status);
                    }

                    return edidReference.ToValueType<IEDID>(acceptType);
                }
            }

            throw new NVIDIANotSupportedException("This operation is not supported.");
        }

        /// <summary>
        ///     [PRIVATE]
        ///     Gets the GPU manufacturing foundry of the passed GPU handle.
        /// </summary>
        /// <param name="gpuHandle">The handle of the GPU to retrieve this information from.</param>
        /// <returns>The GPU manufacturing foundry of the GPU.</returns>
        public static GPUFoundry GetFoundry(PhysicalGPUHandle gpuHandle)
        {
            var status = DelegateFactory.GetDelegate<Delegates.GPU.NvAPI_GPU_GetFoundry>()(gpuHandle, out var foundry);

            if (status != Status.Ok)
            {
                throw new NVIDIAApiException(status);
            }

            return foundry;
        }

        /// <summary>
        ///     [PRIVATE]
        ///     Gets the current frame buffer width and location for the passed GPU handle.
        /// </summary>
        /// <param name="gpuHandle">The handle of the GPU to perform the operation on.</param>
        /// <param name="width">The frame buffer width.</param>
        /// <param name="location">The frame buffer location.</param>
        public static void GetFrameBufferWidthAndLocation(
            PhysicalGPUHandle gpuHandle,
            out uint width,
            out uint location)
        {
            var status =
                DelegateFactory.GetDelegate<Delegates.GPU.NvAPI_GPU_GetFBWidthAndLocation>()(gpuHandle, out width,
                    out location);

            if (status != Status.Ok)
            {
                throw new NVIDIAApiException(status);
            }
        }

        /// <summary>
        ///     This function retrieves the full GPU name as an ASCII string - for example, "Quadro FX 1400".
        /// </summary>
        /// <param name="gpuHandle">Physical GPU handle to get information about</param>
        /// <returns>Full GPU name as an ASCII string</returns>
        /// <exception cref="NVIDIAApiException">See NVIDIAApiException.Status for the reason of the exception.</exception>
        public static string GetFullName(PhysicalGPUHandle gpuHandle)
        {
            var status = DelegateFactory.GetDelegate<Delegates.GPU.NvAPI_GPU_GetFullName>()(gpuHandle, out var name);

            if (status != Status.Ok)
            {
                throw new NVIDIAApiException(status);
            }

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
            var status = DelegateFactory.GetDelegate<Delegates.GPU.NvAPI_SYS_GetGpuAndOutputIdFromDisplayId>()(
                displayId,
                out gpuHandle, out var outputId);

            if (status != Status.Ok)
            {
                throw new NVIDIAApiException(status);
            }

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
        public static uint GetGPUCoreCount(PhysicalGPUHandle gpuHandle)
        {
            var status =
                DelegateFactory.GetDelegate<Delegates.GPU.NvAPI_GPU_GetGpuCoreCount>()(gpuHandle, out var cores);

            if (status != Status.Ok)
            {
                throw new NVIDIAApiException(status);
            }

            return cores;
        }

        // ReSharper disable once CommentTypo
        /// <summary>
        ///     [PRIVATE]
        ///     Gets the GPUID of the passed GPU handle.
        /// </summary>
        /// <param name="gpuHandle">The GPU handle to get the GPUID for.</param>
        /// <returns>The GPU's GPUID.</returns>
        public static uint GetGPUIDFromPhysicalGPU(PhysicalGPUHandle gpuHandle)
        {
            var status =
                DelegateFactory.GetDelegate<Delegates.GPU.NvAPI_GetGPUIDFromPhysicalGPU>()(gpuHandle, out var gpuId);

            if (status != Status.Ok)
            {
                throw new NVIDIAApiException(status);
            }

            return gpuId;
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
            var status = DelegateFactory.GetDelegate<Delegates.GPU.NvAPI_GPU_GetGPUType>()(gpuHandle, out var type);

            if (status != Status.Ok)
            {
                throw new NVIDIAApiException(status);
            }

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
            var status = DelegateFactory.GetDelegate<Delegates.GPU.NvAPI_GPU_GetIRQ>()(gpuHandle, out var irq);

            if (status != Status.Ok)
            {
                throw new NVIDIAApiException(status);
            }

            return (int) irq;
        }


        /// <summary>
        ///     [PRIVATE]
        ///     Gets the current frame buffer width and location for the passed logical GPU handle.
        /// </summary>
        /// <param name="gpuHandle">The handle of the logical GPU to perform the operation on.</param>
        /// <param name="width">The frame buffer width.</param>
        /// <param name="location">The frame buffer location.</param>
        public static void GetLogicalFrameBufferWidthAndLocation(
            LogicalGPUHandle gpuHandle,
            out uint width,
            out uint location)
        {
            var status =
                DelegateFactory.GetDelegate<Delegates.GPU.NvAPI_GPU_GetLogicalFBWidthAndLocation>()(gpuHandle,
                    out width, out location);

            if (status != Status.Ok)
            {
                throw new NVIDIAApiException(status);
            }
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
            var status =
                DelegateFactory.GetDelegate<Delegates.GPU.NvAPI_GetLogicalGPUFromDisplay>()(display, out var gpu);

            if (status != Status.Ok)
            {
                throw new NVIDIAApiException(status);
            }

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
            var status =
                DelegateFactory.GetDelegate<Delegates.GPU.NvAPI_GetLogicalGPUFromPhysicalGPU>()(gpuHandle, out var gpu);

            if (status != Status.Ok)
            {
                throw new NVIDIAApiException(status);
            }

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
        /// <exception cref="Exception">A delegate callback throws an exception.</exception>
        public static IDisplayDriverMemoryInfo GetMemoryInfo(PhysicalGPUHandle physicalGPUHandle)
        {
            var getMemoryInfo = DelegateFactory.GetDelegate<Delegates.GPU.NvAPI_GPU_GetMemoryInfo>();

            foreach (var acceptType in getMemoryInfo.Accepts())
            {
                var instance = acceptType.Instantiate<IDisplayDriverMemoryInfo>();

                using (var displayDriverMemoryInfo = ValueTypeReference.FromValueType(instance, acceptType))
                {
                    var status = getMemoryInfo(physicalGPUHandle, displayDriverMemoryInfo);

                    if (status == Status.IncompatibleStructureVersion)
                    {
                        continue;
                    }

                    if (status != Status.Ok)
                    {
                        throw new NVIDIAApiException(status);
                    }

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
        /// <param name="displayId">Display identification of the divide to get information about</param>
        /// <returns>Type of the output</returns>
        /// <exception cref="NVIDIAApiException">Status.InvalidArgument: gpuHandle is NULL</exception>
        /// <exception cref="NVIDIAApiException">Status.NvidiaDeviceNotFound: No NVIDIA GPU driving a display was found</exception>
        /// <exception cref="NVIDIAApiException">Status.ExpectedPhysicalGPUHandle: gpuHandle was not a physical GPU handle</exception>
        public static OutputType GetOutputType(PhysicalGPUHandle gpuHandle, uint displayId)
        {
            var status =
                DelegateFactory.GetDelegate<Delegates.GPU.NvAPI_GPU_GetOutputType>()(gpuHandle, displayId,
                    out var type);

            if (status != Status.Ok)
            {
                throw new NVIDIAApiException(status);
            }

            return type;
        }

        /// <summary>
        ///     [PRIVATE]
        ///     Gets the number of GPC (Graphic Processing Clusters) of the passed GPU handle.
        /// </summary>
        /// <param name="gpuHandle">The handle of the GPU to perform the operation on.</param>
        /// <returns>The number of GPC units for the GPU.</returns>
        public static uint GetPartitionCount(PhysicalGPUHandle gpuHandle)
        {
            var status =
                DelegateFactory.GetDelegate<Delegates.GPU.NvAPI_GPU_GetPartitionCount>()(gpuHandle, out var count);

            if (status != Status.Ok)
            {
                throw new NVIDIAApiException(status);
            }

            return count;
        }

        /// <summary>
        ///     [PRIVATE]
        ///     Gets additional information about the PCIe interface and configuration for the passed GPU handle.
        /// </summary>
        /// <param name="gpuHandle">The handle of the GPU to perform the operation on.</param>
        /// <returns>PCIe information and configurations.</returns>
        public static PrivatePCIeInfoV2 GetPCIEInfo(PhysicalGPUHandle gpuHandle)
        {
            var instance = typeof(PrivatePCIeInfoV2).Instantiate<PrivatePCIeInfoV2>();

            using (var pcieInfoReference = ValueTypeReference.FromValueType(instance))
            {
                var status =
                    DelegateFactory.GetDelegate<Delegates.GPU.NvAPI_GPU_GetPCIEInfo>()(gpuHandle, pcieInfoReference);

                if (status != Status.Ok)
                {
                    throw new NVIDIAApiException(status);
                }

                return pcieInfoReference.ToValueType<PrivatePCIeInfoV2>(typeof(PrivatePCIeInfoV2));
            }
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
        // ReSharper disable once TooManyArguments
        public static void GetPCIIdentifiers(
            PhysicalGPUHandle gpuHandle,
            out uint deviceId,
            out uint subSystemId,
            out uint revisionId,
            out uint extDeviceId)
        {
            var status = DelegateFactory.GetDelegate<Delegates.GPU.NvAPI_GPU_GetPCIIdentifiers>()(gpuHandle,
                out deviceId,
                out subSystemId, out revisionId, out extDeviceId);

            if (status != Status.Ok)
            {
                throw new NVIDIAApiException(status);
            }
        }

        /// <summary>
        ///     Gets the reason behind the current decrease in performance.
        /// </summary>
        /// <param name="gpuHandle">The handle of the GPU to perform the operation on.</param>
        /// <returns>A value indicating the reason of current performance decrease.</returns>
        public static PerformanceDecreaseReason GetPerformanceDecreaseInfo(PhysicalGPUHandle gpuHandle)
        {
            var status = DelegateFactory.GetDelegate<Delegates.GPU.NvAPI_GPU_GetPerfDecreaseInfo>()(
                gpuHandle,
                out var decreaseReason
            );

            if (status != Status.Ok)
            {
                throw new NVIDIAApiException(status);
            }

            return decreaseReason;
        }

        /// <summary>
        ///     This function retrieves all available performance states (P-States) information.
        ///     P-States are GPU active/executing performance capability and power consumption states.
        /// </summary>
        /// <param name="physicalGPUHandle">GPU handle to get information about.</param>
        /// <param name="flags">Flag to get specific information about a performance state.</param>
        /// <returns>Retrieved performance states information</returns>
        /// <exception cref="NVIDIAApiException">Status.InvalidArgument: gpuHandle is NULL</exception>
        /// <exception cref="NVIDIAApiException">Status.ExpectedPhysicalGPUHandle: gpuHandle was not a physical GPU handle</exception>
        [SuppressMessage("ReSharper", "EventExceptionNotDocumented")]
        public static IPerformanceStatesInfo GetPerformanceStates(
            PhysicalGPUHandle physicalGPUHandle,
            GetPerformanceStatesInfoFlags flags)
        {
            var getPerformanceStatesInfo = DelegateFactory.GetDelegate<Delegates.GPU.NvAPI_GPU_GetPStatesInfoEx>();

            foreach (var acceptType in getPerformanceStatesInfo.Accepts())
            {
                var instance = acceptType.Instantiate<IPerformanceStatesInfo>();

                using (var performanceStateInfo = ValueTypeReference.FromValueType(instance, acceptType))
                {
                    var status = getPerformanceStatesInfo(physicalGPUHandle, performanceStateInfo, flags);

                    if (status == Status.IncompatibleStructureVersion)
                    {
                        continue;
                    }

                    if (status != Status.Ok)
                    {
                        throw new NVIDIAApiException(status);
                    }

                    return performanceStateInfo.ToValueType<IPerformanceStatesInfo>(acceptType);
                }
            }

            throw new NVIDIANotSupportedException("This operation is not supported.");
        }

        /// <summary>
        ///     This function retrieves all available performance states (P-States) 2.0 information.
        ///     P-States are GPU active/executing performance capability and power consumption states.
        /// </summary>
        /// <param name="physicalGPUHandle">GPU handle to get information about.</param>
        /// <returns>Retrieved performance states 2.0 information</returns>
        /// <exception cref="NVIDIAApiException">Status.InvalidArgument: gpuHandle is NULL</exception>
        /// <exception cref="NVIDIAApiException">Status.ExpectedPhysicalGPUHandle: gpuHandle was not a physical GPU handle</exception>
        [SuppressMessage("ReSharper", "EventExceptionNotDocumented")]
        public static IPerformanceStates20Info GetPerformanceStates20(PhysicalGPUHandle physicalGPUHandle)
        {
            var getPerformanceStates20 = DelegateFactory.GetDelegate<Delegates.GPU.NvAPI_GPU_GetPStates20>();

            foreach (var acceptType in getPerformanceStates20.Accepts())
            {
                var instance = acceptType.Instantiate<IPerformanceStates20Info>();

                using (var performanceStateInfo = ValueTypeReference.FromValueType(instance, acceptType))
                {
                    var status = getPerformanceStates20(physicalGPUHandle, performanceStateInfo);

                    if (status == Status.IncompatibleStructureVersion)
                    {
                        continue;
                    }

                    if (status != Status.Ok)
                    {
                        throw new NVIDIAApiException(status);
                    }

                    return performanceStateInfo.ToValueType<IPerformanceStates20Info>(acceptType);
                }
            }

            throw new NVIDIANotSupportedException("This operation is not supported.");
        }

        /// <summary>
        ///     This function returns the physical size of frame buffer in KB.  This does NOT include any system RAM that may be
        ///     dedicated for use by the GPU.
        ///     TCC_SUPPORTED
        /// </summary>
        /// <param name="gpuHandle">GPU handle to get information about</param>
        /// <returns>Physical size of frame buffer in KB</returns>
        /// <exception cref="NVIDIAApiException">Status.NvidiaDeviceNotFound: No NVIDIA GPU driving a display was found</exception>
        /// <exception cref="NVIDIAApiException">Status.ExpectedPhysicalGPUHandle: gpuHandle was not a physical GPU handle</exception>
        public static int GetPhysicalFrameBufferSize(PhysicalGPUHandle gpuHandle)
        {
            var status =
                DelegateFactory.GetDelegate<Delegates.GPU.NvAPI_GPU_GetPhysicalFrameBufferSize>()(gpuHandle,
                    out var size);

            if (status != Status.Ok)
            {
                throw new NVIDIAApiException(status);
            }

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
            var status =
                DelegateFactory.GetDelegate<Delegates.GPU.NvAPI_SYS_GetPhysicalGpuFromDisplayId>()(displayId,
                    out var gpu);

            if (status != Status.Ok)
            {
                throw new NVIDIAApiException(status);
            }

            return gpu;
        }

        /// <summary>
        ///     [PRIVATE]
        ///     Gets a physical GPU handle from the passed GPUID
        /// </summary>
        /// <param name="gpuId">The GPUID to get the physical handle for.</param>
        /// <returns>The retrieved physical GPU handle.</returns>
        public static PhysicalGPUHandle GetPhysicalGPUFromGPUID(uint gpuId)
        {
            var status =
                DelegateFactory.GetDelegate<Delegates.GPU.NvAPI_GetPhysicalGPUFromGPUID>()(gpuId, out var gpuHandle);

            if (status != Status.Ok)
            {
                throw new NVIDIAApiException(status);
            }

            return gpuHandle;
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
            var status =
                DelegateFactory.GetDelegate<Delegates.GPU.NvAPI_GetPhysicalGPUFromUnAttachedDisplay>()(display,
                    out var gpu);

            if (status != Status.Ok)
            {
                throw new NVIDIAApiException(status);
            }

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
            var status = DelegateFactory.GetDelegate<Delegates.GPU.NvAPI_GetPhysicalGPUsFromDisplay>()(display, gpuList,
                out var count);

            if (status != Status.Ok)
            {
                throw new NVIDIAApiException(status);
            }

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
            var status = DelegateFactory.GetDelegate<Delegates.GPU.NvAPI_GetPhysicalGPUsFromLogicalGPU>()(gpuHandle,
                gpuList,
                out var count);

            if (status != Status.Ok)
            {
                throw new NVIDIAApiException(status);
            }

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
            var status =
                DelegateFactory.GetDelegate<Delegates.GPU.NvAPI_GPU_GetQuadroStatus>()(gpuHandle, out var isQuadro);

            if (status != Status.Ok)
            {
                throw new NVIDIAApiException(status);
            }

            return isQuadro > 0;
        }

        /// <summary>
        ///     [PRIVATE]
        ///     Gets the number of RAM banks for the passed GPU handle.
        /// </summary>
        /// <param name="gpuHandle">The handle of the GPU to retrieve this information from.</param>
        /// <returns>The number of RAM memory banks.</returns>
        public static uint GetRAMBankCount(PhysicalGPUHandle gpuHandle)
        {
            var status =
                DelegateFactory.GetDelegate<Delegates.GPU.NvAPI_GPU_GetRamBankCount>()(gpuHandle, out var bankCount);

            if (status != Status.Ok)
            {
                throw new NVIDIAApiException(status);
            }

            return bankCount;
        }

        /// <summary>
        ///     [PRIVATE]
        ///     Gets the RAM bus width for the passed GPU handle.
        /// </summary>
        /// <param name="gpuHandle">The handle of the GPU to retrieve this information from.</param>
        /// <returns>The RAM memory bus width.</returns>
        public static uint GetRAMBusWidth(PhysicalGPUHandle gpuHandle)
        {
            var status =
                DelegateFactory.GetDelegate<Delegates.GPU.NvAPI_GPU_GetRamBusWidth>()(gpuHandle, out var busWidth);

            if (status != Status.Ok)
            {
                throw new NVIDIAApiException(status);
            }

            return busWidth;
        }

        /// <summary>
        ///     [PRIVATE]
        ///     Gets the RAM maker for the passed GPU handle.
        /// </summary>
        /// <param name="gpuHandle">The handle of the GPU to retrieve this information from.</param>
        /// <returns>The RAM memory maker.</returns>
        public static GPUMemoryMaker GetRAMMaker(PhysicalGPUHandle gpuHandle)
        {
            var status =
                DelegateFactory.GetDelegate<Delegates.GPU.NvAPI_GPU_GetRamMaker>()(gpuHandle, out var ramMaker);

            if (status != Status.Ok)
            {
                throw new NVIDIAApiException(status);
            }

            return ramMaker;
        }

        /// <summary>
        ///     [PRIVATE]
        ///     Gets the RAM type for the passed GPU handle.
        /// </summary>
        /// <param name="gpuHandle">The handle of the GPU to retrieve this information from.</param>
        /// <returns>The RAM memory type.</returns>
        public static GPUMemoryType GetRAMType(PhysicalGPUHandle gpuHandle)
        {
            var status = DelegateFactory.GetDelegate<Delegates.GPU.NvAPI_GPU_GetRamType>()(gpuHandle, out var ramType);

            if (status != Status.Ok)
            {
                throw new NVIDIAApiException(status);
            }

            return ramType;
        }

        /// <summary>
        ///     [PRIVATE]
        ///     Gets the ROP count for the passed GPU handle.
        /// </summary>
        /// <param name="gpuHandle">The handle of the GPU to retrieve this information from.</param>
        /// <returns>The number of ROP units.</returns>
        public static uint GetROPCount(PhysicalGPUHandle gpuHandle)
        {
            var status =
                DelegateFactory.GetDelegate<Delegates.GPU.NvAPI_GPU_GetROPCount>()(gpuHandle, out var ropCount);

            if (status != Status.Ok)
            {
                throw new NVIDIAApiException(status);
            }

            return ropCount;
        }


        /// <summary>
        ///     [PRIVATE]
        ///     Gets the number of shader pipe lines for the passed GPU handle.
        /// </summary>
        /// <param name="gpuHandle">The handle of the GPU to retrieve this information from.</param>
        /// <returns>The number of shader pipelines.</returns>
        public static uint GetShaderPipeCount(PhysicalGPUHandle gpuHandle)
        {
            var status =
                DelegateFactory.GetDelegate<Delegates.GPU.NvAPI_GPU_GetShaderPipeCount>()(gpuHandle, out var spCount);

            if (status != Status.Ok)
            {
                throw new NVIDIAApiException(status);
            }

            return spCount;
        }

        /// <summary>
        ///     This function retrieves the number of Shader SubPipes on the GPU
        ///     On newer architectures, this corresponds to the number of SM units
        /// </summary>
        /// <param name="gpuHandle">GPU handle to get information about</param>
        /// <returns>Number of Shader SubPipes on the GPU</returns>
        /// <exception cref="NVIDIAApiException">Status.NvidiaDeviceNotFound: No NVIDIA GPU driving a display was found</exception>
        /// <exception cref="NVIDIAApiException">Status.ExpectedPhysicalGPUHandle: gpuHandle was not a physical GPU handle</exception>
        public static uint GetShaderSubPipeCount(PhysicalGPUHandle gpuHandle)
        {
            var status =
                DelegateFactory.GetDelegate<Delegates.GPU.NvAPI_GPU_GetShaderSubPipeCount>()(gpuHandle, out var count);

            if (status != Status.Ok)
            {
                throw new NVIDIAApiException(status);
            }

            return count;
        }

        /// <summary>
        ///     [PRIVATE]
        ///     Gets the GPU short name (code name) for the passed GPU handle.
        /// </summary>
        /// <param name="gpuHandle">The handle of the GPU to retrieve this information from.</param>
        /// <returns>The GPU short name.</returns>
        public static string GetShortName(PhysicalGPUHandle gpuHandle)
        {
            var status = DelegateFactory.GetDelegate<Delegates.GPU.NvAPI_GPU_GetShortName>()(gpuHandle, out var name);

            if (status != Status.Ok)
            {
                throw new NVIDIAApiException(status);
            }

            return name.Value;
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
            var status =
                DelegateFactory.GetDelegate<Delegates.GPU.NvAPI_GPU_GetSystemType>()(gpuHandle, out var systemType);

            if (status != Status.Ok)
            {
                throw new NVIDIAApiException(status);
            }

            return systemType;
        }

        /// <summary>
        ///     This function returns the fan speed tachometer reading for the specified physical GPU.
        /// </summary>
        /// <param name="gpuHandle">Physical GPU handle to get tachometer reading from</param>
        /// <returns>The GPU fan speed in revolutions per minute.</returns>
        /// <exception cref="NVIDIAApiException">Status.NvidiaDeviceNotFound: No NVIDIA GPU driving a display was found</exception>
        /// <exception cref="NVIDIAApiException">Status.ExpectedPhysicalGPUHandle: gpuHandle was not a physical GPU handle.</exception>
        public static uint GetTachReading(PhysicalGPUHandle gpuHandle)
        {
            var status = DelegateFactory.GetDelegate<Delegates.GPU.NvAPI_GPU_GetTachReading>()(
                gpuHandle, out var value
            );

            if (status != Status.Ok)
            {
                throw new NVIDIAApiException(status);
            }

            return value;
        }

        /// <summary>
        ///     [PRIVATE]
        ///     Gets the current thermal policies information for the passed GPU handle.
        /// </summary>
        /// <param name="gpuHandle">The handle of the GPU to perform the operation on.</param>
        /// <returns>The current thermal policies information.</returns>
        public static PrivateThermalPoliciesInfoV2 GetThermalPoliciesInfo(PhysicalGPUHandle gpuHandle)
        {
            var instance = typeof(PrivateThermalPoliciesInfoV2).Instantiate<PrivateThermalPoliciesInfoV2>();

            using (var policiesInfoReference = ValueTypeReference.FromValueType(instance))
            {
                var status =
                    DelegateFactory.GetDelegate<Delegates.GPU.NvAPI_GPU_GetThermalPoliciesInfo>()(gpuHandle,
                        policiesInfoReference);

                if (status != Status.Ok)
                {
                    throw new NVIDIAApiException(status);
                }

                return policiesInfoReference.ToValueType<PrivateThermalPoliciesInfoV2>(
                    typeof(PrivateThermalPoliciesInfoV2));
            }
        }

        /// <summary>
        ///     [PRIVATE]
        ///     Gets the thermal policies status for the passed GPU handle.
        /// </summary>
        /// <param name="gpuHandle">The handle of the GPU to perform the operation on.</param>
        /// <returns>The thermal policies status.</returns>
        public static PrivateThermalPoliciesStatusV2 GetThermalPoliciesStatus(PhysicalGPUHandle gpuHandle)
        {
            var instance = typeof(PrivateThermalPoliciesStatusV2).Instantiate<PrivateThermalPoliciesStatusV2>();

            using (var policiesStatusReference = ValueTypeReference.FromValueType(instance))
            {
                var status = DelegateFactory.GetDelegate<Delegates.GPU.NvAPI_GPU_GetThermalPoliciesStatus>()(
                    gpuHandle,
                    policiesStatusReference
                );

                if (status != Status.Ok)
                {
                    throw new NVIDIAApiException(status);
                }

                return policiesStatusReference.ToValueType<PrivateThermalPoliciesStatusV2>(
                    typeof(PrivateThermalPoliciesStatusV2));
            }
        }

        /// <summary>
        ///     This function retrieves the thermal information of all thermal sensors or specific thermal sensor associated with
        ///     the selected GPU. To retrieve info for all sensors, set sensorTarget to ThermalSettingsTarget.All.
        /// </summary>
        /// <param name="physicalGPUHandle">Handle of the physical GPU for which the memory information is to be extracted.</param>
        /// <param name="sensorTarget">Specifies the requested thermal sensor target.</param>
        /// <returns>The device thermal sensors information.</returns>
        /// <exception cref="NVIDIANotSupportedException">This operation is not supported.</exception>
        /// <exception cref="NVIDIAApiException">Status.NvidiaDeviceNotFound: No NVIDIA GPU driving a display was found.</exception>
        /// <exception cref="Exception">A delegate callback throws an exception.</exception>
        public static IThermalSettings GetThermalSettings(
            PhysicalGPUHandle physicalGPUHandle,
            ThermalSettingsTarget sensorTarget = ThermalSettingsTarget.All)
        {
            var getThermalSettings = DelegateFactory.GetDelegate<Delegates.GPU.NvAPI_GPU_GetThermalSettings>();

            foreach (var acceptType in getThermalSettings.Accepts())
            {
                var instance = acceptType.Instantiate<IThermalSettings>();

                using (var gpuThermalSettings = ValueTypeReference.FromValueType(instance, acceptType))
                {
                    var status = getThermalSettings(physicalGPUHandle, sensorTarget, gpuThermalSettings);

                    if (status == Status.IncompatibleStructureVersion)
                    {
                        continue;
                    }

                    if (status != Status.Ok)
                    {
                        throw new NVIDIAApiException(status);
                    }

                    return gpuThermalSettings.ToValueType<IThermalSettings>(acceptType);
                }
            }

            throw new NVIDIANotSupportedException("This operation is not supported.");
        }

        /// <summary>
        ///     [PRIVATE]
        ///     Gets the SM count for the passed GPU handle.
        /// </summary>
        /// <param name="gpuHandle">The handle of the GPU to retrieve this information from.</param>
        /// <returns>The number of SM units.</returns>
        public static uint GetTotalSMCount(PhysicalGPUHandle gpuHandle)
        {
            var status =
                DelegateFactory.GetDelegate<Delegates.GPU.NvAPI_GPU_GetTotalSMCount>()(gpuHandle, out var smCount);

            if (status != Status.Ok)
            {
                throw new NVIDIAApiException(status);
            }

            return smCount;
        }

        /// <summary>
        ///     [PRIVATE]
        ///     Gets the SP count for the passed GPU handle.
        /// </summary>
        /// <param name="gpuHandle">The handle of the GPU to retrieve this information from.</param>
        /// <returns>The number of SP units.</returns>
        public static uint GetTotalSPCount(PhysicalGPUHandle gpuHandle)
        {
            var status =
                DelegateFactory.GetDelegate<Delegates.GPU.NvAPI_GPU_GetTotalSPCount>()(gpuHandle, out var spCount);

            if (status != Status.Ok)
            {
                throw new NVIDIAApiException(status);
            }

            return spCount;
        }

        /// <summary>
        ///     [PRIVATE]
        ///     Gets the TPC count for the passed GPU handle.
        /// </summary>
        /// <param name="gpuHandle">The handle of the GPU to retrieve this information from.</param>
        /// <returns>The number of TPC units.</returns>
        public static uint GetTotalTPCCount(PhysicalGPUHandle gpuHandle)
        {
            var status =
                DelegateFactory.GetDelegate<Delegates.GPU.NvAPI_GPU_GetTotalTPCCount>()(gpuHandle, out var tpcCount);

            if (status != Status.Ok)
            {
                throw new NVIDIAApiException(status);
            }

            return tpcCount;
        }

        /// <summary>
        ///     [PRIVATE]
        ///     Gets the GPU usage metrics for the passed GPU handle.
        /// </summary>
        /// <param name="gpuHandle">The handle of the GPU to perform the operation on.</param>
        /// <returns>The usage information for the selected GPU.</returns>
        public static PrivateUsagesInfoV1 GetUsages(PhysicalGPUHandle gpuHandle)
        {
            var instance = typeof(PrivateUsagesInfoV1).Instantiate<PrivateUsagesInfoV1>();

            using (var usageInfoReference = ValueTypeReference.FromValueType(instance))
            {
                var status = DelegateFactory.GetDelegate<Delegates.GPU.NvAPI_GPU_GetUsages>()(
                    gpuHandle,
                    usageInfoReference
                );

                if (status != Status.Ok)
                {
                    throw new NVIDIAApiException(status);
                }

                return usageInfoReference.ToValueType<PrivateUsagesInfoV1>(typeof(PrivateUsagesInfoV1));
            }
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
            var status =
                DelegateFactory.GetDelegate<Delegates.GPU.NvAPI_GPU_GetVbiosOEMRevision>()(gpuHandle, out var revision);

            if (status != Status.Ok)
            {
                throw new NVIDIAApiException(status);
            }

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
            var status =
                DelegateFactory.GetDelegate<Delegates.GPU.NvAPI_GPU_GetVbiosRevision>()(gpuHandle, out var revision);

            if (status != Status.Ok)
            {
                throw new NVIDIAApiException(status);
            }

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
            var status =
                DelegateFactory.GetDelegate<Delegates.GPU.NvAPI_GPU_GetVbiosVersionString>()(gpuHandle,
                    out var version);

            if (status != Status.Ok)
            {
                throw new NVIDIAApiException(status);
            }

            return version.Value;
        }

        /// <summary>
        ///     [PRIVATE] - [Pascal Only]
        ///     Gets the GPU boost frequency curve controls for the passed GPU handle.
        /// </summary>
        /// <param name="gpuHandle">The handle of the GPU to perform the operation on.</param>
        /// <returns>The retrieved VFP curve.</returns>
        public static PrivateVFPCurveV1 GetVFPCurve(PhysicalGPUHandle gpuHandle)
        {
            var instance = typeof(PrivateVFPCurveV1).Instantiate<PrivateVFPCurveV1>();

            using (var vfpCurveReference = ValueTypeReference.FromValueType(instance))
            {
                var status = DelegateFactory.GetDelegate<Delegates.GPU.NvAPI_GPU_GetVFPCurve>()(
                    gpuHandle,
                    vfpCurveReference
                );

                if (status != Status.Ok)
                {
                    throw new NVIDIAApiException(status);
                }

                return vfpCurveReference.ToValueType<PrivateVFPCurveV1>(typeof(PrivateVFPCurveV1));
            }
        }

        /// <summary>
        ///     This function returns the virtual size of frame buffer in KB. This includes the physical RAM plus any system RAM
        ///     that has been dedicated for use by the GPU.
        /// </summary>
        /// <param name="gpuHandle">Physical GPU handle to get information about</param>
        /// <returns>Virtual size of frame buffer in KB</returns>
        /// <exception cref="NVIDIAApiException">Status.InvalidArgument: display is not valid</exception>
        /// <exception cref="NVIDIAApiException">Status.NvidiaDeviceNotFound: No NVIDIA GPU driving a display was found</exception>
        /// <exception cref="NVIDIAApiException">Status.ExpectedPhysicalGPUHandle: gpuHandle was not a physical GPU handle.</exception>
        public static int GetVirtualFrameBufferSize(PhysicalGPUHandle gpuHandle)
        {
            var status = DelegateFactory.GetDelegate<Delegates.GPU.NvAPI_GPU_GetVirtualFrameBufferSize>()(gpuHandle,
                out var bufferSize);

            if (status != Status.Ok)
            {
                throw new NVIDIAApiException(status);
            }

            return (int) bufferSize;
        }

        /// <summary>
        ///     [PRIVATE]
        ///     Gets the VPE count for the passed GPU handle.
        /// </summary>
        /// <param name="gpuHandle">The handle of the GPU to retrieve this information from.</param>
        /// <returns>The number of VPE units.</returns>
        public static uint GetVPECount(PhysicalGPUHandle gpuHandle)
        {
            var status =
                DelegateFactory.GetDelegate<Delegates.GPU.NvAPI_GPU_GetVPECount>()(gpuHandle, out var vpeCount);

            if (status != Status.Ok)
            {
                throw new NVIDIAApiException(status);
            }

            return vpeCount;
        }

        /// <summary>
        ///     [PRIVATE]
        ///     Gets the performance policies current information for the passed GPU handle.
        /// </summary>
        /// <param name="gpuHandle">The handle of the GPU to perform the operation on.</param>
        /// <returns>The performance policies information.</returns>
        public static PrivatePerformanceInfoV1 PerformancePoliciesGetInfo(PhysicalGPUHandle gpuHandle)
        {
            var instance = typeof(PrivatePerformanceInfoV1).Instantiate<PrivatePerformanceInfoV1>();

            using (var performanceInfoReference = ValueTypeReference.FromValueType(instance))
            {
                var status = DelegateFactory.GetDelegate<Delegates.GPU.NvAPI_GPU_PerfPoliciesGetInfo>()(
                    gpuHandle,
                    performanceInfoReference
                );

                if (status != Status.Ok)
                {
                    throw new NVIDIAApiException(status);
                }

                return performanceInfoReference.ToValueType<PrivatePerformanceInfoV1>(typeof(PrivatePerformanceInfoV1));
            }
        }

        /// <summary>
        ///     [PRIVATE]
        ///     Gets the performance policies status for the passed GPU handle.
        /// </summary>
        /// <param name="gpuHandle">The handle of the GPU to perform the operation on.</param>
        /// <returns>The performance policies status of the GPU.</returns>
        public static PrivatePerformanceStatusV1 PerformancePoliciesGetStatus(PhysicalGPUHandle gpuHandle)
        {
            var instance = typeof(PrivatePerformanceStatusV1).Instantiate<PrivatePerformanceStatusV1>();

            using (var performanceStatusReference = ValueTypeReference.FromValueType(instance))
            {
                var status = DelegateFactory.GetDelegate<Delegates.GPU.NvAPI_GPU_PerfPoliciesGetStatus>()(
                    gpuHandle,
                    performanceStatusReference
                );

                if (status != Status.Ok)
                {
                    throw new NVIDIAApiException(status);
                }

                return performanceStatusReference.ToValueType<PrivatePerformanceStatusV1>(
                    typeof(PrivatePerformanceStatusV1));
            }
        }

        /// <summary>
        ///     This function resets ECC memory error counters.
        /// </summary>
        /// <param name="gpuHandle">A handle identifying the physical GPU for which ECC error information is to be cleared.</param>
        /// <param name="resetCurrent">Reset the current ECC error counters.</param>
        /// <param name="resetAggregated">Reset the aggregate ECC error counters.</param>
        public static void ResetECCErrorInfo(
            PhysicalGPUHandle gpuHandle,
            bool resetCurrent,
            bool resetAggregated)
        {
            var status = DelegateFactory.GetDelegate<Delegates.GPU.NvAPI_GPU_ResetECCErrorInfo>()(
                gpuHandle,
                (byte) (resetCurrent ? 1 : 0),
                (byte) (resetAggregated ? 1 : 0)
            );

            if (status != Status.Ok)
            {
                throw new NVIDIAApiException(status);
            }
        }

        /// <summary>
        ///     [PRIVATE]
        ///     Restores the cooler policy table to default for the passed GPU handle and cooler index.
        /// </summary>
        /// <param name="gpuHandle">The handle of the GPU to perform the operation on.</param>
        /// <param name="policy">The cooler policy to restore to default.</param>
        /// <param name="indexes">The indexes of the coolers to restore their policy tables to default.</param>
        public static void RestoreCoolerPolicyTable(
            PhysicalGPUHandle gpuHandle,
            CoolerPolicy policy,
            uint[] indexes = null)
        {
            var status = DelegateFactory.GetDelegate<Delegates.GPU.NvAPI_GPU_RestoreCoolerPolicyTable>()(
                gpuHandle,
                indexes,
                (uint) (indexes?.Length ?? 0),
                policy
            );

            if (status != Status.Ok)
            {
                throw new NVIDIAApiException(status);
            }
        }

        /// <summary>
        ///     [PRIVATE]
        ///     Restores the cooler settings to default for the passed GPU handle and cooler index.
        /// </summary>
        /// <param name="gpuHandle">The handle of the GPU to perform the operation on.</param>
        /// <param name="indexes">The indexes of the coolers to restore their settings to default.</param>
        public static void RestoreCoolerSettings(
            PhysicalGPUHandle gpuHandle,
            uint[] indexes = null)
        {
            var status = DelegateFactory.GetDelegate<Delegates.GPU.NvAPI_GPU_RestoreCoolerSettings>()(
                gpuHandle,
                indexes,
                (uint) (indexes?.Length ?? 0)
            );

            if (status != Status.Ok)
            {
                throw new NVIDIAApiException(status);
            }
        }


        /// <summary>
        ///     [PRIVATE] - [Pascal Only]
        ///     Sets the clock boost lock status for the passed GPU handle.
        /// </summary>
        /// <param name="gpuHandle">The handle of the GPU to perform the operation on.</param>
        /// <param name="clockBoostLock">The new clock boost lock status.</param>
        public static void SetClockBoostLock(PhysicalGPUHandle gpuHandle, PrivateClockBoostLockV2 clockBoostLock)
        {
            using (var clockLockReference = ValueTypeReference.FromValueType(clockBoostLock))
            {
                var status = DelegateFactory.GetDelegate<Delegates.GPU.NvAPI_GPU_SetClockBoostLock>()(
                    gpuHandle,
                    clockLockReference
                );

                if (status != Status.Ok)
                {
                    throw new NVIDIAApiException(status);
                }
            }
        }

        /// <summary>
        ///     [PRIVATE] - [Pascal Only]
        ///     Sets the clock boost table for the passed GPU handle.
        /// </summary>
        /// <param name="gpuHandle">The handle of the GPU to perform the operation on.</param>
        /// <param name="clockBoostTable">The new clock table.</param>
        public static void SetClockBoostTable(PhysicalGPUHandle gpuHandle, PrivateClockBoostTableV1 clockBoostTable)
        {
            using (var clockTableReference = ValueTypeReference.FromValueType(clockBoostTable))
            {
                var status = DelegateFactory.GetDelegate<Delegates.GPU.NvAPI_GPU_SetClockBoostTable>()(
                    gpuHandle,
                    clockTableReference
                );

                if (status != Status.Ok)
                {
                    throw new NVIDIAApiException(status);
                }
            }
        }

        /// <summary>
        ///     [PRIVATE]
        ///     Sets the cooler levels for the passed GPU handle.
        /// </summary>
        /// <param name="gpuHandle">The handle of the GPU to perform the operation on.</param>
        /// <param name="index">The cooler index.</param>
        /// <param name="coolerLevels">The cooler level information.</param>
        /// <param name="levelsCount">The number of entries in the cooler level information.</param>
        // ReSharper disable once TooManyArguments
        public static void SetCoolerLevels(
            PhysicalGPUHandle gpuHandle,
            uint index,
            PrivateCoolerLevelsV1 coolerLevels,
            uint levelsCount
        )
        {
            using (var coolerLevelsReference = ValueTypeReference.FromValueType(coolerLevels))
            {
                var status = DelegateFactory.GetDelegate<Delegates.GPU.NvAPI_GPU_SetCoolerLevels>()(
                    gpuHandle,
                    index,
                    coolerLevelsReference,
                    levelsCount
                );

                if (status != Status.Ok)
                {
                    throw new NVIDIAApiException(status);
                }
            }
        }

        /// <summary>
        ///     [PRIVATE]
        ///     Sets the cooler policy table for the passed GPU handle.
        /// </summary>
        /// <param name="gpuHandle">The handle of the GPU to perform the operation on.</param>
        /// <param name="index">The cooler index.</param>
        /// <param name="coolerPolicyTable">The cooler policy table.</param>
        /// <param name="policyLevelsCount">The number of entries in the cooler policy table.</param>
        // ReSharper disable once TooManyArguments
        public static void SetCoolerPolicyTable(
            PhysicalGPUHandle gpuHandle,
            uint index,
            PrivateCoolerPolicyTableV1 coolerPolicyTable,
            uint policyLevelsCount
        )
        {
            var instance = typeof(PrivateCoolerPolicyTableV1).Instantiate<PrivateCoolerPolicyTableV1>();

            using (var policyTableReference = ValueTypeReference.FromValueType(instance))
            {
                var status = DelegateFactory.GetDelegate<Delegates.GPU.NvAPI_GPU_SetCoolerPolicyTable>()(
                    gpuHandle,
                    index,
                    policyTableReference,
                    policyLevelsCount
                );

                if (status != Status.Ok)
                {
                    throw new NVIDIAApiException(status);
                }
            }
        }

        /// <summary>
        ///     [PRIVATE] - [Pascal Only]
        ///     Sets the core voltage boost percentage
        /// </summary>
        /// <param name="gpuHandle">The handle of the GPU to perform the operation on.</param>
        /// <param name="boostPercent">The voltage boost percentages.</param>
        public static void SetCoreVoltageBoostPercent(
            PhysicalGPUHandle gpuHandle,
            PrivateVoltageBoostPercentV1 boostPercent)
        {
            using (var boostPercentReference = ValueTypeReference.FromValueType(boostPercent))
            {
                var status = DelegateFactory.GetDelegate<Delegates.GPU.NvAPI_GPU_SetCoreVoltageBoostPercent>()(
                    gpuHandle,
                    boostPercentReference
                );

                if (status != Status.Ok)
                {
                    throw new NVIDIAApiException(status);
                }
            }
        }

        /// <summary>
        ///     This function updates the ECC memory configuration setting.
        /// </summary>
        /// <param name="gpuHandle">A handle identifying the physical GPU for which to update the ECC configuration setting.</param>
        /// <param name="isEnable">The new ECC configuration setting.</param>
        /// <param name="isEnableImmediately">Request that the new setting take effect immediately.</param>
        public static void SetECCConfiguration(
            PhysicalGPUHandle gpuHandle,
            bool isEnable,
            bool isEnableImmediately)
        {
            var status = DelegateFactory.GetDelegate<Delegates.GPU.NvAPI_GPU_SetECCConfiguration>()(
                gpuHandle,
                (byte) (isEnable ? 1 : 0),
                (byte) (isEnableImmediately ? 1 : 0)
            );

            if (status != Status.Ok)
            {
                throw new NVIDIAApiException(status);
            }
        }

        /// <summary>
        ///     Thus function sets the EDID data for the specified GPU handle and connection bit mask.
        ///     User can either send (Gpu handle and output id) or only display Id in variable outputId parameter and gpuHandle
        ///     parameter can be default handle.
        ///     Note: The EDID will be cached across the boot session and will be enumerated to the OS in this call. To remove the
        ///     EDID set size of EDID to zero. OS and NVAPI connection status APIs will reflect the newly set or removed EDID
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
        ///     EDID set size of EDID to zero. OS and NVAPI connection status APIs will reflect the newly set or removed EDID
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
        /// <exception cref="Exception">A delegate callback throws an exception.</exception>
        public static void SetEDID(PhysicalGPUHandle gpuHandle, uint displayId, IEDID edid)
        {
            var gpuSetEDID = DelegateFactory.GetDelegate<Delegates.GPU.NvAPI_GPU_SetEDID>();

            if (!gpuSetEDID.Accepts().Contains(edid.GetType()))
            {
                throw new NVIDIANotSupportedException("This operation is not supported.");
            }

            using (var edidReference = ValueTypeReference.FromValueType(edid, edid.GetType()))
            {
                var status = gpuSetEDID(gpuHandle, displayId, edidReference);

                if (status != Status.Ok)
                {
                    throw new NVIDIAApiException(status);
                }
            }
        }

        /// <summary>
        ///     [PRIVATE]
        ///     This function sets the performance states (P-States) 2.0 information.
        ///     P-States are GPU active/executing performance capability and power consumption states.
        /// </summary>
        /// <param name="physicalGPUHandle">GPU handle to get information about.</param>
        /// <param name="performanceStates20Info">Performance status 2.0 information to set</param>
        /// <exception cref="NVIDIAApiException">Status.InvalidArgument: gpuHandle is NULL</exception>
        /// <exception cref="NVIDIAApiException">Status.ExpectedPhysicalGPUHandle: gpuHandle was not a physical GPU handle</exception>
        public static void SetPerformanceStates20(
            PhysicalGPUHandle physicalGPUHandle,
            IPerformanceStates20Info performanceStates20Info)
        {
            using (var performanceStateInfo =
                ValueTypeReference.FromValueType(performanceStates20Info, performanceStates20Info.GetType()))
            {
                var status = DelegateFactory.GetDelegate<Delegates.GPU.NvAPI_GPU_SetPStates20>()(
                    physicalGPUHandle,
                    performanceStateInfo
                );

                if (status != Status.Ok)
                {
                    throw new NVIDIAApiException(status);
                }
            }
        }

        /// <summary>
        ///     [PRIVATE]
        ///     Sets the thermal policies status for the passed GPU handle.
        /// </summary>
        /// <param name="gpuHandle">The handle of the GPU to perform the operation on.</param>
        /// <param name="thermalPoliciesStatus">The new thermal limiter policy to apply.</param>
        public static void SetThermalPoliciesStatus(
            PhysicalGPUHandle gpuHandle,
            PrivateThermalPoliciesStatusV2 thermalPoliciesStatus)
        {
            using (var policiesStatusReference = ValueTypeReference.FromValueType(thermalPoliciesStatus))
            {
                var status = DelegateFactory.GetDelegate<Delegates.GPU.NvAPI_GPU_SetThermalPoliciesStatus>()(
                    gpuHandle,
                    policiesStatusReference
                );

                if (status != Status.Ok)
                {
                    throw new NVIDIAApiException(status);
                }
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
            var status =
                DelegateFactory.GetDelegate<Delegates.GPU.NvAPI_GPU_ValidateOutputCombination>()(gpuHandle, outputIds);

            if (status == Status.InvalidCombination)
            {
                return false;
            }

            if (status != Status.Ok)
            {
                throw new NVIDIAApiException(status);
            }

            return true;
        }
    }
}