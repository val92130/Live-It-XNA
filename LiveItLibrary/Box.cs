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
using WindowsGame1.Texturing;

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
        List<Animal> _animalList;
        private  Rectangle _source;
        SpriteAnimation _animation;
        Texture2D _animationWater;

        public Box( int line, int column, MainGame Game )
        {
            this._game = Game;
            this._line = line;
            this._column = column;
            this._ground = EBoxGround.Grass;
            this._relativePosition = new Point( line, column );
            this._relativeSize = new Rectangle(0,0, this._game.BoxSize, this._game.BoxSize );
            this._animalList = new List<Animal>();
            _position = new Point( this._line * this._game.BoxSize, this._column * this._game.BoxSize );
            _ground = GameVariables.DefaultBoxTexture;
            _animationWater = _game.Content.Load<Texture2D>("Textures/Water-SpriteSheet");
            _animation = new SpriteAnimation(_game, _animationWater, this.RelativeArea, 512, 505, 512, 0, 4, 80f);
        }

        #region Position and Neighbors
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
                return new Rectangle( this.Location.X, this.Location.Y, this._game.BoxSize, this._game.BoxSize );
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
        public Rectangle RelativeArea
        {
            get
            {
                return new Rectangle( this.RelativePosition.X, this.RelativePosition.Y, this.RelativeSize.Width, this.RelativeSize.Height );
            }
        }

        #endregion

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
        public List<Animal> Animals
        {
            get
            {
                return _animalList;
            }
        }
        public void RemoveFromList( Animal a )
        {
            if( this._animalList.Contains( a ) )
            {
                a.RemoveFromList( this );
                this._animalList.Remove( a );
            }
        }
        public Point Location
        {
            get
            {
                return new Point( this._line * this._game.BoxSize, this._column * this._game.BoxSize );
            }
        }
        public Rectangle Source
        {
            get
            {
                return this._source;
            }

            set
            {
                this._source = value;
            }
        }

        internal void Draw( GraphicsDevice Graphics, SpriteBatch spriteBatch, Rectangle target, Rectangle viewPort, GameTime gameTime )
        {
            _animation.Update(gameTime);
            if( _graphics == null )
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
            if (this.Ground == EBoxGround.Water)
            {
                spriteBatch.Draw(_game.GameTexture.GetTexture(EBoxGround.Grass), new Rectangle(newXpos, newYpos, newSize, newSize), _animation.SourceRect, Color.White * 0.6f);
                spriteBatch.Draw(_animationWater, new Rectangle(newXpos, newYpos, newSize, newSize), _animation.SourceRect, Color.White * 0.7f);
            }
            else
            {
                spriteBatch.Draw(_game.GameTexture.GetTexture(this.Ground), this.RelativeArea, _color);
            }

        }

        public void Update()
        {
            foreach (Animal a in _game.Animals)
            {
                if (_animalList.Contains(a))
                {
                    if (!a.Area.Intersects(this.Area))
                    {
                        _animalList.Remove(a);
                    }
                }
                if (a.Area.Intersects(this.Area))
                {
                    this._animalList.Add(a);
                }
                
            }
        }
        internal void DrawMiniMap(GraphicsDevice Graphics,  SpriteBatch spriteBatch, Rectangle target, Rectangle viewPort)
        {
            var newSize = (int)((this.Source.Width / (double)viewPort.Width) * target.Width + 2);
            int newXpos =
                (int)(this.Area.X / (this._game.BoxSize / ((this.Source.Width / (double)viewPort.Width) * target.Width)))
                - (int)
                  (viewPort.X / (this._game.BoxSize / ((this.Source.Width / (double)viewPort.Width) * target.Width)));
            int newYpos =
                (int)(this.Area.Y / (this._game.BoxSize / ((this.Source.Width / (double)viewPort.Width) * target.Width)))
                - (int)
                  (viewPort.Y / (this._game.BoxSize / ((this.Source.Width / (double)viewPort.Width) * target.Width)));

            spriteBatch.Draw(_game.GameTexture.GetTexture(this.Ground, true), new Rectangle(newXpos + target.X, newYpos + target.Y, newSize, newSize), _color );
        }
    }
}
