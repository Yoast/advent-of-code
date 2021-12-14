using Puzzles.Tools;
using System.Diagnostics;
using System.Text;

namespace Puzzles;
public class Polymerization 
{
    public string Chain { get; private set; }

    public Dictionary<string, string> Rules { get; private set; }
    public Dictionary<string, string[]> SplitterRules { get; private set; }
    public Polymerization(string input)
    {
        var parts = input.ReplaceLineEndings().Split(Environment.NewLine);
        Chain = parts[0];
        Rules = parts.Skip(2)
            .Select(p => p.Split(" -> "))
            .ToDictionary(p => p[0], p => p[0][0] + p[1]); // HC -> B becomes HC -> HB
        SplitterRules = parts.Skip(2)
            .Select(p => p.Split(" -> "))
            .ToDictionary(p => p[0], p => new string[] { p[0][0] + p[1].ToString(), p[1].ToString() + p[0][1] }); 
        // HC => B becomes HC -> HB, CB
    }

    public Dictionary<string, int> Count =>
        Chain.ToCharArray()
             .GroupBy(element => element.ToString())
             .ToDictionary(element => element.Key, elements => elements.Count());

    public int CheckSum
    {
        get
        {
            var count = Count;
            return Count.Values.Max() - Count.Values.Min();
        }
    }

    public string Step(int count = 1)
    {
        var max = Int32.MaxValue -1;
        var oldChain = new StringBuilder(max, max);
        oldChain.Append(Chain);

        var newChain = new StringBuilder(max, max);
        var sw = new Stopwatch();

        for (var step = 0; step < count; step++)
        {
            sw.Restart();
            for (var i = 0; i < oldChain.Length - 1; i++)
            {
                string key = new string(new[] { oldChain[i], oldChain[i + 1] });
                if (Rules.TryGetValue(key, out var value))
                {
                    newChain.Append(value);
                }
                else
                {
                    // rule not found
                    Debugger.Break();
                    continue;
                }
            }
            newChain.Append(oldChain[oldChain.Length - 1]);

            sw.Stop();
            Write(step, sw.Elapsed.Seconds, newChain);

            oldChain.Clear();
            oldChain.Append(newChain);
            newChain.Clear();
        }
        Chain = oldChain.ToString();
        return Chain;
    }

    public string[] Inject(string pair)
    {
        return SplitterRules[pair];
    }

    public long Run(int count) {
        var pairs = Chain.ToPairs();
        
        var newPairs = new List<string>();

        var pairCount = pairs.GroupBy(p => p).ToDictionary(p => p.Key, p => p.Count());
        for(var i = 0; i < count; i++)
        {
            foreach(var pair in pairs)
            {
                var split = Inject(pair);
                newPairs.AddRange(split);
                foreach(var newPair in split)
                {
                    if (!pairCount.ContainsKey(newPair))
                    {
                        pairCount.Add(newPair, 0);
                    }

                    // each new pair occurs as often as the old pair existed in the chain
                    // we don't need to calculate every pair, just the distinct ones.
                    if (pairCount.TryGetValue(pair, out var multiplier))
                    {
                        pairCount[newPair] += multiplier;
                    } else
                        pairCount[newPair] += 1;
                }
            }
            pairs = newPairs.ToArray();
            newPairs.Clear();
        }

        var allItems = pairs.Select(x => x[0]);
        var counts = allItems
            .GroupBy(x => x)
            .ToDictionary(x => x.Key, x => x.Count());
        
        var last = pairs.Last().Last();
        counts[last] += 1;

        return counts.Values.Max() - counts.Values.Min();
    }

    private static void Write(int step, int t, StringBuilder sb)
    {
        using (var writer = File.AppendText(@"C:\Git\advent-of-code\2021\joostboomkamp\AdventOfCode\day14.log"))
        {
            writer.Write(step);
            writer.WriteLine($" @{DateTime.Now}, t={t}s");
            writer.WriteLine(sb.ToString());
            writer.WriteLine();
            writer.Flush();
        }
    }
}
