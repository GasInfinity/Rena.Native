using System.Buffers;
using System.Text;
using System.Text.Unicode;

namespace Rena.Native.Extensions;

/// <summary>
/// Provides extension methods for the <see cref="StringBuilder"/> class.
/// </summary>
public static class StringBuilderExtensions
{
    /// <summary>
    /// Appends the characters represented by a UTF-8 encoded byte span to the current instance of <see cref="StringBuilder"/>.
    /// </summary>
    /// <param name="builder">The <see cref="StringBuilder"/> to which the characters are appended.</param>
    /// <param name="utf8String">The UTF-8 encoded byte span containing the characters to append.</param>
    /// <returns>The same <see cref="StringBuilder"/> instance after the append operation.</returns>
    public static StringBuilder Append(this StringBuilder builder, ReadOnlySpan<byte> utf8String)
    {
        const int MaxStackalloc = 256; // 256 * 2 = 512 bytes allocated on the stack (should be sufficient)

        Span<char> utf16Data = stackalloc char[MaxStackalloc];

        var utf16Part = utf16Data;
        var utf8Part = utf8String;

        OperationStatus status = Utf8.ToUtf16(utf8Part, utf16Data, out int bytesRead, out int charsWritten);
        while (true)
        {
            utf16Data = utf16Data.SliceUnsafe(..charsWritten);
            utf8Part = utf8Part.SliceUnsafe(bytesRead..);

            builder.Append(utf16Data);

            if (status == OperationStatus.Done)
                break;

            status = Utf8.ToUtf16(utf8Part, utf16Data, out bytesRead, out charsWritten);
        }

        return builder;
    }
}