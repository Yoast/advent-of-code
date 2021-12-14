using FluentAssertions;
using NUnit.Framework;
using Puzzles;
using System.Collections.Generic;
using System.Linq;

namespace AOCTests;
public class Day13Tests
{
    [Test]
    public void Example()
    {
        var input = InputDay13.Example;
        var cut = new Origami(input);

        var sheet = cut.Fold(0, 7);
        sheet.ToString("").Trim().Should().Be(@"#.##..#..#.
#...#......
......#...#
#...#......
.#.#..#.###
...........
...........");
        cut.Dots.Should().Be(17);
    }

    [Test]
    public void Example_twofold()
    {
        var input = InputDay13.Example;
        var cut = new Origami(input);

        var sheet = cut.Fold(0, 7);
        sheet = cut.Fold(5, 0);
        sheet.ToString("").Trim().Should().Be(@"#####
#...#
#...#
#...#
#####
.....
.....");
        cut.Dots.Should().Be(16);
    }

    [Test]
    public void Example_Puzzle()
    {
        var input = InputDay13.Puzzle;
        var cut = new Origami(input);

        var sheet = cut.Fold(cut.Instructions[0]);
        var dots = cut.Dots;

        dots.Should().Be(743);
    }


    [Test]
    public void Example_Puzzle2()
    {
        var input = InputDay13.Puzzle2;
        var cut = new Origami(input);

        cut.Fold(cut.Instructions[0]);
        cut.Fold(cut.Instructions[1]);
        cut.Fold(cut.Instructions[2]);
        cut.Fold(cut.Instructions[3]);
        cut.Fold(cut.Instructions[4]);
        cut.Fold(cut.Instructions[5]);
        cut.Fold(cut.Instructions[6]);
        cut.Fold(cut.Instructions[7]);
        cut.Fold(cut.Instructions[8]);
        cut.Fold(cut.Instructions[9]);
        cut.Fold(cut.Instructions[10]);
        var sheet = cut.Fold(cut.Instructions[11]);
        sheet.ToString(" ").ReplaceLineEndings().Should().Be(
@"# # # . . . # # . . # # # . . # . . . . . # # . . # . . # . # . . # . # . . . . 
# . . # . # . . # . # . . # . # . . . . # . . # . # . # . . # . . # . # . . . . 
# . . # . # . . . . # . . # . # . . . . # . . # . # # . . . # # # # . # . . . . 
# # # . . # . . . . # # # . . # . . . . # # # # . # . # . . # . . # . # . . . . 
# . # . . # . . # . # . . . . # . . . . # . . # . # . # . . # . . # . # . . . . 
# . . # . . # # . . # . . . . # # # # . # . . # . # . . # . # . . # . # # # # . 
");
        var dots = cut.Dots;

        dots.Should().Be(94);
    }
}
