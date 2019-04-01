namespace NvAPIWrapper.Native.GPU
{
    public enum ECCConfiguration : uint
    {
        NotSupported = 0,

        /// <summary>
        ///     Changes require a POST to take effect
        /// </summary>
        Deferred,

        /// <summary>
        ///     Changes can optionally be made to take effect immediately
        /// </summary>
        Immediate
    }
}