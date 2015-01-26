// --------------------------------------------------------------------------------------------------------------------
// <copyright file="Player.cs" company="">
//   
// </copyright>
// <summary>
//   The player.
// </summary>
// --------------------------------------------------------------------------------------------------------------------
namespace WindowsGame1
{
    using System;
    using System.Collections.Generic;
    using Microsoft.Xna.Framework;
    using Microsoft.Xna.Framework.Graphics;
    using WindowsGame1.Texturing;


    public class Player
    {
        #region Fields

        private Vector2 _direction;

        bool leftCollide, rightCollide, upCollide, downCollide;

        private MainGame _game;

        private Point _position;

        private Point _relativePosition;

        private Rectangle _relativeSize;

        private bool _isInHouse;

        private Car _car;

        private Rectangle _size;

        private  int _acceleration;

        List<EBoxGround> _collisionTextures;

        protected Texture2D _spriteSheet;
        protected SpriteAnimation _animationLeft;
        protected SpriteAnimation _animationRight;
        protected SpriteAnimation _animationUp;
        protected SpriteAnimation _animationDown;

        #endregion

        #region Constructors and Destructors

        public Player( MainGame Game, Point startPosition )
        {
            this._game = Game;
            this._position = startPosition;
            this._size = new Rectangle(0,0, 200, 200 );
            this.Texture = EPlayerTexture.MainPlayer;
            this.Speed = 3;
            MaxSpeed = 30;
            this.Acceleration = 10;
            _spriteSheet = Game.Content.Load<Texture2D>("Textures/Player/Player-SpriteSheet");
            _animationUp = new SpriteAnimation(Game, _spriteSheet, this.RelativeArea, 60, 70, 65, 0, 5);
            _animationDown = new SpriteAnimation(Game, _spriteSheet, this.RelativeArea, 60, 70, 65, 130, 5);
            _animationLeft = new SpriteAnimation(Game, _spriteSheet, this.RelativeArea, 60, 70, 65, 70, 5);
            _animationRight = new SpriteAnimation(Game, _spriteSheet, this.RelativeArea, 60, 70, 65, 200, 5);

            _collisionTextures = new List<EBoxGround>() { EBoxGround.Wall, EBoxGround.Water };
        }

        #endregion

        #region Public Properties

        public Rectangle Area
        {
            get
            {
                return new Rectangle( this._position.X, this._position.Y, this._size.Width, this._size.Height);
            }
        }

        public bool IsInHouse
        {
            get { return _isInHouse; }
            set { _isInHouse = value; }
        }

        public Car Car
        {
            get
            {
                return _car;
            }
            set
            {
                _car = value;
            }
        } 

        public int Acceleration
        {
            get
            {
                return _acceleration;
            }
            set
            {
                if( value < 1 )
                {
                    _acceleration = 1;
                }
                else
                {
                    _acceleration = value;
                }
            }
        }

        /// <summary>
        ///     Gets the area bottom.
        /// </summary>
        public Rectangle AreaBottom
        {
            get
            {
                return new Rectangle(
                    this._position.X, this._position.Y + (this._size.Height / 3), this._size.Width, this._size.Height / 3);
            }
        }

        public Rectangle RelativeArea
        {
            get
            {
                return new Rectangle(this.RelativePosition.X, this.RelativePosition.Y, this.RelativeSize.Width, this.RelativeSize.Height);
            }
        }

        /// <summary>
        ///     Gets or sets the box list.
        /// </summary>
        public List<Box> BoxList { get; set; }

        /// <summary>
        ///     Gets or sets the car.
        /// </summary>
        //public Car Car { get; set; }

        /// <summary>
        ///     Gets the direction.
        /// </summary>
        public Vector2 Direction
        {
            get
            {
                return this._direction;
            }

            internal set
            {
                this._direction = value;
            }
        }

        /// <summary>
        ///     Gets or sets the e moving direction.
        /// </summary>
        public EMovingDirection EMovingDirection { get; set; }

        /// <summary>
        ///     Gets or sets a value indicating whether is moving.
        /// </summary>
        public bool IsMoving { get; set; }

        /// <summary>
        ///     Gets or sets the position.
        /// </summary>
        public Point Position
        {
            get
            {
                return this._position;
            }

            set
            {
                this._position = value;
            }
        }

        /// <summary>
        ///     Gets or sets the relative position.
        /// </summary>
        public Point RelativePosition
        {
            get
            {
                return this._relativePosition;
            }

            set
            {
                this._relativePosition = value;
            }
        }

        /// <summary>
        ///     Gets or sets the relative size.
        /// </summary>
        public Rectangle RelativeSize
        {
            get
            {
                return this._relativeSize;
            }

            set
            {
                this._relativeSize = value;
            }
        }

        /// <summary>
        ///     Gets the size.
        /// </summary>
        public Rectangle Size
        {
            get
            {
                return this._size;
            }

            internal set
            {
                this._size = value;
            }
        }

        public bool LeftCollide
        {
            get { return leftCollide; }
        }
        public bool RightCollide
        {
            get { return rightCollide; }
        }
        public bool UpCollide
        {
            get { return upCollide; }
        }
        public bool DownCollide
        {
            get { return downCollide; }
        }

        /// <summary>
        ///     Gets or sets the speed.
        /// </summary>
        public int Speed { get; set; }


        public int MaxSpeed { get; set; }

        /// <summary>
        ///     Gets the texture.
        /// </summary>
        public EPlayerTexture Texture { get; internal set; }

        #endregion

