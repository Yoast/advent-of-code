namespace Puzzles;
public class School
{
    protected List<List<byte>> Fish { get; set; } = new List<List<byte>> (size);
    private static int limit = 1000;
    private static int size = 2000;

    public long Count() => Fish.Sum(c => c.Count);

    public int Time { get; protected set; } = 0;

    public School(byte[] input)
    {
        var newList = new List<byte> (size);
        newList.AddRange(input);
        Fish.Add(newList);
    }

    public override string ToString()
    {
        if (Fish[0].Count > limit) 
            return "too much data";
        return "After " + string.Format("{0,4}", Time) + " days: " + string.Join(", ", Fish[0].Select(x => x));
    }

    public void Tick(int count = 1)
    {
        for (var t = 0; t < count; t++)
        {
            Time++;

            var spawn = 0;
            foreach(var fish in Fish)
            {
                for(var i = 0; i < fish.Count; i++)
                {
                    if (fish[i] == 0)
                    {
                        spawn++;
                        fish[i] = 6;
                    } else
                    {
                        fish[i] -= 1;
                    }
                }
            }

            // find a list to spawn to
            for(var i = 0; i < Fish.Count; i++)
            {
                if (Fish[i].Count < limit)
                {
                    Spawn(i, spawn);
                } else
                {
                    if (i == Fish.Count - 1)
                    {
                        Fish.Add(new List<byte>(size));
                        Spawn(i + 1, spawn);
                    }
                }                
            }
        }
    }

    private void Spawn(int pool, int count)
    {
        for (var i = 0; i < count; i++)
        {
            Fish[pool].Add(8);
        }
    }
}
