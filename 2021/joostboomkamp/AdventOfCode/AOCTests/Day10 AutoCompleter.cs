using FluentAssertions;
using NUnit.Framework;
using Puzzles;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AOCTests;
public class Day10Tests
{
    [Test]
    public void DoExample()
    {
        new SyntaxScorer().ScoreScript(InputDay10.Puzzle).Should().Be(215229);
    }

    [Test]
    public void Example2()
    {
        var cut = new SyntaxScorer();
        var score = cut.AutoComplete("<{([{{}}[<[[[<>{}]]]>[]]");

        score.Should().Be(294);

        var result = cut.AutoCompleteScript(InputDay10.Example);

        result.Should().Be(288957);
    }

    [Test]
    public void Puzzle2()
    {
        var cut = new SyntaxScorer();

        var result = cut.AutoCompleteScript(InputDay10.Puzzle);

        result.Should().Be(1105996483L);
    }
}
