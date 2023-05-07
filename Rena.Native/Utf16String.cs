using System.Runtime.InteropServices;
using System.Text;

namespace Rena.Native;

public unsafe static class Utf16String
{
    public static string FromUtf8NullTerminated(byte* value)
        => Encoding.UTF8.GetString(MemoryMarshal.CreateReadOnlySpanFromNullTerminated(value));

    public static string FromUtf8(byte* value, int length)
        => Encoding.UTF8.GetString(new Span<byte>(value, length));
}