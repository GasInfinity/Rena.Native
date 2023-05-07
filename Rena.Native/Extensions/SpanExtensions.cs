using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;

namespace Rena.Native.Extensions;

public static class SpanExtensions
{
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static Span<T> SliceUnsafe<T>(this Span<T> span, int offset, int length)
        => MemoryMarshal.CreateSpan(ref Unsafe.Add(ref MemoryMarshal.GetReference(span), offset), length);

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static ReadOnlySpan<T> SliceUnsafe<T>(this ReadOnlySpan<T> span, int offset, int length)
        => MemoryMarshal.CreateReadOnlySpan(ref Unsafe.Add(ref MemoryMarshal.GetReference(span), offset), length);

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static Span<T> SliceUnsafe<T>(this Span<T> span, int length)
        => MemoryMarshal.CreateSpan(ref MemoryMarshal.GetReference(span), length);

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static ReadOnlySpan<T> SliceUnsafe<T>(this ReadOnlySpan<T> span, int length)
        => MemoryMarshal.CreateReadOnlySpan(ref MemoryMarshal.GetReference(span), length);

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static Span<T> SliceUnsafe<T>(this Span<T> span, Range range)
    {
        (var offset, var length) = range.GetOffsetAndLength(span.Length);
        return span.SliceUnsafe(offset, length);
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static ReadOnlySpan<T> SliceUnsafe<T>(this ReadOnlySpan<T> span, Range range)
    {
        (var offset, var length) = range.GetOffsetAndLength(span.Length);
        return span.SliceUnsafe(offset, length);
    }
}