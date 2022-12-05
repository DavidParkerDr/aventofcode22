namespace Day5
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
            List<Stack<char>> stacks = new List<Stack<char>>();
            while (!complete)
            {
                string input = Console.ReadLine();
                if (char.IsDigit(input[1]))
                {
                    input = input.Replace(" ", "");
                    for(int i = 0; i < input.Length; i++)
                    {
                        Stack<char> stack = new Stack<char>();
                        stacks.Add(stack);
                    }
                    complete = true;
                    continue;
                }
                else
                {
                    lines.Add(input);
                }
            }
            for(int i = lines.Count - 1; i >= 0; i--)
            {
                string line = lines[i];
                int count = 0;
                for (int index = 1; index < line.Length; index+=4)
                {
                   
                    char c = line[index];
                    if(char.IsLetter(c))
                    {
                        stacks[count].Push(c);
                    }
                    count++;
                }
            }
            Console.ReadLine();
            complete = false;
            while (!complete)
            {
                string input = Console.ReadLine();
                if (input == null)
                {
                    complete = true;
                    continue;
                }
                string[] inputs = input.Split(' ');

                int number = int.Parse(inputs[1]);
                int start = int.Parse(inputs[3]) - 1;
                int end = int.Parse(inputs[5]) - 1;
                Stack<char> tempStack = new Stack<char>();
                for(int i = 0; i < number; i++)
                {
                    char top = stacks[start].Peek();
                    stacks[start].Pop();
                    tempStack.Push(top);
                }
                for (int i = 0; i < number; i++)
                {
                    char top = tempStack.Peek();
                    tempStack.Pop();
                    stacks[end].Push(top);
                }
            }
            for(int i = 0; i < stacks.Count; i++)
            {
                Stack<char> stack = stacks[i];
                if(stack.Count == 0)
                {
                    Console.Write(" ");
                }
                else
                {
                    Console.Write(stack.Peek());
                }
            }    
            Console.WriteLine();
        }
    }
}