namespace NvAPIWrapper.Native.Display
{
    public enum TVFormat : uint
    {
        None = 0,

        // ReSharper disable once InconsistentNaming
        SD_NTSCM = 0x00000001,
        // ReSharper disable once InconsistentNaming
        SD_NTSCJ = 0x00000002,
        // ReSharper disable once InconsistentNaming
        SD_PALM = 0x00000004,
        // ReSharper disable once InconsistentNaming
        SD_PALBDGH = 0x00000008,
        // ReSharper disable once InconsistentNaming
        SD_PALN = 0x00000010,
        // ReSharper disable once InconsistentNaming
        SD_PALNC = 0x00000020,
        // ReSharper disable once InconsistentNaming
        SD576i = 0x00000100,
        // ReSharper disable once InconsistentNaming
        SD480i = 0x00000200,
        // ReSharper disable once InconsistentNaming
        ED480p = 0x00000400,
        // ReSharper disable once InconsistentNaming
        ED576p = 0x00000800,
        // ReSharper disable once InconsistentNaming
        HD720p = 0x00001000,
        // ReSharper disable once InconsistentNaming
        HD1080i = 0x00002000,
        // ReSharper disable once InconsistentNaming
        HD1080p = 0x00004000,
        // ReSharper disable once InconsistentNaming
        HD720p50 = 0x00008000,
        // ReSharper disable once InconsistentNaming
        HD1080p24 = 0x00010000,
        // ReSharper disable once InconsistentNaming
        HD1080i50 = 0x00020000,
        // ReSharper disable once InconsistentNaming
        HD1080p50 = 0x00040000,
        // ReSharper disable once InconsistentNaming
        UHD4Kp30 = 0x00080000,
        // ReSharper disable once InconsistentNaming
        UHD4Kp30_3840 = UHD4Kp30,
        // ReSharper disable once InconsistentNaming
        UHD4Kp25 = 0x00100000,
        // ReSharper disable once InconsistentNaming
        UHD4Kp25_3840 = UHD4Kp25,
        // ReSharper disable once InconsistentNaming
        UHD4Kp24 = 0x00200000,
        // ReSharper disable once InconsistentNaming
        UHD4Kp24_3840 = UHD4Kp24,
        // ReSharper disable once InconsistentNaming
        UHD4Kp24_SMPTE = 0x00400000,
        // ReSharper disable once InconsistentNaming
        UHD4Kp50_3840 = 0x00800000,
        // ReSharper disable once InconsistentNaming
        UHD4Kp60_3840 = 0x00900000,
        // ReSharper disable once InconsistentNaming
        UHD4Kp30_4096 = 0x00A00000,
        // ReSharper disable once InconsistentNaming
        UHD4Kp25_4096 = 0x00B00000,
        // ReSharper disable once InconsistentNaming
        UHD4Kp24_4096 = 0x00C00000,
        // ReSharper disable once InconsistentNaming
        UHD4Kp50_4096 = 0x00D00000,
        // ReSharper disable once InconsistentNaming
        UHD4Kp60_4096 = 0x00E00000,

        SDOther = 0x01000000,
        EDOther = 0x02000000,
        HDOther = 0x04000000,

        Any = 0x80000000
    }
}