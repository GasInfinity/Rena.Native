using System.Runtime.CompilerServices;

namespace Rena.Native.Extensions;

public static class EnumExtensions
{
    public static unsafe bool HasFlag<T>(this T value, T flag)
        where T : unmanaged, Enum
    {
        if (sizeof(T) == 1)
            return (Unsafe.As<T, byte>(ref value) | Unsafe.As<T, byte>(ref flag)) == Unsafe.As<T, byte>(ref value);
        else if (sizeof(T) == 2)
            return (Unsafe.As<T, short>(ref value) | Unsafe.As<T, short>(ref flag)) == Unsafe.As<T, short>(ref value);
        else if (sizeof(T) == 4)
            return (Unsafe.As<T, int>(ref value) | Unsafe.As<T, int>(ref flag)) == Unsafe.As<T, int>(ref value);
        else // if(sizeof(T) == 8)
            return (Unsafe.As<T, long>(ref value) | Unsafe.As<T, long>(ref flag)) == Unsafe.As<T, long>(ref value);
    }
}
