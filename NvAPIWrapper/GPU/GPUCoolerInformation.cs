using System.Collections.Generic;
using System.Linq;
using NvAPIWrapper.Native;
using NvAPIWrapper.Native.Exceptions;
using NvAPIWrapper.Native.General;
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
            // TODO: Better support of ClientFanCoolers set of APIs
        }

        /// <summary>
        ///     Gets a list of all available coolers along with their current settings and status
        /// </summary>
        public IEnumerable<GPUCooler> Coolers
        {
            get
            {
                PrivateCoolerSettingsV1? settings = null;

                try
                {
                    settings = GPUApi.GetCoolerSettings(PhysicalGPU.Handle);
                }
                catch (NVIDIAApiException e)
                {
                    if (e.Status != Status.NotSupported)
                    {
                        throw;
                    }
                }

                if (settings != null)
                {
                    for (var i = 0; i < settings.Value.CoolerSettings.Length; i++)
                    {
                        yield return new GPUCooler(
                            i,
                            settings.Value.CoolerSettings[i],
                            i == 0 ? (int) GPUApi.GetTachReading(PhysicalGPU.Handle) : -1
                        );
                    }

                    yield break;
                }

                PrivateFanCoolersStatusV1? status = null;
                PrivateFanCoolersInfoV1? info = null;
                PrivateFanCoolersControlV1? control = null;

                try
                {
                    status = GPUApi.GetClientFanCoolersStatus(PhysicalGPU.Handle);
                    info = GPUApi.GetClientFanCoolersInfo(PhysicalGPU.Handle);
                    control = GPUApi.GetClientFanCoolersControl(PhysicalGPU.Handle);
                }
                catch (NVIDIAApiException e)
                {
                    if (e.Status != Status.NotSupported)
                    {
                        throw;
                    }
                }

                if (status != null && info != null && control != null)
                {
                    for (var i = 0; i < status.Value.FanCoolersStatusEntries.Length; i++)
                    {
                        if (info.Value.FanCoolersInfoEntries.Length > i &&
                            control.Value.FanCoolersControlEntries.Length > i)
                        {
                            yield return new GPUCooler(
                                info.Value.FanCoolersInfoEntries[i],
                                status.Value.FanCoolersStatusEntries[i],
                                control.Value.FanCoolersControlEntries[i]
                            );
                        }
                    }

                    yield break;
                }

                throw new NVIDIAApiException(Status.NotSupported);
            }
        }

        /// <summary>
        ///     Gets the GPU fan speed in revolutions per minute
        /// </summary>
        public int CurrentFanSpeedInRPM
        {
            get
            {
                try
                {
                    return (int) GPUApi.GetTachReading(PhysicalGPU.Handle);
                }
                catch
                {
                    return Coolers.FirstOrDefault(cooler => cooler.Target == CoolerTarget.All)?.CurrentFanSpeedInRPM ??
                           0;
                }
            }
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
            try
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

                return;
            }
            catch (NVIDIAApiException e)
            {
                if (e.Status != Status.NotSupported)
                {
                    throw;
                }
            }

            var currentControl = GPUApi.GetClientFanCoolersControl(PhysicalGPU.Handle);

            for (var i = 0; i < currentControl.FanCoolersControlEntries.Length; i++)
            {
                if (currentControl.FanCoolersControlEntries[i].CoolerId == coolerId)
                {
                    currentControl.FanCoolersControlEntries[i].ControlMode =
                        policy == CoolerPolicy.Manual ? FanCoolersControlMode.Manual : FanCoolersControlMode.Auto;
                    currentControl.FanCoolersControlEntries[i].Level =
                        policy == CoolerPolicy.Manual ? (uint) newLevel : 0u;

                    break;
                }
            }

            GPUApi.SetClientFanCoolersControl(PhysicalGPU.Handle, currentControl);
        }

        /// <summary>
        ///     Changes a cooler setting by modifying the policy
        /// </summary>
        /// <param name="coolerId">The cooler identification number (index) to change the settings.</param>
        /// <param name="policy">The new cooler policy.</param>
        public void SetCoolerSettings(int coolerId, CoolerPolicy policy)
        {
            try
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

                return;
            }
            catch (NVIDIAApiException e)
            {
                if (e.Status != Status.NotSupported)
                {
                    throw;
                }
            }

            var currentControl = GPUApi.GetClientFanCoolersControl(PhysicalGPU.Handle);

            for (var i = 0; i < currentControl.FanCoolersControlEntries.Length; i++)
            {
                if (currentControl.FanCoolersControlEntries[i].CoolerId == coolerId)
                {
                    currentControl.FanCoolersControlEntries[i].ControlMode =
                        policy == CoolerPolicy.Manual ? FanCoolersControlMode.Manual : FanCoolersControlMode.Auto;
                    currentControl.FanCoolersControlEntries[i].Level =
                        policy == CoolerPolicy.Manual ? 100u : 0u;

                    break;
                }
            }

            GPUApi.SetClientFanCoolersControl(PhysicalGPU.Handle, currentControl);
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