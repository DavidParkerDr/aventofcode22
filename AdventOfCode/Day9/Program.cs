using System.Globalization;

namespace Day9
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
            Coordinate headPosition = new Coordinate(0,0);
            Coordinate tailPosition = new Coordinate(0,0);
            int maxX = 0;
            int maxY = 0;
            int minX = 0;
            int minY = 0;
            List<Coordinate> list = new List<Coordinate>();
            list.Add(tailPosition);
            while (!complete)
            {
                string input = Console.ReadLine();
                if (input == null)
                {
                    complete = true;
                    continue;
                }
                //Console.WriteLine(input);
                string[] directions = input.Split(' ');
                string direction = directions[0];
                int steps = int.Parse(directions[1]);
                for(int i=0; i<steps; i++)
                {
                    headPosition.TakeStep(direction);
                    //Console.WriteLine("Head: " + headPosition.ToString());
                    Coordinate newTailPosition = new Coordinate(tailPosition.X, tailPosition.Y);
                    newTailPosition.Follow(headPosition);
                    tailPosition = newTailPosition;
                    //Console.WriteLine("Tail: " + tailPosition.ToString());
                    if(!ListContainsCoordinate(list, newTailPosition))
                    {
                        list.Add(tailPosition);
                    }
                }
                
                
                if(headPosition.X > maxX)
                {
                    maxX = headPosition.X;
                }
                if (headPosition.Y > maxY)
                {
                    maxY = headPosition.Y;
                }
                if (headPosition.X < minX)
                {
                    minX = headPosition.X;
                }
                if (headPosition.Y < minY)
                {
                    minY = headPosition.Y;
                }
            }
            Console.WriteLine("(" + minX + "," + minY + ")");
            Console.WriteLine("(" + maxX + "," + maxY + ")");
            Console.WriteLine(list.Count);

        }
        public static bool ListContainsCoordinate(List<Coordinate> list, Coordinate position)
        {
            foreach(Coordinate existingPosition in list)
            {
                if(Coordinate.Equals(existingPosition, position))
                {
                    return true;
                }
            }
            return false;
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
        public override bool Equals(object? obj)
        {
            if(obj is Coordinate)
            {
                if(X == ((Coordinate)obj).X && Y == ((Coordinate)obj).Y)
                {
                    return true;
                }
            }
            return false;
        }
        public override string ToString()
        {
            return "(" + X + ", " + Y + ")";
        }
        public void GoUp()
        {
            Y++;
        }
        public void GoDown()
        {
            Y--;
        }
        public void GoLeft()
        {
            X--;
        }
        public void GoRight()
        {
            X++;
        }
        public void TakeStep(string direction)
        {
            if(direction == "U")
            {
                GoUp();
            } 
            else if (direction == "D")
            {
                GoDown();
            }
            else if (direction == "L")
            {
                GoLeft();
            }
            else if (direction == "R")
            {
                GoRight();
            }
        }
        public void Follow(Coordinate head)
        {
            if(head.X == this.X)
            {
                // same row
                if(head.Y == this.Y)
                {
                    // over lap
                }
                else
                {
                    int difference = head.Y - this.Y;
                    int distance = Math.Abs(difference);
                    if (distance > 1)
                    {
                        // too far away
                        if (difference < 0)
                        {
                            GoDown();
                        }
                        else
                        {
                            GoUp();
                        }
                    }
                    else
                    {
                        // close enough
                    }
                }
            }
            else
            {
                // different row
                if (head.Y == this.Y)
                {
                    // same column
                    int difference = head.X - this.X;
                    int distance = Math.Abs(difference);
                    if (distance > 1)
                    {
                        // too far away
                        if (difference < 0)
                        {
                            GoLeft();
                        }
                        else
                        {
                            GoRight();
                        }
                    }
                    else
                    {
                        // close enough
                    }
                }
                else
                {
                    // different rows and columns
                    int differenceX = head.X - this.X;
                    int differenceY = head.Y - this.Y;
                    int distanceX = Math.Abs(differenceX);
                    int distanceY = Math.Abs(differenceY);
                    
                    if(distanceX > 1 || distanceY > 1)
                    {
                        // too far way
                        
                        if(differenceX < 0)
                        {
                            GoLeft();
                        }
                        else
                        {
                            GoRight();
                        }
                        if (differenceY < 0)
                        {
                            GoDown();
                        }
                        else
                        {
                            GoUp();
                        }
                    }
                    else
                    {
                        // close enough
                    }
                }
            }
        }
    }
}