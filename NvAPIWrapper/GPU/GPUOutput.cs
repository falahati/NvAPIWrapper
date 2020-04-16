using System;
using NvAPIWrapper.Display;
using NvAPIWrapper.Native;
using NvAPIWrapper.Native.GPU;
using NvAPIWrapper.Native.GPU.Structures;
using NvAPIWrapper.Native.Interfaces.Display;

namespace NvAPIWrapper.GPU
{
    /// <summary>
    ///     Represents a single GPU output
    /// </summary>
    public class GPUOutput : IEquatable<GPUOutput>
    {
        internal GPUOutput(OutputId outputId, PhysicalGPUHandle gpuHandle)
        {
            OutputId = outputId;
            OutputType = !gpuHandle.IsNull ? GPUApi.GetOutputType(gpuHandle, outputId) : OutputType.Unknown;
            PhysicalGPU = new PhysicalGPU(gpuHandle);
        }

        internal GPUOutput(OutputId outputId, PhysicalGPU gpu)
            : this(outputId, gpu?.Handle ?? PhysicalGPUHandle.DefaultHandle)
        {
            PhysicalGPU = gpu;
        }

        /// <summary>
        ///     Gets the corresponding Digital Vibrance Control information
        /// </summary>
        public DVCInformation DigitalVibranceControl
        {
            get => new DVCInformation(OutputId);
        }

        /// <summary>
        ///     Gets the output HDMI support information
        /// </summary>
        public IHDMISupportInfo HDMISupportInfo
        {
            get => DisplayApi.GetHDMISupportInfo(OutputId);
        }

        /// <summary>
        ///     Gets the corresponding HUE information
        /// </summary>
        public HUEInformation HUEControl
        {
            get => new HUEInformation(OutputId);
        }

        /// <summary>
        ///     Gets the output identification as a single bit unsigned integer
        /// </summary>
        public OutputId OutputId { get; }

        /// <summary>
        ///     Gets the output type
        /// </summary>
        public OutputType OutputType { get; }

        /// <summary>
        ///     Gets the corresponding physical GPU
        /// </summary>
        public PhysicalGPU PhysicalGPU { get; }

        /// <inheritdoc />
        public bool Equals(GPUOutput other)
        {
            if (ReferenceEquals(null, other))
            {
                return false;
            }

            if (ReferenceEquals(this, other))
            {
                return true;
            }

            return PhysicalGPU.Equals(other.PhysicalGPU) && OutputId == other.OutputId;
        }

        /// <summary>
        ///     Checks for equality between two objects of same type
        /// </summary>
        /// <param name="left">The first object</param>
        /// <param name="right">The second object</param>
        /// <returns>true, if both objects are equal, otherwise false</returns>
        public static bool operator ==(GPUOutput left, GPUOutput right)
        {
            return right?.Equals(left) ?? ReferenceEquals(left, null);
        }

        /// <summary>
        ///     Checks for inequality between two objects of same type
        /// </summary>
        /// <param name="left">The first object</param>
        /// <param name="right">The second object</param>
        /// <returns>true, if both objects are not equal, otherwise false</returns>
        public static bool operator !=(GPUOutput left, GPUOutput right)
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

            return Equals((GPUOutput) obj);
        }

        /// <inheritdoc />
        public override int GetHashCode()
        {
            unchecked
            {
                return ((PhysicalGPU != null ? PhysicalGPU.GetHashCode() : 0) * 397) ^ (int) OutputId;
            }
        }

        /// <inheritdoc />
        public override string ToString()
        {
            return $"{OutputId} {OutputType} @ {PhysicalGPU}";
        }

        /// <summary>
        ///     Overrides the refresh rate on this output.
        ///     The new refresh rate can be applied right away or deferred to be applied with the next OS
        ///     mode-set.
        ///     The override is good for only one mode-set (regardless whether it's deferred or immediate).
        /// </summary>
        /// <param name="refreshRate">The refresh rate to be applied.</param>
        /// <param name="isDeferred">
        ///     A boolean value indicating if the refresh rate override should be deferred to the next OS
        ///     mode-set.
        /// </param>
        public void OverrideRefreshRate(float refreshRate, bool isDeferred = false)
        {
            DisplayApi.SetRefreshRateOverride(OutputId, refreshRate, isDeferred);
        }
    }
}