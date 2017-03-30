using NvAPIWrapper.Native.Display;
using NvAPIWrapper.Native.Display.Structures;
using NvAPIWrapper.Native.Exceptions;
using NvAPIWrapper.Native.GPU;
using NvAPIWrapper.Native.Interfaces.Display;

namespace NvAPIWrapper.Display
{
    public class PathTargetInfo
    {
        private TimingOverride _timingOverride;

        public PathTargetInfo(IPathTargetInfo info)
        {
            DisplayDevice = new DisplayDevice(info.DisplayId);
            Rotation = info.Details.Rotation;
            Scaling = info.Details.Scaling;
            ConnectorType = info.Details.ConnectorType;
            TVFormat = info.Details.TVFormat;
            RefreshRateInMillihertz = info.Details.RefreshRateInMillihertz;
            TimingOverride = info.Details.TimingOverride;
            IsInterlaced = info.Details.IsInterlaced;
            IsClonePrimary = info.Details.IsClonePrimary;
            IsClonePanAndScanTarget = info.Details.IsClonePanAndScanTarget;
            DisableVirtualModeSupport = info.Details.DisableVirtualModeSupport;
            IsPreferredUnscaledTarget = info.Details.IsPreferredUnscaledTarget;
            if (info is PathTargetInfoV2)
            {
                WindowsCCDTargetId = ((PathTargetInfoV2) info).WindowsCCDTargetId;
            }
        }

        public PathTargetInfo(DisplayDevice device)
        {
            DisplayDevice = device;
        }

        public Rotate Rotation { get; set; }
        public Scaling Scaling { get; set; }
        public uint RefreshRateInMillihertz { get; set; }
        public ConnectorType ConnectorType { get; set; }
        public TVFormat TVFormat { get; set; }

        public TimingOverride TimingOverride
        {
            get { return _timingOverride; }
            set
            {
                if (value == TimingOverride.Custom)
                {
                    throw new NVIDIANotSupportedException("Custom timing is not supported yet.");
                }
                _timingOverride = value;
            }
        }

        public DisplayDevice DisplayDevice { get; }
        public uint WindowsCCDTargetId { get; }
        public bool IsInterlaced { get; set; }

        public bool IsClonePrimary { get; set; }

        public bool IsClonePanAndScanTarget { get; set; }

        public bool DisableVirtualModeSupport { get; set; }

        public bool IsPreferredUnscaledTarget { get; set; }

        public PathAdvancedTargetInfo GetPathAdvancedTargetInfo()
        {
            if (TVFormat == TVFormat.None)
            {
                return new PathAdvancedTargetInfo(Rotation, Scaling, RefreshRateInMillihertz, TimingOverride,
                    IsInterlaced, IsClonePrimary, IsClonePanAndScanTarget, DisableVirtualModeSupport,
                    IsPreferredUnscaledTarget);
            }
            return new PathAdvancedTargetInfo(Rotation, Scaling, TVFormat, ConnectorType, RefreshRateInMillihertz,
                TimingOverride, IsInterlaced, IsClonePrimary, IsClonePanAndScanTarget, DisableVirtualModeSupport,
                IsPreferredUnscaledTarget);
        }

        public PathTargetInfoV1 GetPathTargetInfoV1()
        {
            var pathAdvancedTargetInfo = GetPathAdvancedTargetInfo();
            return new PathTargetInfoV1(DisplayDevice.DisplayId, pathAdvancedTargetInfo);
        }

        public PathTargetInfoV2 GetPathTargetInfoV2()
        {
            var pathAdvancedTargetInfo = GetPathAdvancedTargetInfo();
            return new PathTargetInfoV2(DisplayDevice.DisplayId, WindowsCCDTargetId, pathAdvancedTargetInfo);
        }
    }
}