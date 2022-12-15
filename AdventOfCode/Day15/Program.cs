namespace Day15
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
            bool firstCoordinate = true;
            int minX = -1;
            int minY = -1;
            int maxX = -1;
            int maxY = -1;
            List<Coordinate> sensors = new List<Coordinate>();
            List<Coordinate> beacons = new List<Coordinate>();
            while (!complete)
            {
                string input = Console.ReadLine();
                if (input == null)
                {
                    complete = true;
                    continue;
                }
                // code here
                input = input.Replace("Sensor at x=", "");
                input = input.Replace(" closest beacon is at x=", "");
                input = input.Replace(" y=", "");
                string[] parts = input.Split(':');
                string[] sensorParts = parts[0].Split(',');
                string[] beaconParts = parts[1].Split(',');
                Coordinate sensor = new Coordinate(int.Parse(sensorParts[0]), int.Parse(sensorParts[1]));
                sensors.Add(sensor);
                Coordinate beacon = new Coordinate(int.Parse(beaconParts[0]), int.Parse(beaconParts[1]));
                beacons.Add(beacon);
                Coordinate difference = beacon.Subtract(sensor);
                int manhattanDistance = Math.Abs(difference.X) + Math.Abs(difference.Y);
                if (firstCoordinate)
                {
                    minX = sensor.X - manhattanDistance;
                    maxX = sensor.X + manhattanDistance;
                    minY = sensor.Y - manhattanDistance;
                    maxY = sensor.Y + manhattanDistance;
                    firstCoordinate = false;
                }
                else
                {
                    if (sensor.X + manhattanDistance > maxX)
                    {
                        maxX = sensor.X + manhattanDistance;
                    }
                    if (sensor.Y + manhattanDistance > maxY)
                    {
                        maxY = sensor.Y + manhattanDistance;
                    }
                    if (sensor.X - manhattanDistance < minX)
                    {
                        minX = sensor.X - manhattanDistance;
                    }
                    if (sensor.Y - manhattanDistance < minY)
                    {
                        minY = sensor.Y - manhattanDistance;
                    }
                }
                if (beacon.X + manhattanDistance > maxX)
                {
                    maxX = beacon.X + manhattanDistance;
                }
                if (beacon.Y + manhattanDistance > maxY)
                {
                    maxY = beacon.Y + manhattanDistance;
                }
                if (beacon.X - manhattanDistance < minX)
                {
                    minX = beacon.X - manhattanDistance;
                }
                if (beacon.Y - manhattanDistance < minY)
                {
                    minY = beacon.Y - manhattanDistance;
                }                

            }
            Coordinate min = new Coordinate(minX, minY);
            Coordinate max = new Coordinate(maxX, maxY);
            Coordinate range = max.Subtract(min);
            Console.WriteLine(range);
            Cave cave = new Cave(min, max);
            for(int i = 0; i < sensors.Count; i++)
            {
                Coordinate sensor = sensors[i];
                
                Coordinate beacon = beacons[i];
                cave.AddPair(sensor, beacon);
            }
          //  cave.Draw();
            int deadZone =cave.CountDeadZoneInRow(2000000);
            Console.WriteLine(deadZone);
           // cave.DrawRow(10);

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
        public void AddPair(Coordinate sensor, Coordinate beacon)
        {
            AddSensor(sensor);
            AddBeacon(beacon);
            Coordinate difference = beacon.Subtract(sensor);
            int manhattanDistance = Math.Abs(difference.X) + Math.Abs(difference.Y);
            MarkDeadZone(sensor, manhattanDistance);

        }
        public int CountDeadZoneInRow(int row)
        {
            int count = 0;
            int adjustedRow = row - Min.X;
            for (int column = 0; column < Grid.GetLength(1); column++)
            {
                if (Grid[adjustedRow, column] == '#' || Grid[adjustedRow, column] == 'S')
                {
                    count++;
                }
            }
            return count;
        }
        public void MarkDeadZone(Coordinate sensor, int manhattanDistance)
        {
            Coordinate adjustedPosition = sensor.Subtract(Min);
            int yTop = adjustedPosition.Y - manhattanDistance;
            int yBottom = adjustedPosition.Y + manhattanDistance;
            int count = -1;
            for (int y = yTop; y <= adjustedPosition.Y; y++)
            {
                count++;
                int xLeft = adjustedPosition.X - count;
                int xRight = adjustedPosition.X + count;
                for (int x = xLeft; x <= xRight; x++)
                {
                    if (Grid[x, y] == '.')
                    {
                        Grid[x, y] = '#';
                    }
                }

            }
            for (int y = adjustedPosition.Y; y <= yBottom; y++)
            {
                int xLeft = adjustedPosition.X - count;
                int xRight = adjustedPosition.X + count;
                for (int x = xLeft; x <= xRight; x++)
                {
                    if (Grid[x, y] == '.')
                    {
                        Grid[x, y] = '#';
                    }
                }
                count--;
            }
        }
        public void AddSensor(Coordinate position)
        {
            Coordinate adjustedPosition = position.Subtract(Min);
            Grid[adjustedPosition.X, adjustedPosition.Y] = 'S';
        }
        public void AddBeacon(Coordinate position)
        {
            Coordinate adjustedPosition = position.Subtract(Min);
            Grid[adjustedPosition.X, adjustedPosition.Y] = 'B';
        }

        public void Initialise()
        {
            for (int row = 0; row < Grid.GetLength(0); row++)
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
        public void DrawRow(int row)
        {
            int adjustedRow = row - Min.X;
            for (int column = 0; column < Grid.GetLength(0); column++)
            {
                Console.Write(Grid[column, adjustedRow]);
            }
            Console.WriteLine();
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