using System;
using Xunit;
using LAB2;

namespace LAB2.Tests
{
    public class ProgramTests
    {
        [Fact]
        public void TestMinimumPath_SingleCell()
        {
            int[,] grid = { { 0 } };
            char[,] expected = { { '#' } };
            char[,] actual = Program.FindMinimumPath(grid);
            Assert.Equal(expected, actual);
        }

        [Fact]
        public void TestMinimumPath_TwoByTwoGrid()
        {
            int[,] grid = { { 1, 2 }, { 1, 1 } };
            char[,] expected = { { '#', '.' }, { '#', '#' } };
            char[,] actual = Program.FindMinimumPath(grid);
            Assert.Equal(expected, actual);
        }

        [Fact]
        public void TestMinimumPath_NegativeValues()
        {
            int[,] grid = { { -1, -1 }, { -1, -1 } };
            char[,] expected = { { '#', '.' }, { '#', '#' } };
            char[,] actual = Program.FindMinimumPath(grid);
            Assert.Equal(expected, actual);
        }

        [Fact]
        public void TestMinimumPath_LargeValues()
        {
            int[,] grid = { { 1_000_000, 2_000_000 }, { 1_000_000, 1_000_000 } };
            char[,] expected = { { '#', '.' }, { '#', '#' } };
            char[,] actual = Program.FindMinimumPath(grid);
            Assert.Equal(expected, actual);
        }

        [Fact]
        public void TestMinimumPath_AllZeros()
        {
            int[,] grid = { { 0, 0 }, { 0, 0 } };
            char[,] expected = { { '#', '.' }, { '#', '#' } };
            char[,] actual = Program.FindMinimumPath(grid);
            Assert.Equal(expected, actual);
        }
    }
}
