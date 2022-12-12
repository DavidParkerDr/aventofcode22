namespace Day12
{
    internal class Program
    {
        static void Main(string[] args)
        {
            if (args.Length == 1)
            {
                StreamReader sr = new StreamReader(args[0]);
                Console.SetIn(sr);
            }
            bool complete = false;
            List<string> lines = new List<string>();
            while (!complete)
            {
                string input = Console.ReadLine();
                if (input == null)
                {
                    complete = true;
                    continue;
                }
                // code here
                lines.Add(input);
            }

            int numRows = lines.Count;
            int numColumns = lines[0].Length;
            Square[,] squares = new Square[numRows, numColumns];
            for(int row = 0; row < numRows; row++)
            {
                string line = lines[row];
                for(int column = 0; column < numColumns; column++)
                {
                    char height = line[column];
                    Square square = new Square(row, column, height);
                    squares[row, column] = square;
                    if (row > 0)
                    {
                        square.AddUp(squares[row - 1, column]);
                    }
                    if (column > 0)
                    {
                        square.AddLeft(squares[row, column - 1]);
                    }

                }
            }
            Console.WriteLine('b' - 'a');
        }
    }
    class Square
    {
        public Square? Up { get; set; }
        public Square? Right { get; set; }
        public Square? Down { get; set; }
        public Square? Left { get; set; }
        public int X { get; set; }
        public int Y { get; set; }
        public char Height { get; set; }
        public Square(int x, int y, char height)
        {
            Height = height;
            X = x;
            Y = y;
            Up = null;
            Right = null;
            Down = null;
            Left = null;
        }
        public void AddUp(Square up)
        {
            if (up.Height - Height < 2)
            {
                Up = up;
            }
            up.AddDown(this);
        }
        public void AddRight(Square right)
        {
            if (right.Height - Height < 2)
            {
                Right = right;
            }            
        }
        public void AddDown(Square down)
        {
            if (down.Height - Height < 2)
            {
                Down = down;
            }            
        }
        public void AddLeft(Square left)
        {
            if (left.Height - Height < 2)
            {
                Left = left;
            }
            left.AddRight(this);
        }
    }
}