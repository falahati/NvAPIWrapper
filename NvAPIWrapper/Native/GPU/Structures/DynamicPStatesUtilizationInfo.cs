using System.Runtime.InteropServices;

namespace NvAPIWrapper.Native.GPU.Structures
{
    /// <summary>
    ///     Holds information about the dynamic states (such as gpu utilization)
    /// </summary>
    [StructLayout(LayoutKind.Sequential)]
    public struct DynamicPStatesUtilizationInfo
    {
        internal readonly uint _IsPresent;
        internal readonly uint _Percentage;
    }
}
