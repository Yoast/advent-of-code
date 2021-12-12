using FluentAssertions;
using NUnit.Framework;
using Puzzles;

namespace AOCTests;
public class Day12Tests
{
    [Test]
    public void Example()
    {
        var input = InputDay12.Example1;
        var cut = new Pathfinding(input);

        var paths = cut.Paths();
        cut.PathCount.Should().Be(10);
    }

    [Test]
    public void Example2()
    {
        var input = InputDay12.Example2;
        var cut = new Pathfinding(input);

        var paths = cut.Paths();
        cut.PathCount.Should().Be(19);
    }

    [Test]
    public void Example3()
    {
        var input = InputDay12.Example3;
        var cut = new Pathfinding(input);

        var paths = cut.Paths();
        cut.PathCount.Should().Be(226);
    }

    [Test]
    public void Puzzle1()
    {
        var input = InputDay12.Puzzle;
        var cut = new Pathfinding(input);

        var paths = cut.Paths();
        cut.PathCount.Should().Be(1337);
    }
}
