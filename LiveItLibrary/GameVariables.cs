using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace WindowsGame1
{
    public class GameVariables : Microsoft.Xna.Framework.Game
    {

        #region Camera
        public static int ZoomValue
        {
            get
            {
                return 10000;
            }
        }
        public static int CameraSpeed
        {
            get
            {
                return 2000;
            }
        }
        public static int MinViewPortSize
        {
            get
            {
                return 400;
            }
        }
        public static int DefaultViewPortSize
        {
            get
            {
                return 500;
            }
        }
        public static Point DefaultViewPortPosition
        {
            get
            {
                return new Point(0,0);
            }
        }

        public static Rectangle DefaultMiniMap
        {
            get
            {
                Rectangle r = new Rectangle(0,0,(int)( GraphicsAdapter.DefaultAdapter.CurrentDisplayMode.Height * 0.3),(int)( GraphicsAdapter.DefaultAdapter.CurrentDisplayMode.Height * 0.3) );
                r.Y = GraphicsAdapter.DefaultAdapter.CurrentDisplayMode.Height - r.Height;
                return r;
            }
        }
        #endregion

        #region Controls

        public static Keys UpKey
        {
            get
            {
                return Keys.Z;
            }
        }
        public static Keys DownKey
        {
            get
            {
                return Keys.S;
            }
        }
        public static Keys LeftKey
        {
            get
            {
                return Keys.Q;
            }
        }
        public static Keys RightKey
        {
            get
            {
                return Keys.D;
            }
        }



        #endregion

        #region Box

        public static EBoxGround DefaultBoxTexture
        {
            get
            {
                return EBoxGround.Grass;
            }
        }

        #endregion

        #region GUI
        public static int ButtonHeight
        {
            get
            {
                return (int)(GraphicsAdapter.DefaultAdapter.CurrentDisplayMode.Height * 0.03);
            }

        }
        public static int ButtonWidth {
            get
            {
                return (int)(GraphicsAdapter.DefaultAdapter.CurrentDisplayMode.Width * 0.08);
            }
        }
        public static int ButtonMarginLeft
        {
            get
            {
                return (int)(GraphicsAdapter.DefaultAdapter.CurrentDisplayMode.Width * 0.01);
            }
        }
        public static int ButtonMarginRight
        {
            get
            {
                return (int)(GraphicsAdapter.DefaultAdapter.CurrentDisplayMode.Width * 0.01);
            }
        }
        public static Color ButtonActiveColor
        {
            get
            {
                return Color.Red;
            }
        }
        public static Color ButtonHoverColor
        {
            get
            {
                return Color.Silver;
            }
        }
        public static Color BorderColor
        {
            get
            {
                return Color.Black;
            }
        }
        public static int ButtonBorderWidth
        {
            get
            {
                return 5;
            }
        }
        public static Color DefaultBorderColor
        {
            get
            {
                return Color.WhiteSmoke;
            }
        }
        public static int ButtonMarginTop
        {
            get
            {
                return (int)(GraphicsAdapter.DefaultAdapter.CurrentDisplayMode.Height * 0.02);
            }
        }


        #endregion

        
    }
}
