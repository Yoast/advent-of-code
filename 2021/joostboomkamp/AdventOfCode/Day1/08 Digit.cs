using Puzzles.Input;

namespace Puzzles;

[Flags]
public enum Position : byte
{
	None = 0,
	Top = 1,
	TopLeft = 2,
	TopRight = 4,
	Center = 8,
	BottomLeft = 16,
	BottomRight = 32,
	Bottom = 64,
}

public class Digits
{
	public string[] Example = new[] { "be cfbegad cbdgef fgaecd cgeb fdcge agebfd fecdb fabcd edb | fdgacbe cefdb cefbgd gcbe",
	"edbfga begcd cbg gc gcadebf fbgde acbgfd abcde gfcbed gfec | fcgedb cgb dgebacf gc",
	"fgaebd cg bdaec gdafb agbcfd gdcbef bgcad gfac gcb cdgabef | cg cg fdcagb cbg",
	"fbegcd cbd adcefb dageb afcb bc aefdc ecdab fgdeca fcdbega | efabcd cedba gadfec cb",
	"aecbfdg fbg gf bafeg dbefa fcge gcbea fcaegb dgceab fcbdga | gecf egdcabf bgf bfgea",
	"fgeab ca afcebg bdacfeg cfaedg gcfdb baec bfadeg bafgc acf | gebdcfa ecba ca fadegcb",
	"dbcfg fgd bdegcaf fgec aegbdf ecdfab fbedc dacgb gdcebf gf | cefg dcbef fcge gbcadfe",
	"bdfegc cbegaf gecbf dfcage bdacg ed bedf ced adcbefg gebcd | ed bcgafe cdgba cbgef",
	"egadfb cdbfeg cegd fecab cgb gbdefca cg fgcdab egfdb bfceg | gbdfcae bgc cg cgb",
	"gcafb gcf dcaebfg ecagb gf abcdeg gaef cafbge fdbac fegbdc | fgae cfgab fg bagce" };

	private Dictionary<int, Position> Map = new Dictionary<int, Position>
	{
		{ 0, Position.Top | Position.TopLeft | Position.TopRight | Position.BottomLeft | Position.BottomRight | Position.Bottom },
		{ 1, Position.TopRight | Position.BottomRight },
		{ 2, Position.Top | Position.TopRight | Position.Center | Position.BottomLeft | Position.Bottom },
		{ 3, Position.Top | Position.TopRight | Position.Center | Position.BottomRight | Position.Bottom },
		{ 4, Position.TopLeft | Position.Center | Position.TopRight | Position.BottomRight },
		{ 5, Position.Top | Position.TopLeft | Position.Center | Position.BottomRight | Position.Bottom },
		{ 6, Position.Top | Position.TopLeft | Position.Center  | Position.BottomLeft | Position.BottomRight | Position.Bottom },
		{ 7, Position.Top | Position.TopRight | Position.BottomRight },
		{ 8, Position.Top | Position.TopLeft | Position.TopRight | Position.Center | Position.BottomLeft | Position.BottomRight | Position.Bottom },
		{ 9, Position.Top | Position.TopLeft | Position.TopRight | Position.Center | Position.BottomRight | Position.Bottom }
	};

	public void Run()
	{
		var count = Run(InputDay08.Example);
		Console.WriteLine(count);
		count = Run(InputDay08.Puzzle);
		Console.WriteLine(count);
	}

	public int Run(string[] input)
	{
		var output = new List<string[]>();
		foreach (var line in input)
		{
			output.Add(line.Split(" | ")[1].Split(" "));
		}
		var lengths = new[] { 2, 3, 4, 7 };
		return output.Sum(o => o.Count(digit => lengths.Contains(digit.Length)));
	}

