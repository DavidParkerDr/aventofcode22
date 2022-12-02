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

        public override int Fight(Rock other)
        {
            //draw
            return 3 + Score;
        }
        public override int Fight(Scissors other)
        {
            //win
            return 6 + Score;
        }
        public override int Fight(Paper other)
        {
            //loss
            return 0 + Score;
        }
    }
}
