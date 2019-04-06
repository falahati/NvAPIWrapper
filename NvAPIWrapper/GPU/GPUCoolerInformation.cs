using System.Collections.Generic;
using System.Linq;
using NvAPIWrapper.Native;
using NvAPIWrapper.Native.GPU;
using NvAPIWrapper.Native.GPU.Structures;

namespace NvAPIWrapper.GPU
{
    /// <summary>
    ///     Contains information about the GPU coolers and current fan speed
    /// </summary>
    public class GPUCoolerInformation
    {
        internal GPUCoolerInformation(PhysicalGPU physicalGPU)
        {
            PhysicalGPU = physicalGPU;

            // TODO: Add Support For Pascal Only Policy Table Method
            // TODO: GPUApi.GetCoolerPolicyTable & GPUApi.SetCoolerPolicyTable & GPUApi.RestoreCoolerPolicyTable
        }

        /// <summary>
        ///     Gets a list of all available coolers along with their current settings and status
        /// </summary>
        public IEnumerable<GPUCooler> Coolers
        {
            get => GPUApi.GetCoolerSettings(PhysicalGPU.Handle).CoolerSettings
                .Select((setting, i) => new GPUCooler(i, setting));
        }

        /// <summary>
        ///     Gets the GPU fan speed in revolutions per minute
        /// </summary>
        public int CurrentFanSpeedInRPM
        {
            get => (int) GPUApi.GetTachReading(PhysicalGPU.Handle);
        }

        /// <summary>
        ///     Gets the current fan speed in percentage if available
        /// </summary>
        public int CurrentFanSpeedLevel
        {
            get
            {
                try
                {
                    return (int) GPUApi.GetCurrentFanSpeedLevel(PhysicalGPU.Handle);
                }
                catch
                {
                    return Coolers.FirstOrDefault(cooler => cooler.Target == CoolerTarget.All)?.CurrentLevel ?? 0;
                }
            }
        }

        /// <summary>
        ///     Gets the physical GPU that this instance describes
        /// </summary>
        public PhysicalGPU PhysicalGPU { get; }

        /// <inheritdoc />
        public override string ToString()
        {
            return $"{CurrentFanSpeedInRPM} RPM ({CurrentFanSpeedLevel}%)";
        }

        /// <summary>
        ///     Resets one or more cooler settings to default.
        /// </summary>
        /// <param name="coolerIds">The cooler identification numbers (indexes) to reset their settings to default.</param>
        public void RestoreCoolerSettingsToDefault(params int[] coolerIds)
        {
            GPUApi.RestoreCoolerSettings(PhysicalGPU.Handle, coolerIds.Select(i => (uint) i).ToArray());
        }

        /// <summary>
        ///     Changes a cooler settings by modifying the policy and the current level
        /// </summary>
        /// <param name="coolerId">The cooler identification number (index) to change the settings.</param>
        /// <param name="policy">The new cooler policy.</param>
        /// <param name="newLevel">The new cooler level. Valid only if policy is set to manual.</param>
        public void SetCoolerSettings(int coolerId, CoolerPolicy policy, int newLevel)
        {
            GPUApi.SetCoolerLevels(
                PhysicalGPU.Handle,
                (uint) coolerId,
                new PrivateCoolerLevelsV1(new[]
                    {
                        new PrivateCoolerLevelsV1.CoolerLevel(policy, (uint) newLevel)
                    }
                ),
                1
            );
        }

        /// <summary>
        ///     Changes a cooler setting by modifying the policy
        /// </summary>
        /// <param name="coolerId">The cooler identification number (index) to change the settings.</param>
        /// <param name="policy">The new cooler policy.</param>
        public void SetCoolerSettings(int coolerId, CoolerPolicy policy)
        {
            GPUApi.SetCoolerLevels(
                PhysicalGPU.Handle,
                (uint) coolerId,
                new PrivateCoolerLevelsV1(new[]
                    {
                        new PrivateCoolerLevelsV1.CoolerLevel(policy)
                    }
                ),
                1
            );
        }

        /// <summary>
        ///     Changes a cooler settings by modifying the policy to manual and sets a new level
        /// </summary>
        /// <param name="coolerId">The cooler identification number (index) to change the settings.</param>
        /// <param name="newLevel">The new cooler level.</param>
        public void SetCoolerSettings(int coolerId, int newLevel)
        {
            SetCoolerSettings(coolerId, CoolerPolicy.Manual, newLevel);
        }
    }
}