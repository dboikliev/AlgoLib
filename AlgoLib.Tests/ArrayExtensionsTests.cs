using AlgoLib.Extensions;
using Xunit;

namespace AlgoLib.Tests
{
    public class ArrayExtensionsTests
    {
        [Fact]
        public void ShouldSwapFirstAndSecondElement()
        {
            var array = new[] {1, 2, 3, 4, 5};
            
            array.Swap(0, 1);
            Assert.Equal(new [] { 2, 1, 3, 4, 5 }, array);
        }
    }
}