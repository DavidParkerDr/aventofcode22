using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Day2
{
    internal class Scissors : Move
    {
        public Scissors(int score = 3) : base(score)
        {
        }

        public override int Lose()
        {
            Move yourMove = new Paper();
            return 0 + yourMove.Score;
        }
        public override int Draw()
        {
            Move yourMove = new Scissors();
            return 3 + yourMove.Score;
        }
        public override int Win()
        {
            Move yourMove = new Rock();
            return 6 + yourMove.Score;
        }
    }
}
