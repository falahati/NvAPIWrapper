using System;
using System.Collections.Generic;
using System.Linq;
using NvAPIWrapper.Display;
using NvAPIWrapper.Native;
using NvAPIWrapper.Native.Exceptions;
using NvAPIWrapper.Native.General;
using NvAPIWrapper.Native.GPU;
using NvAPIWrapper.Native.GPU.Structures;
using NvAPIWrapper.Native.Interfaces.GPU;

namespace NvAPIWrapper.GPU
{
    /// <summary>
    ///     Represents a physical NVIDIA GPU
    /// </summary>
    public class PhysicalGPU : IEquatable<PhysicalGPU>
    {
        /// <summary>
        ///     Creates a new PhysicalGPU
        /// </summary>
        /// <param name="handle">Physical GPU handle</param>
        public PhysicalGPU(PhysicalGPUHandle handle)
        {
            Handle = handle;
        }

        /// <summary>
        ///     Gets all active outputs of this GPU
        /// </summary>
        public GPUOutput[] ActiveOutputs
        {
            get
            {
                var outputs = new List<GPUOutput>();
                var allOutputs = GPUApi.GetActiveOutputs(Handle);

                foreach (OutputId outputId in Enum.GetValues(typeof(OutputId)))
                {
                    if (outputId != OutputId.Invalid && allOutputs.HasFlag(outputId))
                    {
                        outputs.Add(new GPUOutput(outputId, this));
                    }
                }

                return outputs.ToArray();
            }
        }

        /// <summary>
        ///     Gets accelerated graphics port information
        /// </summary>
        public AGPInformation AGPInformation
        {
            get => new AGPInformation(
                GPUApi.GetAGPAperture(Handle),
                GPUApi.GetCurrentAGPRate(Handle)
            );
        }

        /// <summary>
        ///     Gets GPU base clock frequencies
        /// </summary>
        public IClockFrequencies BaseClockFrequencies
        {
            get => GPUApi.GetAllClockFrequencies(Handle, new ClockFrequenciesV2(ClockType.BaseClock));
        }

        /// <summary>
        ///     Gets GPU video BIOS information
        /// </summary>
        public VideoBIOS Bios
        {
            get => new VideoBIOS(
                GPUApi.GetVBIOSRevision(Handle),
                (int) GPUApi.GetVBIOSOEMRevision(Handle),
                GPUApi.GetVBIOSVersionString(Handle)
            );
        }

        /// <summary>
        ///     Gets the board information
        /// </summary>
        public BoardInfo Board
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

        /// <summary>
        ///     Gets GPU boost clock frequencies
        /// </summary>
        public IClockFrequencies BoostClockFrequencies
        {
            get => GPUApi.GetAllClockFrequencies(Handle, new ClockFrequenciesV2(ClockType.BoostClock));
        }

        /// <summary>
        ///     Gets GPU bus information
        /// </summary>
        public GPUBus BusInfo
        {
            get => new GPUBus(
                GPUApi.GetBusId(Handle),
                GPUApi.GetBusSlotId(Handle),
                GPUApi.GetBusType(Handle)
            );
        }

        /// <summary>
        ///     Gets corresponding logical GPU
        /// </summary>
        public LogicalGPU CorrespondingLogicalGPU
        {
            get => new LogicalGPU(GPUApi.GetLogicalGPUFromPhysicalGPU(Handle));
        }

        /// <summary>
        ///     Gets total number of cores defined for this GPU, or zero for older architectures
        /// </summary>
        public int CUDACores
        {
            get => GPUApi.GetGPUCoreCount(Handle);
        }

        /// <summary>
        ///     Gets GPU current clock frequencies
        /// </summary>
        public IClockFrequencies CurrentClockFrequencies
        {
            get => GPUApi.GetAllClockFrequencies(Handle);
        }

        /// <summary>
        ///     Gets number of PCIE lanes being used for the PCIE interface downstream
        /// </summary>
        public int CurrentPCIEDownStreamWidth
        {
            get => GPUApi.GetCurrentPCIEDownStreamWidth(Handle);
        }

        /// <summary>
        ///     Gets GPU dynamic performance states information (utilization domains)
        /// </summary>
        public DynamicPerformanceStatesInfo DynamicPerformanceStatesInfo
        {
            get => GPUApi.GetDynamicPerformanceStatesInfoEx(Handle);
        }

