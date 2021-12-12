using FluentAssertions;
using NUnit.Framework;
using Puzzles;
using Puzzles.Tools;
namespace AOCTests;
public class Day11Tests
{
    [Test]
    public void ShouldFlash()
    {
        // arrange
        var input = @"11111
19991
19191
19991
11111".SplitByLine();
        var cut = new LuminescentOctopi(input);
        // act
        cut.Step();
        var result = cut.ToString();
        result.Should().Be(@"34543
40004
50005
40004
34543");
        cut.Step();
        result = cut.ToString();
        result.Should().Be(@"45654
51115
61116
51115
45654");
    }

    [TestCase(1, @"6594254334
3856965822
6375667284
7252447257
7468496589
5278635756
3287952832
7993992245
5957959665
6394862637")]
    [TestCase(2, @"8807476555
5089087054
8597889608
8485769600
8700908800
6600088989
6800005943
0000007456
9000000876
8700006848")]
    [TestCase(3, @"0050900866
8500800575
9900000039
9700000041
9935080063
7712300000
7911250009
2211130000
0421125000
0021119000")]
    [TestCase(4, @"2263031977
0923031697
0032221150
0041111163
0076191174
0053411122
0042361120
5532241122
1532247211
1132230211")]
    public void Example(int steps, string expected)
    {
        var input = InputDay11.Example;
        var cut = new LuminescentOctopi(input);

        var result = cut.Steps(steps);
        
        cut.ToString().Trim().Should().Be(expected.ReplaceLineEndings());
    }

    [Test]
    public void Example()
    {
        var input = InputDay11.Example;
        var cut = new LuminescentOctopi(input);

        var result = cut.Steps(100);
        result.Should().Be(1656);
    }

    [Test]
    public void Puzzle()
    {
        var input = InputDay11.Puzzle;
        var cut = new LuminescentOctopi(input);

        var result = cut.Steps(100);
        result.Should().Be(1640);
    }


    [Test]
    public void Example2()
    {
        var input = InputDay11.Example;
        var cut = new LuminescentOctopi(input);

        var step = 0;
        var flashing = 0;
        do
        {
            flashing = cut.Step();
            step++;
        } while (flashing < 100);

        step.Should().Be(195);
    }

    [Test]
    public void Puzzle2()
    {
        var input = InputDay11.Puzzle;
        var cut = new LuminescentOctopi(input);

        var step = 0;
        var flashing = 0;
        do
        {
            flashing = cut.Step();
            step++;
        } while (flashing < 100);

        step.Should().Be(312);
    }
}
