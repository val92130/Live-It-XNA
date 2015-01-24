using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using WindowsGame1.Enums;

namespace WindowsGame1.MapElements
{
    public class Rock : MapElement
    {
        public Rock(MainGame Game, Point StartPosition)
            : base(Game, StartPosition)
        {
            var r = new Random();
            int _random = r.Next(50, 300);
            var r2 = new Random();
            this.Texture = EmapElements.Rock;
            this.Size = new Rectangle(0,0,_random, _random);
        }
    }
}
