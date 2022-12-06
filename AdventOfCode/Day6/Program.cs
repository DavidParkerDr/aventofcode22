namespace Day6
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
            Queue<char> queue = new Queue<char>();
            int count = 0;
            string input = Console.ReadLine();
            for(int i = 0; i < input.Length; i++)
            {
                char c = input[i];
                queue.Enqueue(c);
                if (i > 3)
                {
                    queue.Dequeue();
                    if (IsUnique(queue))
                    {
                        count = i+1;
                        break;
                    }
                }
                
                
            }

            

            Console.WriteLine(count);
        }
        public static bool IsUnique(Queue<char> queue)
        {
            for(int i = 0; i < queue.Count; i++)
            {
                char currentChar = queue.ElementAt(i);
                for(int j = i+1; j < queue.Count; j++)
                {
                    char otherChar = queue.ElementAt(j);
                    if(currentChar == otherChar)
                    {
                        return false;
                    }
                }
            }
            return true;
        }
    }
}