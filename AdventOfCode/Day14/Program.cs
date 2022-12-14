namespace Day14
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
            List<List<Coordinate>> formations = new List<List<Coordinate>>();
            int minX = -1;
            int minY = -1;
            int maxX = -1;
            int maxY = -1;
            bool firstCoordinate = true;
            while (!complete)
            {
                string input = Console.ReadLine();
                if (input == null)
                {
                    complete = true;
                    continue;
                }
                // code here
                string [] parts = input.Split(" -> ");
                List<Coordinate> coordinates = new List<Coordinate>();
                foreach(string part in parts)
                {
                    string[] coordinateString = part.Split(",");
                    int x = int.Parse(coordinateString[0]);
                    int y = int.Parse(coordinateString[1]);
                    Coordinate coordinate = new Coordinate(x, y);
                    coordinates.Add(coordinate);
                    if(firstCoordinate)
                    {
                        minX = x;
                        maxX = x;
                        minY = y;
                        maxY = y;
                        firstCoordinate = false;
                    }
                    else
                    {
                        if(x > maxX)
                        {
                            maxX = x;
                        }
                        if (y > maxY)
                        {
                            maxY = y;
                        }
                        if (x < minX)
                        {
                            minX = x;
                        }
                        if (y < minY)
                        {
                            minY = y;
                        }
                    }
                }
                formations.Add(coordinates);
            }
            

            Coordinate floorStart = new Coordinate(minX - maxY, maxY + 2);
            Coordinate floorEnd = new Coordinate(maxX + maxY, maxY + 2);
            List<Coordinate> floor = new List<Coordinate>();
            floor.Add(floorStart);
            floor.Add(floorEnd);
            formations.Add(floor);
            Coordinate max = new Coordinate(maxX + maxY + 1, maxY + 2 + 1);
            Coordinate min = new Coordinate(minX-maxY, 0);
            Cave cave = new Cave(min, max);
            foreach(List<Coordinate> formation in formations)
            {
                cave.AddFormation(formation);
            }
            Coordinate sandSpawnSite = new Coordinate(500, 0);
            int count = 0;
            while(!cave.SpawnSand(sandSpawnSite))
            {
                count++;
               // cave.Draw();
            }
            
            Console.WriteLine(count);
        }
    }
    class Cave
    {
        public char[,] Grid { get; set; }
        public Coordinate Bounds { get; set; }
        public Coordinate Min { get; set; }
        public Coordinate Max { get; set; }
        public Cave(Coordinate min, Coordinate max)
        {
            Min = min;
            Max = max;
            Bounds = max.Subtract(min);
            Grid = new char[Bounds.X, Bounds.Y];
            Initialise();
        }
        public void AddFormation(List<Coordinate> formation)
        {
            for(int i = 1; i < formation.Count; i++)
            {
                Coordinate start = formation[i-1];
                Coordinate end = formation[i];
                Coordinate difference = end.Subtract(start);
                int magnitude = difference.Magnitude();
                Coordinate direction = difference.Normalise();
                Coordinate position = start;
                AddPosition(position);
                for (int j = 0; j < magnitude; j++)
                {
                    position = position.Add(direction);
                    AddPosition(position);
                }
            }
        }
        public void AddPosition(Coordinate position)
        {
            Coordinate adjustedPosition = position.Subtract(Min);
            Grid[adjustedPosition.X, adjustedPosition.Y] = '#';
        }
        public bool AddSand(Coordinate position, Coordinate originalPosition)
        {
            if(Grid[originalPosition.X, originalPosition.Y] == 'O')
            {
                return true;
            }
            
            Grid[position.X, position.Y] = 'O';
            return false;
            
        }
        public void Initialise()
        {
            for(int row = 0; row < Grid.GetLength(0); row++)
            {
                for (int column = 0; column < Grid.GetLength(1); column++)
                {
                    Grid[row, column] = '.';
                }
            }
        }
        public void Draw()
        {
            for (int row = 0; row < Grid.GetLength(1); row++)
            {
                for (int column = 0; column < Grid.GetLength(0); column++)
                {
                    Console.Write(Grid[column, row]);
                }
                Console.WriteLine();
            }
        }
        public bool SpawnSand(Coordinate spawnSite)
        {
            Coordinate adjustedPosition = spawnSite.Subtract(Min);
            Coordinate originalPosition = adjustedPosition;
            bool falling = true;
            do
            {
                Coordinate newPosition = CanFall(adjustedPosition);
                if(newPosition.Equals(adjustedPosition))
                {
                    falling = false;
                    return AddSand(newPosition, originalPosition);
                }
                else if(!newPosition.Equals(originalPosition))
                {
                    adjustedPosition = newPosition;
                }
                else
                {
                    return true;
                }
            }
            while(falling);
            return false;
        }
        public Coordinate CanFall(Coordinate position)
        {
            Coordinate newPosition = CanFallDown(position);
            if (!newPosition.Equals(position))
            {
                return newPosition;
            }
            else {
                newPosition = CanFallDownLeft(position);
                if (!newPosition.Equals(position))
                {
                    return newPosition;
                }
                else
                {
                    newPosition = CanFallDownRight(position);
                    if (!newPosition.Equals(position))
                    {
                        return newPosition;
                    }
                }
            }
            return position;
        }
        public Coordinate CanFallDown(Coordinate position)
        {
            Coordinate oneBelow = position.Add(new Coordinate(0, 1));
            if (InsideBounds(oneBelow))
            {
                if (Grid[oneBelow.X, oneBelow.Y] == '.')
                {
                    return oneBelow;
                }
            }
            else
            {
                return oneBelow;
            }
            
            return position;
        }
        public bool InsideBounds(Coordinate position)
        {
            if (position.X < 0)
            {
                return false;
            }
            else if(position.X >= Bounds.X)
            {
                return false;
            }
            if (position.Y < 0)
            {
                return false;
            }
            else if (position.Y >= Bounds.Y)
            {
                return false;
            }
            return true;
        }
        public Coordinate CanFallDownLeft(Coordinate position)
        {
            Coordinate oneBelowAndLeft = position.Add(new Coordinate(-1, 1));
            if (InsideBounds(oneBelowAndLeft))
            {
                if (Grid[oneBelowAndLeft.X, oneBelowAndLeft.Y] == '.')
                {
                    return oneBelowAndLeft;
                }
            }
            else
            {
                return oneBelowAndLeft;
            }

            return position;
        }
        public Coordinate CanFallDownRight(Coordinate position)
        {
            Coordinate oneBelowAndRight = position.Add(new Coordinate(1, 1));
            if (InsideBounds(oneBelowAndRight))
            {
                if (Grid[oneBelowAndRight.X, oneBelowAndRight.Y] == '.')
                {
                    return oneBelowAndRight;
                }
            }
            else
            {
                return oneBelowAndRight;
            }

            return position;
        }
    }
    class Coordinate
    {
        public int X;
        public int Y;

        public Coordinate(int x, int y)
        {
            X = x;
            Y = y;
        }
        public bool Equals(Coordinate other)
        {
            Coordinate difference = other.Subtract(this);

            return difference.Magnitude() == 0;
        }

        public Coordinate Subtract(Coordinate other)
        {
            return new Coordinate(X - other.X, Y - other.Y);
        }
        public Coordinate Add(Coordinate other)
        {
            return new Coordinate(X + other.X, Y + other.Y);
        }

        public int Magnitude()
        {
            int result = (int)Math.Sqrt(X * X + Y * Y);
            return result;
        }
        public Coordinate Normalise()
        {
            int x = X / Magnitude();
            int y = Y / Magnitude();
            return new Coordinate(x, y);
        }
        
        public override string ToString()
        {
            return "(" + X + ", " + Y + ")";
        }
        
    }
}