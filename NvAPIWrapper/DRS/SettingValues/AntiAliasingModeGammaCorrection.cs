namespace NvAPIWrapper.DRS.SettingValues
{
    public enum AntiAliasingModeGammaCorrection : uint
    {
        Mask = 0x3,

        Off = 0x0,

        OnIfFOS = 0x1,

        OnAlways = 0x2,

        Maximum = 0x2,

        Default = 0x0,

        DefaultTesla = 0x2,

        DefaultFermi = 0x2
    }
}