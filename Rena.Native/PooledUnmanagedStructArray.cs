using System.Buffers;
using System.Diagnostics;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;

namespace Rena.Native;

public readonly struct PooledUnmanagedStructArray<T>
    where T : unmanaged
{
    private readonly byte[] data;
    private readonly int length;

    public Span<T> Data
        => MemoryMarshal.CreateSpan(ref Unsafe.As<byte, T>(ref data[0]), length);

    public PooledUnmanagedStructArray(ArrayPool<byte> pool, int length)
    {
        Debug.Assert(length > 0);
        this.length = length;

        var byteLength = checked(length * Unsafe.SizeOf<T>());
        data = pool.Rent(byteLength);
    }

    public void Return(ArrayPool<byte> pool)
        => pool.Return(data);
}