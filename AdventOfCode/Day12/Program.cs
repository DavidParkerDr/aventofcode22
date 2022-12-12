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
            Square startSquare = null;
            Square endSquare = null;
            List<Square> unvisitedSquares = new List<Square>();
            for(int row = 0; row < numRows; row++)
            {
                string line = lines[row];
                for(int column = 0; column < numColumns; column++)
                {
                    char height = line[column];
                    bool isStartSquare = false;
                    bool isEndSquare = false;
                    if (height == 'S')
                    {
                        height = 'a';
                        isStartSquare = true;
                    }
                    else if(height == 'E')
                    {
                        height = 'z';
                        isEndSquare = true;
                    }
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
                    if (isStartSquare)
                    {
                        startSquare = square;
                        square.TentativeDistance = 0;
                    }
                    else
                    {
                        if (isEndSquare)
                        {
                            square.IsEndSquare = true;
                            endSquare = square;
                        }
                        unvisitedSquares.Add(square);
                    }
                }
            }
            Square currentSquare = startSquare;
            do
            {
               // DisplayGrid(squares);
                currentSquare.Visit();
                unvisitedSquares.Remove(currentSquare);
                currentSquare = FindLowestUnvisitedSquare(unvisitedSquares);
            }
            while (unvisitedSquares.Count > 0);

            Console.WriteLine(endSquare.TentativeDistance);
        }
        public static Square FindLowestUnvisitedSquare(List<Square> unvisitedSquares)
        {
            Square lowestSquare = null;
            foreach(Square square in unvisitedSquares)
            {
                if(lowestSquare == null || lowestSquare.TentativeDistance == -1 || (square.TentativeDistance != -1 && lowestSquare.TentativeDistance > square.TentativeDistance))
                {
                    lowestSquare = square;
                }
            }
            return lowestSquare;
        }
        public static void DisplayGrid(Square[,] squares)
        {
            Console.Clear();
            for (int row = 0; row < squares.GetLength(0); row++)
            {                
                for (int column = 0; column < squares.GetLength(1); column++)
                {
                    Square square = squares[row, column];
                    if(square.Visited)
                    {
                        Console.Write("#");
                    }
                    else
                    {
                        if (square.TentativeDistance == -1)
                        {
                            Console.Write(".");
                        }
                        else
                        {
                            Console.Write(square.TentativeDistance);
                        }
                    }
                }
                Console.WriteLine();
            }
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
        public bool IsEndSquare { get; set; }
        public bool Visited { get; set; }
        public int TentativeDistance { get; set; }
        public Square(int x, int y, char height)
        {
            Height = height;
            X = x;
            Y = y;
            Up = null;
            Right = null;
            Down = null;
            Left = null;
            IsEndSquare = false;
            Visited = false;
            TentativeDistance = -1;
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
        public void UpdateTentativeDistance(int newDistance)
        {
            if (TentativeDistance == -1 || TentativeDistance > newDistance)
            {
                TentativeDistance = newDistance;
            }
        }
        public void Visit()
        {
            
            int currentDistance = TentativeDistance + 1;
            if (Up != null)
            {
                Up.UpdateTentativeDistance(currentDistance);
            }
            if (Right != null)
            {
                Right.UpdateTentativeDistance(currentDistance);
            }
            if (Down != null)
            {
                Down.UpdateTentativeDistance(currentDistance);
            }
            if (Left != null)
            {
                Left.UpdateTentativeDistance(currentDistance);
            }
            Visited = true;
        }
    }
}