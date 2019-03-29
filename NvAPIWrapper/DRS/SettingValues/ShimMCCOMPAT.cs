namespace NvAPIWrapper.DRS.SettingValues
{
    public enum ShimMCCOMPAT : uint
    {
        Integrated = 0x0,

        Enable = 0x1,

        UserEditable = 0x2,

        Mask = 0x3,

        VideoMask = 0x4,

        VaryingBit = 0x8,

        AutoSelect = 0x10,

        OverrideBit = 0x80000000,

        Default = 0x10
    }
}