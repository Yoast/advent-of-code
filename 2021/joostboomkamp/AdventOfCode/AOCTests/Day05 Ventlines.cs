using FluentAssertions;
using NUnit.Framework;
using Puzzles;
using Puzzles.Input;

namespace AOCTests;

public class Day05Tests
{
    [SetUp]
    public void Setup()
    {
    }

    [Test]
    public void Test_Example()
    {
        // Arrange
        var input = InputDay05.Example;
        var cut = new HydrothermalVents(input);

        // Act
        var result = cut.CountRiskyZones;

        // Assert
        result.Should().Be(5);
    }

    [Test]
    public void Test_Puzzle()
    {
        // Arrange
        var input = InputDay05.Puzzle;
        var cut = new HydrothermalVents(input);

        // Act
        var result = cut.CountRiskyZones;

        // Assert
        result.Should().Be(6189);
    }

    [Test]
    public void Test_Example2()
    {
        // Arrange
        var input = InputDay05.Example;
        var cut = new HydrothermalVents(input, false);

        // Act
        var result = cut.CountRiskyZones;

        // Assert
        result.Should().Be(12);
    }

    [Test]
    public void Test_Puzzle2()
    {
        // Arrange
        var input = InputDay05.Puzzle;
        var cut = new HydrothermalVents(input, false);

        // Act
        var result = cut.CountRiskyZones;

        // Assert
        result.Should().Be(19164);
    }
}