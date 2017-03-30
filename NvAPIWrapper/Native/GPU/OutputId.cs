using System;

namespace NvAPIWrapper.Native.GPU
{
    [Flags]
    public enum OutputId : uint
    {
        None = 0,
        Output1 = 1u << 0,
        Output2 = 1u << 1,
        Output3 = 1u << 2,
        Output4 = 1u << 3,
        Output5 = 1u << 4,
        Output6 = 1u << 5,
        Output7 = 1u << 6,
        Output8 = 1u << 7,
        Output9 = 1u << 8,
        Output10 = 1u << 9,
        Output11 = 1u << 10,
        Output12 = 1u << 11,
        Output13 = 1u << 12,
        Output14 = 1u << 13,
        Output15 = 1u << 14,
        Output16 = 1u << 15,
        Output17 = 1u << 16,
        Output18 = 1u << 17,
        Output19 = 1u << 18,
        Output20 = 1u << 19,
        Output21 = 1u << 20,
        Output22 = 1u << 21,
        Output23 = 1u << 22,
        Output24 = 1u << 23,
        Output25 = 1u << 24,
        Output26 = 1u << 25,
        Output27 = 1u << 29,
        Output28 = 1u << 27,
        Output29 = 1u << 28,
        Output30 = 1u << 29,
        Output31 = 1u << 30,
        Output32 = 1u << 31
    }
}