using FluentAssertions;
using NUnit.Framework;
using Puzzles;
using Puzzles.Input;

namespace AOCTests;

public class Day04Tests
{
    [SetUp]
    public void Setup()
    {
    }

    [Test]
    public void Test_Parse_Example()
    {
        // Arrange
        var input = InputDay04.ExampleCards;

        // Act
        var result = BingoCard.Parse(input[0]);

        // Assert
        result.Print().Should().Be(@"22 13 17 11  0
 8  2 23  4 24
21  9 14 16  7
 6 10  3 18  5
 1 12 20 15 19
");
    }

    [Test]
    public void Test_Example()
    {
        // Arrange
        var input = InputDay04.Example;
        var cards = InputDay04.ExampleCards;
        var game = new BingoGame(cards, input);

        // Act
        var result = game.Run();
        
        // Assert
        result.Should().Be(24 * 188);
    }

        [Test]
    public void Test_Puzzle()
    {
        // Arrange
        var input = InputDay04.Puzzle;
        var cards = InputDay04.PuzzleCards;
        var game = new BingoGame(cards, input);

        // Act
        var result = game.Run();

        // Assert
        result.Should().Be(14 * 824);
    }


    [Test]
    public void Test_Puzzle2()
    {
        // Arrange
        var input = InputDay04.Puzzle;
        var cards = InputDay04.PuzzleCards;
        var game = new BingoGame(cards, input);

        // Act
        var result = game.Run(true);

        // Assert
        result.Should().Be(1284);
    }
}