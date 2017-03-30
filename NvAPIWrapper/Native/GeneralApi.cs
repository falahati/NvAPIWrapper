using NvAPIWrapper.Native.Exceptions;
using NvAPIWrapper.Native.General;
using NvAPIWrapper.Native.General.Structures;
using NvAPIWrapper.Native.Helpers;
using NvAPIWrapper.Native.Helpers.Structures;
using NvAPIWrapper.Native.Interfaces.General;

namespace NvAPIWrapper.Native
{
    public static class GeneralApi
    {
        public static string GetInterfaceVersionString()
        {
            ShortString version;
            var status = DelegateFactory.Get<Delegates.General.NvAPI_GetInterfaceVersionString>()(out version);
            if (status != Status.Ok)
            {
                throw new NVIDIAApiException(status);
            }
            return version.Value;
        }

        public static string GetErrorMessage(Status status)
        {
            ShortString message;
            status = DelegateFactory.Get<Delegates.General.NvAPI_GetErrorMessage>()(status, out message);
            if (status != Status.Ok)
            {
                return null;
            }
            return message.Value;
        }

        public static LIDDockParameters GetLidAndDockInfo()
        {
            var dockinfo = typeof(LIDDockParameters).Instantiate<LIDDockParameters>();
            var status = DelegateFactory.Get<Delegates.General.NvAPI_SYS_GetLidAndDockInfo>()(ref dockinfo);
            if (status != Status.Ok)
            {
                throw new NVIDIAApiException(status);
            }
            return dockinfo;
        }

        public static void Initialize()
        {
            var status = DelegateFactory.Get<Delegates.General.NvAPI_Initialize>()();
            if (status != Status.Ok)
            {
                throw new NVIDIAApiException(status);
            }
        }

        public static void Unload()
        {
            var status = DelegateFactory.Get<Delegates.General.NvAPI_Unload>()();
            if (status != Status.Ok)
            {
                throw new NVIDIAApiException(status);
            }
        }

        public static IChipsetInfo GetChipsetInfo()
        {
            var getChipSetInfo = DelegateFactory.Get<Delegates.General.NvAPI_SYS_GetChipSetInfo>();
            foreach (var acceptType in getChipSetInfo.Accepts())
            {
                var instance = acceptType.Instantiate<IChipsetInfo>();
                using (var chipsetInfoReference = ValueTypeReference.FromValueType(instance, acceptType))
                {
                    var status = getChipSetInfo(chipsetInfoReference);
                    if (status == Status.IncompatibleStructVersion)
                    {
                        continue;
                    }
                    if (status != Status.Ok)
                    {
                        throw new NVIDIAApiException(status);
                    }
                    return chipsetInfoReference.ToValueType<IChipsetInfo>(acceptType);
                }
            }
            throw new NVIDIANotSupportedException("This operation is not supported.");
        }
    }
}