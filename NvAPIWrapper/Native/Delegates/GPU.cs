using System.Runtime.InteropServices;
using NvAPIWrapper.Native.Attributes;
using NvAPIWrapper.Native.Display.Structures;
using NvAPIWrapper.Native.General;
using NvAPIWrapper.Native.General.Structures;
using NvAPIWrapper.Native.GPU;
using NvAPIWrapper.Native.GPU.Structures;
using NvAPIWrapper.Native.Helpers;
using NvAPIWrapper.Native.Helpers.Structures;

// ReSharper disable InconsistentNaming

namespace NvAPIWrapper.Native.Delegates
{
    internal static class GPU
    {
        [FunctionId(FunctionId.NvAPI_EnumLogicalGPUs)]
        public delegate Status NvAPI_EnumLogicalGPUs(
            [In] [Out] [MarshalAs(UnmanagedType.LPArray, SizeConst = LogicalGPUHandle.MaxLogicalGPUs)]
            LogicalGPUHandle[]
                gpuHandles,
            [Out] out uint gpuCount);

        [FunctionId(FunctionId.NvAPI_EnumPhysicalGPUs)]
        public delegate Status NvAPI_EnumPhysicalGPUs(
            [In] [Out] [MarshalAs(UnmanagedType.LPArray, SizeConst = PhysicalGPUHandle.MaxPhysicalGPUs)]
            PhysicalGPUHandle[]
                gpuHandles,
            [Out] out uint gpuCount);

        [FunctionId(FunctionId.NvAPI_EnumTCCPhysicalGPUs)]
        public delegate Status NvAPI_EnumTCCPhysicalGPUs(
            [In] [Out] [MarshalAs(UnmanagedType.LPArray, SizeConst = PhysicalGPUHandle.MaxPhysicalGPUs)]
            PhysicalGPUHandle[]
                gpuHandles,
            [Out] out uint gpuCount);

        [FunctionId(FunctionId.NvAPI_GetLogicalGPUFromDisplay)]
        public delegate Status NvAPI_GetLogicalGPUFromDisplay(
            [In] DisplayHandle displayHandle,
            [Out] out LogicalGPUHandle gpuHandle);

        [FunctionId(FunctionId.NvAPI_GetLogicalGPUFromPhysicalGPU)]
        public delegate Status NvAPI_GetLogicalGPUFromPhysicalGPU(
            [In] PhysicalGPUHandle physicalGPUHandle,
            [Out] out LogicalGPUHandle logicalGPUHandle);

        [FunctionId(FunctionId.NvAPI_GetPhysicalGPUFromUnAttachedDisplay)]
        public delegate Status NvAPI_GetPhysicalGPUFromUnAttachedDisplay(
            [In] UnAttachedDisplayHandle displayHandle,
            [Out] out PhysicalGPUHandle gpuHandle);

        [FunctionId(FunctionId.NvAPI_GetPhysicalGPUsFromDisplay)]
        public delegate Status NvAPI_GetPhysicalGPUsFromDisplay(
            [In] DisplayHandle displayHandle,
            [In] [Out] [MarshalAs(UnmanagedType.LPArray, SizeConst = PhysicalGPUHandle.MaxPhysicalGPUs)]
            PhysicalGPUHandle[]
                gpuHandles,
            [Out] out uint gpuCount);

        [FunctionId(FunctionId.NvAPI_GetPhysicalGPUsFromLogicalGPU)]
        public delegate Status NvAPI_GetPhysicalGPUsFromLogicalGPU(
            [In] LogicalGPUHandle logicalGPUHandle,
            [In] [Out] [MarshalAs(UnmanagedType.LPArray, SizeConst = PhysicalGPUHandle.MaxPhysicalGPUs)]
            PhysicalGPUHandle[]
                gpuHandles,
            [Out] out uint gpuCount);

        [FunctionId(FunctionId.NvAPI_GPU_GetActiveOutputs)]
        public delegate Status NvAPI_GPU_GetActiveOutputs(
            [In] PhysicalGPUHandle physicalGpu,
            [Out] out OutputId outputMask);

        [FunctionId(FunctionId.NvAPI_GPU_GetAGPAperture)]
        public delegate Status NvAPI_GPU_GetAGPAperture(
            [In] PhysicalGPUHandle physicalGpu,
            [Out] out uint size);

        [FunctionId(FunctionId.NvAPI_GPU_GetAllClockFrequencies)]
        public delegate Status NvAPI_GPU_GetAllClockFrequencies(
            [In] PhysicalGPUHandle physicalGpu,
            [In] [Accepts(typeof(ClockFrequenciesV3), typeof(ClockFrequenciesV2), typeof(ClockFrequenciesV1))]
            ValueTypeReference nvClocks);

