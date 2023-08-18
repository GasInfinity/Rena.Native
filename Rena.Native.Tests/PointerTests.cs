using NUnit.Framework;

namespace Rena.Native.Tests;

[TestFixture]
public unsafe class PointerTests
{
    [Test]
    public void IsNull_ReturnFalse()
    {
        int* stackallocInt = stackalloc int[1];
        Pointer<int> pointer = new (stackallocInt);

        Assert.IsFalse(pointer.IsNull, "Stackallocked integer must not be null");
    }

    [Test]
    public void IsNull_ReturnTrue()
    {
        Pointer<int> pointer = default;

        Assert.IsTrue(pointer.IsNull, "A default/null pointer must be null");
    }
}
