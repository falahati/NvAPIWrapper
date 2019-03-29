using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using NvAPIWrapper.Native.Helpers;

namespace NvAPIWrapper.Native.DRS.Structures
{
    [StructLayout(LayoutKind.Sequential, Pack = 8)]
    public struct DRSGPUSupport
    {
        internal uint _Flags;

        public bool SupportsGeForce
        {
            get => _Flags.GetBit(0);
            set => _Flags = _Flags.SetBit(0, value);
        }

        public bool SupportsQuadro
        {
            get => _Flags.GetBit(1);
            set => _Flags = _Flags.SetBit(1, value);
        }

        public bool SupportsNVS
        {
            get => _Flags.GetBit(2);
            set => _Flags = _Flags.SetBit(2, value);
        }

        /// <inheritdoc />
        public override string ToString()
        {
            var supportedGPUs = new List<string>();

            if (SupportsGeForce)
            {
                supportedGPUs.Add("GeForce");
            }

            if (SupportsQuadro)
            {
                supportedGPUs.Add("Quadro");
            }

            if (SupportsNVS)
            {
                supportedGPUs.Add("NVS");
            }

            if (supportedGPUs.Any())
            {
                return $"[{_Flags}] = {string.Join(", ", supportedGPUs)}";
            }

            return $"[{_Flags}]";
        }
    }
}