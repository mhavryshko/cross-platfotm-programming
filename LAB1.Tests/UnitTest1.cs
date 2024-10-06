using System;
using Xunit;
using LAB1;

namespace LAB1.Tests
{
    public class ProgramTests
    {

        [Fact]
        public void CountWays_KEqualsN_ReturnsValidCount()
        {
            long result = Program.CountWays(5, 5);
            Assert.Equal(120, result);
        }

        [Fact]
        public void BinomialCoefficient_ValidInputs_ReturnsCorrectValue()
        {
            long result = Program.BinomialCoefficient(5, 3);
            Assert.Equal(10, result);
        }

        [Fact]
        public void BinomialCoefficient_KEqualsN_ReturnsOne()
        {
            long result = Program.BinomialCoefficient(6, 6);
            Assert.Equal(1, result);
        }

        [Fact]
        public void Factorial_ValidInput_ReturnsCorrectFactorial()
        {
            long result = Program.Factorial(5);
            Assert.Equal(120, result);
        }

        [Fact]
        public void Factorial_Zero_ReturnsOne()
        {
            long result = Program.Factorial(0);
            Assert.Equal(1, result);
        }
    }
}
