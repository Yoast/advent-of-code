namespace Puzzles;

public class School
{
    protected Dictionary<int, long> Fish = new Dictionary<int, long>{
        {  0, 0 },
        {  1, 0 },
        {  2, 0 },
        {  3, 0 },
        {  4, 0 },
        {  5, 0 },
        {  6, 0 },
        {  7, 0 },
        {  8, 0 },
    };

    public long Count => Fish.Values.Sum();
    public int Time { get; protected set; } = 0;

    public School(byte[] input)
    {
        foreach (var value in input)
        {
            Fish[value] += 1;
        }
    }

    public void Tick(int count = 1)
    {
        for (var t = 0; t < count; t++)
        {
            ++Time;

            // count the new fish
            var spawn = Fish[0];

            for(var index = 0; index < Fish.Keys.Count-1; index++)
            {
                Fish[index] = Fish[index + 1];
            }
            Fish[6] += spawn; // reset those who gave birth
            Fish[8] = spawn; // create new spawn
        }
    }
}
