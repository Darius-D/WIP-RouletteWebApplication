using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection.Metadata.Ecma335;
using System.Security.AccessControl;
using System.Threading.Tasks;

namespace GamblingGame.Models
{
    public class Game
    {
        public List<int> Box = new List<int>();
        public double BetAmount;
        public bool FirstTwelve;
        public bool SecondTwelve;
        public bool ThirdTwelve;
        public bool High;
        public bool Low;
        public bool Black;
        public bool Red;
        public bool Odd;
        public bool Even;

    }

}
