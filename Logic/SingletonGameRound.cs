using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Logic
{
    public class SingletonGameRound
    {
        public string CodeGame { get; set; }
        public int TotalPlayers { get; set; }
        public int SpeedGame { get; set; }
        public bool PrivateGame { get; set; }
        public int Bet { get; set; }
        public static SingletonGameRound GameRound { get; set; }
    }


}