	public int Solve(string input)
    {

		//  5:      6:      7:      8:      9:
		// aaaa		aaaa    aaaa	aaaa    aaaa
		// b	.	b	.	.	c	b	c	b	c
		// b	.	b	.	.	c	b	c	b	c
		// dddd		dddd	....	dddd	dddd
		// .	f	e	f	.	f	e   f	.	f
		// .	f	e	f	.	f	e	f	.	f
		//  gggg     gggg	 ....	gggg	 gggg

		// 0 ABCEFG      6  
		// 1 CF     2
		// 2 ACDEG     5
		// 3 ACDEG     5
		// 4 BCDF     4
		// 5 ABDFG     5
		// 6 ABDEFG      6
		// 7 ACF     3
		// 8 ABCDEFG      7 
		// 9 ABCDFG      6

		// 1 uses two segments; CF                                                                       CF/FC
		// 7 uses 3 segments; the one that's extra from a 1 is A                                            A
		// 8 uses 7 segments;
		// 0 uses 6 segments; the one that's not in the 7, not in the supposed 0, but is in the 8, is D     D
		// 4 uses 4 segments; the one that's not in 1 and not D is B                                        B
		// 9 has ABCDF and G                                                                                G
		// 5 uses 5 segments; ABDFG                                                                         F C
		// remaining segment is E                                                                           E

		var split = input.Split(" | ");
		var digits = split[0].Split(" ").ToList();
		var codeToDecipher = split[1].Split(" ");

		var known = new Dictionary<Position, char>();

		// 1 => right side is good_enough_for_now.
		var oneParts = digits.Single(one => one.Length == 2);
		
		// 7 => Top is known
		var sevenParts = digits.Single(seven => seven.Length == 3);
		known[Position.Top] = sevenParts.Single(c => !oneParts.Contains(c));

		// 8 => all parts
		var eightParts = digits.Single(seven => seven.Length == 7);

		// 4
		var fourParts = digits.Single(four => four.Length == 4);

		// 0 => Center is known; it is the one character in the four
		// that the 0 doesn't have, and it is in the eight, but not the zero.
		var zeroParts = digits.Single(zero => zero.Length == 6 &&
			sevenParts.All(c => zero.Contains(c)) &&
			fourParts.Count(c => !zero.Contains(c)) == 1);
		known[Position.Center] = eightParts.Single(c => !zeroParts.Contains(c));

		// 4 => TopLeft is known
		//var fourParts = digits.Single(x => x.Length == 4);
		known[Position.TopLeft] = 
			fourParts.Single(c => !oneParts.Contains(c) && 
				c != known[Position.Center]);

		// 9 => all known parts AND bottom; 
		var nineParts = digits.Where(nine =>
			nine.Length == 6 &&
			oneParts.All(c => nine.Contains(c)) && // the 6 has just the topleft of the oneParts.
			known.Values.All(c => nine.Contains(c))).First();
		// Bottom is known;
		known[Position.Bottom] = 
			nineParts.Single(c => 
				!oneParts.Contains(c) && 
				!known.Values.Contains(c));

		var fiveTargets = new [] { known[Position.Top], known[Position.Bottom], known[Position.Center], known[Position.TopLeft] };
		var fiveParts = digits.Single(x =>
			x.Length == 5 &&
			((x.Contains(oneParts[0]) && !x.Contains(oneParts[1])) ||
			 (x.Contains(oneParts[1]) && !x.Contains(oneParts[0]))) &&
			fiveTargets.All(t => x.Contains(t)));
		// BottomRight is known; Top Right is known;
		var b_r_index = fiveParts.Contains(oneParts[0])
			? 0 : 1;
		known[Position.BottomRight] = oneParts[b_r_index];
		known[Position.TopRight] = oneParts[1 - b_r_index];

		// last remaining = bottomleft
		known[Position.BottomLeft] = eightParts.Single(x => !known.Values.Contains(x));

		// transform the known map to numbers;
		var output = new List<int>(4);

		// every position now has a corresponding character. we need to swap values and keys.
		var characterMap = known.ToDictionary(x => x.Value, x => x.Key);
		foreach (var code in codeToDecipher)
        {
			var id = Position.None;
			foreach(var c in code)
            {
				id |= characterMap[c];
            }
			var match = Map.Single(x => x.Value == id);
			output.Add(match.Key);
        }

		return 1000 * output[0] + 100 * output[1] + 10 * output[2] + output[3];
	}
}