using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;
using NvAPIWrapper.Native.Attributes;

namespace NvAPIWrapper.Native.GPU.Structures
{
    [StructLayout(LayoutKind.Sequential)]
    public struct ThermalSeonsorInfoV1 : IThermalSeonsorInfo
    {
        internal readonly ThermalSettingsControllerType _Controller;
        internal readonly uint _DefaultMinTemp;
        internal readonly uint _DefaultMaxTemp;
        internal readonly uint _CurrentTemp;
        internal readonly ThermalSettingsTargetType _Target;

        public ThermalSettingsControllerType Controller => _Controller;

        public int DefaultMinTemp => (int)_DefaultMinTemp;

        public int DefaultMaxTemp => (int)_DefaultMaxTemp;

        public int CurrentTemp => (int)_CurrentTemp;

        public ThermalSettingsTargetType Target => _Target;
    }
}
