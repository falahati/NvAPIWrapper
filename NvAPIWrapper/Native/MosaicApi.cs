using System;
using System.Linq;
using NvAPIWrapper.Native.Exceptions;
using NvAPIWrapper.Native.General;
using NvAPIWrapper.Native.Helpers;
using NvAPIWrapper.Native.Helpers.Structures;
using NvAPIWrapper.Native.Interfaces.Mosaic;
using NvAPIWrapper.Native.Mosaic;
using NvAPIWrapper.Native.Mosaic.Structures;

namespace NvAPIWrapper.Native
{
    public static class MosaicApi
    {
        public static ISupportedTopologiesInfo GetSupportedTopologiesInfo(TopologyType topologyType)
        {
            var mosaicGetSupportedTopoInfo = DelegateFactory.Get<Delegates.Mosaic.NvAPI_Mosaic_GetSupportedTopoInfo>();
            foreach (var acceptType in mosaicGetSupportedTopoInfo.Accepts())
            {
                var instance = acceptType.Instantiate<ISupportedTopologiesInfo>();
                using (var supportedTopologiesInfoByRef = ValueTypeReference.FromValueType(instance, acceptType))
                {
                    var status = mosaicGetSupportedTopoInfo(supportedTopologiesInfoByRef, topologyType);
                    if (status == Status.IncompatibleStructVersion)
                    {
                        continue;
                    }
                    if (status != Status.Ok)
                    {
                        throw new NVIDIAApiException(status);
                    }
                    return supportedTopologiesInfoByRef.ToValueType<ISupportedTopologiesInfo>(acceptType);
                }
            }
            throw new NVIDIANotSupportedException("This operation is not supported.");
        }

        public static void EnableCurrentTopology(bool enable)
        {
            var status = DelegateFactory.Get<Delegates.Mosaic.NvAPI_Mosaic_EnableCurrentTopo>()((uint) (enable ? 1 : 0));
            if (status != Status.Ok)
            {
                throw new NVIDIAApiException(status);
            }
        }

        public static void GetCurrentTopology(out TopologyBrief topoBrief, out IDisplaySettings displaySettings,
            out int overlapX, out int overlapY)
        {
            var mosaicGetCurrentTopo = DelegateFactory.Get<Delegates.Mosaic.NvAPI_Mosaic_GetCurrentTopo>();
            topoBrief = typeof(TopologyBrief).Instantiate<TopologyBrief>();
            foreach (var acceptType in mosaicGetCurrentTopo.Accepts())
            {
                displaySettings = acceptType.Instantiate<IDisplaySettings>();
                using (var displaySettingsByRef = ValueTypeReference.FromValueType(displaySettings, acceptType))
                {
                    var status = mosaicGetCurrentTopo(ref topoBrief, displaySettingsByRef, out overlapX, out overlapY);
                    if (status == Status.IncompatibleStructVersion)
                    {
                        continue;
                    }
                    if (status != Status.Ok)
                    {
                        throw new NVIDIAApiException(status);
                    }
                    displaySettings = displaySettingsByRef.ToValueType<IDisplaySettings>(acceptType);
                    return;
                }
            }
            throw new NVIDIANotSupportedException("This operation is not supported.");
        }

        public static void GetOverlapLimits(TopologyBrief topoBrief, IDisplaySettings displaySettings,
            out int minOverlapX, out int maxOverlapX, out int minOverlapY, out int maxOverlapY)
        {
            var mosaicGetOverlapLimits = DelegateFactory.Get<Delegates.Mosaic.NvAPI_Mosaic_GetOverlapLimits>();
            if (!mosaicGetOverlapLimits.Accepts().Contains(displaySettings.GetType()))
            {
                throw new ArgumentException("Parameter type is not supported.", nameof(displaySettings));
            }
            using (
                var displaySettingsByRef = ValueTypeReference.FromValueType(displaySettings, displaySettings.GetType()))
            {
                var status = mosaicGetOverlapLimits(topoBrief, displaySettingsByRef, out minOverlapX, out maxOverlapX,
                    out minOverlapY, out maxOverlapY);
                if (status != Status.Ok)
                {
                    throw new NVIDIAApiException(status);
                }
            }
        }

        public static TopologyGroup GetTopologyGroup(TopologyBrief topoBrief)
        {
            var result = typeof(TopologyGroup).Instantiate<TopologyGroup>();
            var status = DelegateFactory.Get<Delegates.Mosaic.NvAPI_Mosaic_GetTopoGroup>()(topoBrief, ref result);
            if (status != Status.Ok)
            {
                throw new NVIDIAApiException(status);
            }
            return result;
        }

