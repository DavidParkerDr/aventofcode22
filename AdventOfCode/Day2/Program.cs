namespace Day2
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
                if(input == null)
                {
                    complete = true;
                    continue;
                }
                string[] inputs = input.Split(' ');

                
                Move yourMove = null;
                switch (inputs[1])
                {
                    case "X":
                        yourMove = new Rock();
                        break;
                    case "Y":
                        yourMove = new Paper();
                        break;
                    case "Z":
                        yourMove = new Scissors();
                        break;
                }
                int roundScore = 0;

                switch (inputs[0])
                {
                    case "A":
                        Rock rockMove = new Rock();
                        roundScore = yourMove.Fight(rockMove);
                        break;
                    case "B":
                        Paper paperMove = new Paper();
                        roundScore = yourMove.Fight(paperMove);
                        break;
                    case "C":
                        Scissors scissorsMove = new Scissors();
                        roundScore = yourMove.Fight(scissorsMove);
                        break;
                }
                totalScore += roundScore;
            }
            Console.WriteLine(totalScore);
        }
    }
}