        /// <summary>
        ///     Gets GPU full name
        /// </summary>
        public string FullName
        {
            get => GPUApi.GetFullName(Handle);
        }

        /// <summary>
        ///     Gets GPU type
        /// </summary>
        public GPUType GPUType
        {
            get => GPUApi.GetGPUType(Handle);
        }

        /// <summary>
        ///     Gets the physical GPU handle
        /// </summary>
        public PhysicalGPUHandle Handle { get; }

        /// <summary>
        ///     Gets GPU interrupt number
        /// </summary>
        public int IRQ
        {
            get => GPUApi.GetIRQ(Handle);
        }

        /// <summary>
        ///     Gets a boolean value indicating the Quadro line of products
        /// </summary>
        public bool IsQuadro
        {
            get => GPUApi.GetQuadroStatus(Handle);
        }

        /// <summary>
        ///     Gets GPU memory information
        /// </summary>
        public IDisplayDriverMemoryInfo MemoryInfo
        {
            get => GPUApi.GetMemoryInfo(Handle);
        }

        /// <summary>
        ///     Gets the PCI identifiers
        /// </summary>
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

        /// <summary>
        ///     Gets GPU physical frame buffer size in KB. This does NOT include any system RAM that may be dedicated for use by
        ///     the GPU.
        /// </summary>
        public int PhysicalFrameBufferSize
        {
            get => GPUApi.GetPhysicalFrameBufferSize(Handle);
        }

        /// <summary>
        ///     Gets number of GPU Shader SubPipes or SM units
        /// </summary>
        public int ShaderSubPipeLines
        {
            get => GPUApi.GetShaderSubPipeCount(Handle);
        }

        /// <summary>
        ///     Gets GPU system type
        /// </summary>
        public SystemType SystemType
        {
            get => GPUApi.GetSystemType(Handle);
        }

        /// <summary>
        ///     Gets GPU thermal sensors information
        /// </summary>
        public IThermalSensor[] ThermalSensors
        {
            get => GPUApi.GetThermalSettings(Handle);
        }

        /// <summary>
        ///     Gets virtual size of framebuffer in KB for this GPU. This includes the physical RAM plus any system RAM that has
        ///     been dedicated for use by the GPU.
        /// </summary>
        public int VirtualFrameBufferSize
        {
            get => GPUApi.GetVirtualFrameBufferSize(Handle);
        }

        /// <inheritdoc />
        public bool Equals(PhysicalGPU other)
        {
            if (ReferenceEquals(null, other))
            {
                return false;
            }

            if (ReferenceEquals(this, other))
            {
                return true;
            }

            return Handle.Equals(other.Handle);
        }

        /// <summary>
        ///     Gets all physical GPUs
        /// </summary>
        /// <returns>An array of physical GPUs</returns>
        public static PhysicalGPU[] GetPhysicalGPUs()
        {
            return GPUApi.EnumPhysicalGPUs().Select(handle => new PhysicalGPU(handle)).ToArray();
        }

        /// <summary>
        ///     Gets all physical GPUs in TCC state
        /// </summary>
        /// <returns>An array of physical GPUs</returns>
        public static PhysicalGPU[] GetTCCPhysicalGPUs()
        {
            return GPUApi.EnumTCCPhysicalGPUs().Select(handle => new PhysicalGPU(handle)).ToArray();
        }

        /// <summary>
        ///     Checks for equality between two objects of same type
        /// </summary>
        /// <param name="left">The first object</param>
        /// <param name="right">The second object</param>
        /// <returns>true, if both objects are equal, otherwise false</returns>
        public static bool operator ==(PhysicalGPU left, PhysicalGPU right)
        {
            return right?.Equals(left) ?? ReferenceEquals(left, null);
        }

        /// <summary>
        ///     Checks for inequality between two objects of same type
        /// </summary>
        /// <param name="left">The first object</param>
        /// <param name="right">The second object</param>
        /// <returns>true, if both objects are not equal, otherwise false</returns>
        public static bool operator !=(PhysicalGPU left, PhysicalGPU right)
        {
            return !(left == right);
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

            return Equals((PhysicalGPU) obj);
        }

        /// <inheritdoc />
        public override int GetHashCode()
        {
            return Handle.GetHashCode();
        }

        /// <inheritdoc />
        public override string ToString()
        {
            return FullName;
        }

