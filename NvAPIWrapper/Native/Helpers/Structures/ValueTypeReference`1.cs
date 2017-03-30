using System;
using System.Runtime.InteropServices;
using NvAPIWrapper.Native.Interfaces;

namespace NvAPIWrapper.Native.Helpers.Structures
{
    [StructLayout(LayoutKind.Sequential)]
    internal struct ValueTypeReference<T> : IDisposable, IHandle where T : struct
    {
        private ValueTypeReference underlyingReference;

        public IntPtr MemoryAddress => underlyingReference.MemoryAddress;

        public static ValueTypeReference<T> Null => new ValueTypeReference<T>();
        public bool IsNull => underlyingReference.IsNull;

        public ValueTypeReference(IntPtr memoryAddress)
        {
            underlyingReference = new ValueTypeReference(memoryAddress);
        }

        private ValueTypeReference(ValueTypeReference underlyingReference)
        {
            this.underlyingReference = underlyingReference;
        }

        public static ValueTypeReference<T> FromValueType(T valueType)
        {
            return new ValueTypeReference<T>(ValueTypeReference.FromValueType(valueType));
        }

        public static ValueTypeReference<T> FromValueType(object valueType, Type type)
        {
            return new ValueTypeReference<T>(ValueTypeReference.FromValueType(valueType, type));
        }

        public T ToValueType(Type type)
        {
            return underlyingReference.ToValueType<T>(type);
        }

        public T? ToValueType()
        {
            return underlyingReference.ToValueType<T>();
        }

        public void Dispose()
        {
            if (!IsNull)
            {
                underlyingReference.Dispose();
            }
        }
    }
}