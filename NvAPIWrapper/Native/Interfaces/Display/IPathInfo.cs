using System;
using System.Collections.Generic;
using NvAPIWrapper.Native.Display.Structures;

namespace NvAPIWrapper.Native.Interfaces.Display
{
    public interface IPathInfo : IDisposable
    {
        uint SourceId { get; }
        IEnumerable<IPathTargetInfo> TargetsInfo { get; }
        SourceModeInfo SourceModeInfo { get; }
    }
}