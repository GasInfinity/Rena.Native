using System.Runtime.CompilerServices;

namespace Rena.Native.Extensions;

public static class BoolExtensions
{
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static byte AsByteFast(this bool value)
        => Unsafe.As<bool, byte>(ref value);
}