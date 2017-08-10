using System.Runtime.InteropServices;

namespace NvAPIWrapper.Native.GPU.Structures
{
    /// <summary>
    ///     Dynamic PStates utilization info structure
    /// </summary>
    [StructLayout(LayoutKind.Sequential)]
    public struct DynamicPStatesUtilizationInfo
    {
        internal readonly uint _IsPresent;
        internal readonly uint _Percentage;

        public bool IsPresent => _IsPresent == 1;

        public uint Percentage => _Percentage;

        public override string ToString()
        {
            return $"IsPresent={_IsPresent}, Percentage={_Percentage}";
        }
    }
}
