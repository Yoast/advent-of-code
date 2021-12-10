using FluentAssertions;
using NUnit.Framework;
using Puzzles;
using Puzzles.Tools;
using System.Linq;

namespace AOCTests
{
    public class Day09Tests
    {

        [Test]
        public void TestExample()
        {
            // arrange
            var map = new HeatMap(InputDay09.Example);

            // act
            var list = map.DeepestLevels().ToArray();

            // assert
            list.Count().Should().Be(4);
            list.Sum().Should().Be(11);
            list.Risk().Should().Be(15);
        }

        [Test]
        public void TestPuzzle()
        {
            // arrange
            var map = new HeatMap(InputDay09.Puzzle);

            // act
            var list = map.DeepestLevels().ToArray();

            // assert
            list.Count().Should().Be(251);
            list.Sum().Should().Be(329);
            list.Risk().Should().Be(580);
        }

        [Test]
        public void TestExample2()
        {
            // arrange
            var map = new HeatMap(InputDay09.Example);

            // act
            var basins = map.Basins();

            // assert
            basins.Should().Be(1134);
        }

        [Test]
        public void TestPuzzle2()
        {
            // arrange
            var map = new HeatMap(InputDay09.Puzzle);

            // act
            var basins = map.Basins();

            // assert
            basins.Should().Be(856716);
        }
    }
}
