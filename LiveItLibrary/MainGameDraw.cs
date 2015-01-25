using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Media;

namespace WindowsGame1
{
    public partial class MainGame
    {
        public void LoadContent()
        {
            _introVideo = Content.Load<Video>("Videos/LiveIt");
            _videoPlayer = new VideoPlayer();
            _videoPlayer.Play(_introVideo);
        }
        public void Draw(GraphicsDevice Graphics, SpriteBatch spriteBatch, GameTime gameTime)
        {
                _gameTime = gameTime;
                Camera.Draw(_gameTime);
                DrawButtons();
               PlayIntro(spriteBatch);
        }

        private void PlayIntro(SpriteBatch spriteBatch)
        {
            if (_videoPlayer.State != MediaState.Stopped)
                _videoTexture = _videoPlayer.GetTexture();
            if (_videoTexture != null)
            {
                if (_videoPlayer.State != MediaState.Stopped)
                {
                    _soundEnvironment.StopAllSounds();
                    spriteBatch.Draw(_videoTexture, new Rectangle(0, 0, GraphicsAdapter.DefaultAdapter.CurrentDisplayMode.Width, GraphicsAdapter.DefaultAdapter.CurrentDisplayMode.Height + 2), Color.White);
                }
                else
                {
                    _videoTexture = null;
                    _soundEnvironment.PlayAllSounds();
                }
            }
        }

        public void Update(GameTime gameTime)
        {
            Camera.Update(_gameTime);

            _keyControl.UpdateInput();

            _soundEnvironment.Update();

            UpdateGUI(gameTime);

            foreach (Animal a in this.Animals)
            {
                a.Update(gameTime);
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
