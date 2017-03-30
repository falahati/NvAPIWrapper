using System;
using System.Runtime.InteropServices;
using NvAPIWrapper.Native.Helpers;
using NvAPIWrapper.Native.Helpers.Structures;
using NvAPIWrapper.Native.Interfaces;
using NvAPIWrapper.Native.Interfaces.Display;

namespace NvAPIWrapper.Native.Display.Structures
{
    [StructLayout(LayoutKind.Sequential, Pack = 8)]
    public struct PathTargetInfoV1 : IPathTargetInfo, IInitializable, IDisposable, IAllocatable
    {
        internal uint _DisplayId;
        internal ValueTypeReference<PathAdvancedTargetInfo> _Details;
        public uint DisplayId => _DisplayId;
        public PathAdvancedTargetInfo Details => _Details.ToValueType() ?? default(PathAdvancedTargetInfo);
        public PathTargetInfoV1(uint displayId) : this()
        {
            _DisplayId = displayId;
        }

        public PathTargetInfoV1(uint displayId, PathAdvancedTargetInfo details) : this(displayId)
        {
            _Details = ValueTypeReference<PathAdvancedTargetInfo>.FromValueType(details);
        }

        public void Dispose()
        {
            _Details.Dispose();
        }

        void IAllocatable.Allocate()
        {
            if (_Details.IsNull)
            {
                var detail = typeof(PathAdvancedTargetInfo).Instantiate<PathAdvancedTargetInfo>();
                _Details = ValueTypeReference<PathAdvancedTargetInfo>.FromValueType(detail);
            }
        }
    }
}