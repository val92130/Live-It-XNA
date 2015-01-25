using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;

namespace WindowsGame1
{
    public class Wild : Animal
    {
        public Wild( MainGame Game, Point Position )
            : base( Game, Position )
        {
        }

        public List<EAnimalTexture> TargetAnimals { get; protected set; }
    }
}
