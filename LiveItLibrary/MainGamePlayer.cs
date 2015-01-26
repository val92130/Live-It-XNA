using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace WindowsGame1
{
    
    public partial class MainGame
    {
        bool _isPlayer;
        bool _isInCar;

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
        public bool IsInCar
        {
            get
            {
                return _isInCar;
            }
            set
            {
                _isInCar = value;
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
