using System.Linq;
using NvAPIWrapper.Native;
using NvAPIWrapper.Native.Display;
using NvAPIWrapper.Native.Display.Structures;
using NvAPIWrapper.Native.Exceptions;
using NvAPIWrapper.Native.General;
using NvAPIWrapper.Native.Helpers;
using NvAPIWrapper.Native.Interfaces.Display;

namespace NvAPIWrapper.Display
{
    public class PathInfo
    {
        public PathInfo(Resolution resolution, Format colorFormat, PathTargetInfo[] physicalDisplays)
        {
            Resolution = resolution;
            ColorFormat = colorFormat;
            TargetsInfo = physicalDisplays;
        }

        public PathInfo(IPathInfo info)
        {
            SourceId = info.SourceId;
            Resolution = info.SourceModeInfo.Resolution;
            ColorFormat = info.SourceModeInfo.ColorFormat;
            Position = info.SourceModeInfo.Position;
            SpanningOrientation = info.SourceModeInfo.SpanningOrientation;
            IsGDIPrimary = info.SourceModeInfo.IsGDIPrimary;
            IsSLIFocus = info.SourceModeInfo.IsSLIFocus;
            TargetsInfo =
                info.TargetsInfo.Select(targetInfo => new PathTargetInfo(targetInfo)).ToArray();
            if (info is PathInfoV2)
            {
                OSAdapterLUID = ((PathInfoV2)info).OSAdapterLUID;
            }
        }

        public LUID? OSAdapterLUID { get; }

        public Resolution Resolution { get; set; }
        public Format ColorFormat { get; set; }
        public Position Position { get; set; }
        public SpanningOrientation SpanningOrientation { get; set; }

        public bool IsGDIPrimary { get; set; }

        public bool IsSLIFocus { get; set; }
        public uint SourceId { get; set; }
        public PathTargetInfo[] TargetsInfo { get; }

        public SourceModeInfo GetSourceModeInfo()
        {
            return new SourceModeInfo(Resolution, ColorFormat, Position, SpanningOrientation, IsGDIPrimary, IsSLIFocus);
        }

        public PathTargetInfoV1[] GetPathTargetInfoV1s()
        {
            return TargetsInfo.Select(config => config.GetPathTargetInfoV1()).ToArray();
        }

        public PathTargetInfoV2[] GetPathTargetInfoV2s()
        {
            return TargetsInfo.Select(config => config.GetPathTargetInfoV2()).ToArray();
        }

        public PathInfoV1 GetPathInfoV1()
        {
            var sourceModeInfo = GetSourceModeInfo();
            var pathTargetInfoV1 = GetPathTargetInfoV1s();
            return new PathInfoV1(pathTargetInfoV1, sourceModeInfo, SourceId);
        }

        public PathInfoV2 GetPathInfoV2()
        {
            var sourceModeInfo = GetSourceModeInfo();
            var pathTargetInfoV2 = GetPathTargetInfoV2s();
            return new PathInfoV2(pathTargetInfoV2, sourceModeInfo, SourceId);
        }


        public static PathInfo[] GetDisplaysConfig()
        {
            var configs = DisplayApi.GetDisplayConfig();
            var logicalDisplays = configs.Select(info => new PathInfo(info)).ToArray();
            configs.DisposeAll();
            return logicalDisplays;
        }

        public static void SetDisplaysConfig(PathInfo[] pathInfos, DisplayConfigFlags flags)
        {
            try
            {
                var configsV2 = pathInfos.Select(config => config.GetPathInfoV2()).Cast<IPathInfo>().ToArray();
                DisplayApi.SetDisplayConfig(configsV2, flags);
                configsV2.DisposeAll();
            }
            catch (NVIDIAApiException ex)
            {
                if (ex.Status != Status.IncompatibleStructVersion)
                {
                    throw;
                }
            }
            catch (NVIDIANotSupportedException)
            {
                // ignore
            }
            var configsV1 = pathInfos.Select(config => config.GetPathInfoV1()).Cast<IPathInfo>().ToArray();
            DisplayApi.SetDisplayConfig(configsV1, flags);
            configsV1.DisposeAll();
        }
    }
}