        public static void SetCurrentTopology(TopologyBrief topoBrief, IDisplaySettings displaySettings, int overlapX,
            int overlapY, bool enable)
        {
            var mosaicSetCurrentTopo = DelegateFactory.Get<Delegates.Mosaic.NvAPI_Mosaic_SetCurrentTopo>();
            if (!mosaicSetCurrentTopo.Accepts().Contains(displaySettings.GetType()))
            {
                throw new ArgumentException("Parameter type is not supported.", nameof(displaySettings));
            }
            using (
                var displaySettingsByRef = ValueTypeReference.FromValueType(displaySettings, displaySettings.GetType()))
            {
                var status = mosaicSetCurrentTopo(topoBrief, displaySettingsByRef, overlapX, overlapY,
                    (uint) (enable ? 1 : 0));
                if (status != Status.Ok)
                {
                    throw new NVIDIAApiException(status);
                }
            }
        }

        public static void SetDisplayGrids(IGridTopology[] gridTopologies,
            SetDisplayTopologyFlag flags = SetDisplayTopologyFlag.NoFlag)
        {
            using (var gridTopologiesByRef = ValueTypeArray.FromArray(gridTopologies.AsEnumerable()))
            {
                var status =
                    DelegateFactory.Get<Delegates.Mosaic.NvAPI_Mosaic_SetDisplayGrids>()(gridTopologiesByRef,
                        (uint) gridTopologies.Length,
                        flags);
                if (status != Status.Ok)
                {
                    throw new NVIDIAApiException(status);
                }
            }
        }

        public static DisplayTopologyStatus[] ValidateDisplayGrids(IGridTopology[] gridTopologies,
            SetDisplayTopologyFlag flags = SetDisplayTopologyFlag.NoFlag)
        {
            using (var gridTopologiesByRef = ValueTypeArray.FromArray(gridTopologies.AsEnumerable()))
            {
                var statuses =
                    typeof(DisplayTopologyStatus).Instantiate<DisplayTopologyStatus>().Repeat(gridTopologies.Length);
                var status =
                    DelegateFactory.Get<Delegates.Mosaic.NvAPI_Mosaic_ValidateDisplayGrids>()(flags, gridTopologiesByRef,
                        ref statuses, (uint) gridTopologies.Length);
                if (status != Status.Ok)
                {
                    throw new NVIDIAApiException(status);
                }
                return statuses;
            }
        }

        public static IDisplaySettings[] EnumDisplayModes(IGridTopology gridTopology)
        {
            var mosaicEnumDisplayModes = DelegateFactory.Get<Delegates.Mosaic.NvAPI_Mosaic_EnumDisplayModes>();
            using (var gridTopologyByRef = ValueTypeReference.FromValueType(gridTopology, gridTopology.GetType()))
            {
                var totalAvailable = 0u;
                var status = mosaicEnumDisplayModes(gridTopologyByRef, ValueTypeArray.Null, ref totalAvailable);
                if (status != Status.Ok)
                {
                    throw new NVIDIAApiException(status);
                }

                if (totalAvailable == 0)
                {
                    return new IDisplaySettings[0];
                }

                foreach (var acceptType in mosaicEnumDisplayModes.Accepts(2))
                {
                    var counts = totalAvailable;
                    var instance = acceptType.Instantiate<IDisplaySettings>();
                    using (
                        var displaySettingByRef =
                            ValueTypeArray.FromArray(instance.Repeat((int) counts).AsEnumerable()))
                    {
                        status = mosaicEnumDisplayModes(gridTopologyByRef, displaySettingByRef, ref counts);
                        if (status == Status.IncompatibleStructVersion)
                        {
                            continue;
                        }
                        if (status != Status.Ok)
                        {
                            throw new NVIDIAApiException(status);
                        }
                        return displaySettingByRef.ToArray<IDisplaySettings>((int) counts, acceptType);
                    }
                }
                throw new NVIDIANotSupportedException("This operation is not supported.");
            }
        }

        public static IGridTopology[] EnumDisplayGrids()
        {
            var mosaicEnumDisplayGrids =
                DelegateFactory.Get<Delegates.Mosaic.NvAPI_Mosaic_EnumDisplayGrids>();

            var totalAvailable = 0u;
            var status = mosaicEnumDisplayGrids(ValueTypeArray.Null, ref totalAvailable);
            if (status != Status.Ok)
            {
                throw new NVIDIAApiException(status);
            }

            if (totalAvailable == 0)
            {
                return new IGridTopology[0];
            }

            foreach (var acceptType in mosaicEnumDisplayGrids.Accepts())
            {
                var counts = totalAvailable;
                var instance = acceptType.Instantiate<IGridTopology>();
                using (
                    var gridTopologiesByRef = ValueTypeArray.FromArray(instance.Repeat((int) counts).AsEnumerable()))
                {
                    status = mosaicEnumDisplayGrids(gridTopologiesByRef, ref counts);
                    if (status == Status.IncompatibleStructVersion)
                    {
                        continue;
                    }
                    if (status != Status.Ok)
                    {
                        throw new NVIDIAApiException(status);
                    }
                    return gridTopologiesByRef.ToArray<IGridTopology>((int) counts, acceptType);
                }
            }
            throw new NVIDIANotSupportedException("This operation is not supported.");
        }
    }
}