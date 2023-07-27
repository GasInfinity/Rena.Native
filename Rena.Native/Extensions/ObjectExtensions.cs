using System.Diagnostics;
using System.Runtime.CompilerServices;

namespace Rena.Native.Extensions;

/// <summary>
/// Provides extension methods for working with objects.
/// </summary>
public static class ObjectExtensions
{
    /// <summary>
    /// Casts an object to a specified type <typeparamref name="T"/> without type checking or boxing/unboxing.
    /// </summary>
    /// <typeparam name="T">The target type to cast the object to. Must be a reference type.</typeparam>
    /// <param name="value">The object to cast.</param>
    /// <param name="valueExpression">The expression string representing the value. Used for debugging purposes. Default is "??".</param>
    /// <returns>The object cast to the specified type <typeparamref name="T"/>.</returns>
    /// <remarks>
    /// Using this method incorrectly can lead to runtime exceptions or undefined behavior if the object is not of the specified type.
    /// </remarks>
    public static T CastUnsafe<T>(this object value, [CallerArgumentExpression(nameof(value))] string valueExpression = "??")
        where T : class
    {
        Debug.Assert(value is T, $"{valueExpression} with type '{value.GetType().FullName}' is not '{typeof(T).FullName}'!");
        return Unsafe.As<T>(value);
    }
}