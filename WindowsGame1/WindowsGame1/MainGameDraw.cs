using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework.Input;

namespace WindowsGame1
{
    public partial class MainGame
    {
        public void LoadContent()
        {

        }
        public void Draw(GraphicsDevice Graphics, SpriteBatch spriteBatch, GameTime gameTime)
        {
            _gameTime = gameTime;
            Camera.Draw(_gameTime);
            DrawButtons();
        }

        public void Update(GameTime gameTime)
        {

            Camera.Update(_gameTime);
           
            _keyControl.UpdateInput();

            _soundEnvironment.Update();

            UpdateGUI(gameTime);

            foreach (Box b in this.Boxes)
            {
                b.Update();
            }
        }


        public void FillBox(Box target, EBoxGround targetColor, EBoxGround Color)
        {
            if (target.Ground == targetColor && Color != target.Ground)
            {
                target.Ground = Color;
                if (target.Top != null)
                {
                    this.FillBox(target.Top, targetColor, Color);
                }

                if (target.Bottom != null)
                {
                    this.FillBox(target.Bottom, targetColor, Color);
                }

                if (target.Left != null)
                {
                    this.FillBox(target.Left, targetColor, Color);
                }

                if (target.Right != null)
                {
                    this.FillBox(target.Right, targetColor, Color);
                }
            }
        }
    }
}
