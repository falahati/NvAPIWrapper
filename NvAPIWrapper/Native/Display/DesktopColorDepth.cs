namespace NvAPIWrapper.Native.Display
{
    public enum DesktopColorDepth
    {
        /// <summary>
        ///     set if the current setting should be kept
        /// </summary>
        Default = 0x0,

        /// <summary>
        ///     8 bit int per color component (8 bit int alpha)
        /// </summary>
        BPC8 = 0x1,

        /// <summary>
        ///     10 bit int per color component (2 bit int alpha)
        /// </summary>
        BPC10 = 0x2,

        /// <summary>
        ///     16 bit float per color component (16 bit float alpha)
        /// </summary>
        BPC16Float = 0x3,

        /// <summary>
        ///     16 bit float per color component (16 bit float alpha) wide color gamut
        /// </summary>
        BPC16FloatWcg = 0x4,

        /// <summary>
        ///     16 bit float per color component (16 bit float alpha) HDR
        /// </summary>
        BPC16FloatHDR = 0x5,

        /// <summary>
        ///     must be set to highest enum value
        /// </summary>
        MaxValue = BPC16FloatHDR
    }
}