        /// <summary>
        ///     Get a list of all connected display devices on this GPU
        /// </summary>
        /// <param name="flags">ConnectedIdsFlag flag</param>
        /// <returns>An array of display devices</returns>
        public DisplayDevice[] GetConnectedDisplayDevices(ConnectedIdsFlag flags)
        {
            return GPUApi.GetConnectedDisplayIds(Handle, flags).Select(display => new DisplayDevice(display)).ToArray();
        }

        /// <summary>
        ///     Get the display device connected to a specific GPU output
        /// </summary>
        /// <param name="output">The GPU output to get connected display device for</param>
        /// <returns>DisplayDevice connected to the specified GPU output</returns>
        public DisplayDevice GetDisplayDeviceByOutput(GPUOutput output)
        {
            return new DisplayDevice(GPUApi.GetDisplayIdFromGPUAndOutputId(Handle, output.OutputId));
        }

        /// <summary>
        ///     Get a list of all display devices on any possible output
        /// </summary>
        /// <returns>An array of display devices</returns>
        public DisplayDevice[] GetDisplayDevices()
        {
            return GPUApi.GetAllDisplayIds(Handle).Select(display => new DisplayDevice(display)).ToArray();
        }

        /// <summary>
        ///     Reads EDID data of an output
        /// </summary>
        /// <param name="output">The GPU output to read EDID information for</param>
        /// <returns>A byte array containing EDID data</returns>
        public byte[] ReadEDIDData(GPUOutput output)
        {
            try
            {
                var data = new byte[0];
                var identification = 0;
                var totalSize = EDIDV3.MaxDataSize;

                for (var offset = 0; offset < totalSize; offset += EDIDV3.MaxDataSize)
                {
                    var edid = GPUApi.GetEDID(Handle, output.OutputId, offset, identification);
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
                if (ex.Status == Status.IncompatibleStructureVersion)
                {
                    return GPUApi.GetEDID(Handle, output.OutputId).Data;
                }

                throw;
            }
        }

        /// <summary>
        ///     Validates a set of GPU outputs to check if they can be active simultaneously
        /// </summary>
        /// <param name="outputs">GPU outputs to check</param>
        /// <returns>true if all specified outputs can be active simultaneously, otherwise false</returns>
        public bool ValidateOutputCombination(GPUOutput[] outputs)
        {
            var gpuOutpudIds =
                outputs.Aggregate(OutputId.Invalid, (current, gpuOutput) => current | gpuOutput.OutputId);

            return GPUApi.ValidateOutputCombination(Handle, gpuOutpudIds);
        }

        /// <summary>
        ///     Writes EDID data of an output
        /// </summary>
        /// <param name="output">The GPU output to write EDID information for</param>
        /// <param name="edidData">A byte array containing EDID data</param>
        public void WriteEDIDData(GPUOutput output, byte[] edidData)
        {
            WriteEDIDData((uint) output.OutputId, edidData);
        }

        /// <summary>
        ///     Writes EDID data of an display
        /// </summary>
        /// <param name="display">The display device to write EDID information for</param>
        /// <param name="edidData">A byte array containing EDID data</param>
        public void WriteEDIDData(DisplayDevice display, byte[] edidData)
        {
            WriteEDIDData(display.DisplayId, edidData);
        }

        private void WriteEDIDData(uint displayOutputId, byte[] edidData)
        {
            try
            {
                for (var offset = 0; offset < edidData.Length; offset += EDIDV3.MaxDataSize)
                {
                    var array = new byte[Math.Min(EDIDV3.MaxDataSize, edidData.Length - offset)];
                    Array.Copy(edidData, offset, array, 0, array.Length);
                    var instance = EDIDV3.CreateWithData(0, (uint) offset, array, edidData.Length);
                    GPUApi.SetEDID(Handle, displayOutputId, instance);
                }

                return;
            }
            catch (NVIDIAApiException ex)
            {
                if (ex.Status != Status.IncompatibleStructureVersion)
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
                    GPUApi.SetEDID(Handle, displayOutputId, EDIDV2.CreateWithData(array, edidData.Length));
                }

                return;
            }
            catch (NVIDIAApiException ex)
            {
                if (ex.Status != Status.IncompatibleStructureVersion)
                {
                    throw;
                }
            }
            catch (NVIDIANotSupportedException)
            {
                // ignore
            }

            GPUApi.SetEDID(Handle, displayOutputId, EDIDV1.CreateWithData(edidData));
        }
    }
}