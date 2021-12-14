using Puzzles.Tools;
using System.Drawing;

namespace Puzzles;
public class LuminescentOctopi
{
    protected Matrix2d<int> Map { get; set; }

    public LuminescentOctopi(string[] input)
    {
        Map = Matrix2d<int>.ParseInt(input);
    }

    public override string ToString()
    {
        return Map.ToString("").Trim();
    }

    public int Steps(int count)
    {
        var sum = 0;
        for (int i = 0; i < count; i++)
        {
            sum += Step();
        }
        return sum;
    }

    public int Step(int step = 1)
    {
        /*         
First, the energy level of each octopus increases by 1.

Then, any octopus with an energy level greater than 9 flashes. 
This increases the energy level of all adjacent octopuses by 1, including octopuses that are diagonally 
adjacent. If this causes an octopus to have an energy level greater than 9, it also flashes. This process 
    continues as long as new octopuses keep having their energy level increased beyond 9. 
    (An octopus can only flash at most once per step.)

Finally, any octopus that flashed during this step has its energy level set to 0, as it used all of its 
energy to flash.
         */
        // build energy
        for (var y = 0; y < Map.Height; y++)
        {
            for (var x = 0; x < Map.Width; x++)
            {
                Map[x, y] += step;
            }
        }

        // check if any are flashing, and propagate the energy
        var flashed = new List<Point>();
        do
        {
            var flashing = Map
                .Where(p => p > 9)
                .Where(p => !flashed.Contains(p));
            foreach (var point in flashing)
            {
                var area = Map.SurroundingPoints(point);
                foreach (var energizedPoint in area)
                {
                    Map[energizedPoint.X, energizedPoint.Y] += step;
                }
                flashed.Add(point);
            }
        } while (Map
                .Where(p => p > 9)
                .Where(p => !flashed.Contains(p)).Any());


        foreach (var point in flashed)
        {
            Map[point] = 0;
        }

        // reset
        var countFlashing = Map.Sum(p => p == 0); // flashed.Count
        return countFlashing;
    }
}
