
namespace NvAPIWrapper.Native.Interfaces.DRS
{
    public interface IDRSApplication
    {
        bool IsPredefined { get; }
        string ApplicationName { get; }
        string FriendlyName { get; }
        string LauncherName { get; }
    }
}
