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
            int total = 0;
            int startIndex = 19;
            int interval = 40;
            for (int i = startIndex; i < values.Count; i+= interval)
            {
                int value = values[i];
                total += value * (i+1);
                
            }
            Console.WriteLine(total);
        }
    }
}