namespace Day8
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
                lines.Add(input);
            }
            int columns = lines[0].Length;
            int rows = lines.Count;
            int[,] forest = new int[rows, columns];
            bool[,] visible = new bool[rows, columns];
            for (int row = 0; row < rows; row++)
            {
                string line = lines[row];
                for (int column = 0; column < columns; column++)
                {
                    char heightChar = line[column];
                    int treeHeight = int.Parse(heightChar.ToString());
                    forest[row, column] = treeHeight;
                    visible[row, column] = false;
                }
            }
            int countTrees1 = 0;
            int countTrees2 = 0;
            for (int row = 0; row < rows; row++)
            {
                int tallestTree = -1;
                int countCurrent = 0;
                for (int column = 0; column < columns; column++)
                {
                    //go right
                    int tree = forest[row, column];
                    if(tree > tallestTree)
                    {
                        visible[row, column] = true;
                        tallestTree = tree;
                        countCurrent++;
                    }
                    countTrees1++;
                }
                Console.WriteLine("CountCurrent Right " + countCurrent);
                countCurrent = 0;
                tallestTree = -1;
                for (int column = columns - 1; column >= 0; column--)
                {
                    //go left
                    int tree = forest[row, column];
                    if (tree > tallestTree)
                    {
                        visible[row, column] = true;
                        tallestTree = tree;
                        countCurrent++;
                    }
                    countTrees2++;
                }
                Console.WriteLine("CountCurrent Left " + countCurrent);
            }
            Console.WriteLine(countTrees1);
            Console.WriteLine(countTrees2);
            countTrees1 = 0;
            countTrees2 = 0;
            for (int column = 0; column < columns; column++)
            {
                int tallestTree = -1;
                int countCurrent = 0;
                for (int row = 0; row < rows; row++)
                {
                    //go down
                    int tree = forest[row, column];
                    if (tree > tallestTree)
                    {
                        visible[row, column] = true;
                        tallestTree = tree;
                        countCurrent++;
                    }
                    countTrees1++;
                }
                Console.WriteLine("CountCurrent Down " + countCurrent);
                countCurrent = 0;
                tallestTree = -1;
                for (int row = rows - 1; row >= 0; row--)
                {
                    //go up
                    int tree = forest[row, column];
                    if (tree > tallestTree)
                    {
                        visible[row, column] = true;
                        tallestTree = tree;
                        countCurrent++;
                    }
                    countTrees2++;
                }
                Console.WriteLine("CountCurrent Up " + countCurrent);
            }
            Console.WriteLine(countTrees1);
            Console.WriteLine(countTrees2);
            int visibleTreeCount = 0;

            for (int row = 0; row < rows; row++)
            {
                for (int column = 0; column < columns; column++)
                {
                    if(visible[row, column])
                    {
                        visibleTreeCount++;
                    }
                }
            }
            Console.WriteLine(visibleTreeCount);
        }
    }
}