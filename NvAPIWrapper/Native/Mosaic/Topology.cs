namespace NvAPIWrapper.Native.Mosaic
{
    public enum Topology
    {
        None,
        // Basic
        // ReSharper disable once InconsistentNaming
        Basic_Begin,
        Basic_1X2 = Basic_Begin,
        Basic_2X1,
        Basic_1X3,
        Basic_3X1,
        Basic_1X4,
        Basic_4X1,
        Basic_2X2,
        Basic_2X3,
        Basic_2X4,
        Basic_3X2,
        Basic_4X2,
        Basic_1X5,
        Basic_1X6,
        Basic_7X1,
        // ReSharper disable once InconsistentNaming
        Basic_End = Basic_7X1 + 9,

        //Passive Stereo
        // ReSharper disable once InconsistentNaming
        PassiveStereo_Begin,
        PassiveStereo_1X2 = PassiveStereo_Begin,
        PassiveStereo_2X1,
        PassiveStereo_1X3,
        PassiveStereo_3X1,
        PassiveStereo_1X4,
        PassiveStereo_4X1,
        PassiveStereo_2X2,
        // ReSharper disable once InconsistentNaming
        PassiveStereo_End = PassiveStereo_2X2 + 4,
        Max
    }
}