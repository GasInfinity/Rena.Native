using System.Diagnostics;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;

namespace Rena.Native;

public readonly struct UnmanagedStructArray<T>
    where T : unmanaged
{
    public readonly byte[] Data;
    public readonly int Length;

    public Span<T> Span
        => MemoryMarshal.CreateSpan(ref Unsafe.As<byte, T>(ref Data[0]), Length);

    public UnmanagedStructArray(byte[] data, int length)
    {
        Debug.Assert(data != null, "Data must be a valid reference to a byte array.");
        Debug.Assert(length >= 0, "Length must be a non negative value");

        Data = data;
        Length = length;
    }
}