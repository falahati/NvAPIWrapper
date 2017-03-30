using System;

namespace NvAPIWrapper.Native.Interfaces
{
    public interface IHandle
    {
        IntPtr MemoryAddress { get; }
        bool IsNull { get; }
    }
}