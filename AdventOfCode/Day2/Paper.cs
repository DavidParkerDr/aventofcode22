using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Day2
{
    internal class Paper : Move
    {
        public Paper(int score = 2) : base(score)
        {
        }

        public override int Fight(Rock other)
        {
            //win
            return 6 + Score;
        }
        public override int Fight(Scissors other)
        {
            //loss
            return 0 + Score;
        }
        public override int Fight(Paper other)
        {
            //draw
            return 3 + Score;
        }
    }
}
