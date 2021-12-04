using System.Text;

namespace Puzzles
{
    public class BingoGame
    {
        private BingoCard[] Cards;
        private int[] Input;

        public BingoGame(string[] cards, int[] input)
        {
            this.Cards = cards.Select(BingoCard.Parse).ToArray();
            this.Input = input;
        }

        public int Run(bool winLast = false)
        {
            foreach(var number in Input)
            {
                foreach (var card in Cards)
                {
                    if (card.Contains(number))
                    {
                        if (card.HasFullLine)
                        {
                            if ( !winLast || Cards.All(x => x.HasFullLine))
                            {
                                return number * card.SumUnmarked();
                            } 
                        }
                    }
                }
            }
            return 0;
        }
    }

    public class BingoCard
    {
        protected int[][] Data { get; set; } = new[]
        {
            new [] {0,0,0,0,0},
            new [] {0,0,0,0,0},
            new [] {0,0,0,0,0},
            new [] {0,0,0,0,0},
            new [] {0,0,0,0,0}
        };

        protected bool[][] Checked { get; set; } = new[]
        {
            new [] { false, false, false, false, false },
            new [] { false, false, false, false, false },
            new [] { false, false, false, false, false },
            new [] { false, false, false, false, false },
            new [] { false, false, false, false, false }
        };

        public bool HasFullLine { get; protected set; } = false;

        public bool Contains(int number)
        {
            for(var y = 0; y<Data.Length; y++)
            {
                for (var x = 0; x < Data[y].Length; x++)
                {
                    if (this[x,y] == number)
                    {
                        // switch order
                        Checked[y][x] = true;

                        // check column;
                        if (Checked[y].All(val => val))
                        {
                            HasFullLine = true;
                        }
                        // check row;
                        if (Checked.All(row => row[x]))
                        {
                            HasFullLine = true;
                        }

                        return true;
                    }
                }
            }
            return false;
        }

        public int SumUnmarked()
        {
            int sum = 0;
            for (var y = 0; y < Data.Length; y++)
            {
                for(var x = 0; x < Data[y].Length; x++)
                {
                    if (!Checked[y][x])
                    {
                        sum += this[x, y];
                    }
                }
            }
            return sum;
        }

        // Data is a vertical array of rows first.
        // Reverse Y and X access order, to make the matrix easier to debug
        public int this[int x, int y] {
            get { return Data[y][x]; }
            set { Data[y][x] = value; }
        }

        public static BingoCard Parse (string source)
        {
            var card = new BingoCard ();
            var y = 0;
            foreach (var line in source.Split("\n"))
            {
                var numbers = line.Split(" ", StringSplitOptions.RemoveEmptyEntries);
                for(var x=0; x<numbers.Length; x++)
                {
                    card[x,y] = int.Parse(numbers[x]);
                }
                y++;        
            }

            return card;
        }

        public string Print()
        {
            var s = new StringBuilder();
            foreach(var row in Data)
            {
                var line = string.Join(' ', row.Select(number => string.Format("{0,2}", number)));
                s.AppendLine(line);
            };
            
            return s.ToString();
        }
    }
}
