using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Audio;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.GamerServices;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Media;

namespace WindowsGame1
{
    public class Box
    {
        Point _position;
        int _width;
        int _height;
        Color _color = Color.White;
        GraphicsDevice _graphics;
        MainGame _game;
        int _line;
        int _column;
        private Point _relativePosition;
        private Rectangle _relativeSize;
        EBoxGround _ground;

        public Box(Point Position, int Width, int Height, MainGame Game)
        {
            _position = Position;
            _height = Height;
            _width = Width;
            _game = Game;
            _line = (int)Position.X;
            _column = (int)Position.Y;
            _position = new Point(this._line * this._game.BoxSize, this._column * this._game.BoxSize);
            _ground = EBoxGround.Dirt;
        }
        public Box Top
        {
            get
            {
                return this._game[this._line, this._column - 1];
            }
        }
        public Box Bottom
        {
            get
            {
                return this._game[this._line, this._column + 1];
            }
        }
        public Box Left
        {
            get
            {
                return this._game[this._line - 1, this._column];
            }
        }
        public Box Right
        {
            get
            {
                return this._game[this._line + 1, this._column];
            }
        }
        public Rectangle Area
        {
            get
            {
                return new Rectangle(Position.X, Position.Y, _width, _height);
            }
        }
        public Point Position
        {
            get
            {
                return _position;
            }
            set
            {
                _position = value;
            }
        }

        public Point RelativePosition
        {
            get
            {
                return _relativePosition;
            }
            private set
            {
                _relativePosition = value;
            }
        }
        public Rectangle RelativeSize
        {
            get
            {
                return _relativeSize;
            }
            private set
            {
                _relativeSize = value;
            }
        }

        public EBoxGround Ground
        {
            get
            {
                return _ground;
            }
            set
            {
                _ground = value;
            }
        }

        public Rectangle RelativeArea
        {
            get
            {
                return new Rectangle(this.RelativePosition.X, this.RelativePosition.Y, this.RelativeSize.Width, this.RelativeSize.Height);
            }
        }

        internal void Draw(GraphicsDevice Graphics, SpriteBatch spriteBatch,Rectangle target, Rectangle viewPort, GameTime gameTime)
        {
            if (_graphics == null)
            {
                _graphics = Graphics;
            }
            var newSize = (int)((this.Area.Width / (double)viewPort.Width) * target.Width + 2);
            int newXpos =
                (int)(this.Area.X / (this._game.BoxSize / ((this.Area.Width / (double)viewPort.Width) * target.Width)))
                - (int)
                  (viewPort.X / (this._game.BoxSize / ((this.Area.Width / (double)viewPort.Width) * target.Width)));
            int newYpos =
                (int)(this.Area.Y / (this._game.BoxSize / ((this.Area.Width / (double)viewPort.Width) * target.Width)))
                - (int)
                  (viewPort.Y / (this._game.BoxSize / ((this.Area.Width / (double)viewPort.Width) * target.Width)));
            this._relativePosition.X = newXpos;
            this._relativePosition.Y = newYpos;
            this._relativeSize.Height = newSize;
            this._relativeSize.Width = newSize;

            spriteBatch.Draw(_game.GameTexture.GetTexture(this), this.RelativeArea, _color);
        }
    }
}
