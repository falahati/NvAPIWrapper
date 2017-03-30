using System;

namespace NvAPIWrapper.Native.Interfaces
{
    internal interface IAllocatable : IInitializable, IDisposable
    {
        void Allocate();
    }
}