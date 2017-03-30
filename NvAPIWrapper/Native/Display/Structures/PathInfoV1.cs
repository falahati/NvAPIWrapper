using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using NvAPIWrapper.Native.Attributes;
using NvAPIWrapper.Native.General.Structures;
using NvAPIWrapper.Native.Helpers;
using NvAPIWrapper.Native.Helpers.Structures;
using NvAPIWrapper.Native.Interfaces;
using NvAPIWrapper.Native.Interfaces.Display;

namespace NvAPIWrapper.Native.Display.Structures
{
    [StructLayout(LayoutKind.Sequential, Pack = 8)]
    [StructureVersion(1)]
    public struct PathInfoV1 : IPathInfo, IInitializable, IAllocatable
    {
        internal StructureVersion _Version;
        internal uint _ReservedSourceId;
        internal uint _TargetInfoCount;
        internal ValueTypeArray<PathTargetInfoV1> _TargetsInfo;
        internal ValueTypeReference<SourceModeInfo> _SourceModeInfo;

        public uint SourceId => _ReservedSourceId;

        public IEnumerable<IPathTargetInfo> TargetsInfo
            => _TargetsInfo.ToArray((int) _TargetInfoCount)?.Cast<IPathTargetInfo>() ?? new IPathTargetInfo[0];

        public SourceModeInfo SourceModeInfo => _SourceModeInfo.ToValueType() ?? default(SourceModeInfo);

        public PathInfoV1(PathTargetInfoV1[] targetInformations, SourceModeInfo sourceModeInfo, uint sourceId = 0)
        {
            this = typeof(PathInfoV1).Instantiate<PathInfoV1>();
            _TargetInfoCount = (uint) targetInformations.Length;
            _TargetsInfo = ValueTypeArray<PathTargetInfoV1>.FromArray(targetInformations);
            _SourceModeInfo = ValueTypeReference<SourceModeInfo>.FromValueType(sourceModeInfo);
            _ReservedSourceId = sourceId;
        }

        public PathInfoV1(PathTargetInfoV1[] targetInformations, uint sourceId = 0)
        {
            this = typeof(PathInfoV1).Instantiate<PathInfoV1>();
            _TargetInfoCount = (uint) targetInformations.Length;
            _TargetsInfo = ValueTypeArray<PathTargetInfoV1>.FromArray(targetInformations);
            _SourceModeInfo = ValueTypeReference<SourceModeInfo>.Null;
            _ReservedSourceId = sourceId;
        }

        public PathInfoV1(uint sourceId)
        {
            this = typeof(PathInfoV1).Instantiate<PathInfoV1>();
            _TargetInfoCount = 0;
            _TargetsInfo = ValueTypeArray<PathTargetInfoV1>.Null;
            _SourceModeInfo = ValueTypeReference<SourceModeInfo>.Null;
            _ReservedSourceId = sourceId;
        }

        public PathInfoV1(SourceModeInfo sourceModeInfo, uint sourceId)
        {
            this = typeof(PathInfoV1).Instantiate<PathInfoV1>();
            _TargetInfoCount = 0;
            _TargetsInfo = ValueTypeArray<PathTargetInfoV1>.Null;
            _SourceModeInfo = ValueTypeReference<SourceModeInfo>.FromValueType(sourceModeInfo);
            _ReservedSourceId = sourceId;
        }

        public void Dispose()
        {
            TargetsInfo.DisposeAll();
            _TargetsInfo.Dispose();
            _SourceModeInfo.Dispose();
        }

        void IAllocatable.Allocate()
        {
            if ((_TargetInfoCount > 0) && _TargetsInfo.IsNull)
            {
                var targetInfo = typeof(PathTargetInfoV1).Instantiate<PathTargetInfoV1>();
                var targetInfoList = targetInfo.Repeat((int) _TargetInfoCount).AllocateAll();
                _TargetsInfo = ValueTypeArray<PathTargetInfoV1>.FromArray(targetInfoList.ToArray());
            }
            if (_SourceModeInfo.IsNull)
            {
                var sourceModeInfo = typeof(SourceModeInfo).Instantiate<SourceModeInfo>();
                _SourceModeInfo = ValueTypeReference<SourceModeInfo>.FromValueType(sourceModeInfo);
            }
        }
    }
}