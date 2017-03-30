using System;
using System.Collections.Generic;
using System.Linq;
using NvAPIWrapper.Native;
using NvAPIWrapper.Native.Exceptions;
using NvAPIWrapper.Native.General;
using NvAPIWrapper.Native.Interfaces.Mosaic;
using NvAPIWrapper.Native.Mosaic;
using NvAPIWrapper.Native.Mosaic.Structures;

namespace NvAPIWrapper.Mosaic
{
    [Obsolete("Using Mosaic API Phase 1, Please consider using TopologyGrid class on newer drivers", false)]
    public class Topology
    {
        public Topology(int width, int height, int bitsPerPixel, int frequency, Native.Mosaic.Topology topology,
            int overlapX, int overlapY)
        {
            Width = width;
            Height = height;
            BitsPerPixel = bitsPerPixel;
            Frequency = frequency;
            FrequencyInMillihertz = (uint) (Frequency*1000);
            TopologyMode = topology;
            OverlapX = overlapX;
            OverlapY = overlapY;
        }

        public Topology(int width, int height, int bitsPerPixel, int frequency, uint frequencyInMillihertz,
            Native.Mosaic.Topology topology, int overlapX, int overlapY)
            : this(width, height, bitsPerPixel, frequency, topology, overlapX, overlapY)
        {
            FrequencyInMillihertz = frequencyInMillihertz;
        }

        public int Width { get; }
        public int Height { get; }
        public int BitsPerPixel { get; }
        public int Frequency { get; }
        public uint FrequencyInMillihertz { get; }
        public Native.Mosaic.Topology TopologyMode { get; }
        public int OverlapX { get; }
        public int OverlapY { get; }

        public TopologyDetails[] Details
        {
            get
            {
                var brief = GetTopologyBrief();
                return
                    MosaicApi.GetTopologyGroup(brief)
                        .TopologyDetails.Select(detail => new TopologyDetails(detail))
                        .ToArray();
            }
        }

        public DisplaySettingsV1 GetDisplaySettingsV1()
        {
            return new DisplaySettingsV1(Width, Height, BitsPerPixel, Frequency);
        }

        public DisplaySettingsV2 GetDisplaySettingsV2()
        {
            return new DisplaySettingsV2(Width, Height, BitsPerPixel, Frequency, FrequencyInMillihertz);
        }

        public TopologyBrief GetTopologyBrief()
        {
            return new TopologyBrief(TopologyMode);
        }

        public void SetAsCurrentTopology(bool apply = false)
        {
            var brief = GetTopologyBrief();
            var displaySettingsV2 = GetDisplaySettingsV2();
            try
            {
                MosaicApi.SetCurrentTopology(brief, displaySettingsV2, OverlapX, OverlapY, apply);
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
            var displaySettingsV1 = GetDisplaySettingsV1();
            MosaicApi.SetCurrentTopology(brief, displaySettingsV1, OverlapX, OverlapY, apply);
        }

        private void GetOverlapLimits(out int minX, out int maxX, out int minY, out int maxY)
        {
            var brief = GetTopologyBrief();
            var displaySettingsV2 = GetDisplaySettingsV2();
            try
            {
                MosaicApi.GetOverlapLimits(brief, displaySettingsV2, out minX, out maxX, out minY, out maxY);
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
            var displaySettingsV1 = GetDisplaySettingsV1();
            MosaicApi.GetOverlapLimits(brief, displaySettingsV1, out minX, out maxX, out minY, out maxY);
        }

        public IEnumerable<int> GetOverlapXRange()
        {
            int minX;
            int maxX;
            int minY;
            int maxY;
            GetOverlapLimits(out minX, out maxX, out minY, out maxY);
            return Enumerable.Range(minX, maxX);
        }

        public IEnumerable<int> GetOverlapYRange()
        {
            int minX;
            int maxX;
            int minY;
            int maxY;
            GetOverlapLimits(out minX, out maxX, out minY, out maxY);
            return Enumerable.Range(minY, maxY);
        }

        public static void EnableCurrent()
        {
            MosaicApi.EnableCurrentTopology(true);
        }

        public static void DisableCurrent()
        {
            MosaicApi.EnableCurrentTopology(false);
        }

        public static bool IsCurrentTopologyEnabled()
        {
            TopologyBrief brief;
            IDisplaySettings displaySettings;
            int overlapX;
            int overlapY;
            MosaicApi.GetCurrentTopology(out brief, out displaySettings, out overlapX, out overlapY);
            return brief.IsEnable;
        }

        public static bool IsCurrentTopologyPossible()
        {
            TopologyBrief brief;
            IDisplaySettings displaySettings;
            int overlapX;
            int overlapY;
            MosaicApi.GetCurrentTopology(out brief, out displaySettings, out overlapX, out overlapY);
            return brief.IsPossible;
        }

        public static Topology GetCurrentTopology()
        {
            TopologyBrief brief;
            IDisplaySettings displaySettings;
            int overlapX;
            int overlapY;
            MosaicApi.GetCurrentTopology(out brief, out displaySettings, out overlapX, out overlapY);
            return new Topology(displaySettings.Width, displaySettings.Height, displaySettings.BitsPerPixel,
                displaySettings.Frequency, displaySettings.FrequencyInMillihertz, brief.Topology, overlapX, overlapY);
        }

        public static Native.Mosaic.Topology[] GetSupportedTopologyModes(TopologyType type = TopologyType.Basic)
        {
            return
                MosaicApi.GetSupportedTopologiesInfo(type)
                    .TopologyBriefs.Where(topologyBrief => topologyBrief.IsPossible)
                    .Select(topologyBrief => topologyBrief.Topology)
                    .ToArray();
        }

        public static IDisplaySettings[] GetSupportedTopologySettings(TopologyType type = TopologyType.Basic)
        {
            return MosaicApi.GetSupportedTopologiesInfo(type).DisplaySettings.ToArray();
        }
    }
}