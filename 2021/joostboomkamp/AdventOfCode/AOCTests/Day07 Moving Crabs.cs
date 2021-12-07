using FluentAssertions;
using NUnit.Framework;
using Puzzles;
using Puzzles.Input;

namespace AOCTests;

public class Day07Tests
{
    [SetUp]
    public void Setup()
    {
    }

    [Test]
    public void Test_Example()
    {
        // Arrange
        var input = InputDay07.Example;
        var cut = new Crabs(input);

        // Act
        var result = cut.FindCheapestMove();

        // Assert
        result.Item1.Should().Be(2);
        result.Item2.Should().Be(37);
    }

    [Test]
    public void Test_Puzzle()
    {
        // Arrange
        var input = InputDay07.Puzzle;
        var cut = new Crabs(input);

        // Act
        var result = cut.FindCheapestMove();

        // Assert
        result.Item1.Should().Be(363);
        result.Item2.Should().Be(341534);
    }

    [TestCase(1, 1)]
    [TestCase(2, 3)]
    [TestCase(3, 6)]
    [TestCase(4, 10)]
    [TestCase(5, 15)]
    public void TestFac(int input, int expected)
    {
        // arrange
        var cut = new Crabs(new[] { 0 });

        // act
        var result = cut.GetStepCost(input);

        // assert
        result.Should().Be(expected);
    }

    [Test]
    public void Test_Example2()
    {
        // Arrange
        var input = InputDay07.Example;
        var cut = new Crabs(input);

        // Act
        var result = cut.FindCheapestMove2();

        // Assert
        result.Item1.Should().Be(5);
        result.Item2.Should().Be(168);
    }

    [Test]
    public void Test_Puzzle2()
    {
        // Arrange
        var input = InputDay07.Puzzle;
        var cut = new Crabs(input);

        // Act
        var result = cut.FindCheapestMove2();

        // Assert
        result.Item1.Should().Be(484);
        result.Item2.Should().Be(93397632);
    }
}