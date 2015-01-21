using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Input;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace WindowsGame1
{
    public class GameVariables
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
                return 600;
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
                return 50;
            }
        }
        public static int ButtonMarginLeft
        {
            get
            {
                return 10;
            }
        }
        public static int ButtonMarginTop
        {
            get
            {
                return 100;
            }
        }

        #endregion
    }
}
