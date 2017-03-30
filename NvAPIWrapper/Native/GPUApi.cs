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
    public static class GPUApi
    {
        public static LogicalGPUHandle[] EnumLogicalGPUs()
        {
            var gpuList =
                typeof(LogicalGPUHandle).Instantiate<LogicalGPUHandle>().Repeat(LogicalGPUHandle.MaxLogicalGPUs);
            uint count;
            var status = DelegateFactory.Get<Delegates.GPU.NvAPI_EnumLogicalGPUs>()(gpuList, out count);
            if (status != Status.Ok)
            {
                throw new NVIDIAApiException(status);
            }
            return gpuList.Take((int) count).ToArray();
        }

        public static PhysicalGPUHandle[] EnumTCCPhysicalGPUs()
        {
            var gpuList =
                typeof(PhysicalGPUHandle).Instantiate<PhysicalGPUHandle>().Repeat(PhysicalGPUHandle.PhysicalGPUs);
            uint count;
            var status = DelegateFactory.Get<Delegates.GPU.NvAPI_EnumTCCPhysicalGPUs>()(gpuList, out count);
            if (status != Status.Ok)
            {
                throw new NVIDIAApiException(status);
            }
            return gpuList.Take((int) count).ToArray();
        }

        public static PhysicalGPUHandle[] EnumPhysicalGPUs()
        {
            var gpuList =
                typeof(PhysicalGPUHandle).Instantiate<PhysicalGPUHandle>().Repeat(PhysicalGPUHandle.PhysicalGPUs);
            uint count;
            var status = DelegateFactory.Get<Delegates.GPU.NvAPI_EnumPhysicalGPUs>()(gpuList, out count);
            if (status != Status.Ok)
            {
                throw new NVIDIAApiException(status);
            }
            return gpuList.Take((int) count).ToArray();
        }

        public static BoardInfo GetBoardInfo(PhysicalGPUHandle gpuHandle)
        {
            var boardInfo = typeof(BoardInfo).Instantiate<BoardInfo>();
            var status = DelegateFactory.Get<Delegates.GPU.NvAPI_GPU_GetBoardInfo>()(gpuHandle, ref boardInfo);
            if (status != Status.Ok)
            {
                throw new NVIDIAApiException(status);
            }
            return boardInfo;
        }

        public static void GetPCIIdentifiers(PhysicalGPUHandle gpuHandle, out uint deviceId, out uint subSystemId,
            out uint revisionId, out uint extDeviceId)
        {
            var status = DelegateFactory.Get<Delegates.GPU.NvAPI_GPU_GetPCIIdentifiers>()(gpuHandle, out deviceId,
                out subSystemId, out revisionId, out extDeviceId);
            if (status != Status.Ok)
            {
                throw new NVIDIAApiException(status);
            }
        }

        public static int GetShaderSubPipeCount(PhysicalGPUHandle gpuHandle)
        {
            uint count;
            var status = DelegateFactory.Get<Delegates.GPU.NvAPI_GPU_GetShaderSubPipeCount>()(gpuHandle, out count);
            if (status != Status.Ok)
            {
                throw new NVIDIAApiException(status);
            }
            return (int) count;
        }

        public static int GetPhysicalFrameBufferSize(PhysicalGPUHandle gpuHandle)
        {
            uint size;
            var status = DelegateFactory.Get<Delegates.GPU.NvAPI_GPU_GetPhysicalFrameBufferSize>()(gpuHandle, out size);
            if (status != Status.Ok)
            {
                throw new NVIDIAApiException(status);
            }
            return (int) size;
        }

        public static bool GetQuadroStatus(PhysicalGPUHandle gpuHandle)
        {
            uint isQuadro;
            var status = DelegateFactory.Get<Delegates.GPU.NvAPI_GPU_GetQuadroStatus>()(gpuHandle, out isQuadro);
            if (status != Status.Ok)
            {
                throw new NVIDIAApiException(status);
            }
            return isQuadro > 0;
        }

        public static int GetBusId(PhysicalGPUHandle gpuHandle)
        {
            uint busId;
            var status = DelegateFactory.Get<Delegates.GPU.NvAPI_GPU_GetBusId>()(gpuHandle, out busId);
            if (status != Status.Ok)
            {
                throw new NVIDIAApiException(status);
            }
            return (int) busId;
        }

        public static uint GetVBIOSOEMRevision(PhysicalGPUHandle gpuHandle)
        {
            uint revision;
            var status = DelegateFactory.Get<Delegates.GPU.NvAPI_GPU_GetVbiosOEMRevision>()(gpuHandle, out revision);
            if (status != Status.Ok)
            {
                throw new NVIDIAApiException(status);
            }
            return revision;
        }

        public static uint GetVBIOSRevision(PhysicalGPUHandle gpuHandle)
        {
            uint revision;
            var status = DelegateFactory.Get<Delegates.GPU.NvAPI_GPU_GetVbiosRevision>()(gpuHandle, out revision);
            if (status != Status.Ok)
            {
                throw new NVIDIAApiException(status);
            }
            return revision;
        }

        public static GPUBusType GetBusType(PhysicalGPUHandle gpuHandle)
        {
            GPUBusType busType;
            var status = DelegateFactory.Get<Delegates.GPU.NvAPI_GPU_GetBusType>()(gpuHandle, out busType);
            if (status != Status.Ok)
            {
                throw new NVIDIAApiException(status);
            }
            return busType;
        }

        public static SystemType GetSystemType(PhysicalGPUHandle gpuHandle)
        {
            SystemType systemType;
            var status = DelegateFactory.Get<Delegates.GPU.NvAPI_GPU_GetSystemType>()(gpuHandle, out systemType);
            if (status != Status.Ok)
            {
                throw new NVIDIAApiException(status);
            }
            return systemType;
        }


        public static GPUType GetGPUType(PhysicalGPUHandle gpuHandle)
        {
            GPUType type;
            var status = DelegateFactory.Get<Delegates.GPU.NvAPI_GPU_GetGPUType>()(gpuHandle, out type);
            if (status != Status.Ok)
            {
                throw new NVIDIAApiException(status);
            }
            return type;
        }


        public static OutputType GetOutputType(PhysicalGPUHandle gpuHandle, OutputId outputId)
        {
            OutputType type;
            var status = DelegateFactory.Get<Delegates.GPU.NvAPI_GPU_GetOutputType>()(gpuHandle, outputId, out type);
            if (status != Status.Ok)
            {
                throw new NVIDIAApiException(status);
            }
            return type;
        }

        public static int GetIRQ(PhysicalGPUHandle gpuHandle)
        {
            uint irq;
            var status = DelegateFactory.Get<Delegates.GPU.NvAPI_GPU_GetIRQ>()(gpuHandle, out irq);
            if (status != Status.Ok)
            {
                throw new NVIDIAApiException(status);
            }
            return (int) irq;
        }

        public static int GetBusSlotId(PhysicalGPUHandle gpuHandle)
        {
            uint busId;
            var status = DelegateFactory.Get<Delegates.GPU.NvAPI_GPU_GetBusSlotId>()(gpuHandle, out busId);
            if (status != Status.Ok)
            {
                throw new NVIDIAApiException(status);
            }
            return (int) busId;
        }


        public static LogicalGPUHandle GetLogicalGPUFromPhysicalGPU(PhysicalGPUHandle gpuHandle)
        {
            LogicalGPUHandle gpu;
            var status = DelegateFactory.Get<Delegates.GPU.NvAPI_GetLogicalGPUFromPhysicalGPU>()(gpuHandle, out gpu);
            if (status != Status.Ok)
            {
                throw new NVIDIAApiException(status);
            }
            return gpu;
        }

        public static uint GetDisplayIdFromGpuAndOutputId(PhysicalGPUHandle gpuHandle, OutputId outputId)
        {
            uint display;
            var status = DelegateFactory.Get<Delegates.GPU.NvAPI_SYS_GetDisplayIdFromGpuAndOutputId>()(gpuHandle,
                outputId, out display);
            if (status != Status.Ok)
            {
                throw new NVIDIAApiException(status);
            }
            return display;
        }


        public static PhysicalGPUHandle GetPhysicalGpuFromDisplayId(uint displayId)
        {
            PhysicalGPUHandle gpu;
            var status = DelegateFactory.Get<Delegates.GPU.NvAPI_SYS_GetPhysicalGpuFromDisplayId>()(displayId, out gpu);
            if (status != Status.Ok)
            {
                throw new NVIDIAApiException(status);
            }
            return gpu;
        }

        public static OutputId GetGpuAndOutputIdFromDisplayId(uint displayId, out PhysicalGPUHandle gpuHandle)
        {
            OutputId outputId;
            var status = DelegateFactory.Get<Delegates.GPU.NvAPI_SYS_GetGpuAndOutputIdFromDisplayId>()(displayId,
                out gpuHandle, out outputId);
            if (status != Status.Ok)
            {
                throw new NVIDIAApiException(status);
            }
            return outputId;
        }

        public static LogicalGPUHandle GetLogicalGPUFromDisplay(DisplayHandle display)
        {
            LogicalGPUHandle gpu;
            var status = DelegateFactory.Get<Delegates.GPU.NvAPI_GetLogicalGPUFromDisplay>()(display, out gpu);
            if (status != Status.Ok)
            {
                throw new NVIDIAApiException(status);
            }
            return gpu;
        }

        public static PhysicalGPUHandle GetPhysicalGPUFromUnAttachedDisplay(UnAttachedDisplayHandle display)
        {
            PhysicalGPUHandle gpu;
            var status = DelegateFactory.Get<Delegates.GPU.NvAPI_GetPhysicalGPUFromUnAttachedDisplay>()(display, out gpu);
            if (status != Status.Ok)
            {
                throw new NVIDIAApiException(status);
            }
            return gpu;
        }

        public static OutputId GetActiveOutputs(PhysicalGPUHandle gpuHandle)
        {
            OutputId outputMask;
            var status = DelegateFactory.Get<Delegates.GPU.NvAPI_GPU_GetActiveOutputs>()(gpuHandle, out outputMask);
            if (status != Status.Ok)
            {
                throw new NVIDIAApiException(status);
            }
            return outputMask;
        }

        public static int GetAGPAperture(PhysicalGPUHandle gpuHandle)
        {
            uint agpAperture;
            var status = DelegateFactory.Get<Delegates.GPU.NvAPI_GPU_GetAGPAperture>()(gpuHandle, out agpAperture);
            if (status != Status.Ok)
            {
                throw new NVIDIAApiException(status);
            }
            return (int) agpAperture;
        }

        public static int GetCurrentAGPRate(PhysicalGPUHandle gpuHandle)
        {
            uint agpRate;
            var status = DelegateFactory.Get<Delegates.GPU.NvAPI_GPU_GetCurrentAGPRate>()(gpuHandle, out agpRate);
            if (status != Status.Ok)
            {
                throw new NVIDIAApiException(status);
            }
            return (int) agpRate;
        }

        public static int GetCurrentPCIEDownStreamWidth(PhysicalGPUHandle gpuHandle)
        {
            uint width;
            var status = DelegateFactory.Get<Delegates.GPU.NvAPI_GPU_GetCurrentPCIEDownstreamWidth>()(gpuHandle,
                out width);
            if (status != Status.Ok)
            {
                throw new NVIDIAApiException(status);
            }
            return (int) width;
        }

        public static int GetGPUCoreCount(PhysicalGPUHandle gpuHandle)
        {
            uint cores;
            var status = DelegateFactory.Get<Delegates.GPU.NvAPI_GPU_GetGpuCoreCount>()(gpuHandle, out cores);
            if (status != Status.Ok)
            {
                throw new NVIDIAApiException(status);
            }
            return (int) cores;
        }

        public static int GetVirtualFrameBufferSize(PhysicalGPUHandle gpuHandle)
        {
            uint bufferSize;
            var status = DelegateFactory.Get<Delegates.GPU.NvAPI_GPU_GetVirtualFrameBufferSize>()(gpuHandle,
                out bufferSize);
            if (status != Status.Ok)
            {
                throw new NVIDIAApiException(status);
            }
            return (int) bufferSize;
        }

        public static string GetFullName(PhysicalGPUHandle gpuHandle)
        {
            ShortString name;
            var status = DelegateFactory.Get<Delegates.GPU.NvAPI_GPU_GetFullName>()(gpuHandle, out name);
            if (status != Status.Ok)
            {
                throw new NVIDIAApiException(status);
            }
            return name.Value;
        }

        public static string GetVBIOSVersionString(PhysicalGPUHandle gpuHandle)
        {
            ShortString version;
            var status = DelegateFactory.Get<Delegates.GPU.NvAPI_GPU_GetVbiosVersionString>()(gpuHandle, out version);
            if (status != Status.Ok)
            {
                throw new NVIDIAApiException(status);
            }
            return version.Value;
        }

        public static void ValidateOutputCombination(PhysicalGPUHandle gpuHandle, OutputId outputIds)
        {
            var status = DelegateFactory.Get<Delegates.GPU.NvAPI_GPU_ValidateOutputCombination>()(gpuHandle, outputIds);
            if (status != Status.Ok)
            {
                throw new NVIDIAApiException(status);
            }
        }

        public static void SetEDID(PhysicalGPUHandle gpuHandle, OutputId outputId, IEDID edid)
        {
            var gpuSetEDID = DelegateFactory.Get<Delegates.GPU.NvAPI_GPU_SetEDID>();
            if (!gpuSetEDID.Accepts().Contains(edid.GetType()))
            {
                throw new NVIDIANotSupportedException("This operation is not supported.");
            }
            using (var edidReference = ValueTypeReference.FromValueType(edid, edid.GetType()))
            {
                var status = gpuSetEDID(gpuHandle, outputId, edidReference);
                if (status != Status.Ok)
                {
                    throw new NVIDIAApiException(status);
                }
            }
        }

        public static EDIDV3 GetEDID(PhysicalGPUHandle gpuHandle, OutputId outputId, int offset,
            int readIdentification = 0)
        {
            var gpuGetEDID = DelegateFactory.Get<Delegates.GPU.NvAPI_GPU_GetEDID>();
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

        public static IEDID GetEDID(PhysicalGPUHandle gpuHandle, OutputId outputId)
        {
            var gpuGetEDID = DelegateFactory.Get<Delegates.GPU.NvAPI_GPU_GetEDID>();
            foreach (var acceptType in gpuGetEDID.Accepts())
            {
                using (var edidReference = ValueTypeReference.FromValueType(acceptType.Instantiate<IEDID>(), acceptType)
                )
                {
                    var status = gpuGetEDID(gpuHandle, outputId, edidReference);
                    if (status != Status.IncompatibleStructVersion)
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

        public static PhysicalGPUHandle[] GetPhysicalGPUsFromDisplay(DisplayHandle display)
        {
            var gpuList =
                typeof(PhysicalGPUHandle).Instantiate<PhysicalGPUHandle>().Repeat(PhysicalGPUHandle.MaxPhysicalGPUs);
            uint count;
            var status = DelegateFactory.Get<Delegates.GPU.NvAPI_GetPhysicalGPUsFromDisplay>()(display, gpuList,
                out count);
            if (status != Status.Ok)
            {
                throw new NVIDIAApiException(status);
            }
            return gpuList.Take((int) count).ToArray();
        }

        public static PhysicalGPUHandle[] GetPhysicalGPUsFromLogicalGPU(LogicalGPUHandle gpuHandle)
        {
            var gpuList =
                typeof(PhysicalGPUHandle).Instantiate<PhysicalGPUHandle>().Repeat(PhysicalGPUHandle.MaxPhysicalGPUs);
            uint count;
            var status = DelegateFactory.Get<Delegates.GPU.NvAPI_GetPhysicalGPUsFromLogicalGPU>()(gpuHandle, gpuList,
                out count);
            if (status != Status.Ok)
            {
                throw new NVIDIAApiException(status);
            }
            return gpuList.Take((int) count).ToArray();
        }

        public static DisplayIdsV2[] GetAllDisplayIds(PhysicalGPUHandle gpuHandle)
        {
            var gpuGetConnectedDisplayIds = DelegateFactory.Get<Delegates.GPU.NvAPI_GPU_GetAllDisplayIds>();
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

        public static DisplayIdsV2[] GetConnectedDisplayIds(PhysicalGPUHandle gpuHandle, ConnectedIdsFlag flags)
        {
            var gpuGetConnectedDisplayIds = DelegateFactory.Get<Delegates.GPU.NvAPI_GPU_GetConnectedDisplayIds>();
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
    }
}