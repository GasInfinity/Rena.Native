using System.Buffers;

namespace Rena.Native.Buffers.Extensions;

/// <summary>
/// Provides extension methods for the <see cref="ArrayPool{T}"/> class.
/// </summary>
public static class ArrayPoolExtensions
{
    /// <summary>
    /// Rents a pooled array from the specified <see cref="ArrayPool{T}"/>.
    /// </summary>
    /// <typeparam name="T">The type of elements in the array.</typeparam>
    /// <param name="pool">The <see cref="ArrayPool{T}"/> from which to rent the array.</param>
    /// <param name="length">The desired length of the array.</param>
    /// <returns>A <see cref="PooledArray{T}"/> instance representing the rented array.</returns>
    public static PooledArray<T> Rent<T>(this ArrayPool<T> pool, int length)
        => new(pool, length);
}