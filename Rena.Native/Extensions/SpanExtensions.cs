using System.Diagnostics.CodeAnalysis;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;

namespace Rena.Native.Extensions;

public static class SpanExtensions // TODO: Add bound checks with Debug.Assert
{
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static Span<T> SliceUnsafe<T>(this Span<T> span, int offset, int length)
        => MemoryMarshal.CreateSpan(ref Unsafe.Add(ref MemoryMarshal.GetReference(span), offset), length);

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static ReadOnlySpan<T> SliceUnsafe<T>(this ReadOnlySpan<T> span, int offset, int length)
        => MemoryMarshal.CreateReadOnlySpan(ref Unsafe.Add(ref MemoryMarshal.GetReference(span), offset), length);

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static Span<T> SliceUnsafe<T>(this Span<T> span, int offset)
        => MemoryMarshal.CreateSpan(ref Unsafe.Add(ref MemoryMarshal.GetReference(span), offset), span.Length - offset);

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static ReadOnlySpan<T> SliceUnsafe<T>(this ReadOnlySpan<T> span, int offset)
        => MemoryMarshal.CreateReadOnlySpan(ref Unsafe.Add(ref MemoryMarshal.GetReference(span), offset), span.Length - offset);

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
    public static ReadOnlySpan<byte> CastFast(this ReadOnlySpan<sbyte> span)
        => MemoryMarshal.CreateReadOnlySpan(ref Unsafe.As<sbyte, byte>(ref MemoryMarshal.GetReference(span)), span.Length);

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static Span<T> OptimizeUnsafe<T>(this Span<T> span, [ConstantExpected] int constant)
        => MemoryMarshal.CreateSpan(ref MemoryMarshal.GetReference(span), constant);

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static Span<T> OptimizeUnsafe<T>(this ReadOnlySpan<T> span, [ConstantExpected] int constant)
        => MemoryMarshal.CreateSpan(ref MemoryMarshal.GetReference(span), constant);

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static void WriteUnsafe<T>(this Span<T> span, int offset, T value)
        => Unsafe.Add(ref MemoryMarshal.GetReference(span), offset) = value;

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static ref readonly T ReadUnsafe<T>(this ReadOnlySpan<T> span, int offset)
        => ref Unsafe.Add(ref MemoryMarshal.GetReference(span), offset);

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static ref T ReadUnsafe<T>(this Span<T> span, int offset)
        => ref Unsafe.Add(ref MemoryMarshal.GetReference(span), offset);
}