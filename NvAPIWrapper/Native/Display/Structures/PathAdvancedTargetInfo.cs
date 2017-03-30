using System.Runtime.InteropServices;
using NvAPIWrapper.Native.Attributes;
using NvAPIWrapper.Native.Exceptions;
using NvAPIWrapper.Native.General.Structures;
using NvAPIWrapper.Native.GPU;
using NvAPIWrapper.Native.Helpers;
using NvAPIWrapper.Native.Interfaces;

namespace NvAPIWrapper.Native.Display.Structures
{
    [StructLayout(LayoutKind.Sequential, Pack = 8)]
    [StructureVersion(1)]
    public struct PathAdvancedTargetInfo : IInitializable
    {
        internal StructureVersion _Version;
        internal Rotate _Rotation;
        internal Scaling _Scaling;
        internal uint _RefreshRateInMillihertz;
        internal uint _RawReserved;
        internal ConnectorType _ConnectorType;
        internal TVFormat _TVFormat;
        internal TimingOverride _TimingOverride;
        internal Timing _Timing;

        public PathAdvancedTargetInfo(Rotate rotation, Scaling scale, uint refreshRateInMillihertz = 0,
            TimingOverride timingOverride = TimingOverride.Current, bool isInterlaced = false,
            bool isClonePrimary = false, bool isClonePanAndScanTarget = false, bool disableVirtualModeSupport = false,
            bool isPreferredUnscaledTarget = false)
        {
            if (timingOverride == TimingOverride.Custom)
            {
                throw new NVIDIANotSupportedException("Custom timing is not supported yet.");
            }
            this = typeof(PathAdvancedTargetInfo).Instantiate<PathAdvancedTargetInfo>();
            _Rotation = rotation;
            _Scaling = scale;
            _RefreshRateInMillihertz = refreshRateInMillihertz;
            _TimingOverride = timingOverride;
            IsInterlaced = isInterlaced;
            IsClonePrimary = isClonePrimary;
            IsClonePanAndScanTarget = isClonePanAndScanTarget;
            DisableVirtualModeSupport = disableVirtualModeSupport;
            IsPreferredUnscaledTarget = isPreferredUnscaledTarget;
        }

        public PathAdvancedTargetInfo(Rotate rotation, Scaling scale, TVFormat tvFormat,
            ConnectorType connectorType, uint refreshRateInMillihertz = 0,
            TimingOverride timingOverride = TimingOverride.Current, bool isInterlaced = false,
            bool isClonePrimary = false, bool isClonePanAndScanTarget = false, bool disableVirtualModeSupport = false,
            bool isPreferredUnscaledTarget = false)
            : this(
                rotation, scale, refreshRateInMillihertz, timingOverride, isInterlaced, isClonePrimary,
                isClonePanAndScanTarget,
                disableVirtualModeSupport, isPreferredUnscaledTarget)
        {
            if (tvFormat == TVFormat.None)
            {
                throw new NVIDIANotSupportedException(
                    "This overload is for TV displays, use the other overload(s) if the display is not a TV.");
            }
            this = typeof(PathAdvancedTargetInfo).Instantiate<PathAdvancedTargetInfo>();
            _TVFormat = tvFormat;
            _ConnectorType = connectorType;
        }

        public Rotate Rotation => _Rotation;
        public Scaling Scaling => _Scaling;
        public uint RefreshRateInMillihertz => _RefreshRateInMillihertz;
        public ConnectorType ConnectorType => _ConnectorType;
        public TVFormat TVFormat => _TVFormat;
        public TimingOverride TimingOverride => _TimingOverride;
        public Timing Timing => _Timing;

        public bool IsInterlaced
        {
            get { return _RawReserved.GetBit(0); }
            private set { _RawReserved = _RawReserved.SetBit(0, value); }
        }

        public bool IsClonePrimary
        {
            get { return _RawReserved.GetBit(1); }
            private set { _RawReserved = _RawReserved.SetBit(1, value); }
        }

        public bool IsClonePanAndScanTarget
        {
            get { return _RawReserved.GetBit(2); }
            private set { _RawReserved = _RawReserved.SetBit(2, value); }
        }

        public bool DisableVirtualModeSupport
        {
            get { return _RawReserved.GetBit(3); }
            private set { _RawReserved = _RawReserved.SetBit(3, value); }
        }

        public bool IsPreferredUnscaledTarget
        {
            get { return _RawReserved.GetBit(4); }
            private set { _RawReserved = _RawReserved.SetBit(4, value); }
        }
    }
}