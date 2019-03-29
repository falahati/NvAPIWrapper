namespace NvAPIWrapper.Native.Interfaces.DRS
{
    public interface IDRSApplication
    {
        string ApplicationName { get; }
        string FriendlyName { get; }
        bool IsPredefined { get; }
        string LauncherName { get; }
    }
}