using System.Diagnostics;
using System.Runtime.CompilerServices;

namespace Rena.Native.Extensions;

public static class ObjectExtensions
{
    public static T UnsafeCast<T>(this object value, [CallerArgumentExpression(nameof(value))] string valueExpression = "??")
        where T : class
    {
        Debug.Assert(value is T, $"{valueExpression} with type '{value.GetType().FullName}' is not '{typeof(T).FullName}'!");
        return Unsafe.As<T>(value);
    }
}