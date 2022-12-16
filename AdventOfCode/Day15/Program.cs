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
            Dictionary<int, List<RowRange>> rowRanges = new Dictionary<int, List<RowRange>>();
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
                bool containsBeacon = false;
                foreach (Coordinate existingBeacon in beacons)
                {
                    if(existingBeacon.Equals(beacon))
                    {
                        containsBeacon = true;
                    }
                }
                if (!containsBeacon)
                {
                    beacons.Add(beacon);
                }
            
                
                Coordinate difference = beacon.Subtract(sensor);
                int manhattanDistance = Math.Abs(difference.X) + Math.Abs(difference.Y);
                List<RowRange> rows = MarkDeadZone(sensor, manhattanDistance);  
                foreach(RowRange rowRange in rows)
                {
                    int row = rowRange.Row;
                    if (rowRanges.ContainsKey(row))
                    {
                        List<RowRange> rowRangesAtRow = rowRanges[row];
                        for (int i = 0; i < rowRangesAtRow.Count; i++)
                        {
                            RowRange existingRowRange = rowRangesAtRow[i];
                            bool combined = existingRowRange.Combine(rowRange);
                            if (!combined)
                            {
                                rowRangesAtRow.Add(rowRange);
                            }
                            else
                            {
                                
                                for (int j = 0; j < rowRangesAtRow.Count; )
                                {
                                    if(j==i)
                                    {
                                        j++;
                                        continue;
                                    }
                                    RowRange otherExistingRowRange = rowRangesAtRow[j];
                                    bool combinedAgain = existingRowRange.Combine(otherExistingRowRange);
                                    if(combinedAgain)
                                    {
                                        rowRangesAtRow.RemoveAt(j);
                                        List<RowRange> dicRows = rowRanges[row];
                                    }
                                    else
                                    {
                                        j++;
                                    }
                                }
                                break;
                            }
                        }                        
                    }
                    else
                    {
                        List<RowRange> rowRangesAtRow = new List<RowRange>();
                        rowRangesAtRow.Add(rowRange);
                        rowRanges[row] = rowRangesAtRow;
                    }
                }

            }
            int lowerBound = 0;
            int upperBound = 4000000;
            //upperBound = 20;
            foreach(KeyValuePair<int, List<RowRange>> kvp in rowRanges)
            {
                if (kvp.Key >= lowerBound && kvp.Key <= upperBound)
                {
                    List<RowRange> rowRangesAtRowFinal = kvp.Value;
                    if (rowRangesAtRowFinal.Count > 1)
                    {
                        RowRange rowRange1 = rowRangesAtRowFinal[0];
                        RowRange rowRange2 = rowRangesAtRowFinal[1];
                        if(rowRange2.Start.X - rowRange1.End.X == 2)
                        {
                            long x = rowRange1.End.X + 1;
                            long y = kvp.Key;
                            long frequency = (x * 4000000) + y;
                            Console.WriteLine(frequency);
                        }

                        
                    }
                }
            }
            
           // cave.DrawRow(10);
        }
        public static List<RowRange> MarkDeadZone(Coordinate sensor, int manhattanDistance)
        {
            List<RowRange> list = new List<RowRange>();
            int yTop = sensor.Y - manhattanDistance;
            int yBottom = sensor.Y + manhattanDistance;
            int count = -1;
            for (int y = yTop; y <= sensor.Y; y++)
            {
                count++;
                int xLeft = sensor.X - count;
                int xRight = sensor.X + count;
                Coordinate start = new Coordinate(xLeft, y);
                Coordinate end = new Coordinate(xRight, y);
                RowRange range = new RowRange(start, end);
                list.Add(range);
            }
            for (int y = sensor.Y; y <= yBottom; y++)
            {
                int xLeft = sensor.X - count;
                int xRight = sensor.X + count;
                Coordinate start = new Coordinate(xLeft, y);
                Coordinate end = new Coordinate(xRight, y);
                RowRange range = new RowRange(start, end);
                list.Add(range);
                count--;
            }
            return list;
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
    class RowRange
    {
        public int Row { get; set; }
        public int Width { get; set; }
        public Coordinate Start { get; set; }
        public Coordinate End { get; set; }
        public RowRange(Coordinate start, Coordinate end)
        {
            Start = start;
            End = end;
            Row = start.Y;
            Width = end.X - start.X;
        }
        public bool InRange(Coordinate position)
        {
            if(position.Y != Row)
            {
                return false;
            }
            if(position.X >= Start.X && position.X <= End.X)
            {
                return true;
            }
            return false;
        }
        public void CalculateWidth()
        {
            Width = (End.X - Start.X) + 1;
            
        }
        public bool Combine(RowRange other)
        {
            bool combined = false;

            if(other.Start.X >= Start.X && other.End.X <= End.X)
            {
                //other wholly in this
                return true;
            }

            if(other.Start.X <= Start.X)
            {
                if(other.End.X >= Start.X)
                {
                    Start = other.Start;
                    
                    combined = true;
                }
            }
            if(other.Start.X <= this.End.X)
            {
                if (other.End.X >= this.End.X)
                {
                    End = other.End;
                    combined = true;
                }
            }
            CalculateWidth();
            return combined;
        }
        public override string ToString()
        {
            return Start + " -> " + End + " (" + Width + ")";
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