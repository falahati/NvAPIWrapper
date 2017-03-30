using System;
using NvAPIWrapper.Native.General;

namespace NvAPIWrapper.Native.Exceptions
{
    internal class NVIDIAApiException : Exception
    {
        public NVIDIAApiException(Status status)
        {
            Status = status;
        }

        public Status Status { get; }

        public override string Message => GeneralApi.GetErrorMessage(Status) ?? Status.ToString();
    }
}