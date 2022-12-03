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
            while (!complete)
            {
                string input = Console.ReadLine();
                if (input == null)
                {
                    complete = true;
                    continue;
                }
                int numberOfItems = input.Length;
                int halfIndex = numberOfItems / 2;

                string firstHalf = input.Substring(0, halfIndex);
                string secondHalf = input.Substring(halfIndex);
                char duplicateItem = ' ';
                foreach(char c in firstHalf)
                {
                    if(secondHalf.Contains(c))
                    {
                        duplicateItem = c;
                        break;
                    }
                }
                int value = calculateValue(duplicateItem);
                totalScore += value;
                printRuckSack(firstHalf, secondHalf, duplicateItem);
                Console.WriteLine(value);
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