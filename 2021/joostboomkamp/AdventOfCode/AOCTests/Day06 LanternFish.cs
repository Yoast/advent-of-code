using FluentAssertions;
using NUnit.Framework;
using Puzzles;
using Puzzles.Input;

namespace AOCTests;
public class Day06_LanternFish
{

    [SetUp]
    public void Setup()
    {
    }

    [Test]
    public void Test_Day06_80Days()
    {
        // arrange
        var input = InputDay06.Example;
        var cut = new School(input);

        // act
        cut.Tick(80);

        // assert
        cut.Count.Should().Be(5934);
    }

    [Test]
    public void Test_Day06_80Days_puzzle()
    {
        // arrange
        var input = InputDay06.Puzzle;
        var cut = new School(input);

        // act
        cut.Tick(80);

        // assert
        cut.Count.Should().Be(371379);
    }

    [Test]
    public void Test_Day06_256Days_example()
    {
        // arrange
        var input = InputDay06.Puzzle;
        var cut = new School(input);

        // act
        cut.Tick(255);

        // assert
        cut.Count.Should().Be(1525126772942L);
    }

    [Test]
    public void Test_Day06_256Days_puzzle()
    {
        // arrange
        var input = InputDay06.Puzzle;
        var cut = new School(input);

        // act
        cut.Tick(256);

        // assert
        cut.Count.Should().Be(1674303997472L);
    }
}
