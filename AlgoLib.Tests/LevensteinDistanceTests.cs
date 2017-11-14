using System;
using AlgoLib.Functions;
using Xunit;

namespace AlgoLib.Tests
{
    public class LevensteinDistanceTests
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
        
        [Fact]
        public void Should_Return3_ForSmiteAndTree()
        {
            int distance = EditDistance.LevensteinDistance("smite", "tree");
            Assert.Equal(4, distance);
        }
        
         
        [Fact]
        public void Should_Return3_ForSmiteAndTee()
        {
            int distance = EditDistance.LevensteinDistance("smite", "tee");
            Assert.Equal(4, distance);
        }
    }
}