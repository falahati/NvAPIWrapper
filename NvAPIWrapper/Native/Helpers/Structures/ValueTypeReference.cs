using System;
using System.Runtime.InteropServices;
using NvAPIWrapper.Native.Interfaces;

namespace NvAPIWrapper.Native.Helpers.Structures
{
    [StructLayout(LayoutKind.Sequential)]
    internal struct ValueTypeReference : IDisposable, IHandle, IEquatable<ValueTypeReference>
    {
        private readonly IntPtr memoryAddress;

        // ReSharper disable once ConvertToAutoProperty
        public IntPtr MemoryAddress => memoryAddress;

        public static ValueTypeReference Null => new ValueTypeReference();
        public bool IsNull => MemoryAddress == IntPtr.Zero;

        public ValueTypeReference(IntPtr memoryAddress)
        {
            this.memoryAddress = memoryAddress;
        }

        public static ValueTypeReference FromValueType<T>(T valueType) where T : struct
        {
            return FromValueType(valueType, typeof(T));
        }

        public static ValueTypeReference FromValueType(object valueType, Type type)
        {
            if (!type.IsValueType)
                throw new ArgumentException("Only Value Types are acceptable.", nameof(type));
            var memoryAddress = Marshal.AllocHGlobal(Marshal.SizeOf(type));
            if (memoryAddress != IntPtr.Zero)
            {
                var result = new ValueTypeReference(memoryAddress);
                Marshal.StructureToPtr(valueType, memoryAddress, false);
                return result;
            }
            return Null;
        }

        public bool Equals(ValueTypeReference other)
        {
            return memoryAddress.Equals(other.memoryAddress);
        }

        public override bool Equals(object obj)
        {
            if (ReferenceEquals(null, obj)) return false;
            return obj is ValueTypeReference && Equals((ValueTypeReference) obj);
        }

        public override int GetHashCode()
        {
            return memoryAddress.GetHashCode();
        }

        public static bool operator ==(ValueTypeReference left, ValueTypeReference right)
        {
            return left.Equals(right);
        }

        public static bool operator !=(ValueTypeReference left, ValueTypeReference right)
        {
            return !left.Equals(right);
        }

        public T ToValueType<T>(Type type)
        {
            if (MemoryAddress == IntPtr.Zero)
                return default(T);
            if (!type.IsValueType)
                throw new ArgumentException("Only Value Types are acceptable.", nameof(type));
            return (T) Marshal.PtrToStructure(MemoryAddress, type);
        }

        public T? ToValueType<T>() where T : struct
        {
            if (IsNull)
                return null;
            return ToValueType<T>(typeof(T));
        }

        public void Dispose()
        {
            if (!IsNull)
                Marshal.FreeHGlobal(MemoryAddress);
        }
    }
}