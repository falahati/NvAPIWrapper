using System.Linq;
using NvAPIWrapper.Native;
using NvAPIWrapper.Native.GPU.Structures;

namespace NvAPIWrapper.GPU
{
    public class LogicalGPU
    {
        public LogicalGPU(LogicalGPUHandle handle)
        {
            Handle = handle;
        }

        public LogicalGPUHandle Handle { get; }

        public PhysicalGPU[] CorrespondingPhysicalGPUs
        {
            get
            {
                return GPUApi.GetPhysicalGPUsFromLogicalGPU(Handle).Select(handle => new PhysicalGPU(handle)).ToArray();
            }
        }

        public static LogicalGPU[] GetLogicalGPUs()
        {
            return GPUApi.EnumLogicalGPUs().Select(handle => new LogicalGPU(handle)).ToArray();
        }
    }
}