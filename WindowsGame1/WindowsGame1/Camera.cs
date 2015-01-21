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
        public Camera(MainGame Game, SpriteBatch spriteBatch, GraphicsDevice Graphics)
        {       
            _game = Game;
            _graphics = Graphics;
            _game.Camera = this;
            _spriteBatch = spriteBatch;
            _boxList = new List<Box>();
            _screen = new Rectangle(0, 0, GraphicsAdapter.DefaultAdapter.CurrentDisplayMode.Width, GraphicsAdapter.DefaultAdapter.CurrentDisplayMode.Height);
            this._viewPort = new Rectangle(0, 0, 5000, 5000);
        }
        public void Draw(Microsoft.Xna.Framework.GameTime gameTime)
        {
            for (int i = 0; i < this._boxList.Count; i++)
            {
                this._boxList[i].Draw(_graphics, _spriteBatch, _screen, _viewPort, gameTime);
            }
        }

        public void Update(GameTime gameTime)
        {
            _boxList = _game.GetOverlappedBoxes(_viewPort);
            //this._viewPort.Width += (int)(500 * gameTime.ElapsedGameTime.TotalSeconds);
            //this._viewPort.Height += (int)(500 * gameTime.ElapsedGameTime.TotalSeconds);          
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
        public void Zoom(int centimeters)
        {
            if (_viewPort.Width < GameVariables.MinViewPortSize)
            {
                _viewPort.Width = GameVariables.MinViewPortSize;
                _viewPort.Height = GameVariables.MinViewPortSize;
            }
            else
            {
                _viewPort.Width += centimeters;
                _viewPort.Height += centimeters;
            }

        }


    }
}
