namespace NvAPIWrapper.Native.GPU
{
    /// <summary>
    ///     List of possible thermal sensor controllers
    /// </summary>
    public enum ThermalSettingsController
    {
        None = 0,
        GPU,
        ADM1032,
        MAX6649,
        MAX1617,
        LM99,
        LM89,
        LM64,
        ADT7473,
        SBMAX6649,
        VideoBiosEvent,
        OperatingSystem,
        Unknown = -1
    }
}