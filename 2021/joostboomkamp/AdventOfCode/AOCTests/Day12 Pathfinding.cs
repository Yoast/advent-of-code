using FluentAssertions;
using NUnit.Framework;
using Puzzles;
using System.Collections.Generic;
using System.Linq;

namespace AOCTests;
public class Day12Tests
{
    [Test]
    public void Example()
    {
        var input = InputDay12.Example1;
        var cut = new Pathfinding(input);

        var paths = cut.GetPaths();
        cut.PathCount.Should().Be(10);
    }

    [Test]
    public void Example2()
    {
        var input = InputDay12.Example2;
        var cut = new Pathfinding(input);

        var paths = cut.GetPaths();
        cut.PathCount.Should().Be(19);
    }

    [Test]
    public void Example3()
    {
        var input = InputDay12.Example3;
        var cut = new Pathfinding(input);

        var paths = cut.GetPaths();
        cut.PathCount.Should().Be(226);
    }

    [Test]
    public void Puzzle1()
    {
        var input = InputDay12.Puzzle;
        var cut = new Pathfinding(input);

        var paths = cut.GetPaths();
        cut.PathCount.Should().Be(3761);
    }

    [Test]
    public void TestPartial()
    {
        var input = InputDay12.Example1;
        var cut = new Pathfinding(input);
        cut.AllowRevisit = true;
        
        var result = cut.PathsFrom(cut.Caves["A"], new List<string> { "A", "b", "d", "b" });
        result.Count().Should().Be(2);
    }

    [Test]
    public void Example2_1()
    {
        var input = InputDay12.Example1;
        var cut = new Pathfinding(input);
        cut.AllowRevisit = true;

        var paths = cut.GetPaths();
        cut.PathCount.Should().Be(36);
    }

    [Test]
    public void Example2_2()
    {
        var input = InputDay12.Example2;
        var cut = new Pathfinding(input);
        cut.AllowRevisit = true;

        var paths = cut.GetPaths();
        cut.PathCount.Should().Be(108);
    }
}
