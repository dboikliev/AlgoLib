using System;
using AlgoLib.Functions;
using Xunit;

namespace AlgoLib.Tests
{
    public class HammingDistanceTests
    {
        [Fact]
        public void Should_ThrowInvalidOperationException_ForStringsOfDifferentLength()
        {
            var ex = Assert.Throws<InvalidOperationException>(() => EditDistance.HammingDistance("a", "aa"));
            
            Assert.Equal("a and b must be of equal length.", ex.Message);
        }
    }
}