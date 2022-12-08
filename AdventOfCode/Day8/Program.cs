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
            for (int row = 0; row < rows; row++)
            {
                string line = lines[row];
                for (int column = 0; column < columns; column++)
                {
                    char heightChar = line[column];
                    int treeHeight = int.Parse(heightChar.ToString());
                    forest[row, column] = treeHeight;
                }
            }

            int highestFound = 0;
            for (int row = 0; row < rows; row++)
            {
                for (int column = 0; column < columns; column++)
                {
                    int tree = forest[row, column];
                    int product = 1;
                    int count = GoUp(forest, row - 1, column, tree);
                    product *= count;
                    count = GoDown(forest, row + 1, rows, column, tree);
                    product *= count;
                    count = GoLeft(forest, row, column - 1, tree);
                    product *= count;
                    count = GoRight(forest, row, column + 1, columns, tree);
                    product *= count;
                    if(product> highestFound)
                    {
                        highestFound = product;
                    }
                    
                }
            }
            
            Console.WriteLine(highestFound);

            int rowTest = 3;
            int columnTest = 2;
            int treeTest = forest[rowTest, columnTest];
            int productTest = 1;
            int countTest = GoUp(forest, rowTest - 1, columnTest, treeTest);
            productTest *= countTest;
            Console.WriteLine(countTest);
            countTest = GoDown(forest, rowTest + 1, rows, columnTest, treeTest);
            productTest *= countTest;
            Console.WriteLine(countTest);
            countTest = GoLeft(forest, rowTest, columnTest - 1, treeTest);
            productTest *= countTest;
            Console.WriteLine(countTest);
            countTest = GoRight(forest, rowTest, columnTest + 1, columns, treeTest);
            productTest *= countTest;
            Console.WriteLine(countTest);
            Console.WriteLine(productTest);
        }
        public static int GoUp(int[,] forest, int row, int column, int height)
        {
            if(row < 0)
            {
                return 0;
            }
            int count = 0;
            int tree = forest[row, column];
            if(tree >= height)
            {
                return 1;
            }
            if(row > 0)
            {
                count += GoUp(forest, row-1, column, height);
            }
            return count+1;
        }
        public static int GoDown(int[,] forest, int row, int rows, int column, int height)
        {
            if (row > rows-1)
            {
                return 0;
            }
            int count = 0;
            int tree = forest[row, column];
            if (tree >= height)
            {
                return 1;
            }
            if (row < rows-1)
            {
                count += GoDown(forest, row + 1, rows, column, height);
            }
            return count + 1;
        }
        public static int GoLeft(int[,] forest, int row, int column, int height)
        {
            if (column < 0)
            {
                return 0;
            }
            int count = 0;
            int tree = forest[row, column];
            if (tree >= height)
            {
                return 1;
            }
            if (column > 0)
            {
                count += GoLeft(forest, row, column-1, height);
            }
            return count + 1;
        }
        public static int GoRight(int[,] forest, int row, int column, int columns, int height)
        {
            if (column > columns-1)
            {
                return 0;
            }
            int count = 0;
            int tree = forest[row, column];
            if (tree >= height)
            {
                return 1;
            }
            if (column < columns - 1)
            {
                count += GoRight(forest, row, column + 1, columns, height);
            }
            return count + 1;
        }
    }
}