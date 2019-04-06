using NvAPIWrapper.Native.GPU;
using NvAPIWrapper.Native.GPU.Structures;

namespace NvAPIWrapper.GPU
{
    /// <summary>
    ///     Holds information regarding a GPU cooler entry
    /// </summary>
    public class GPUCooler
    {
        internal GPUCooler(int coolerId, PrivateCoolerSettingsV1.CoolerSetting coolerSetting)
        {
            CoolerId = coolerId;
            CurrentLevel = (int) coolerSetting.CurrentLevel;
            DefaultMinimumLevel = (int) coolerSetting.DefaultMinimumLevel;
            DefaultMaximumLevel = (int) coolerSetting.DefaultMaximumLevel;
            CurrentMinimumLevel = (int) coolerSetting.CurrentMinimumLevel;
            CurrentMaximumLevel = (int) coolerSetting.CurrentMaximumLevel;
            CoolerType = coolerSetting.CoolerType;
            CoolerController = coolerSetting.CoolerController;
            DefaultPolicy = coolerSetting.DefaultPolicy;
            CurrentPolicy = coolerSetting.CurrentPolicy;
            Target = coolerSetting.Target;
            ControlMode = coolerSetting.ControlMode;
        }

        /// <summary>
        ///     Gets the cooler control mode
        /// </summary>
        public CoolerControlMode ControlMode { get; }

        /// <summary>
        ///     Gets the cooler controller
        /// </summary>
        public CoolerController CoolerController { get; }

        /// <summary>
        ///     Gets the cooler identification number or index
        /// </summary>
        public int CoolerId { get; }

        /// <summary>
        ///     Gets the cooler type
        /// </summary>
        public CoolerType CoolerType { get; }

        /// <summary>
        ///     Gets the cooler current level in percentage
        /// </summary>
        public int CurrentLevel { get; }


        /// <summary>
        ///     Gets the cooler current maximum level in percentage
        /// </summary>
        public int CurrentMaximumLevel { get; }


        /// <summary>
        ///     Gets the cooler current minimum level in percentage
        /// </summary>
        public int CurrentMinimumLevel { get; }

        /// <summary>
        ///     Gets the cooler current policy
        /// </summary>
        public CoolerPolicy CurrentPolicy { get; }

        /// <summary>
        ///     Gets the cooler default maximum level in percentage
        /// </summary>
        public int DefaultMaximumLevel { get; }

        /// <summary>
        ///     Gets the cooler default minimum level in percentage
        /// </summary>
        public int DefaultMinimumLevel { get; }

        /// <summary>
        ///     Gets the cooler default policy
        /// </summary>
        public CoolerPolicy DefaultPolicy { get; }

        /// <summary>
        ///     Gets the cooler target
        /// </summary>
        public CoolerTarget Target { get; }

        /// <inheritdoc />
        public override string ToString()
        {
            return $"[{CoolerId} @ {CoolerController}] {Target}: {CurrentLevel}%";
        }
    }
}