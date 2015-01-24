using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using WindowsGame1.Enums;
using WindowsGame1.MapElements;

namespace WindowsGame1
{

    public partial class MainGame
    {
        private List<MapElement> _mapElements = new List<MapElement>();
        public void CreateMapElement(EmapElements eMapElementType, Point StartPosition)
        {
            MapElement m;
            switch (eMapElementType)
            {
                case EmapElements.Tree:
                    m = new Tree(this, StartPosition);
                    break;
                case EmapElements.Rock:
                    m = new Rock(this, StartPosition);
                    break;
                default:
                    throw new NotSupportedException("Unknown Map Element type");
            }
            _mapElements.Add(m);
        }

        public List<MapElement> MapElements
        {
            get
            {
                return _mapElements;
            }

        }

    }
}
