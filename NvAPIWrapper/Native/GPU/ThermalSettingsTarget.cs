namespace NvAPIWrapper.Native.GPU
{
    /// <summary>
    ///     List of possible thermal targets
    /// </summary>
    public enum ThermalSettingsTarget
    {
        None = 0,

        /// <summary>
        ///     GPU core temperature requires
        /// </summary>
        GPU = 1,

        /// <summary>
        ///     GPU memory temperature
        /// </summary>
        Memory = 2,

        /// <summary>
        ///     GPU power supply temperature
        /// </summary>
        PowerSupply = 4,

        /// <summary>
        ///     GPU board ambient temperature
        /// </summary>
        Board = 8,

        /// <summary>
        ///     Visual Computing Device Board temperature requires NvVisualComputingDeviceHandle
        /// </summary>
        VisualComputingBoard = 9,

        /// <summary>
        ///     Visual Computing Device Inlet temperature requires NvVisualComputingDeviceHandle
        /// </summary>
        VisualComputingInlet = 10,

        /// <summary>
        ///     Visual Computing Device Outlet temperature requires NvVisualComputingDeviceHandle
        /// </summary>
        VisualComputingOutlet = 11,

        /// <summary>
        ///     Used for retrieving all termal settings
        /// </summary>
        All = 15,

        /// <summary>
        ///     Unknown thermal target
        /// </summary>
        Unknown = -1
    }
}