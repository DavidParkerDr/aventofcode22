using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Day2
{
    internal class Rock : Move
    {
        public Rock(int score = 1) : base(score)
        {
        }

        public override int Lose()
        {
            Move yourMove = new Scissors();
            return 0 + yourMove.Score;
        }
        public override int Draw()
        {
            Move yourMove = new Rock();
            return 3 + yourMove.Score;
        }
        public override int Win()
        {
            Move yourMove = new Paper();
            return 6 + yourMove.Score;
        }
    }
}
