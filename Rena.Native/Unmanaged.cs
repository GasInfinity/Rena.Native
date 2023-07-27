using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;

namespace Rena.Native;

/// <summary>
/// Provides utility methods to work with unmanaged types.
/// </summary>
public static class Unmanaged
{
    /// <summary>
    /// Creates the view of a reference to an unmanaged value over a <see cref="Span{T}"/> of bytes.
    /// </summary>
    /// <typeparam name="T">The type of the value to convert.</typeparam>
    /// <param name="value">The value to convert.</param>
    /// <returns>A view of the value in a <see cref="Span{T}"/>.</returns>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static Span<byte> AsBytes<T>(ref T value)
        where T : unmanaged
        => MemoryMarshal.CreateSpan(ref Unsafe.As<T, byte>(ref value), Unsafe.SizeOf<T>());

    /// <summary>
    /// Creates the view of a read-only reference to an unmanaged value over a <see cref="ReadOnlySpan{T}"/> of bytes.
    /// </summary>
    /// <typeparam name="T">The type of the value to convert.</typeparam>
    /// <param name="value">The value to convert.</param>
    /// <returns>A view of the value in a <see cref="ReadOnlySpan{T}"/>.</returns>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static ReadOnlySpan<byte> AsReadOnlyBytes<T>(in T value)
        where T : unmanaged
        => MemoryMarshal.CreateReadOnlySpan(ref Unsafe.As<T, byte>(ref Unsafe.AsRef(value)), Unsafe.SizeOf<T>());
}
