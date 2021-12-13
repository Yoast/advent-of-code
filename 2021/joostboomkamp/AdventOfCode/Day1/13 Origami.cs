using Puzzles.Tools;
using System.Drawing;

namespace Puzzles;
public class Origami
{
    private Matrix2d<string> Sheet;
    public string EmptyValue = ".";
    public string SetValue = "#";

    public List<Point> Instructions = new List<Point>();

    public Origami(string[] input)
    {
        var points = new List<Point>();
        for(var i = 0; i < input.Length; i++)
        {
            if (input[i].Contains(","))
            {
                var parts = input[i].Split(",").Select(int.Parse).ToArray();
                points.Add(new Point(parts[0],parts[1]));
            } else 
            if (input[i] == string.Empty)
            {
                var raw_instructions = input.Skip(i+1).ToArray(); // first empty?
                foreach (var instr in raw_instructions)
                {
                    var parts = instr.Replace("fold along ", "").Split("=");
                    var coord = int.Parse(parts[1]);
                    Instructions.Add(new Point((parts[0] == "x") ? coord : 0, (parts[0] == "y") ? coord : 0));
                }
                // we've read all lines now, despite i being stuck at the first empty line
                break;
            }
        }

        Sheet = new Matrix2d<string>(points.Max(p => p.X) + 1, points.Max(p => p.Y) + 1, EmptyValue);
        foreach (var p in points)
        {
            Sheet[p] = "#";
        }
    }

    public Matrix2d<string> Run()
    {
        
        return Sheet;
    }

    public string ToString(string separator = "")
    {
        return Sheet.ToString(separator);
    }

    public int Dots => Sheet.Count(x => x == SetValue);

    public Matrix2d<string> Fold(IEnumerable<Point> points) => 
        points.Select(p => Fold(p.X, p.Y)).Last();

    public Matrix2d<string> Fold(Point p) => 
        Fold(p.X, p.Y);

    public Matrix2d<string> Fold(int alongX = 0, int alongY = 0)
    {
        if ( alongX > 0 && alongY > 0 )
        {
            throw new ArgumentException("Cannot fold both at the same time without adding a diagonal reference");
        }

        Matrix2d<string> newSheet;

        Matrix2d<string> otherHalf;
        // fold up or left?
        if (alongY == 0)
        {
            // fold left

            // the target sheet is the left half of the existing sheet up to alongX
            newSheet = Sheet.Subset(0, 0, alongX, Sheet.Height);

            otherHalf = Sheet.Subset(alongX + 1, 0, Sheet.Width, Sheet.Height);
            otherHalf.FlipHorizontal();
        } else
        {
            // fold up
            
            // the target sheet is the top half of the existing sheet up to alongY
            newSheet = Sheet.Subset(0, 0, Sheet.Width, alongY);
            
            otherHalf = Sheet.Subset(0, alongY + 1, Sheet.Width, Sheet.Height);
            otherHalf.FlipVertical();
        }

        foreach (var p in otherHalf.Where(v => v != EmptyValue))
        {
            newSheet[p] = otherHalf[p];
        }
        Sheet = newSheet;

        return newSheet;
    }
}

