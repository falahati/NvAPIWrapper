using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using NvAPIWrapper.Native.Interfaces;

namespace NvAPIWrapper.Native.Helpers.Structures
{
    [StructLayout(LayoutKind.Sequential)]
    internal struct ValueTypeArray<T> : IDisposable, IHandle where T : struct
    {
        private ValueTypeArray underlyingArray;

        public IntPtr MemoryAddress => underlyingArray.MemoryAddress;

        public static ValueTypeArray<T> Null => new ValueTypeArray<T>();
        public bool IsNull => underlyingArray.IsNull;

        public ValueTypeArray(IntPtr memoryAddress)
        {
            underlyingArray = new ValueTypeArray(memoryAddress);
        }

        private ValueTypeArray(ValueTypeArray underlyingArray)
        {
            this.underlyingArray = underlyingArray;
        }

        public static ValueTypeArray<T> FromArray(T[] array)
        {
            return new ValueTypeArray<T>(ValueTypeArray.FromArray(array));
        }

        public static ValueTypeArray<T> FromArray(IEnumerable<T> list, Type type)
        {
            return new ValueTypeArray<T>(ValueTypeArray.FromArray(list.Cast<object>(), type));
        }

        public T[] ToArray(int count)
        {
            return underlyingArray.ToArray<T>(count, typeof(T));
        }

        public T[] ToArray(int count, Type type)
        {
            return underlyingArray.ToArray<T>(0, count, type);
        }

        public T[] ToArray(int start, int count)
        {
            return underlyingArray.ToArray<T>(start, count, typeof(T)).ToArray();
        }

        public T[] ToArray(int start, int count, Type type)
        {
            return underlyingArray.ToArray<T>(start, count, type);
        }

        public IEnumerable<T> AsEnumerable(int count)
        {
            return underlyingArray.AsEnumerable<T>(count, typeof(T));
        }

        public IEnumerable<T> AsEnumerable(int count, Type type)
        {
            return underlyingArray.AsEnumerable<T>(0, count, type);
        }

        public IEnumerable<T> AsEnumerable(int start, int count)
        {
            return underlyingArray.AsEnumerable<T>(start, count, typeof(T));
        }

        public IEnumerable<T> AsEnumerable(int start, int count, Type type)
        {
            return underlyingArray.AsEnumerable<T>(start, count, type);
        }

        public void Dispose()
        {
            if (!IsNull)
            {
                underlyingArray.Dispose();
            }
        }
    }
}