using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using WindowsGame1.Texturing;

namespace WindowsGame1
{
    public class Animal
    {
        private  MainGame _game;
        private  Point _position;
        private  int _health;
        private  int _hunger;
        private  int _thirst;
        private  List<Animal> _animalsAround = new List<Animal>();
        private  Vector2 _direction;
        protected Rectangle _size;
        protected int _viewDistance;
        private  bool _isDead;
        private  bool _walking;
        private  Point _relativePosition;
        private  Rectangle _relativeSize;
        private  int _speed;
        private  EAnimalTexture _texture;
        private List<Box> BoxList = new List<Box>();
        private  Point _targetLocation;
        private SpriteBatch _spriteBatch;
        private GraphicsDevice _graphics;
        public  EBoxGround FavoriteEnvironnment;
        private bool _isInWater;
        protected int DefaultSpeed;
        protected SpriteAnimation _animationLeft;
        protected SpriteAnimation _animationRight;
        protected SpriteAnimation _animationUp;
        protected SpriteAnimation _animationDown;
        protected Texture2D _spriteSheet;
        protected Rectangle _sourceRect;
        protected List<Box> WalkableBoxes = new List<Box>();
        public Animal(MainGame Game, Point Position)
        {
            this._game = Game;
            this._position = Position;
            this._health = 100;
            this._hunger = 0;
            this._thirst = 0;
        }

        public List<Animal> AnimalsAround
        {
            get
            {
                return this._animalsAround;
            }
        }

