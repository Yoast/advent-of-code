namespace Puzzles;

public class SyntaxScorer
{
	protected Dictionary<char, char> ChunkDelimiters = new Dictionary<char, char> {
		{ '(', ')' },
		{ '[', ']' },
		{ '{', '}' },
		{ '<', '>' },
	};

	protected Dictionary<char, int> Scores = new Dictionary<char, int> {
		{ ')', 3 },
		{ ']', 57 },
		{ '}', 1197 },
		{ '>', 25137 },
	};

	protected Dictionary<char, int> acScores = new Dictionary<char, int> {
		{ ')', 1 },
		{ ']', 2 },
		{ '}', 3 },
		{ '>', 4 },
	};

	public long ScoreLine(string line)
	{
		var expectedClosing = new Stack<char>();
		var expected = ' ';
		foreach (var x in line)
		{
			if (ChunkDelimiters.ContainsKey(x))
			{
				expectedClosing.Push(expected);
				expected = ChunkDelimiters[x];
			}
			else
			{
				if (x == expected)
				{
					expected = expectedClosing.Pop();
					continue;
				}			

				return Scores[x];
			}
		}
		return 0;
	}

	public long AutoComplete(string line)
    {
		var expectedClosing = new Stack<char>();
		var expected = ' ';
		foreach (var x in line)
		{
			if (ChunkDelimiters.ContainsKey(x))
			{
				expectedClosing.Push(expected);
				expected = ChunkDelimiters[x];
			}
			else
			{
				if (x == expected)
				{
					expected = expectedClosing.Pop();
				}
				else
				{
					Console.Error.WriteLine("lolwut");
				}
			} 
		}
		expectedClosing.Push(expected);

		var completors = expectedClosing.Reverse().Skip(1).Reverse().ToArray();
		long score = 0;
		foreach (var c in completors)
		{
			score = 5 * score + acScores[c];
		}
		return score;
	}

	public long ScoreScript(string[] script)
	{
		return script.Sum(line => ScoreLine(line));
	}

	public long AutoCompleteScript(string[] input)
	{
		var incompleteLines = input.Where(line => ScoreLine(line) == 0).ToList();
		
		var scores = incompleteLines.Select(AutoComplete).ToList();

		scores.Sort();
		var index = (scores.Count - 1) / 2;
		return scores[index]; // 1274121 // 379529966
	}
}