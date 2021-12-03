using System.Text;

namespace Puzzles;
public class Diagnostics
{
    public DiagnosticReport GetPowerConsumption(string[] input)
    {
        var gamma = new StringBuilder();
        var epsilon = new StringBuilder();

        for (var index=0; index<input[0].Length; index++)
        {
            var zeroes = input.Count(line => line[index] == '0');
            var ones = input.Count(line => line[index] == '1');

            if (zeroes > ones)
            {
                gamma.Append('0');
                epsilon.Append('1');
            }
            else
            {
                gamma.Append('1');
                epsilon.Append('0');
            }

            zeroes = 0;
            ones = 0;
        }

        return new DiagnosticReport
        {
            GammaRate = Convert.ToInt32(gamma.ToString(), 2),
            EpsilonRate = Convert.ToInt32(epsilon.ToString(), 2),
        };
    }

    public DiagnosticReport GetLifeSupportRating(string[] input)
    {
        var oxygenGeneratorRatingBits = FilterByBitCriteria(input);
        var CO2ScrubberRatingBits = FilterByBitCriteria(input, false, 0);


        return new DiagnosticReport
        {
            OxygenGeneratorRating = Convert.ToInt32(oxygenGeneratorRatingBits.ToString(), 2),
            CO2ScrubberRating = Convert.ToInt32(CO2ScrubberRatingBits.ToString(), 2),
        };
    }

    protected string FilterByBitCriteria(string[] input, bool findMostCommon = true, int index=0)
    {
        var zeroes = input.Count(line => line[index] == '0');
        var ones = input.Count(line => line[index] == '1');

        var mostCommon = zeroes > ones ? '0' : '1';
        var leastCommon = zeroes < ones ? '0' : '1';

        var target = findMostCommon ? mostCommon : leastCommon;
        if (mostCommon == leastCommon)
        {
            target = findMostCommon ? '1' : '0';
        }

        var newInput = input.Where(line => line[index] == target).ToArray();
        if (newInput.Length==1)
        {
            return newInput[0];
        }

        return FilterByBitCriteria(newInput, findMostCommon, ++index);
    }
}

public class DiagnosticReport
{
    public int GammaRate { get; set; }
    public int EpsilonRate { get; set; }
    public int PowerConsumption => GammaRate * EpsilonRate;

    public int OxygenGeneratorRating { get; set; }
    public int CO2ScrubberRating { get; set; }

    public int LifeSupportRating => OxygenGeneratorRating * CO2ScrubberRating;

}
