using System;

namespace NvAPIWrapper.GPU
{
    /// <summary>
    ///     Contains information about the GPU Video BIOS
    /// </summary>
    public struct VideoBIOS : IEquatable<VideoBIOS>
    {
        internal VideoBIOS(uint revision, int oemRevision, string versionString)
        {
            Revision = revision;
            OEMRevision = oemRevision;
            VersionString = versionString.ToUpper();
        }

        /// <summary>
        ///     Gets the revision of the video BIOS
        /// </summary>
        public uint Revision { get; }

        /// <summary>
        ///     Gets the the OEM revision of the video BIOS
        /// </summary>
        public int OEMRevision { get; }

        /// <summary>
        ///     Gets the full video BIOS version string
        /// </summary>
        public string VersionString { get; }

        /// <inheritdoc />
        public override string ToString()
        {
            return AsVersion().ToString();
        }

        /// <inheritdoc />
        public bool Equals(VideoBIOS other)
        {
            return Revision == other.Revision && OEMRevision == other.OEMRevision;
        }

        /// <inheritdoc />
        public override bool Equals(object obj)
        {
            if (ReferenceEquals(null, obj))
            {
                return false;
            }

            return obj is VideoBIOS && Equals((VideoBIOS) obj);
        }

        /// <inheritdoc />
        public override int GetHashCode()
        {
            unchecked
            {
                return ((int) Revision * 397) ^ OEMRevision;
            }
        }

        /// <summary>
        ///     Checks for equality between two objects of same type
        /// </summary>
        /// <param name="left">The first object</param>
        /// <param name="right">The second object</param>
        /// <returns>true, if both objects are equal, otherwise false</returns>
        public static bool operator ==(VideoBIOS left, VideoBIOS right)
        {
            return left.Equals(right);
        }

        /// <summary>
        ///     Checks for inequality between two objects of same type
        /// </summary>
        /// <param name="left">The first object</param>
        /// <param name="right">The second object</param>
        /// <returns>true, if both objects are not equal, otherwise false</returns>
        public static bool operator !=(VideoBIOS left, VideoBIOS right)
        {
            return !left.Equals(right);
        }

        /// <summary>
        ///     Returns the video BIOS version as a .Net Version object
        /// </summary>
        /// <returns>A Version object representing the video BIOS version</returns>
        public Version AsVersion()
        {
            return new Version(
                (int) ((Revision >> 28) + ((Revision << 4) >> 28) * 16), // 8 bit little endian
                (int) (((Revision << 8) >> 28) + ((Revision << 12) >> 28) * 16), // 8 bit little endian
                (int) ((Revision << 16) >> 16), // 16 bit big endian
                OEMRevision // 8 bit integer
            );
        }
    }
}