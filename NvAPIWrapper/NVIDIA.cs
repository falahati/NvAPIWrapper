using NvAPIWrapper.Native;
using NvAPIWrapper.Native.General.Structures;
using NvAPIWrapper.Native.Interfaces.General;

namespace NvAPIWrapper
{
    /// <summary>
    ///     .Net friendly version of system and general functions of NVAPI library
    /// </summary>
    public static class NVIDIA
    {
        /// <summary>
        ///     Gets information about the system's chipset.
        /// </summary>
        public static IChipsetInfo ChipsetInfo => GeneralApi.GetChipsetInfo();

        /// <summary>
        ///     Gets NVIDIA driver branch version as string
        /// </summary>
        public static string DriverBranchVersion
        {
            get
            {
                string branchVersion;
                GeneralApi.GetDriverAndBranchVersion(out branchVersion);
                return branchVersion;
            }
        }

        /// <summary>
        ///     Gets NVIDIA driver version
        /// </summary>
        public static uint DriverVersion
        {
            get
            {
                string branchVersion;
                return GeneralApi.GetDriverAndBranchVersion(out branchVersion);
            }
        }

        /// <summary>
        ///     Gets NVAPI interface version as string
        /// </summary>
        public static string InterfaceVersionString => GeneralApi.GetInterfaceVersionString();

        /// <summary>
        ///     Gets the current lid and dock information.
        /// </summary>
        public static LidDockParameters LidAndDockParameters => GeneralApi.GetLidAndDockInfo();

        /// <summary>
        ///     Initializes the NvAPI library (if not already initialized) but always increments the ref-counter.
        /// </summary>
        public static void Initialize()
        {
            GeneralApi.Initialize();
        }

        /// <summary>
        ///     Decrements the ref-counter and when it reaches ZERO, unloads NVAPI library.
        /// </summary>
        public static void Unload()
        {
            GeneralApi.Unload();
        }
    }
}