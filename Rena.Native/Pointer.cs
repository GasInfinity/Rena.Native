using System.Diagnostics.CodeAnalysis;

namespace Rena.Native;

public unsafe readonly struct Pointer<T> : IEquatable<Pointer<T>>, ISpanFormattable
    where T : unmanaged
{
    public readonly T* Ptr;

    public bool IsNull
        => Ptr == null;

    public T Value
        => Ptr[0];

    public ref T Managed
        => ref Ptr[0];

    public Pointer(T* ptr)
        => Ptr = ptr;

    public bool Equals(Pointer<T> other)
        => Ptr == other.Ptr;

    public string ToString(string? format, IFormatProvider? formatProvider)
        => $"(0x{(nuint)Ptr:X})<{typeof(T).Name}>*";

    public bool TryFormat(Span<char> destination, out int charsWritten, ReadOnlySpan<char> format, IFormatProvider? provider)
        => destination.TryWrite($"(0x{(nuint)Ptr:X})<{typeof(T).Name}>*", out charsWritten);

    public override bool Equals([NotNullWhen(true)] object? obj)
        => obj is Pointer<T> value && Equals(value);

    public override int GetHashCode()
        => ((nuint)Ptr).GetHashCode();

    public override string ToString()
        => ToString(null, null);

    public static implicit operator T*(Pointer<T> value)
        => value.Ptr;

    public static implicit operator Pointer<T>(T* ptr)
        => new(ptr);

    public static bool operator ==(Pointer<T> left, Pointer<T> right)
        => left.Equals(right);

    public static bool operator !=(Pointer<T> left, Pointer<T> right)
        => !(left == right);
}