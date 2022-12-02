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

        public override int Fight(Rock other)
        {
            //loss
            return 0 + Score;
        }
        public override int Fight(Scissors other)
        {
            //draw
            return 3 + Score;
        }
        public override int Fight(Paper other)
        {
            //win
            return 6 + Score;
        }
    }
}
