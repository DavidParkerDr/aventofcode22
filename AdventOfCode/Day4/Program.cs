namespace Day4
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
            int count = 0;
            List<string> rucksacks = new List<string>();
            while (!complete)
            {
                string input = Console.ReadLine();
                if (input == null)
                {
                    complete = true;
                    continue;
                }
                string[] tasks = input.Split(',');
                string[] firstTaskRange = tasks[0].Split('-');
                string[] secondTaskRange = tasks[1].Split('-');
                int firstTaskLowerBound = int.Parse(firstTaskRange[0]);
                int firstTaskUpperBound = int.Parse(firstTaskRange[1]);
                int secondTaskLowerBound = int.Parse(secondTaskRange[0]);
                int secondTaskUpperBound = int.Parse(secondTaskRange[1]);

                if((firstTaskLowerBound >= secondTaskLowerBound && firstTaskUpperBound <= secondTaskUpperBound)|| (secondTaskLowerBound >= firstTaskLowerBound && secondTaskUpperBound <= firstTaskUpperBound))
                {
                    count++;
                }
            }

            Console.WriteLine(count);
        }
    }
}