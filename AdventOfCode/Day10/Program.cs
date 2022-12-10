namespace Day10
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
            List<int> values = new List<int>();
            int register = 1;
            int pendingValue = 0;
            int loopLength = 0;
            while (!complete)
            {
                
                
                string input = Console.ReadLine();
                if (input == null)
                {
                    complete = true;
                    continue;
                }
                string[] instructions = input.Split(' ');
                string instruction = instructions[0];
                if(instruction == "noop")
                {
                    pendingValue = 0;
                    loopLength = 1;
                }
                else
                {
                    string valueString = instructions[1];
                    pendingValue = int.Parse(valueString);
                    loopLength = 2;
                }
                for(int i = 0; i < loopLength; i ++)
                {
                    values.Add(register);
                }
                register += pendingValue;


            }
            values.Add(register);
            int count = 0;
            int screenWidth = 40;
            int spriteWidth = 1;
            foreach(int value in values)
            {
                int leftBound = value - spriteWidth;
                int rightBound = value + spriteWidth;
                if(count <= rightBound && count >= leftBound)
                {
                    Console.Write("#");
                }
                else
                {
                    Console.Write(".");
                }
                count++;
                if(count == screenWidth)
                {
                    Console.WriteLine();
                    count = 0;
                }
            }
        }
    }
}