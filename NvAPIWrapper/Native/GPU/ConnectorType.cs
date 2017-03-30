namespace NvAPIWrapper.Native.GPU
{
    public enum ConnectorType : uint
    {
        VGA15Pin = 0x00000000,
        // ReSharper disable once InconsistentNaming
        TV_Composite = 0x00000010,
        // ReSharper disable once InconsistentNaming
        TV_SVideo = 0x00000011,
        // ReSharper disable once InconsistentNaming
        TV_HDTVComponent = 0x00000013,
        // ReSharper disable once InconsistentNaming
        TV_Scart = 0x00000014,
        // ReSharper disable once InconsistentNaming
        TV_CompositeScartOnEIAJ4120 = 0x00000016,
        // ReSharper disable once InconsistentNaming
        TV_HDTV_EIAJ4120 = 0x00000017,
        // ReSharper disable once InconsistentNaming
        PC_POD_HDTV_YPRPB = 0x00000018,
        // ReSharper disable once InconsistentNaming
        PC_POD_SVideo = 0x00000019,
        // ReSharper disable once InconsistentNaming
        PC_POD_Composite = 0x0000001A,
        DVI_I_TV_SVideo = 0x00000020,
        // ReSharper disable once InconsistentNaming
        DVI_I_TV_COMPOSITE = 0x00000021,
        // ReSharper disable once InconsistentNaming
        DVI_I = 0x00000030,
        // ReSharper disable once InconsistentNaming
        DVI_D = 0x00000031,
        ADC = 0x00000032,
        // ReSharper disable once InconsistentNaming
        LFH_DVI_I1 = 0x00000038,
        // ReSharper disable once InconsistentNaming
        LFH_DVI_I2 = 0x00000039,
        SPWG = 0x00000040,
        OEM = 0x00000041,
        DisplayPortExternal = 0x00000046,
        DisplayPortInternal = 0x00000047,
        DisplayPortMiniExternal = 0x00000048,
        // ReSharper disable once InconsistentNaming
        HDMI_A = 0x00000061,
        // ReSharper disable once InconsistentNaming
        HDMI_CMini = 0x00000063,
        LFHDisplayPort1 = 0x00000064,
        LFHDisplayport2 = 0x00000065,
        VirtualWFD = 0x00000070,
        Unknown = 0xFFFFFFFF
    }
}