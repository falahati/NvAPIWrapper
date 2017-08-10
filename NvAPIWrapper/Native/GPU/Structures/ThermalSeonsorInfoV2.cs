using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;
using NvAPIWrapper.Native.Attributes;
using NvAPIWrapper.Native.Interfaces.GPU;

namespace NvAPIWrapper.Native.GPU.Structures
{
    [StructLayout(LayoutKind.Sequential)]
    public struct ThermalSeonsorInfoV2 : IThermalSeonsorInfo
    {
        internal readonly ThermalSettingsControllerType _Controller;
        internal readonly int _DefaultMinTemp;
        internal readonly int _DefaultMaxTemp;
        internal readonly int _CurrentTemp;
        internal readonly ThermalSettingsTargetType _Target;
        
        public ThermalSettingsControllerType Controller => _Controller;

        public int DefaultMinTemp => _DefaultMinTemp;

        public int DefaultMaxTemp => _DefaultMaxTemp;

        public int CurrentTemp => _CurrentTemp;

        public ThermalSettingsTargetType Target => _Target;
    }
}
