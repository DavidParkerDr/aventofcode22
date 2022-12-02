using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Day2
{
    internal class Move
    {
        public int Score { get; private set; }
        public Move(int score)
        {
            Score = score;
        }
        public virtual int Fight(Rock other)
        {
            //draw
            return 3 + Score;
        }
        public virtual int Fight(Scissors other)
        {
            //win
            return 6 + Score;
        }
        public virtual int Fight(Paper other)
        {
            //loss
            return 0 + Score;
        }
    }
}
