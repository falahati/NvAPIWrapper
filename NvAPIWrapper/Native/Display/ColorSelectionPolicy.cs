namespace NvAPIWrapper.Native.Display
{
    public enum ColorSelectionPolicy
    {
        /// <summary>
        ///     app/nvcpl make decision to select the desire color format
        /// </summary>
        User = 0,

        /// <summary>
        ///     driver/ OS make decision to select the best color format
        /// </summary>
        BestQuality = 1,

        Default = BestQuality,

        Unknown = 0xFF
    }
}
