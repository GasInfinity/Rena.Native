using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;

namespace Rena.Native;

public unsafe readonly struct UnmanagedSpan<T> : ISpanFormattable
    where T : unmanaged
{
    public readonly T* Pointer;
    public readonly nuint Length;

    public bool IsEmpty
        => Pointer == null || Length == 0;

    public ref T this[nuint index]
        => ref Pointer[index];

    public UnmanagedSpan(T* pointer, nuint length)
    {
        Pointer = pointer;
        Length = length;
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public void Fill(T value)
    {
        if (Unsafe.SizeOf<T>() == 1)
        {
            NativeMemory.Fill(Pointer, Length, *(byte*)&value);
        }
        else
        {
            foreach (var span in GetChunks()) // Span is already VERY optimized for this
                span.Fill(value);
        }
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public void Clear()
        => NativeMemory.Clear(Pointer, Length);

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public bool TryCopyTo(UnmanagedSpan<T> other)
    {
        if (Length <= other.Length)
        {
            NativeMemory.Copy(Pointer, other.Pointer, Length);
            return true;
        }

        return false;
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public Span<T> SliceSpanUnsafe(nuint offset, int length)
        => MemoryMarshal.CreateSpan(ref Pointer[offset], length);

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public Span<T> SliceSpanUnsafe(nuint offset)
        => MemoryMarshal.CreateSpan(ref Pointer[offset], (int)nuint.Min(Length - offset, int.MaxValue));

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public UnmanagedSpan<T> SliceUnsafe(nuint offset, nuint length)
        => new(Pointer + offset, length);

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public UnmanagedSpan<T> SliceUnsafe(nuint offset)
        => new(Pointer + offset, Length - offset);

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public SpanEnumerator GetChunks()
        => new(this);

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public ElementEnumerator GetEnumerator()
        => new(this);

    public bool TryFormat(Span<char> destination, out int charsWritten, ReadOnlySpan<char> format, IFormatProvider? provider)
        => destination.TryWrite($"UnmanagedSpan<{typeof(T).FullName}>[0x{(nuint)Pointer:X}][{Length}]", out charsWritten);

    public string ToString(string? format, IFormatProvider? formatProvider)
        => $"UnmanagedSpan<{typeof(T).FullName}>[0x{(nuint)Pointer:X}][{Length}]";

    public override string ToString()
        => ToString(null, null);

    public struct ElementEnumerator
    {
        private T* current;
        private nuint remaining;

        public readonly ref T Current
            => ref this.current[0];

        public ElementEnumerator(UnmanagedSpan<T> span)
        {
            this.current = span.Pointer;
            this.remaining = span.Length;
        }

        public bool MoveNext()
        {
            if (this.remaining-- == 0)
                return false;

            ++this.current;
            return true;
        }
    }

    public struct SpanEnumerator
    {
        private T* current;
        private nuint remaining;

        public Span<T> Current
        {
            get
            {
                if(this.remaining <= int.MaxValue)
                {
                    Span<T> span = MemoryMarshal.CreateSpan(ref this.current[0], (int)this.remaining);
                    this.remaining = 0;
                    return span;
                }

                return MemoryMarshal.CreateSpan(ref this.current[0], int.MaxValue);
            }
        }

        public SpanEnumerator(UnmanagedSpan<T> span)
        {
            this.current = span.Pointer;
            this.remaining = span.Length;
        }

        public bool MoveNext()
        {
            if (this.remaining == 0)
                return false;

            if (this.remaining <= int.MaxValue)
                return true;

            this.remaining -= int.MaxValue;
            this.current += int.MaxValue;
            return true;
        }

        public readonly SpanEnumerator GetEnumerator()
            => this;
    }
}
