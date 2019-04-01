using System.Diagnostics.CodeAnalysis;

namespace NvAPIWrapper.Native.GPU
{
    [SuppressMessage("ReSharper", "IdentifierTypo")]
    public enum GPUMemoryMaker : uint
    {
        Unknown = 0,
        Samsung,
        Qimonda,
        Elpida,
        Etron,
        Nanya,
        Hynix,
        Mosel,
        Winbond,
        Elite,
        Micron,
    }
}