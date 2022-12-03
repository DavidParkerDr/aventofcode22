namespace Day3
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
            int totalScore = 0;
            List<string> rucksacks = new List<string>();
            while (!complete)
            {
                string input = Console.ReadLine();
                if (input == null)
                {
                    complete = true;
                    continue;
                }
                rucksacks.Add(input);
                if (rucksacks.Count == 3)
                {
                    char duplicateItem = ' ';
                    foreach (char c in rucksacks[0])
                    {
                        if (rucksacks[1].Contains(c) && rucksacks[2].Contains(c))
                        {
                            duplicateItem = c;
                            break;
                        }
                    }
                    int value = calculateValue(duplicateItem);
                    totalScore += value;
                    rucksacks.Clear();
                }
            }
            
            Console.WriteLine(totalScore);
        }
        public static int calculateValue(char pItem)
        {
            int value = 0;
            string allChars = "abcdefghijklmnopqrstuvwxyzABCDEFGHIJKLMNOPQRSTUVWXYZ";
            foreach(char c in allChars)
            {
                value++;
                if(c == pItem)
                {
                    return value;
                }
            }
            return value;
        }
        public static void printRuckSack(string firstHalf, string secondHalf, char duplicateItem)
        {
            printCompartment(firstHalf, duplicateItem);
            printCompartment(secondHalf, duplicateItem);
        }
        public static void printCompartment(string compartment, char duplicateItem)
        {
            foreach (char c in compartment)
            {
                if(c == duplicateItem)
                {
                    Console.ForegroundColor = ConsoleColor.Green;
                }
                else
                {
                    Console.ForegroundColor = ConsoleColor.White;
                }
                Console.Write(c);
            }
            Console.WriteLine();
          
        }
    }
}