using Puzzles;
using FluentAssertions;
using NUnit.Framework;
using Puzzles.Input;

namespace AOCTests;

public class Day01Tests
{
    [SetUp]
    public void Setup()
    {
    }

    [Test]
    public void Test_Day01_Example()
    {
        // Arrange
        var input = InputDay01.Example;
        var cut = new SonarSweep();

        // Act
        var result = cut.Compute(input);

        // Assert
        result.Should().Be(7);
    }


    [Test]
    public void Test_Day01_Puzzle()
    {
        // Arrange
        var input = InputDay01.Puzzle;
        var cut = new SonarSweep();

        // Act
        var result = cut.Compute(input);

        // Assert
        result.Should().Be(1167);
    }

    [Test]
    public void Test_Day01_Example_2()
    {
        // Arrange
        var input = InputDay01.Example;
        var cut = new SonarSweep();

        // Act
        var result = cut.ComputeSliding(input);

        // Assert
        result.Should().Be(5);
    }


    [Test]
    public void Test_Day01_Puzzle_2()
    {
        // Arrange
        var input = InputDay01.Puzzle;
        var cut = new SonarSweep();

        // Act
        var result = cut.ComputeSliding(input);

        // Assert
        result.Should().Be(1130);
    }
}