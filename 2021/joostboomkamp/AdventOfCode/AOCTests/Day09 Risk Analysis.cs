using FluentAssertions;
using NUnit.Framework;
using Puzzles;
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
            var list = map.CoolestSpots().ToArray();

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
            var list = map.CoolestSpots().ToArray();

            // assert
            list.Count().Should().Be(251);
            list.Sum().Should().Be(329);
            list.Risk().Should().Be(580);
        }


    }
}
