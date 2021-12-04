namespace Puzzles.Tools
{
    public class Matrix2d<T>
    {
        protected T[][] Data { get; set; }

        public int Width { get; }
        public int Height { get; }

        public Matrix2d(int width, int height = 0) 
        {
            Width = width;
            Height = height;

            Data = new T[Height][];

            for(var y = 0; y < Height; y++)
            {
                Data[y] = new T[Width];
            }
        }

        public bool Contains(T needle)
        {
            for (var y = 0; y < Data.Length; y++)
            {
                if (Data[y].Any(other => other?.Equals(needle) == true))
                {
                    return true;
                }
            }
            return false;
        }
    
        // Data is a vertical array of rows first.
        // Reverse Y and X access order, to make the matrix easier to debug
        public T this[int x, int y]
        {
            get { return Data[y][x]; }
            set { Data[y][x] = value; }
        }
    }
}
