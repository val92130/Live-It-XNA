using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using WindowsGame1.Enums;

namespace WindowsGame1.MapElements
{
    public class Tree : MapElement
    {
        public Tree(MainGame Game, Point startPosition)
            : base(Game, startPosition)
        {
            var r = new Random();
            int random = r.Next(800, 1300);
            var randomVegList = new List<EmapElements>
                                    {
                                        EmapElements.Tree, 
                                        EmapElements.Tree2, 
                                        EmapElements.Tree3
                                    };
            var r2 = new Random();

            this.Texture = randomVegList[r2.Next(0, randomVegList.Count)];
            this.Size = new Rectangle(0,0,random, random);
        }
    }
}
