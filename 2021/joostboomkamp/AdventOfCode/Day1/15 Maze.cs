using Puzzles.Tools;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Puzzles;
public class Maze
{
    public Matrix2d<int> Map { get; private set; }
    private Matrix2d<int> TotalCostMap { get; set; }

    public Maze(string[] input, bool WTFReallyMan = false)
    {
        Map = Matrix2d<int>.ParseInt(input);

        if ( WTFReallyMan)
        {
            Map = Bloat5x5(Map);
        }
        
        TotalCostMap = new Matrix2d<int>(Map.Width, Map.Height, int.MaxValue);
        Map[0, 0] = 0; // does not cost anything

        CalculateCost();

        Console.WriteLine(TotalCostMap.ToString());
    }

    private Matrix2d<int> Bloat5x5(Matrix2d<int> originalMap)
    {
        var incrementmap = new[] { "01234", "12345", "23456", "34567", "45678" };
        var lookupTable = Matrix2d<int>.ParseInt(incrementmap);

        var bloatedMap = new Matrix2d<int>(originalMap.Width * 5, originalMap.Height * 5);
        for (var y = 0; y < originalMap.Height; y++)
        {
            for (var x = 0; x < originalMap.Width; x++)
            {
                bloatedMap[x, y] = originalMap[x, y];
                // for each x,y in the original grid, extrapolate
                // 5x5 values in the resulting grid, multiplied and increased.
                for (var multiY = 1; multiY < 5; multiY++)
                {
                    for (var multiX = 1; multiX < 5; multiX++)
                    {
                        var delta = lookupTable[multiX, multiY];

                        var score = originalMap[x, y] + delta;
                        if (score > 9)
                            score -= 9;

                        var newX = originalMap.Width * multiX + x;
                        var newY = originalMap.Height * multiY + y;
                        bloatedMap[newX, newY] = score;
                    }
                }
            }
        }
        if (bloatedMap.Any(x => x == 0 || x > 9))
        {
            throw new Exception();
        }

        return bloatedMap;
    }

    public void CalculateCost()
    {
        //*1*1 6 3 7 5 1 7 4 2
        //*1*3 8 1 3 7 3 6 7 2 
        //*2*1*3*6*5*1*1*3 2 8
        // 3 6 9 4 9 3*1*5*6 9
        // 7 4 6 3 4 1 7*1*1 1
        // 1 3 1 9 1 2 8*1*3*7
        // 1 3 5 9 9 1 2 4*2*1
        // 3 1 2 5 4 2 1 6*3*9
        // 1 2 9 3 1 3 8 5*2*1*
        // 2 3 1 1 9 4 4 5 8*1*
        var w = Map.Width - 1;
        var h = Map.Height - 1;
        var bottomRightCost = Map[w, h];

        // bottom right corner
        TotalCostMap[w, h] = bottomRightCost;
        TotalCostMap[w - 1, h] = bottomRightCost + Map[w - 1, h];
        TotalCostMap[w, h - 1] = bottomRightCost + Map[w, h - 1];
        TotalCostMap[w - 1, h - 1] = Map[w - 1, h - 1] + Math.Min(TotalCostMap[w, h - 1], TotalCostMap[w - 1, h]);

        // bottom line
        for (var x = w - 2; x >= 0; x--)
        {
            TotalCostMap[x, h] = TotalCostMap[x + 1, h] + Map[x, h];
        }
        // right line
        for (var y = h - 2; y >= 0; y--)
        {
            TotalCostMap[w, y] = TotalCostMap[w, y + 1] + Map[w, y];
        }
        // playing field UP/RTL
        for (var y = h - 1; y >= 0; y--) {
            for (var x = w - 1; x >= 0; x--)
            {
                var right = TotalCostMap[x + 1, y];
                var down = TotalCostMap[x, y + 1];
                var score = Math.Min(right, down);
                TotalCostMap[x, y] = Map[x,y] + score;
            }
        }
    }

    public int GetCheapestPathToExit(int x, int y)
    {
        //*1*1 6 3 7 5 1 7 4 2
        //*1*3 8 1 3 7 3 6 7 2 
        //*2*1*3*6*5*1*1*3 2 8
        // 3 6 9 4 9 3*1*5*6 9
        // 7 4 6 3 4 1 7*1*1 1
        // 1 3 1 9 1 2 8*1*3*7
        // 1 3 5 9 9 1 2 4*2*1
        // 3 1 2 5 4 2 1 6*3*9
        // 1 2 9 3 1 3 8 5*2*1*
        // 2 3 1 1 9 4 4 5 8*1*
        if (TotalCostMap[x, y] < int.MaxValue)
        {
            return TotalCostMap[x, y];
        }

        var right = x < Map.Width-1 ? TotalCostMap[x + 1, y    ] : int.MaxValue;
        var down = y < Map.Height-1 ? TotalCostMap[x    , y + 1] : int.MaxValue;
        return Map[x, y] + Math.Min(right, down);
    }
}
