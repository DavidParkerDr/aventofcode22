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

                
                
                int roundScore = 0;

                switch (inputs[1])
                {
                    case "X":
                        //lose
                        break;
                    case "Y":
                        //draw
                        break;
                    case "Z":
                        //win
                        break;
                }

                switch (inputs[0])
                {
                    case "A":
                        Rock rockMove = new Rock();
                        switch (inputs[1])
                        {
                            case "X":
                                //lose
                                roundScore = rockMove.Lose();
                                break;
                            case "Y":
                                //draw
                                roundScore = rockMove.Draw();
                                break;
                            case "Z":
                                //win
                                roundScore = rockMove.Win();
                                break;
                        }
                        break;
                    case "B":
                        Paper paperMove = new Paper();
                        switch (inputs[1])
                        {
                            case "X":
                                //lose
                                roundScore = paperMove.Lose();
                                break;
                            case "Y":
                                //draw
                                roundScore = paperMove.Draw();
                                break;
                            case "Z":
                                //win
                                roundScore = paperMove.Win();
                                break;
                        }
                        break;
                    case "C":
                        Scissors scissorsMove = new Scissors();
                        switch (inputs[1])
                        {
                            case "X":
                                //lose
                                roundScore = scissorsMove.Lose();
                                break;
                            case "Y":
                                //draw
                                roundScore = scissorsMove.Draw();
                                break;
                            case "Z":
                                //win
                                roundScore = scissorsMove.Win();
                                break;
                        }
                        break;
                }
                
                totalScore += roundScore;
            }
            Console.WriteLine(totalScore);
        }
    }
}