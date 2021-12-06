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

public class School2
{
    protected List<Queue<byte>> Fish { get; set; } = new List<Queue<byte>> ();
    private static int limit = 10000000;

    public long Count => Fish.Sum(q => q.Count);

    public int Time { get; protected set; } = 0;

    public School2(byte[] input)
    {
        var fishQ = new Queue<byte> (input);
        Fish.Add(fishQ);
    }

    public override string ToString()
    {
        if (Fish[0].Count > 100)
            return "too much data";

        var items = Fish[0].ToArray();
        return "After " + string.Format("{0,4}", Time) + " days: " + string.Join(", ", items);
        Fish[0] = new Queue<byte>(items);
    }

    public void Tick(int count = 1)
    {
        for (var t = 0; t < count; t++)
        {
            Time++;

            var newFish = new List<Queue<byte>>();

            var fishes = Fish.Count;
            for (var q =0; q < fishes; q++)
            {
                var newQ = new Queue<byte>();
                var spawnCount = 0;

                do
                {
                    var f = Fish[q].Dequeue();
                    if (f == 0)
                    {
                        newQ.Enqueue(6);
                        spawnCount++;
                    }
                    else
                    {
                        newQ.Enqueue(--f);
                    }
                } while (Fish[q].Any());

                if (newQ.Count > limit)
                {
                    newFish.Add(newQ);
                    newQ = new Queue<byte>();
                }

                for (var i = 0; i < spawnCount; i++)
                {
                    newQ.Enqueue(8);
                }
                newFish.Add(newQ);
            }
            Fish = newFish;
        }
    }

            //    // count new spawn
            //    var spawn = 0;
            //foreach(var fish in Fish)
            //{
            //    for(var i = 0; i < fish.Count; i++)
            //    {
            //        if (fish[i] == 0)
            //        {
            //            spawn++;
            //            fish[i] = 6;
            //        } else
            //        {
            //            fish[i] -= 1;
            //        }
            //    }
            //}

            //// find a list to spawn to
            //for(var i = 0; i < Fish.Count; i++)
            //{
            //    if (Fish[i].Count < limit)
            //    {
            //        Spawn(i, spawn);
            //        spawn = 0;
            //    }               
            //}
            //if (spawn > 0)
            //{
            //    Fish.Add(new List<byte>(size));
            //    Spawn(Fish.Count-1, spawn);
            //}
            //}

    //private void Spawn(int pool, int count)
    //{
    //    for (var i = 0; i < count; i++)
    //    {
    //        Fish[pool].Add(8);
    //    }
    //}
}
