using NUnit.Framework;
using Puzzles.Input;
using Puzzles;
using FluentAssertions;

namespace AOCTests;
public class Day14Tests
{
    [TestCase(1, "NCNBCHB")]
    [TestCase(2, "NBCCNBBBCBHCB")]
    [TestCase(3, "NBBBCNCCNBBNBNBBCHBHHBCHB")]
    [TestCase(4, "NBBNBNBBCCNBCNCCNBBNBBNBBBNBBNBBCBHCBHHNHCBBCBHCB")]
    public void TestExample(int count, string expected)
    {
        // arrange
        var input = InputDay14.Example;
        var cut = new Polymerization(input);

        // act
        var result = cut.Step(count);
        
        // assert
        result.Should().Be(expected);
    }

    [Test]
    public void TestExampleCount()
    {
        // arrange
        var input = InputDay14.Example;
        var cut = new Polymerization(input);
        var result = cut.Step(10);

        // act
        var count = cut.Count;

        // assert
        count["B"].Should().Be(1749);
        count["H"].Should().Be(161);
        cut.CheckSum.Should().Be(1588);
    }

    [Test]
    public void TestPuzzle()
    {
        // arrange
        var input = InputDay14.Puzzle;
        var cut = new Polymerization(input);
        var result = cut.Step(10);

        // act
        var count = cut.Count;

        // assert
        cut.CheckSum.Should().Be(2587);
    }

    [Test]
    public void TestPuzzle2()
    {
        // arrange
        var input = InputDay14.Puzzle;
        var cut = new Polymerization(input);
        var result = cut.Step(40);

        // act
        var count = cut.Count;

        // assert
        cut.CheckSum.Should().Be(1337);
    }

    [Test]
    public void RunPuzzle()
    {
        // arrange
        var input = InputDay14.Puzzle;
        var cut = new Polymerization(input);

        // act
        var result = cut.Run(10);

        // assert
        result.Should().Be(2587);
    }

    [Test]
    public void RunPuzzle2()
    {
        // arrange
        var input = InputDay14.Puzzle;
        var cut = new Polymerization(input);

        // act
        var result = cut.Run(40);

        // assert
        result.Should().Be(19375);
    }
}
