using System.Buffers;
using System.Diagnostics;
using System.Runtime.CompilerServices;

namespace Rena.Native.Extensions;

public static class ArrayPoolExtensions
{
    public static UnmanagedStructArray<T> Rent<T>(this ArrayPool<byte> pool, int length)
        where T : unmanaged
    {
        Debug.Assert(length >= 0, "Length must be a non negative value");
        return new(pool.Rent(checked(length * Unsafe.SizeOf<T>())), length);
    }

    public static void Return<T>(this ArrayPool<byte> pool, UnmanagedStructArray<T> array)
        where T : unmanaged
        => pool.Return(array.Data);
}