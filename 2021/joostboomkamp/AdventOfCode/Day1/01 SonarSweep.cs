namespace Puzzles
{
    public class SonarSweep
    {
        public int Compute(int[] input)
        {
            var prev = 0;
            var measurement = 0;
            var incrementCount = 0;

            for (var i = 1; i < input.Length; i++)
            {
                measurement = input[i];
                if (measurement > prev)
                {
                    incrementCount++;
                }
                prev = measurement;
            }

            return incrementCount;
        }

        public int ComputeSliding(int[] input)
        {
            var index = 0;
            var prev = 0;
            var measurement = 0;
            var incrementCount = -1; // skip the first iteration or subtract one from the count

            do
            {
                var buffer = input.Skip(index).Take(3).ToArray();
                measurement = buffer.Sum();
                if (measurement > prev)
                {
                    incrementCount++;
                }
                prev = measurement;
                index++;
            } while (index < input.Length - 2);
            return incrementCount;
        }
    }
}
