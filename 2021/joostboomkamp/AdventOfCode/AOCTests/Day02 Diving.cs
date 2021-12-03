using FluentAssertions;
using NUnit.Framework;
using Puzzles;
using Puzzles.Input;
using System;
using System.Linq;

namespace AOCTests;

public class Day02Tests
{
    [SetUp]
    public void Setup()
    {
    }

    [Test]
    public void Test_Example()
    {
        // Arrange
        var input = InputDay02.Example.Split(Environment.NewLine).Select(Step.Parse);
        var cut = new Diving();

        // Act
        foreach (var step in input)
        {
            cut.Move(step);
        }
        var result = cut.Result;

        // Assert
        result.Should().Be(150);
    }


    [Test]
    public void Test_Puzzle()
    {
        // Arrange
        var input = InputDay02.Puzzle.Split(Environment.NewLine).Select(Step.Parse);
        var cut = new Diving();

        // Act
        foreach (var step in input)
        {
            cut.Move(step);
        }
        var result = cut.Result;

        // Assert
        result.Should().Be(1936494);
    }


    [Test]
    public void Test_Example2()
    {
        // Arrange
        var input = InputDay02.Example.Split(Environment.NewLine).Select(Step.Parse);
        var cut = new Diving();

        // Act
        foreach (var step in input)
        {
            cut.Travel(step);
        }
        var result = cut.Result;

        // Assert
        result.Should().Be(900);
    }


    [Test]
    public void Test_Puzzle2()
    {
        // Arrange
        var input = InputDay02.Puzzle.Split(Environment.NewLine).Select(Step.Parse);
        var cut = new Diving();

        // Act
        foreach (var step in input)
        {
            cut.Travel(step);
        }
        var result = cut.Result;

        // Assert
        result.Should().Be(1997106066);
    }
}