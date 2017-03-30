using NvAPIWrapper.Native.GPU;

namespace NvAPIWrapper.GPU
{
    public class GPUBus
    {
        internal GPUBus(int busId, int busSlot, GPUBusType busType)
        {
            BusId = busId;
            BusSlot = busSlot;
            BusType = busType;
        }

        public int BusId { get; }
        public int BusSlot { get; }
        public GPUBusType BusType { get; }
    }
}