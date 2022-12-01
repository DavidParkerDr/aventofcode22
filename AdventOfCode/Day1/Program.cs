namespace Day1
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
            List<int> allElvesCalories = new List<int>();
            int currentElfCalories = 0;
            while (!complete)
            {
                string input = Console.ReadLine();
                if(input == null)
                {
                    complete = true;
                }
                if (string.IsNullOrEmpty(input))
                {
                    
                    allElvesCalories.Add(currentElfCalories);  
                    currentElfCalories = 0;
                    continue;
                }
                int itemCalories = int.Parse(input);
                currentElfCalories += itemCalories;
            }
            int highestElfCalories = 0;
            foreach(int calories in allElvesCalories)
            {
                if (calories > highestElfCalories)
                {
                    highestElfCalories = calories;
                }
            }
            Console.WriteLine(highestElfCalories);
        }
    }
}