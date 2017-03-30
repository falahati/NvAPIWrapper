using NvAPIWrapper.Native;

namespace NvAPIWrapper
{
    // ReSharper disable once InconsistentNaming
    public static class NVIDIA
    {
        public static string InterfaceVersionString => GeneralApi.GetInterfaceVersionString();

        public static void Initialize()
        {
            GeneralApi.Initialize();
        }

        public static void Unload()
        {
            GeneralApi.Unload();
        }
    }
}