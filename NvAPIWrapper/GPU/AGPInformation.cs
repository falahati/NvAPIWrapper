namespace NvAPIWrapper.GPU
{
    public class AGPInformation
    {
        internal AGPInformation(int aperture, int currentRate)
        {
            Aperture = aperture;
            CurrentRate = currentRate;
        }

        public int Aperture { get; }
        public int CurrentRate { get; }
    }
}