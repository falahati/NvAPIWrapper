using NvAPIWrapper.Native.GPU;

namespace NvAPIWrapper.GPU
{
    public class GPUOutput
    {
        internal GPUOutput(OutputId outputId, OutputType outputType)
        {
            OutputId = outputId;
            OutputType = outputType;
        }

        public OutputId OutputId { get; }
        public OutputType OutputType { get; }
    }
}