using System;
using System.Collections.Generic;
using System.Linq;
using NvAPIWrapper.Display;
using NvAPIWrapper.Native;
using NvAPIWrapper.Native.Exceptions;
using NvAPIWrapper.Native.General;
using NvAPIWrapper.Native.GPU;
using NvAPIWrapper.Native.GPU.Structures;

namespace NvAPIWrapper.GPU
{
    public class PhysicalGPU
    {
        public PhysicalGPU(PhysicalGPUHandle handle)
        {
            Handle = handle;
        }

        public PhysicalGPUHandle Handle { get; }

        public LogicalGPU CorrespondingLogicalGPU => new LogicalGPU(GPUApi.GetLogicalGPUFromPhysicalGPU(Handle));

        public BoardInfo BoardId
        {
            get
            {
                try
                {
                    return GPUApi.GetBoardInfo(Handle);
                }
                catch (NVIDIAApiException ex)
                {
                    if (ex.Status == Status.NotSupported)
                    {
                        return default(BoardInfo);
                    }
                    throw;
                }
            }
        }

        public PCIIdentifiers PCIIdentifiers
        {
            get
            {
                uint deviceId;
                uint subSystemId;
                uint revisionId;
                uint extDeviceId;
                GPUApi.GetPCIIdentifiers(Handle, out deviceId, out subSystemId, out revisionId, out extDeviceId);
                return new PCIIdentifiers(deviceId, subSystemId, revisionId, (int) extDeviceId);
            }
        }

        public GPUBus BusInfo => new GPUBus(
            GPUApi.GetBusId(Handle),
            GPUApi.GetBusSlotId(Handle),
            GPUApi.GetBusType(Handle)
        );

        public VideoBIOS Bios => new VideoBIOS(
            GPUApi.GetVBIOSRevision(Handle),
            GPUApi.GetVBIOSOEMRevision(Handle),
            GPUApi.GetVBIOSVersionString(Handle)
        );

        public AGPInformation AGPInformation => new AGPInformation(
            GPUApi.GetAGPAperture(Handle),
            GPUApi.GetCurrentAGPRate(Handle)
        );

        public int IRQ => GPUApi.GetIRQ(Handle);
        public SystemType SystemType => GPUApi.GetSystemType(Handle);
        public GPUType GPUType => GPUApi.GetGPUType(Handle);

        public int ShaderSubPipeLines => GPUApi.GetShaderSubPipeCount(Handle);
        public int PhysicalFrameBufferSize => GPUApi.GetPhysicalFrameBufferSize(Handle);
        public bool IsQuadro => GPUApi.GetQuadroStatus(Handle);
        public int CurrentPCIEDownStreamWidth => GPUApi.GetCurrentPCIEDownStreamWidth(Handle);
        public int CUDACores => GPUApi.GetGPUCoreCount(Handle);
        public int VirtualFrameBufferSize => GPUApi.GetVirtualFrameBufferSize(Handle);
        public string FullName => GPUApi.GetFullName(Handle);

        public GPUOutput[] Outputs
        {
            get
            {
                var outputs = new List<GPUOutput>();
                var allOutputs = GPUApi.GetActiveOutputs(Handle);
                foreach (OutputId outputId in Enum.GetValues(typeof(OutputId)))
                {
                    if ((outputId != OutputId.None) && allOutputs.HasFlag(outputId))
                    {
                        outputs.Add(new GPUOutput(
                            outputId,
                            GPUApi.GetOutputType(Handle, outputId)
                        ));
                    }
                }
                return outputs.ToArray();
            }
        }

        public byte[] ReadEDIDData(OutputId outputId)
        {
            try
            {
                var data = new byte[0];
                var identification = 0;
                var totalSize = EDIDV3.MaxDataSize;
                for (var offset = 0; offset < totalSize; offset += EDIDV3.MaxDataSize)
                {
                    var edid = GPUApi.GetEDID(Handle, outputId, offset, identification);
                    identification = edid.Identification;
                    totalSize = edid.TotalSize;

                    var edidData = edid.Data;
                    Array.Resize(ref data, data.Length + edidData.Length);
                    Array.Copy(edidData, 0, data, data.Length - edidData.Length, edidData.Length);
                }
                return data;
            }
            catch (NVIDIAApiException ex)
            {
                if (ex.Status == Status.IncompatibleStructVersion)
                {
                    return GPUApi.GetEDID(Handle, outputId).Data;
                }
                throw;
            }
        }

        public void WriteEDIDData(OutputId outputId, byte[] edidData, int identification = 0)
        {
            try
            {
                for (var offset = 0; offset < edidData.Length; offset += EDIDV3.MaxDataSize)
                {
                    var array = new byte[Math.Min(EDIDV3.MaxDataSize, edidData.Length - offset)];
                    Array.Copy(edidData, offset, array, 0, array.Length);
                    var instance = EDIDV3.CreateWithData((uint) identification, (uint) offset, array, edidData.Length);
                    GPUApi.SetEDID(Handle, outputId, instance);
                }
                return;
            }
            catch (NVIDIAApiException ex)
            {
                if (ex.Status != Status.IncompatibleStructVersion)
                {
                    throw;
                }
            }
            catch (NVIDIANotSupportedException)
            {
                // ignore
            }
            try
            {
                for (var offset = 0; offset < edidData.Length; offset += EDIDV2.MaxDataSize)
                {
                    var array = new byte[Math.Min(EDIDV2.MaxDataSize, edidData.Length - offset)];
                    Array.Copy(edidData, offset, array, 0, array.Length);
                    GPUApi.SetEDID(Handle, outputId, EDIDV2.CreateWithData(array, edidData.Length));
                }
                return;
            }
            catch (NVIDIAApiException ex)
            {
                if (ex.Status != Status.IncompatibleStructVersion)
                {
                    throw;
                }
            }
            catch (NVIDIANotSupportedException)
            {
                // ignore
            }
            GPUApi.SetEDID(Handle, outputId, EDIDV1.CreateWithData(edidData));
        }

        public DisplayDevice[] GetDisplayDevices()
        {
            return GPUApi.GetAllDisplayIds(Handle).Select(display => new DisplayDevice(display)).ToArray();
        }

        public DisplayDevice[] GetConnectedDisplayDevices(ConnectedIdsFlag flags)
        {
            return GPUApi.GetConnectedDisplayIds(Handle, flags).Select(display => new DisplayDevice(display)).ToArray();
        }

        public DisplayDevice GetDisplayDeviceByOutputId(OutputId outputId)
        {
            return new DisplayDevice(GPUApi.GetDisplayIdFromGpuAndOutputId(Handle, outputId));
        }

        public static PhysicalGPU[] GetTCCPhysicalGPUs()
        {
            return GPUApi.EnumTCCPhysicalGPUs().Select(handle => new PhysicalGPU(handle)).ToArray();
        }

        public static PhysicalGPU[] GetPhysicalGPUs()
        {
            return GPUApi.EnumPhysicalGPUs().Select(handle => new PhysicalGPU(handle)).ToArray();
        }
    }
}