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
    }
}
