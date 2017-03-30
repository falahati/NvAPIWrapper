using System.Linq;
using NvAPIWrapper.GPU;
using NvAPIWrapper.Native;
using NvAPIWrapper.Native.Display.Structures;

namespace NvAPIWrapper.Display
{
    public class UnAttachedDisplay
    {
        public UnAttachedDisplay(UnAttachedDisplayHandle handle)
        {
            Handle = handle;
        }

        public UnAttachedDisplay(string displayName)
        {
            Handle = DisplayApi.GetAssociatedUnAttachedNvidiaDisplayHandle(displayName);
        }

        public UnAttachedDisplayHandle Handle { get; }

        public string Name => DisplayApi.GetUnAttachedAssociatedDisplayName(Handle);

        public PhysicalGPU PhysicalGPU => new PhysicalGPU(GPUApi.GetPhysicalGPUFromUnAttachedDisplay(Handle));

        public NvAPIWrapper.Display.Display CreateDisplay()
        {
            return new NvAPIWrapper.Display.Display(DisplayApi.CreateDisplayFromUnAttachedDisplay(Handle));
        }

        public static UnAttachedDisplay[] GetUnAttachedDisplays()
        {
            return
                DisplayApi.EnumNvidiaUnAttachedDisplayHandle().Select(handle => new UnAttachedDisplay(handle)).ToArray();
        }
    }
}