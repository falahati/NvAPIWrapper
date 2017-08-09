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
    public struct ThermalSeonsorInfoV2 : IThermalSeonsorInfo
    {
        internal readonly ThermalControllerType _Controller;
        internal readonly int _DefaultMinTemp;
        internal readonly int _DefaultMaxTemp;
        internal readonly int _CurrentTemp;
        internal readonly ThermalControllerTargetType _Target;


        public ThermalControllerType Controller => _Controller;

        public int DefaultMinTemp => _DefaultMinTemp;

        public int DefaultMaxTemp => _DefaultMaxTemp;

        public int CurrentTemp => _CurrentTemp;

        public ThermalControllerTargetType Target => _Target;
    }
}
