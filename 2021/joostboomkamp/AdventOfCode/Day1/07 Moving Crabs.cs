namespace Puzzles;
public class Crabs
{
    int[] startingPositions { get; set; }
    Dictionary<int, int> Frequency { get; set; }

    private Dictionary<int, int> StepCost { get; set; } = new Dictionary<int, int>
    {
        { 0, 0 },
        { 1, 1 }
    };

    public Crabs(int[] input)
    {
        startingPositions = input;
        Frequency = startingPositions.Distinct().ToDictionary(x => x, x => startingPositions.Count(p => p == x));
        GenerateCost(0, 1000);
    }

    private void GenerateCost(int from, int to)
    {
        for(var i = from; i <= to; i++)
        {
            if (StepCost.ContainsKey(i))
                continue;

            if (!StepCost.ContainsKey(i - 1))
            {
                // prefill
                GenerateCost(2, i);
            }

            StepCost.Add(i, StepCost[i - 1] + i);
        }
    }

    public Tuple<int, int> FindCheapestMove()
    {
        var costPerPosition = startingPositions.Distinct().ToDictionary(x => x, x => 0);

        foreach (var target in startingPositions) // min to max? maybe all have to move?
        {
            var eachCost = startingPositions.Distinct()
                .ToDictionary(x => x, x => Math.Abs(target - x));
            costPerPosition[target] =
                eachCost.Select(cost => cost.Value * Frequency[cost.Key]).Sum();
        }

        var minPair = costPerPosition.OrderBy(x => x.Value).First();
        return Tuple.Create(minPair.Key, minPair.Value);
    }

    public Tuple<int, int> FindCheapestMove2()
    {
        var costFromPosition = new Dictionary<int,int>();
        var max = startingPositions.Max();
        for (var toTarget = 0; toTarget < max; toTarget++)
        {
            var eachCostToTarget = startingPositions.Distinct()
                .ToDictionary(x => x, x => GetStepCost(Math.Abs(toTarget - x)));
            costFromPosition.Add(toTarget, 
                eachCostToTarget.Select(cost => cost.Value * Frequency[cost.Key]).Sum());
        }

        var minPair = costFromPosition.OrderBy(x => x.Value).First();
        return Tuple.Create(minPair.Key, minPair.Value);
    }

    public int GetStepCost(int number)
    {
        if (!StepCost.ContainsKey(number))
        {
            // prefill up to number
            GenerateCost(2, number);
        }
            
        return StepCost[number];
    }
}
