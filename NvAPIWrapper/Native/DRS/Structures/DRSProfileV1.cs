using System.Runtime.InteropServices;
using NvAPIWrapper.Native.Attributes;
using NvAPIWrapper.Native.General.Structures;
using NvAPIWrapper.Native.Helpers;
using NvAPIWrapper.Native.Interfaces;

namespace NvAPIWrapper.Native.DRS.Structures
{
    [StructLayout(LayoutKind.Sequential, Pack = 8)]
    [StructureVersion(1)]
    public struct DRSProfileV1 : IInitializable
    {
        internal StructureVersion _Version;
        internal UnicodeString _ProfileName;
        internal DRSGPUSupport _GPUSupport;
        internal uint _IsPredefined;
        internal uint _NumberOfApplications;
        internal uint _NumberOfSettings;

        public DRSProfileV1(string name, DRSGPUSupport gpuSupport)
        {
            this = typeof(DRSProfileV1).Instantiate<DRSProfileV1>();
            _ProfileName = new UnicodeString(name);
            _GPUSupport = gpuSupport;
        }

        public string Name
        {
            get => _ProfileName.Value;
        }

        public DRSGPUSupport GPUSupport
        {
            get => _GPUSupport;
            set => _GPUSupport = value;
        }

        public bool IsPredefined
        {
            get => _IsPredefined > 0;
        }

        public int NumberOfApplications
        {
            get => (int)_NumberOfApplications;
        }

        public int NumberOfSettings
        {
            get => (int)_NumberOfSettings;
        }
    }
}