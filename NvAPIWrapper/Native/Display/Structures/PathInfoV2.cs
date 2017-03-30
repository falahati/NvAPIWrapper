using System;
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
    [StructureVersion(2)]
    public struct PathInfoV2 : IPathInfo, IInitializable, IAllocatable
    {
        internal StructureVersion _Version;
        internal uint _SourceId;
        internal uint _TargetInfoCount;
        internal ValueTypeArray<PathTargetInfoV2> _TargetsInfo;
        internal ValueTypeReference<SourceModeInfo> _SourceModeInfo;
        internal uint _RawReserved;
        internal ValueTypeReference<LUID> _OSAdapterLUID;

        public uint SourceId => _SourceId;

        public IEnumerable<IPathTargetInfo> TargetsInfo
            => _TargetsInfo.ToArray((int) _TargetInfoCount)?.Cast<IPathTargetInfo>() ?? new IPathTargetInfo[0];

        public SourceModeInfo SourceModeInfo => _SourceModeInfo.ToValueType() ?? default(SourceModeInfo);
        public bool IsNonNVIDIAAdapter => _RawReserved.GetBit(0);
        public LUID? OSAdapterLUID => _OSAdapterLUID.ToValueType();

        public PathInfoV2(PathTargetInfoV2[] targetInformations, SourceModeInfo sourceModeInfo, uint sourceId = 0)
        {
            this = typeof(PathInfoV2).Instantiate<PathInfoV2>();
            _TargetInfoCount = (uint) targetInformations.Length;
            _TargetsInfo = ValueTypeArray<PathTargetInfoV2>.FromArray(targetInformations);
            _SourceModeInfo = ValueTypeReference<SourceModeInfo>.FromValueType(sourceModeInfo);
            _SourceId = sourceId;
        }

        public PathInfoV2(PathTargetInfoV2[] targetInformations, uint sourceId = 0)
        {
            this = typeof(PathInfoV2).Instantiate<PathInfoV2>();
            _TargetInfoCount = (uint) targetInformations.Length;
            _TargetsInfo = ValueTypeArray<PathTargetInfoV2>.FromArray(targetInformations);
            _SourceModeInfo = ValueTypeReference<SourceModeInfo>.Null;
            _SourceId = sourceId;
        }

        public PathInfoV2(uint sourceId)
        {
            this = typeof(PathInfoV2).Instantiate<PathInfoV2>();
            _TargetInfoCount = 0;
            _TargetsInfo = ValueTypeArray<PathTargetInfoV2>.Null;
            _SourceModeInfo = ValueTypeReference<SourceModeInfo>.Null;
            _SourceId = sourceId;
        }

        public PathInfoV2(SourceModeInfo sourceModeInfo, uint sourceId)
        {
            this = typeof(PathInfoV2).Instantiate<PathInfoV2>();
            _TargetInfoCount = 0;
            _TargetsInfo = ValueTypeArray<PathTargetInfoV2>.Null;
            _SourceModeInfo = ValueTypeReference<SourceModeInfo>.FromValueType(sourceModeInfo);
            _SourceId = sourceId;
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
                var targetInfo = typeof(PathTargetInfoV2).Instantiate<PathTargetInfoV2>();
                var targetInfoList = targetInfo.Repeat((int) _TargetInfoCount).AllocateAll();
                _TargetsInfo = ValueTypeArray<PathTargetInfoV2>.FromArray(targetInfoList.ToArray());
            }
            if (_SourceModeInfo.IsNull)
            {
                var sourceModeInfo = typeof(SourceModeInfo).Instantiate<SourceModeInfo>();
                _SourceModeInfo = ValueTypeReference<SourceModeInfo>.FromValueType(sourceModeInfo);
            }
        }
    }
}