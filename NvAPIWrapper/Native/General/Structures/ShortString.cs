using System.Runtime.InteropServices;

namespace NvAPIWrapper.Native.General.Structures
{
    [StructLayout(LayoutKind.Sequential)]
    internal struct ShortString
    {
        public const int ShortStringLength = 64;

        [MarshalAs(UnmanagedType.ByValTStr, SizeConst = ShortStringLength)]
        internal readonly string Value;

        public override string ToString()
        {
            return Value;
        }
    }
}