namespace NvAPIWrapper.GPU
{
    public class PCIIdentifiers
    {
        internal PCIIdentifiers(uint deviceId, uint subSystemId, uint revisionId, int extendedDeviceId)
        {
            DeviceId = deviceId;
            SubSystemId = subSystemId;
            RevisionId = revisionId;
            ExtendedDeviceId = extendedDeviceId;
        }

        public uint DeviceId { get; }
        public uint SubSystemId { get; }
        public uint RevisionId { get; }
        public int ExtendedDeviceId { get; }
    }
}