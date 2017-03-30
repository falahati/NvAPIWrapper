using System;

namespace NvAPIWrapper.Native.Exceptions
{
    internal class NVIDIANotSupportedException : NotSupportedException
    {
        public NVIDIANotSupportedException(string message) : base(message)
        {
        }
    }
}