        [FunctionId(FunctionId.NvAPI_GPU_GetAllDisplayIds)]
        public delegate Status NvAPI_GPU_GetAllDisplayIds(
            [In] PhysicalGPUHandle physicalGpu,
            [Accepts(typeof(DisplayIdsV2))] [In] [Out]
            ValueTypeArray pDisplayIds,
            [In] [Out] ref uint displayIdCount);

        [FunctionId(FunctionId.NvAPI_GPU_GetBoardInfo)]
        public delegate Status NvAPI_GPU_GetBoardInfo(
            [In] PhysicalGPUHandle physicalGpu,
            [Out] [In] ref BoardInfo info);

        [FunctionId(FunctionId.NvAPI_GPU_GetBusId)]
        public delegate Status NvAPI_GPU_GetBusId(
            [In] PhysicalGPUHandle physicalGpu,
            [Out] out uint gpuBusId);

        [FunctionId(FunctionId.NvAPI_GPU_GetBusSlotId)]
        public delegate Status NvAPI_GPU_GetBusSlotId(
            [In] PhysicalGPUHandle physicalGpu,
            [Out] out uint gpuBusSlotId);

        [FunctionId(FunctionId.NvAPI_GPU_GetBusType)]
        public delegate Status NvAPI_GPU_GetBusType(
            [In] PhysicalGPUHandle physicalGpu,
            [Out] out GPUBusType gpuBusType);

        [FunctionId(FunctionId.NvAPI_GPU_GetConnectedDisplayIds)]
        public delegate Status NvAPI_GPU_GetConnectedDisplayIds(
            [In] PhysicalGPUHandle physicalGpu,
            [Accepts(typeof(DisplayIdsV2))] [In] [Out]
            ValueTypeArray pDisplayIds,
            [In] [Out] ref uint displayIdCount,
            [In] ConnectedIdsFlag flags);

        [FunctionId(FunctionId.NvAPI_GPU_GetCurrentAGPRate)]
        public delegate Status NvAPI_GPU_GetCurrentAGPRate(
            [In] PhysicalGPUHandle physicalGpu,
            [Out] out uint rate);

        [FunctionId(FunctionId.NvAPI_GPU_GetCurrentPCIEDownstreamWidth)]
        public delegate Status NvAPI_GPU_GetCurrentPCIEDownstreamWidth(
            [In] PhysicalGPUHandle physicalGpu,
            [Out] out uint width);

        [FunctionId(FunctionId.NvAPI_GPU_GetDynamicPStatesInfoEx)]
        public delegate Status NvAPI_GPU_GetDynamicPStatesInfoEx(
            [In] PhysicalGPUHandle physicalGpu,
            [In] [Accepts(typeof(DynamicPerformanceStatesInfo))]
            ValueTypeReference performanceStatesInfoEx);

        [FunctionId(FunctionId.NvAPI_GPU_GetEDID)]
        public delegate Status NvAPI_GPU_GetEDID(
            [In] PhysicalGPUHandle physicalGpu,
            [In] OutputId outputId,
            [Accepts(typeof(EDIDV3), typeof(EDIDV2), typeof(EDIDV1))] [In]
            ValueTypeReference edid);

        [FunctionId(FunctionId.NvAPI_GPU_GetFullName)]
        public delegate Status NvAPI_GPU_GetFullName(
            [In] PhysicalGPUHandle physicalGpu,
            [Out] out ShortString name);

        [FunctionId(FunctionId.NvAPI_GPU_GetGpuCoreCount)]
        public delegate Status NvAPI_GPU_GetGpuCoreCount(
            [In] PhysicalGPUHandle gpuHandle,
            [Out] out uint count);

        [FunctionId(FunctionId.NvAPI_GPU_GetGPUType)]
        public delegate Status NvAPI_GPU_GetGPUType(
            [In] PhysicalGPUHandle physicalGpu,
            [Out] out GPUType gpuType);

        [FunctionId(FunctionId.NvAPI_GPU_GetIRQ)]
        public delegate Status NvAPI_GPU_GetIRQ(
            [In] PhysicalGPUHandle physicalGpu,
            [Out] out uint gpuIRQ);

        [FunctionId(FunctionId.NvAPI_GPU_GetMemoryInfo)]
        public delegate Status NvAPI_GPU_GetMemoryInfo(
            [In] PhysicalGPUHandle physicalGpu,
            [In]
            [Accepts(typeof(DisplayDriverMemoryInfoV3), typeof(DisplayDriverMemoryInfoV2),
                typeof(DisplayDriverMemoryInfoV1))]
            ValueTypeReference memoryInfo);

