using System.Buffers;
using System.Diagnostics;
using System.Runtime.CompilerServices;

namespace Rena.Native.Buffers;

/// <summary>
/// Represents a pooled array that is rented from <see cref="ArrayPool{T}"/>.
/// </summary>
/// <typeparam name="T">The type of elements in the array.</typeparam>
public struct PooledArray<T> : IDisposable
{
    public readonly ArrayPool<T> Pool;
    public readonly T[] Array;

    /// <summary>
    /// Gets a value indicating whether the <see cref="PooledArray{T}"/> instance is default(<see cref="PooledArray{T}"/>)
    /// </summary>
    public readonly bool IsDefault
        => Pool == null || Array == null;

    /// <summary>
    /// Gets a reference to the element at the specified index in the array.
    /// </summary>
    /// <param name="index">The index of the element to access.</param>
    /// <returns>A reference to the element at the specified index.</returns>
    public readonly ref T this[int index]
        => ref Array[index];

    /// <summary>
    /// Gets the length of the array.
    /// </summary>
    public readonly int Length
        => Array.Length;

    /// <summary>
    /// Initializes a new instance of the <see cref="PooledArray{T}"/> struct, renting an array of the specified capacity from the pool.
    /// </summary>
    /// <param name="pool">The <see cref="ArrayPool{T}"/> from which to rent the array.</param>
    /// <param name="capacity">The desired capacity of the array.</param>
    public PooledArray(ArrayPool<T> pool, int capacity)
    {
        Debug.Assert(capacity >= 0, $"{nameof(capacity)} must be a non negative number");
        Pool = pool;
        Array = pool.Rent(capacity);
    }

    /// <summary>
    /// Initializes a new instance of the <see cref="PooledArray{T}"/> struct, representing the provided pooled array.
    /// </summary>
    /// <param name="pool">The <see cref="ArrayPool{T}"/> from which the array was rented.</param>
    /// <param name="pooledArray">The pooled array to represent.</param>
    public PooledArray(ArrayPool<T> pool, T[] pooledArray)
    {
        Pool = pool;
        Array = pooledArray;
    }

    /// <summary>
    /// Returns a <see cref="Span{T}"/> that represents the entire pooled array.
    /// </summary>
    /// <returns>A <see cref="Span{T}"/> that represents the entire pooled array.</returns>
    public readonly Span<T> AsSpan()
        => Array.AsSpan();

    /// <inheritdoc />
    public void Dispose()
    {
        Pool.Return(Array, RuntimeHelpers.IsReferenceOrContainsReferences<T>());
        this = default;
    }

    public static implicit operator Span<T>(PooledArray<T> array)
        => array.AsSpan();
}