        public Rectangle Area
        {
            get
            {
                return new Rectangle( this._position.X, this._position.Y, this._size.Width, this._size.Height );
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

        public Rectangle RelativeArea
        {
            get
            {
                return new Rectangle( this.RelativePosition.X, this.RelativePosition.Y, this.RelativeSize.Width, this.RelativeSize.Height );
            }
        }
        public Vector2 Direction
        {
            get
            {
                return this._direction;
            }

            set
            {
                this._direction = value;
            }
        }
        public Rectangle FieldOfView
        {
            get
            {
                return new Rectangle(
                    this.Position.X - (this._viewDistance / 2),
                    this.Position.Y - (this._viewDistance / 2),
                    this._viewDistance * 2,
                    this._viewDistance * 2 );
            }
        }
        public int Health
        {
            get
            {
                return this._health;
            }

            internal set
            {
                this._health += value;
                if( this._health - value <= 0 )
                {
                    this._health = 0;
                    this.Die();
                }

                if( this._health + value >= 100 )
                {
                    this._health = 100;
                }

                if( this._health <= 0 )
                {
                    this.Die();
                }
            }
        }
        public int Hunger
        {
            get
            {
                return this._hunger;
            }

            internal set
            {
                this._hunger = value;
                if( this._hunger <= 0 )
                {
                    this._hunger = 0;
                }

                if( this._hunger >= 100 )
                {
                    this._hunger = 100;
                }
            }
        }
        public bool IsDead
        {
            get
            {
                return this._isDead;
            }

            internal set
            {
                this._isDead = value;
            }
        }
        public bool IsInMovement
        {
            get
            {
                return this._walking;
            }

            set
            {
                this._walking = value;
            }
        }

        /// <summary>
        /// Gets the max hunger.
        /// </summary>
        public virtual int MaxHunger
        {
            get
            {
                return 50;
            }
        }

        public EMovingDirection EMovingDirection
        {
            get
            {
                if( this.Direction.X > 0 && this.Direction.X > this.Direction.Y )
                {
                    return EMovingDirection.Right;
                }

                if( this.Direction.X < 0 && this.Direction.X > this.Direction.Y )
                {
                    return EMovingDirection.Left;
                }

                if( this.Direction.Y > 0 && this.Direction.Y > this.Direction.X )
                {
                    return EMovingDirection.Down;
                }

                if( this.Direction.Y < 0 && this.Direction.Y > this.Direction.X )
                {
                    return EMovingDirection.Up;
                }

                return EMovingDirection.Up;
            }
        }
        public virtual Rectangle Size
        {
            get
            {
                return this._size;
            }

            set
            {
                this._size = value;
            }
        }

        /// <summary>
        ///     Gets or sets the speed.
        /// </summary>
        public int Speed
        {
            get
            {
                return this._speed;
            }

            set
            {
                this._speed = value;
            }
        }
        public EAnimalTexture Texture
        {
            get
            {
                return this._texture;
            }

            protected set
            {
                this._texture = value;
            }
        }

        /// <summary>
        ///     Gets the thirst.
        /// </summary>
        public int Thirst
        {
            get
            {
                return this._thirst;
            }

            internal set
            {
                this._thirst = value;
                if( this._thirst <= 0 )
                {
                    this._thirst = 0;
                }

                if( this._thirst >= 100 )
                {
                    this._thirst = 100;
                }
            }
        }

        /// <summary>
        ///     Gets or sets the view distance.
        /// </summary>
        public int ViewDistance
        {
            get
            {
                return this._viewDistance;
            }

            set
            {
                this._viewDistance = value;
            }
        }

        public void AddToList( Box b )
        {
            if( !this.BoxList.Contains( b ) )
            {
                this.BoxList.Add( b );
            }
        }
        public virtual void Behavior()
        {
            this._isDrinking = false;
            if( this.FieldOfView.Intersects( new Rectangle( this.TargetLocation.X, this.TargetLocation.Y, this.FieldOfView.Width, this.FieldOfView.Height ) ) )
            {
                this.IsInMovement = false;
            }

            if( !this.IsInMovement )
            {
                this.ChangePosition();
            }


            if( this.BoxList.Count() != 0 )
            {
                foreach( Box b in this.BoxList )
                {

                    if( b.Area.Intersects( this.FieldOfView ) && this.Thirst >= 60 && b.Ground == EBoxGround.Water )
                    {
                        this.ChangePosition( b.Area.Location );
                    }

                    if( b != null )
                    {
                        if( b.Ground == EBoxGround.Water && this.Texture != EAnimalTexture.Eagle )
                        {
                            this._isInWater = true;

                            if (this.Thirst >= 15)
                            {
                                this._isDrinking = true;
                                this.Speed = 0;
                                this.Thirst -= 5;
                            }
                            else
                            {
                                if (this.WalkableBoxes.Count != 0)
                                {
                                    var r = new Random();
                                    this.ChangePosition(
                                        this.WalkableBoxes[r.Next(0, this.WalkableBoxes.Count)].Location);
                                }
                                else
                                {
                                    this.ChangePosition();
                                }
                            }

                        }
                        else
                        {
                            this._isInWater = false;
                        }
                    }
                }
            }
        }

        public void ChangePosition()
        {
            var r = new Random();
            var newTarget = new Point( r.Next( 0, this._game.MapSize ), r.Next( 0, this._game.MapSize ) );
            this.TargetLocation = newTarget;
            var distance =
                (float)(Math.Pow( this.Position.X - newTarget.X, 2 ) + Math.Pow( this.Position.Y - newTarget.Y, 2 ));
            var _dir = new Vector2( (newTarget.X - this.Position.X) / distance, (newTarget.Y - this.Position.Y) / distance );
            this.Direction = _dir;
            this.IsInMovement = true;
        }

        public void ChangePosition( Point target )
        {
            var r = new Random();
            Point newTarget = target;
            this.TargetLocation = newTarget;
            var distance =
                (float)(Math.Pow( this.Position.X - newTarget.X, 2 ) + Math.Pow( this.Position.Y - newTarget.Y, 2 ));
            var _dir = new Vector2( (newTarget.X - this.Position.X) / distance, (newTarget.Y - this.Position.Y) / distance );
            this.Direction = _dir;
            this.IsInMovement = true;
        }


        public Point TargetLocation
        {
            get
            {
                return this._targetLocation;
            }

            set
            {
                this._targetLocation = value;
            }
        }

        public double DistanceBetweenAnimal( Animal a )
        {
            return Math.Pow( this.Area.X - a.Area.X, 2 ) + Math.Pow( this.Area.Y - a.Area.Y, 2 );
        }

        public void RemoveFromList( Box b )
        {
            if( this.BoxList.Contains( b ) )
            {
                this.BoxList.Remove( b );
            }
        }
        public void Die()
        {
            this.IsDead = true;
            this._game.DeadAnimals += 1;
            foreach( Box b in this._game.Boxes )
            {
                b.RemoveFromList( this );
            }

            this._game.Animals.Remove( this );
        }

        public virtual void Draw( GraphicsDevice Graphics, SpriteBatch spriteBatch, Rectangle target, Rectangle viewPort, GameTime gameTime )
        {

            if( _graphics == null )
            {
                this._graphics = Graphics;
            }
            if( _spriteBatch == null )
            {
                _spriteBatch = spriteBatch;
            }

            var newWidth = (int)((this.Area.Width / (double)viewPort.Width) * target.Width + 1);
            var newHeight = (int)((this.Area.Height / (double)viewPort.Width) * target.Width + 1);
            int newXpos =
                (int)(this.Area.X / (this.Area.Width / ((this.Area.Width / (double)viewPort.Width) * target.Width)))
                - (int)(viewPort.X / (this.Area.Width / ((this.Area.Width / (double)viewPort.Width) * target.Width)));
            int newYpos =
                (int)(this.Area.Y / (this.Area.Width / ((this.Area.Width / (double)viewPort.Width) * target.Width)))
                - (int)(viewPort.Y / (this.Area.Width / ((this.Area.Width / (double)viewPort.Width) * target.Width)));

            this.RelativePosition = new Point( newXpos, newYpos );
            this.RelativeSize = new Rectangle(0,0, newWidth, newHeight );



            if( this.Area.Intersects( viewPort ) )
            {
                switch (this.EMovingDirection)
                {
                    case WindowsGame1.EMovingDirection.Left:
                        spriteBatch.Draw( _spriteSheet, new Rectangle(newXpos, newYpos, newWidth, newHeight), _animationLeft.SourceRect, Color.White );
                        break;
                    case WindowsGame1.EMovingDirection.Right:
                        spriteBatch.Draw(_spriteSheet, new Rectangle(newXpos, newYpos, newWidth, newHeight), _animationRight.SourceRect, Color.White);
                        break;
                    case WindowsGame1.EMovingDirection.Up:
                        spriteBatch.Draw(_spriteSheet, new Rectangle(newXpos, newYpos, newWidth, newHeight), _animationUp.SourceRect, Color.White);
                        break;
                    case WindowsGame1.EMovingDirection.Down:
                        spriteBatch.Draw(_spriteSheet, new Rectangle(newXpos, newYpos, newWidth, newHeight), _animationDown.SourceRect, Color.White);
                        break;
                }
            }


        }

        public void Update(GameTime gameTime)
        {
            switch (this.EMovingDirection)
            {
                case WindowsGame1.EMovingDirection.Left :
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
            this.Position = new Point(
this._position.X + (int)(this.Direction.X * (int)(this.Speed * _game.DeltaTime)),
this._position.Y + (int)(this.Direction.Y * (int)(this.Speed * _game.DeltaTime)) );

            this.Behavior();

            this.BoxList = _game.GetOverlappedBoxes(this.Area);

            this.WalkableBoxes = _game.GetOverlappedBoxes(this.FieldOfView);
            for (int i = 0; i < this.WalkableBoxes.Count; i++ )
            {
                if (this.WalkableBoxes[i].Ground == EBoxGround.Water)
                {
                    WalkableBoxes.Remove(this.WalkableBoxes[i]);
                }
            }
            this.GetAnimalsAround();
        }

        public void GetAnimalsAround()
        {
            for( int i = 0; i < this._game.Animals.Count; i++ )
            {
                if( this.FieldOfView.Intersects( this._game.Animals[i].FieldOfView ) && this._game.Animals[i] != this )
                {
                    if( !this._animalsAround.Contains( this._game.Animals[i] ) )
                    {
                        this._animalsAround.Add( this._game.Animals[i] );
                    }
                }

                if( this._animalsAround.Contains( this._game.Animals[i] )
                    && !this._game.Animals[i].FieldOfView.Intersects( this.FieldOfView ) )
                {
                    this._animalsAround.Remove( this._game.Animals[i] );
                }

                for( int j = 0; j < this._animalsAround.Count(); j++ )
                {
                    if( !this._game.Animals.Contains( this._animalsAround[j] ) )
                    {
                        this._animalsAround.Remove( this._animalsAround[j] );
                    }
                }
            }
        }
        public bool _isDrinking { get; set; }
    }
}
