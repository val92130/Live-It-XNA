using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using WindowsGame1.MapElements;

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
            this._miniMapViewPort = new Rectangle(0, 0, _game.MapSize, _game.MapSize);
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

            foreach (MapElement m in _game.MapElements)
            {
                m.Draw(_graphics, _spriteBatch, _screen, _viewPort);
            }

            DrawFog();

            DrawMiniMapElements(gameTime);

            if( _game.IsPlayer )
            {
                _game.Player.Draw( _graphics, _spriteBatch, _screen, _viewPort, gameTime );
            }


        }

        private void DrawFog()
        {
            if (GameVariables.DrawFog)
            {
                if (_viewPort.Width > _game.MapSize * 0.7)
                {
                    _spriteBatch.Draw(_game.GameTexture.GetTexture(EBoxGround.Fog), _screen, Color.White * ((1f / (float)(_game.MapSize * 3)) * _viewPort.Width));
                }
            }
        }

        private void DrawMiniMapElements(Microsoft.Xna.Framework.GameTime gameTime)
        {
            _spriteBatch.Draw(_game.GameTexture.GetTexture(EBoxGround.Grass), new Rectangle(_miniMap.X, _miniMap.Y - 5, _miniMap.Width + 5, _miniMap.Height + 5), GameVariables.BorderColor);

            for (int i = 0; i < this._miniMapBoxes.Count; i++)
            {
                this._miniMapBoxes[i].DrawMiniMap(_graphics, _spriteBatch, _miniMap, _miniMapViewPort);
            }
            foreach (MapElement m in _game.MapElements)
            {
                m.Draw(_graphics, _spriteBatch, _miniMap, _miniMapViewPort);
            }
            foreach (Animal a in _game.Animals)
            {
                a.Draw(_graphics, _spriteBatch, _miniMap, _miniMapViewPort, gameTime);
            }
            this.DrawViewPortMiniMap(_game.GameTexture.GetTexture(EBoxGround.Grass), _graphics, _spriteBatch, _viewPort, _miniMap, _miniMapViewPort);
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
            AdjustViewPort();
        }
        public void MoveViewPortY(int centimeters)
        {
            _viewPort.Y += centimeters;
            AdjustViewPort();
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

        static Texture2D _pointTexture;
        public void DrawViewPortMiniMap(Texture2D Texture,
    GraphicsDevice graphics, SpriteBatch spriteBatch,
    Rectangle source,
    Rectangle targetMiniMap,
    Rectangle viewPortMiniMap)
        
        {
            if (_pointTexture == null)
            {
                _pointTexture = new Texture2D(spriteBatch.GraphicsDevice, 1, 1);
                _pointTexture.SetData<Color>(new Color[] { Color.White });
            }
            int lineWidth = 1;

            var newSizeMini = (int)((source.Width / (double)viewPortMiniMap.Width) * targetMiniMap.Width + 1);
            var newHeightMini = (int)((source.Height / (double)viewPortMiniMap.Width) * targetMiniMap.Width + 1);
            int newXposMini =
                (int)
                (source.X / (source.Width / ((source.Width / (double)viewPortMiniMap.Width) * targetMiniMap.Width)))
                - (int)
                  (viewPortMiniMap.X
                   / (source.Width / ((source.Width / (double)viewPortMiniMap.Width) * targetMiniMap.Width)));
            int newYposMini =
                (int)
                (source.Y / (source.Width / ((source.Width / (double)viewPortMiniMap.Width) * targetMiniMap.Width)))
                - (int)
                  (viewPortMiniMap.Y
                   / (source.Width / ((source.Width / (double)viewPortMiniMap.Width) * targetMiniMap.Width)));

            spriteBatch.Draw(_pointTexture, new Rectangle(newXposMini + targetMiniMap.X, newYposMini + targetMiniMap.Y, lineWidth, newHeightMini + lineWidth), GameVariables.DefaultBorderColor);
            spriteBatch.Draw(_pointTexture, new Rectangle(newXposMini + targetMiniMap.X, newYposMini + targetMiniMap.Y, newHeightMini + lineWidth, lineWidth), GameVariables.DefaultBorderColor);
            spriteBatch.Draw(_pointTexture, new Rectangle(newXposMini + newHeightMini + targetMiniMap.X, newYposMini + targetMiniMap.Y, lineWidth, newHeightMini + lineWidth), GameVariables.DefaultBorderColor);
            spriteBatch.Draw(_pointTexture, new Rectangle(newXposMini + targetMiniMap.X, newYposMini + newHeightMini + targetMiniMap.Y, newHeightMini + lineWidth, lineWidth), GameVariables.DefaultBorderColor);
        }


    }
}
