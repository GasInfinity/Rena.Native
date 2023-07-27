using System.Buffers;
using System.Buffers.Binary;
using System.Diagnostics.CodeAnalysis;
using System.Runtime.CompilerServices;

namespace Rena.Native.Buffers.Extensions;

/// <summary>
/// Provides extension methods for <see cref="IBufferWriter{byte}"/>
/// </summary>
public static class BufferWriterExtensions
{
    /// <summary>
    /// Writes a raw value of type <typeparamref name="T"/> to the buffer in the native endianness.
    /// </summary>
    /// <typeparam name="T">The blittable, unmanaged type to write.</typeparam>
    /// <param name="writer">The target <see cref="IBufferWriter{byte}"/> instance to which the data will be written.</param>
    /// <param name="value">The value of type <typeparamref name="T"/> to be written to the buffer.</param>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static void WriteRaw<T>(this IBufferWriter<byte> writer, in T value)
        where T : unmanaged
        => writer.Write(Unmanaged.AsReadOnlyBytes(value));

    /// <summary>
    /// Writes a 16-bit unsigned integer (ushort) value to the buffer with the specified endianness.
    /// </summary>
    /// <param name="writer">The target <see cref="IBufferWriter{byte}"/> instance to which the data will be written.</param>
    /// <param name="value">The 16-bit unsigned integer value to be written to the buffer.</param>
    /// <param name="littleEndian">A boolean flag indicating whether to use little-endian byte order (true) or big-endian byte order (false).</param>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static void WriteUInt16(this IBufferWriter<byte> writer, ushort value, [ConstantExpected] bool littleEndian)
    {
        if (littleEndian ^ BitConverter.IsLittleEndian)
            value = BinaryPrimitives.ReverseEndianness(value);

        writer.WriteRaw(value);
    }

    /// <summary>
    /// Writes a 16-bit signed integer (short) value to the buffer with the specified endianness.
    /// </summary>
    /// <param name="writer">The target <see cref="IBufferWriter{byte}"/> instance to which the data will be written.</param>
    /// <param name="value">The 16-bit signed integer value to be written to the buffer.</param>
    /// <param name="littleEndian">A boolean flag indicating whether to use little-endian byte order (true) or big-endian byte order (false).</param>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static void WriteInt16(this IBufferWriter<byte> writer, short value, [ConstantExpected] bool littleEndian)
    {
        if (littleEndian ^ BitConverter.IsLittleEndian)
            value = BinaryPrimitives.ReverseEndianness(value);

        writer.WriteRaw(value);
    }

    /// <summary>
    /// Writes a 32-bit unsigned integer (uint) value to the buffer with the specified endianness.
    /// </summary>
    /// <param name="writer">The target <see cref="IBufferWriter{byte}"/> instance to which the data will be written.</param>
    /// <param name="value">The 32-bit unsigned integer value to be written to the buffer.</param>
    /// <param name="littleEndian">A boolean flag indicating whether to use little-endian byte order (true) or big-endian byte order (false).</param>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static void WriteUInt32(this IBufferWriter<byte> writer, uint value, [ConstantExpected] bool littleEndian)
    {
        if (littleEndian ^ BitConverter.IsLittleEndian)
            value = BinaryPrimitives.ReverseEndianness(value);

        writer.WriteRaw(value);
    }

    /// <summary>
    /// Writes a 32-bit signed integer (int) value to the buffer with the specified endianness.
    /// </summary>
    /// <param name="writer">The target <see cref="IBufferWriter{byte}"/> instance to which the data will be written.</param>
    /// <param name="value">The 32-bit signed integer value to be written to the buffer.</param>
    /// <param name="littleEndian">A boolean flag indicating whether to use little-endian byte order (true) or big-endian byte order (false).</param>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static void WriteInt32(this IBufferWriter<byte> writer, int value, [ConstantExpected] bool littleEndian)
    {
        if (littleEndian ^ BitConverter.IsLittleEndian)
            value = BinaryPrimitives.ReverseEndianness(value);

        writer.WriteRaw(value);
    }

    /// <summary>
    /// Writes a 64-bit unsigned integer (ulong) value to the buffer with the specified endianness.
    /// </summary>
    /// <param name="writer">The target <see cref="IBufferWriter{byte}"/> instance to which the data will be written.</param>
    /// <param name="value">The 64-bit unsigned integer value to be written to the buffer.</param>
    /// <param name="littleEndian">A boolean flag indicating whether to use little-endian byte order (true) or big-endian byte order (false).</param>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static void WriteUInt64(this IBufferWriter<byte> writer, ulong value, [ConstantExpected] bool littleEndian)
    {
        if (littleEndian ^ BitConverter.IsLittleEndian)
            value = BinaryPrimitives.ReverseEndianness(value);

        writer.WriteRaw(value);
    }

    /// <summary>
    /// Writes a 64-bit signed integer (long) value to the buffer with the specified endianness.
    /// </summary>
    /// <param name="writer">The target <see cref="IBufferWriter{byte}"/> instance to which the data will be written.</param>
    /// <param name="value">The 64-bit signed integer value to be written to the buffer.</param>
    /// <param name="littleEndian">A boolean flag indicating whether to use little-endian byte order (true) or big-endian byte order (false).</param>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static void WriteInt64(this IBufferWriter<byte> writer, long value, [ConstantExpected] bool littleEndian)
    {
        if (littleEndian ^ BitConverter.IsLittleEndian)
            value = BinaryPrimitives.ReverseEndianness(value);

        writer.WriteRaw(value);
    }

    /// <summary>
    /// Writes a 32-bit floating-point value (float) to the buffer with the specified endianness.
    /// </summary>
    /// <param name="writer">The target <see cref="IBufferWriter{byte}"/> instance to which the data will be written.</param>
    /// <param name="value">The 32-bit floating-point value to be written to the buffer.</param>
    /// <param name="littleEndian">A boolean flag indicating whether to use little-endian byte order (true) or big-endian byte order (false).</param>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static void WriteSingle(this IBufferWriter<byte> writer, float value, [ConstantExpected] bool littleEndian)
    {
        if (littleEndian ^ BitConverter.IsLittleEndian)
            value = BinaryPrimitives.ReverseEndianness(BitConverter.SingleToUInt32Bits(value));

        writer.WriteRaw(value);
    }

    /// <summary>
    /// Writes a 64-bit floating-point value (double) to the buffer with the specified endianness.
    /// </summary>
    /// <param name="writer">The target <see cref="IBufferWriter{byte}"/> instance to which the data will be written.</param>
    /// <param name="value">The 64-bit floating-point value to be written to the buffer.</param>
    /// <param name="littleEndian">A boolean flag indicating whether to use little-endian byte order (true) or big-endian byte order (false).</param>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static void WriteDouble(this IBufferWriter<byte> writer, double value, [ConstantExpected] bool littleEndian)
    {
        if (littleEndian ^ BitConverter.IsLittleEndian)
            value = BinaryPrimitives.ReverseEndianness(BitConverter.DoubleToUInt64Bits(value));

        writer.WriteRaw(value);
    }
}
