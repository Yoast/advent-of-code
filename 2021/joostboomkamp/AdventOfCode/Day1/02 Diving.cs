using System.Drawing;

namespace Puzzles;

public enum Direction
{
    None = 0,
    Up = 1,
    Down = 2,
    Forward = 3,
    Backward = 4,
}

public class Step
{
    public Direction Direction { get; set; }
    public int Steps { get; set; }

    public static Step Parse(string input)
    {
        var parts = input.Split(' ');

        Direction direction = Direction.None;

        switch (parts[0].ToLower())
        {
            case "up": direction = Direction.Up; break;
            case "down": direction = Direction.Down; break;
            case "forward": direction = Direction.Forward; break;
            case "backward": direction = Direction.Backward; break;
        }

        return new Step
        {
            Direction = direction,
            Steps = int.Parse(parts[1])
        };
    }
}

public class Diving
{
    protected Point Location = new Point(0, 0);

    public int Result => Location.X * Location.Y;
    public int Aim { get; protected set; } = 0;

    public void Move(Step step)
    {
        switch (step.Direction)
        {
            case Direction.Up: Location.Y -= step.Steps; break;
            case Direction.Down: Location.Y += step.Steps; break;
            case Direction.Forward: Location.X += step.Steps; break;
            case Direction.Backward: Location.X -= step.Steps; break;
        }
    }

    public void Travel(Step step)
    {
        switch (step.Direction)
        {
            case Direction.Up: Aim -= step.Steps; break;
            case Direction.Down: Aim += step.Steps; break;
            case Direction.Forward:
                Location.X += step.Steps;
                Location.Y += Aim * step.Steps; break;
            case Direction.Backward:
                Location.X -= step.Steps;
                Location.Y -= Aim * step.Steps; break;
        }

    }
}