        #region Public Methods and Operators
        /// </param>
        public virtual void Draw(
            GraphicsDevice Graphics, SpriteBatch spriteBatch, Rectangle target, Rectangle viewPort, GameTime gameTime )
        {
            var newWidth = (int)((this.Area.Width / (double)viewPort.Width) * target.Width + 1);
            var newHeight = (int)((this.Area.Height / (double)viewPort.Width) * target.Width + 1);
            int newXpos =
                (int)(this.Area.X / (this.Area.Width / ((this.Area.Width / (double)viewPort.Width) * target.Width)))
                - (int)(viewPort.X / (this.Area.Width / ((this.Area.Width / (double)viewPort.Width) * target.Width)));
            int newYpos =
                (int)(this.Area.Y / (this.Area.Width / ((this.Area.Width / (double)viewPort.Width) * target.Width)))
                - (int)(viewPort.Y / (this.Area.Width / ((this.Area.Width / (double)viewPort.Width) * target.Width)));

            this.RelativePosition = new Point(newXpos + target.X, newYpos + target.Y);
            this.RelativeSize = new Rectangle(0,0, newWidth, newHeight );

            this.Position = new Point(
                this._position.X + (int)(this.Direction.X * this.Speed * gameTime.ElapsedGameTime.TotalSeconds),
                this._position.Y + (int)(this.Direction.Y * this.Speed * gameTime.ElapsedGameTime.TotalSeconds));

            BoxList = _game.GetOverlappedBoxes( this.Area );
            switch (this.EMovingDirection)
            {
                case WindowsGame1.EMovingDirection.Left:
                    spriteBatch.Draw(_spriteSheet, new Rectangle(RelativePosition.X, RelativePosition.Y, newWidth, newHeight), _animationLeft.SourceRect, Color.White);
                    break;
                case WindowsGame1.EMovingDirection.Right:
                    spriteBatch.Draw(_spriteSheet, new Rectangle(RelativePosition.X, RelativePosition.Y, newWidth, newHeight), _animationRight.SourceRect, Color.White);
                    break;
                case WindowsGame1.EMovingDirection.Up:
                    spriteBatch.Draw(_spriteSheet, new Rectangle(RelativePosition.X, RelativePosition.Y, newWidth, newHeight), _animationUp.SourceRect, Color.White);
                    break;
                case WindowsGame1.EMovingDirection.Down:
                    spriteBatch.Draw(_spriteSheet, new Rectangle(RelativePosition.X, RelativePosition.Y, newWidth, newHeight), _animationDown.SourceRect, Color.White);
                    break;
                default :
                    spriteBatch.Draw(_spriteSheet, new Rectangle(RelativePosition.X, RelativePosition.Y, newWidth, newHeight), _animationUp.SourceRect, Color.White);
                    break;
            }
            rightCollide = false;
            upCollide = false;
            downCollide = false;
            leftCollide = false;

            foreach( Box b in BoxList )
            {
                if( b.Left != null )
                {
                    if( _collisionTextures.Contains( b.Left.Ground ) )
                    {
                        leftCollide = true;
                    }

                }
                if( b.Right != null )
                {
                    if( _collisionTextures.Contains( b.Right.Ground ) )
                    {
                        rightCollide = true;
                    }
                }
                if( b.Top != null )
                {
                    if( _collisionTextures.Contains( b.Top.Ground ) )
                    {
                        upCollide = true;
                    }
                }
                if( b.Bottom != null )
                {
                    if( _collisionTextures.Contains( b.Bottom.Ground ) )
                    {
                        downCollide = true;
                    }
                }

            }


        }

        public void DrawInMiniMap( GraphicsDevice Graphics, SpriteBatch spriteBatch, Rectangle targetMiniMap, Rectangle viewPortMiniMap, GameTime gameTime )
        {
            var newSizeMini = (int)((this.Area.Width / (double)viewPortMiniMap.Width) * targetMiniMap.Width + 1);
            var newHeightMini = (int)((this.Area.Height / (double)viewPortMiniMap.Width) * targetMiniMap.Width + 1);
            int newXposMini =
                (int)
                (this.Area.X
                 / (this.Area.Width / ((this.Area.Width / (double)viewPortMiniMap.Width) * targetMiniMap.Width)))
                - (int)
                  (viewPortMiniMap.X
                   / (this.Area.Width / ((this.Area.Width / (double)viewPortMiniMap.Width) * targetMiniMap.Width)));
            int newYposMini =
                (int)
                (this.Area.Y
                 / (this.Area.Width / ((this.Area.Width / (double)viewPortMiniMap.Width) * targetMiniMap.Width)))
                - (int)
                  (viewPortMiniMap.Y
                   / (this.Area.Width / ((this.Area.Width / (double)viewPortMiniMap.Width) * targetMiniMap.Width)));

            spriteBatch.Draw( _game.GameTexture.GetTexture( this.Texture ), new Rectangle( newXposMini + targetMiniMap.X, newXposMini + targetMiniMap.Y, newSizeMini, newHeightMini ), Color.White );

        }
        public void Update(GameTime gameTime)
        {
            switch (this.EMovingDirection)
            {
                case WindowsGame1.EMovingDirection.Left:
                    _animationLeft.Update(gameTime);
                    break;
                case WindowsGame1.EMovingDirection.Right:
                    _animationRight.Update(gameTime);
                    break;
                case WindowsGame1.EMovingDirection.Up:
                    _animationUp.Update(gameTime);
                    break;
                case WindowsGame1.EMovingDirection.Down:
                    _animationDown.Update(gameTime);
                    break;
            }
        }


        #endregion
    }
}