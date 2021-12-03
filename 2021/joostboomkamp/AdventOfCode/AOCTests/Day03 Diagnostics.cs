using FluentAssertions;
using NUnit.Framework;
using Puzzles;
using Puzzles.Input;
using System;
using System.Linq;

namespace AOCTests;

public class Day03Tests
{
    [SetUp]
    public void Setup()
    {
    }

    [Test]
    public void Test_Example()
    {
        // Arrange
        var input = InputDay03.Example;
        var cut = new Diagnostics();

        // Act
        var result = cut.GetPowerConsumption(input);

        // Assert
        result.GammaRate.Should().Be(22);
        result.EpsilonRate.Should().Be(9);
        result.PowerConsumption.Should().Be(198);
    }


    [Test]
    public void Test_Puzzle()
    {
        // Arrange
        var input = InputDay03.Puzzle;
        var cut = new Diagnostics();

        // Act
        var result = cut.GetPowerConsumption(input);

        // Assert
        result.GammaRate.Should().Be(935);
        result.EpsilonRate.Should().Be(3160);
        result.PowerConsumption.Should().Be(2954600);
    }


    [Test]
    public void Test_Example2()
    {
        // Arrange
        var input = InputDay03.Example;
        var cut = new Diagnostics();

        // Act
        var result = cut.GetLifeSupportRating(input);

        // Assert
        result.OxygenGeneratorRating.Should().Be(23);
        result.CO2ScrubberRating.Should().Be(10);
        result.LifeSupportRating.Should().Be(230);
    }


    [Test]
    public void Test_Puzzle2()
    {
        // Arrange
        var input = InputDay03.Puzzle;
        var cut = new Diagnostics();

        // Act
        var result = cut.GetLifeSupportRating(input);

        // Assert
        result.OxygenGeneratorRating.Should().Be(573);
        result.CO2ScrubberRating.Should().Be(2902);
        result.LifeSupportRating.Should().Be(1662846);
    }
}