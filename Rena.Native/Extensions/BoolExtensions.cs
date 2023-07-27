using System.Runtime.CompilerServices;

namespace Rena.Native.Extensions;

public static class BoolExtensions
{
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static byte AsByte(this bool value)
        => Unsafe.As<bool, byte>(ref value);
}