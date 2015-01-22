using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace WindowsGame1
{
    public class Camera
    {
        MainGame _game;
        Rectangle _screen;
        Rectangle _viewPort;
        Rectangle _miniMap;
        Rectangle _miniMapViewPort;
        SpriteBatch _spriteBatch;
        GraphicsDevice _graphics;
        List<Box> _boxList;
        List<Box> _miniMapBoxes;
        public Camera(MainGame Game, SpriteBatch spriteBatch, GraphicsDevice Graphics)
        {

            _game = Game;
            _graphics = Graphics;
            _spriteBatch = spriteBatch;
            this._miniMap = GameVariables.DefaultMiniMap;
            this._miniMapViewPort = new Rectangle(0, 0, 10000, 10000);
            _boxList = new List<Box>();
            _screen = new Rectangle(0, 0, GraphicsAdapter.DefaultAdapter.CurrentDisplayMode.Width, GraphicsAdapter.DefaultAdapter.CurrentDisplayMode.Height);
            this._viewPort = new Rectangle(GameVariables.DefaultViewPortPosition.X, GameVariables.DefaultViewPortPosition.Y, GameVariables.DefaultViewPortSize, GameVariables.DefaultViewPortSize);

        }
        public void Draw(Microsoft.Xna.Framework.GameTime gameTime)
        {

            for (int i = 0; i < this._boxList.Count; i++)
            {
               this._boxList[i].Draw(_graphics, _spriteBatch, _screen, _viewPort, gameTime);
            }

            foreach (Animal a in _game.Animals)
            {
                a.Draw(_graphics, _spriteBatch, _screen, _viewPort, gameTime);
            }

            _spriteBatch.Draw(_game.GameTexture.GetTexture(EBoxGround.Grass), new Rectangle(_miniMap.X, _miniMap.Y - 5, _miniMap.Width + 5, _miniMap.Height + 5), GameVariables.BorderColor);

            for (int i = 0; i < this._miniMapBoxes.Count; i++)
            {
                this._miniMapBoxes[i].DrawMiniMap(_graphics, _spriteBatch, _miniMap, _miniMapViewPort);
            }

        }

        public void Update(GameTime gameTime)
        {
            _boxList = _game.GetOverlappedBoxes(_viewPort);
            _miniMapBoxes = _game.GetOverlappedBoxes(_miniMapViewPort);
            AdjustViewPort();
        }

        public List<Box> BoxList
        {
            get
            {
                return _boxList;
            }
        }

        public Rectangle ViewPort
        {
            get
            {
                return _viewPort;
            }
        }

        public void MoveViewPortX(int centimeters)
        {
            _viewPort.X += centimeters;
        }
        public void MoveViewPortY(int centimeters)
        {
            _viewPort.Y += centimeters;
        }
        private void AdjustViewPort()
        {
            if (this._viewPort.Left < 0)
            {
                this._viewPort.X = 0;
            }

            if (this._viewPort.Top < 0)
            {
                this._viewPort.Y = 0;
            }

            if (this._viewPort.Bottom > this._game.MapSize)
            {
                this._viewPort.Y = this._game.MapSize - this._viewPort.Height;
            }

            if (this._viewPort.Right > this._game.MapSize)
            {
                this._viewPort.X = this._game.MapSize - this._viewPort.Width;
            }
        }
        public void Zoom(int meters)
        {
            this._viewPort.Width += meters;
            this._viewPort.Height += meters;
            if (this._viewPort.Width < GameVariables.MinViewPortSize || this._viewPort.Height < GameVariables.MinViewPortSize)
            {
                this._viewPort.Width = GameVariables.MinViewPortSize;
                this._viewPort.Height = GameVariables.MinViewPortSize;
            }

            if (this._viewPort.Width > this._game.MapSize)
            {
                this._viewPort.Height = this._game.MapSize;
                this._viewPort.Width = this._game.MapSize;
            }

        }


    }
}
