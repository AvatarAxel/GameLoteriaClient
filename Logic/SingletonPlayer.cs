using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Logic
{
    public class SingletonPlayer
    {
        public string Email { get; set; }
        public int Coin { get; set; }
        public string Username { get; set; }
        public bool RegisteredUser { get; set;}
        public bool PlayerType { get; set; }
        public bool Verificated { get; set; }
        public static SingletonPlayer PlayerClient { get; set; }
    }
}
