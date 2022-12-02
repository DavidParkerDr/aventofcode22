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
        public virtual int Lose()
        {
            return 0 + Score;
        }
        public virtual int Draw()
        {
            return 3 + Score;
        }
        public virtual int Win()
        {
            return 6 + Score;
        }
    }
}