        [FunctionId(FunctionId.NvAPI_GPU_GetOutputType)]
        public delegate Status NvAPI_GPU_GetOutputType(
            [In] PhysicalGPUHandle physicalGpu,
            [In] uint outputId,
            [Out] out OutputType outputType);

        [FunctionId(FunctionId.NvAPI_GPU_GetPCIIdentifiers)]
        public delegate Status NvAPI_GPU_GetPCIIdentifiers(
            [In] PhysicalGPUHandle physicalGpu,
            [Out] out uint deviceId,
            [Out] out uint subSystemId,
            [Out] out uint revisionId,
            [Out] out uint extDeviceId);

        [FunctionId(FunctionId.NvAPI_GPU_GetPhysicalFrameBufferSize)]
        public delegate Status NvAPI_GPU_GetPhysicalFrameBufferSize(
            [In] PhysicalGPUHandle physicalGpu,
            [Out] out uint size);

        [FunctionId(FunctionId.NvAPI_GPU_GetQuadroStatus)]
        public delegate Status NvAPI_GPU_GetQuadroStatus(
            [In] PhysicalGPUHandle physicalGpu,
            [Out] out uint isQuadro);

        [FunctionId(FunctionId.NvAPI_GPU_GetShaderSubPipeCount)]
        public delegate Status NvAPI_GPU_GetShaderSubPipeCount(
            [In] PhysicalGPUHandle gpuHandle,
            [Out] out uint count);

        [FunctionId(FunctionId.NvAPI_GPU_GetSystemType)]
        public delegate Status NvAPI_GPU_GetSystemType(
            [In] PhysicalGPUHandle physicalGpu,
            [Out] out SystemType systemType);

        [FunctionId(FunctionId.NvAPI_GPU_GetThermalSettings)]
        public delegate Status NvAPI_GPU_GetThermalSettings(
            [In] PhysicalGPUHandle physicalGpu,
            [In] ThermalSettingsTarget sensorIndex,
            [In] [Accepts(typeof(ThermalSettingsV2), typeof(ThermalSettingsV1))]
            ValueTypeReference thermalSettings);

        [FunctionId(FunctionId.NvAPI_GPU_GetVbiosOEMRevision)]
        public delegate Status NvAPI_GPU_GetVbiosOEMRevision(
            [In] PhysicalGPUHandle physicalGpu,
            [Out] out uint biosOEMRevision);

        [FunctionId(FunctionId.NvAPI_GPU_GetVbiosRevision)]
        public delegate Status NvAPI_GPU_GetVbiosRevision(
            [In] PhysicalGPUHandle physicalGpu,
            [Out] out uint biosRevision);

        [FunctionId(FunctionId.NvAPI_GPU_GetVbiosVersionString)]
        public delegate Status NvAPI_GPU_GetVbiosVersionString(
            [In] PhysicalGPUHandle physicalGpu,
            [Out] out ShortString biosVersion);

        [FunctionId(FunctionId.NvAPI_GPU_GetVirtualFrameBufferSize)]
        public delegate Status NvAPI_GPU_GetVirtualFrameBufferSize(
            [In] PhysicalGPUHandle physicalGpu,
            [Out] out uint size);

        [FunctionId(FunctionId.NvAPI_GPU_SetEDID)]
        public delegate Status NvAPI_GPU_SetEDID(
            [In] PhysicalGPUHandle physicalGpu,
            [In] uint outputId,
            [Accepts(typeof(EDIDV3), typeof(EDIDV2), typeof(EDIDV1))] [In]
            ValueTypeReference edid);

        [FunctionId(FunctionId.NvAPI_GPU_ValidateOutputCombination)]
        public delegate Status NvAPI_GPU_ValidateOutputCombination(
            [In] PhysicalGPUHandle physicalGpu,
            [In] OutputId outputMask);

        [FunctionId(FunctionId.NvAPI_SYS_GetDisplayIdFromGpuAndOutputId)]
        public delegate Status NvAPI_SYS_GetDisplayIdFromGpuAndOutputId(
            [In] PhysicalGPUHandle gpu,
            [In] OutputId outputId,
            [Out] out uint displayId);

        [FunctionId(FunctionId.NvAPI_SYS_GetGpuAndOutputIdFromDisplayId)]
        public delegate Status NvAPI_SYS_GetGpuAndOutputIdFromDisplayId(
            [In] uint displayId,
            [Out] out PhysicalGPUHandle gpu,
            [Out] out OutputId outputId);

        [FunctionId(FunctionId.NvAPI_SYS_GetPhysicalGpuFromDisplayId)]
        public delegate Status NvAPI_SYS_GetPhysicalGpuFromDisplayId(
            [In] uint displayId,
            [Out] out PhysicalGPUHandle gpu);
    }
}