using System.Linq;
using NvAPIWrapper.GPU;
using NvAPIWrapper.Native;
using NvAPIWrapper.Native.Display;
using NvAPIWrapper.Native.Display.Structures;
using NvAPIWrapper.Native.GPU;

namespace NvAPIWrapper.Display
{
    public class Display
    {
        public Display(DisplayHandle handle)
        {
            Handle = handle;
        }

        public Display(string displayName)
        {
            Handle = DisplayApi.GetAssociatedNvidiaDisplayHandle(displayName);
        }

        public DisplayHandle Handle { get; }

        public string DisplayName => DisplayApi.GetAssociatedNvidiaDisplayName(Handle);

        public DisplayDevice DisplayDevice => new DisplayDevice(DisplayName);

        public OutputId OutputId => DisplayApi.GetAssociatedDisplayOutputId(Handle);

        public LogicalGPU LogicalGPU => new LogicalGPU(GPUApi.GetLogicalGPUFromDisplay(Handle));

        public PhysicalGPU[] PhysicalGPUs
            => GPUApi.GetPhysicalGPUsFromDisplay(Handle).Select(handle => new PhysicalGPU(handle)).ToArray();

        public TargetViewMode[] GetSupportedViews()
        {
            return DisplayApi.GetSupportedViews(Handle);
        }

        public static Display[] GetDisplays()
        {
            return DisplayApi.EnumNvidiaDisplayHandle().Select(handle => new Display(handle)).ToArray();
        }
    }
}