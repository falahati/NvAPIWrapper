namespace NvAPIWrapper.GPU
{
    public class VideoBIOS
    {
        internal VideoBIOS(uint revision, uint oemRevision, string versionString)
        {
            Revision = revision;
            OEMRevision = oemRevision;
            VersionString = versionString;
        }

        public uint Revision { get; }
        public uint OEMRevision { get; }
        public string VersionString { get; }
    }
}