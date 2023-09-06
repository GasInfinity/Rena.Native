using System.Diagnostics;
using System.Diagnostics.CodeAnalysis;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;

namespace Rena.Native.Extensions;

public static class SpanExtensions
{
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static Span<T> SliceUnsafe<T>(this Span<T> span, int offset, int length)
    {
        Debug.Assert((uint)offset <= (uint)span.Length && length <= (span.Length - offset), $"The provided {nameof(offset)} and {nameof(length)} must be within the bounds of the {nameof(Span<T>)}!");
        return MemoryMarshal.CreateSpan(ref Unsafe.Add(ref MemoryMarshal.GetReference(span), offset), length);
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static ReadOnlySpan<T> SliceUnsafe<T>(this ReadOnlySpan<T> span, int offset, int length)
    {
        Debug.Assert((uint)offset <= (uint)span.Length && length <= (span.Length - offset), $"The provided {nameof(offset)} and {nameof(length)} must be within the bounds of the {nameof(ReadOnlySpan<T>)}!");
        return MemoryMarshal.CreateReadOnlySpan(ref Unsafe.Add(ref MemoryMarshal.GetReference(span), offset), length);
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static Span<T> SliceUnsafe<T>(this Span<T> span, int offset)
    {
        Debug.Assert((uint)offset <= (uint)span.Length, $"The provided {nameof(offset)} must be within the bounds of the {nameof(Span<T>)}!");
        return MemoryMarshal.CreateSpan(ref Unsafe.Add(ref MemoryMarshal.GetReference(span), offset), span.Length - offset);
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static ReadOnlySpan<T> SliceUnsafe<T>(this ReadOnlySpan<T> span, int offset)
    {
        Debug.Assert((uint)offset <= (uint)span.Length, $"The provided {nameof(offset)} must be within the bounds of the {nameof(ReadOnlySpan<T>)}!");
        return MemoryMarshal.CreateReadOnlySpan(ref Unsafe.Add(ref MemoryMarshal.GetReference(span), offset), span.Length - offset);
    }

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

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static Span<T> OptimizeUnsafe<T>(this Span<T> span, [ConstantExpected] int length)
    {
        Debug.Assert(span.Length >= length, $"The provided {nameof(length)} must be within the bounds of the {nameof(Span<T>)}!");
        return MemoryMarshal.CreateSpan(ref MemoryMarshal.GetReference(span), length);
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static Span<T> OptimizeUnsafe<T>(this ReadOnlySpan<T> span, [ConstantExpected] int length)
    {
        Debug.Assert(span.Length >= length, $"The provided {nameof(length)} must be within the bounds of the {nameof(ReadOnlySpan<T>)}!");
        return MemoryMarshal.CreateSpan(ref MemoryMarshal.GetReference(span), length);
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static ref T GetUnsafe<T>(this Span<T> span, int offset)
    {
        Debug.Assert(offset >= 0 && offset < span.Length, $"The provided {nameof(offset)} must be within the bounds of the {nameof(Span<T>)}!");
        return ref Unsafe.Add(ref MemoryMarshal.GetReference(span), offset);
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static ref readonly T GetUnsafe<T>(this ReadOnlySpan<T> span, int offset)
    {
        Debug.Assert(offset >= 0 && offset < span.Length, $"The provided {nameof(offset)} must be within the bounds of the {nameof(ReadOnlySpan<T>)}!");
        return ref Unsafe.Add(ref MemoryMarshal.GetReference(span), offset);
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static ReadOnlySpan<byte> Cast(this ReadOnlySpan<sbyte> span)
        => MemoryMarshal.CreateReadOnlySpan(ref Unsafe.As<sbyte, byte>(ref MemoryMarshal.GetReference(span)), span.Length);
}