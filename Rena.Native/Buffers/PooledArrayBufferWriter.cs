using System.Buffers;
using System.Diagnostics;
using System.Runtime.CompilerServices;

namespace Rena.Native.Buffers;

public sealed class PooledArrayBufferWriter<T> : IBufferWriter<T>, IDisposable
{
    const int DefaultInitialSize = 64;

    private readonly ArrayPool<T> pool;
    private T[] buffer = Array.Empty<T>();
    private int index;

    public ReadOnlyMemory<T> WrittenMemory
        => this.buffer.AsMemory()[..index];

    public ReadOnlySpan<T> WrittenSpan
        => this.buffer.AsSpan()[..index];

    public int Capacity
        => this.buffer.Length;

    public int WrittenCount
        => this.index;

    public int FreeCapacity
        => this.buffer.Length - index;
    
    private Span<T> UnwrittenSpan
        => this.buffer.AsSpan()[index..];

    private Memory<T> UnwrittenMemory
        => this.buffer.AsMemory()[index..];

    public PooledArrayBufferWriter(ArrayPool<T> pool)
        => this.pool = pool;

    public PooledArrayBufferWriter(ArrayPool<T> pool, int initialCapacity)
    {
        Debug.Assert(initialCapacity >= 0, $"{nameof(initialCapacity)} must be a non negative number!");

        this.pool = pool;
        this.buffer = pool.Rent(initialCapacity);
    }

    public void Advance(int count)
    {
        Debug.Assert(count >= 0, $"{nameof(count)} must be a non negative number!");
        Debug.Assert(index + count <= buffer.Length, "You advanced too far!");

        this.index += count;
    }

    public Memory<T> GetMemory(int sizeHint = 0)
    {
        GrowIfNeeded(sizeHint);
        return UnwrittenMemory;
    }

    public Span<T> GetSpan(int sizeHint = 0)
    {
        GrowIfNeeded(sizeHint);
        return UnwrittenSpan;
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public PooledArray<T> Detach()
    {
        PooledArray<T> result = new(pool, this.buffer);
        this.index = 0;
        this.buffer = Array.Empty<T>();
        return result;
    }

    public void Reset()
    {
        if (this.buffer.Length == 0)
            return;

        this.pool.Return(this.buffer);
        this.buffer = Array.Empty<T>();
        this.index = 0;
    }

    public void Dispose()
        => Reset();

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    private void GrowIfNeeded(int sizeHint)
    {
        if (sizeHint == 0)
            sizeHint = 1;

        if (sizeHint <= FreeCapacity)
            return;

        T[] lastBuffer = this.buffer;
        int newSize = checked(int.Max(int.Max(lastBuffer.Length, DefaultInitialSize), sizeHint) * 2); // TODO: Check it ourselves
        T[] newBuffer = this.pool.Rent(newSize);
        
        lastBuffer.CopyTo(newBuffer.AsSpan());
        this.buffer = newBuffer;
        this.pool.Return(lastBuffer, RuntimeHelpers.IsReferenceOrContainsReferences<T>());
    }
}
