using System;
using AlgoLib.Functions;
using Xunit;

namespace AlgoLib.Tests
{
    public class LevensteinDistance
    {
        [Fact]
        public void Should_ReturnZero_ForEqualStrings()
        {
            int distance = EditDistance.LevensteinDistance("kitten", "kitten");
            Assert.Equal(0, distance);
        }
        
        [Fact]
        public void Should_ReturnLengthOfFirstString_WhenSecondIsEmpty()
        {
            var a = "kitten";
            int distance = EditDistance.LevensteinDistance(a, string.Empty);
            Assert.Equal(a.Length, distance);
        }
        
        [Fact]
        public void Should_ReturnLengthOfSecondString_WhenFirstIsEmpty()
        {
            var b = "kitten";
            int distance = EditDistance.LevensteinDistance(string.Empty, b);
            Assert.Equal(b.Length, distance);
        }
       
        [Fact]
        public void Should_Return3_ForSittingAndKitten()
        {
            int distance = EditDistance.LevensteinDistance("sitting", "kitten");
            Assert.Equal(3, distance);
        }
    }
}