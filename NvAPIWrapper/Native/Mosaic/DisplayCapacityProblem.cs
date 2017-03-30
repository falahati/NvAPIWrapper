using System;

namespace NvAPIWrapper.Native.Mosaic
{
    [Flags]
    public enum DisplayCapacityProblem : uint
    {
        NoProblem = 0,
        DisplayOnInvalidGPU = 1,
        DisplayOnWrongConnector = 2,
        NoCommonTimings = 4,
        NoEDIDAvailable = 8,
        MismatchedOutputType = 16,
        NoDisplayConnected = 32,
        NoGPUTopology = 64,
        NotSupported = 128,
        NoSLIBridge = 256,
        ECCEnabled = 512,
        GPUTopologyNotSupported = 1024
    }
}