using System;
using System.Runtime.InteropServices;
using NvAPIWrapper.Native.Interfaces;

namespace NvAPIWrapper.Native.Display.Structures
{
    [StructLayout(LayoutKind.Sequential)]
    public struct DisplayHandle : IHandle
    {
        internal IntPtr _MemoryAddress;
        public IntPtr MemoryAddress => _MemoryAddress;
        public bool IsNull => _MemoryAddress == IntPtr.Zero;
    }
}