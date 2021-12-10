using FluentAssertions;
using NUnit.Framework;
using Puzzles;
using Puzzles.Input;
using System.Linq;

namespace AOCTests;
public class Day08Tests
{
    [Test]
    public void Test_Day08_Example()
    {
        // arrange
        var cut = new Digits();

        // act
        var result = cut.Solve("acedgfb cdfbe gcdfa fbcad dab cefabd cdfgeb eafb cagedb ab | cdfeb fcadb cdfeb cdbaf");

        // assert
        result.Should().Be(5353);
    }

    [Test]
    public void Test_Digits_Batch_Example()
    {
        // arrange
        var cut = new Digits();
        var input = InputDay08.Puzzle;

        // act
        var result =
            input.Sum(cut.Solve);

        // assert
        result.Should().Be(1019355);
    }
}
