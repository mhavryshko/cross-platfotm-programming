using System;
using System.Collections.Generic;
using System.Text;
using Xunit;

namespace LAB3.Tests
{
    public class ProgramTests
    {
        [Fact]
        public void Test_SurroundedStart()
        {
            string[] input = { "5", "#####", "#@###", "#####", "#####", "#####" };
            string expected = "Impossible";
            string result = Program.ProcessBoard(input);
            Assert.Equal(expected, result);
        }

        [Fact]
        public void Test_OneCellBoard()
        {
            string[] input = { "1", "@" };
            string expected = "Impossible";
            string result = Program.ProcessBoard(input);
            Assert.Equal(expected, result);
        }


        [Fact]
        public void Test_SingleLinePath()
        {
            string[] input = { "1", "@" };
            string expected = "Impossible";
            string result = Program.ProcessBoard(input);
            Assert.Equal(expected, result);
        }

        [Fact]
        public void Test_SingleRowWithObstacle()
        {
            string[] input = { "1", "@#" };
            string expected = "Impossible";
            string result = Program.ProcessBoard(input);
            Assert.Equal(expected, result);
        }

        [Fact]
        public void Test_InnerObstacles()
        {
            string[] input = { "5", ".....", ".###.", ".@#..", ".#...", ".....", "@...." };
            string expected = "Impossible";
            string result = Program.ProcessBoard(input);
            Assert.Equal(expected, result);
        }
    }
}