namespace NvAPIWrapper.GPU
{
    /// <summary>
    ///     Contains information about the accelerated graphics port
    /// </summary>
    public struct AGPInformation
    {
        internal AGPInformation(int aperture, int currentRate)
        {
            Aperture = aperture;
            CurrentRate = currentRate;
        }

        /// <inheritdoc />
        public override string ToString()
        {
            return $"AGP Aperture: {Aperture}MB, Current Rate: {CurrentRate}x";
        }

        /// <summary>
        ///     Gets AGP aperture in megabytes
        /// </summary>
        public int Aperture { get; }

        /// <summary>
        ///     Gets current AGP Rate (0 = AGP not present, 1 = 1x, 2 = 2x, etc.)
        /// </summary>
        public int CurrentRate { get; }
    }
}