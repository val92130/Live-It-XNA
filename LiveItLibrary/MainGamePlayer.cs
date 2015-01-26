using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace WindowsGame1
{
    
    public partial class MainGame
    {
        bool _isPlayer;

        public bool IsPlayer
        {
            get
            {
                return _isPlayer;
            }
            set
            {
                _isPlayer = value;
            }
        }

        public Player Player
        {
            get
            {
                return _player;
            }
        }
    }
}
