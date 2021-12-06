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
    public void Test_Day06_Example()
    {
        // arrange
        var input = InputDay06.Example;
        var cut = new School2(input);

        // act
        cut.Tick();

        // assert
        cut.ToString().Should().Be("After    1 days: 2, 3, 2, 0, 1");
    }

    [Test]
    public void Test_Day06_Spawn()
    {
        // arrange
        var input = InputDay06.Example;
        var cut = new School2(input);

        // act
        cut.Tick(4);

        // assert
        cut.ToString().Should().Be("After    4 days: 6, 0, 6, 4, 5, 6, 7, 8, 8");
    }

    [Test]
    public void Test_Day06_Spawn_18()
    {
        // arrange
        var input = InputDay06.Example;
        var cut = new School2(input);

        // act
        cut.Tick(18);

        // assert
        cut.ToString().Should().Be("After   18 days: 6, 0, 6, 4, 5, 6, 0, 1, 1, 2, 6, 0, 1, 1, 1, 2, 2, 3, 3, 4, 6, 7, 8, 8, 8, 8");
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
        cut.Count.Should().Be(1525126772942L);//26984457539L);
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
