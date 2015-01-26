﻿using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using WindowsGame1.Texturing;

namespace WindowsGame1
{
    using System;
    using System.Collections.Generic;
    
    using WindowsGame1;
    using Microsoft.Xna.Framework;

    /// <summary>
    /// The car.
    /// </summary>
    public class Car 
    {
        #region Fields

        /// <summary>
        /// The _map.
        /// </summary>
        private readonly MainGame _game;

        private int _acceleration;

        private int _maxSpeed;

        /// <summary>
        /// The box list.
        /// </summary>
        private List<Box> BoxList;

        /// <summary>
        /// The _direction.
        /// </summary>
        private Vector2 _direction;

        /// <summary>
        /// The _is radio playing.
        /// </summary>
        private bool _isRadioPlaying;

        /// <summary>
        /// The _position.
        /// </summary>
        private Point _position;

        /// <summary>
        /// The _relative position.
        /// </summary>
        private Point _relativePosition;

        /// <summary>
        /// The _relative size.
        /// </summary>
        private Rectangle  _relativeSize;

        /// <summary>
        /// The _size.
        /// </summary>
        private Rectangle _size;

        #endregion

        #region Constructors and Destructors

        /// <summary>
        /// Initializes a new instance of the <see cref="Car"/> class.
        /// </summary>
        /// <param name="map">
        /// The map.
        /// </param>
        /// <param name="startPosition">
        /// The start position.
        /// </param>
        public Car(MainGame mainGame, Point startPosition)
        {
            this._game = mainGame;
            this._position = startPosition;
            this._size = new Rectangle(0, 0, 400, 400);
            this.Texture = ECarTexture.MainPlayerCar;

          
            var random = new Random();
           

            this.BoxList = new List<Box>();
            this.Speed = 20;
            this.MaxSpeed = 150;
            this.Acceleration = 20;
        }

        #endregion

        #region Public Properties

        /// <summary>
        /// Gets the area.
        /// </summary>
        public Rectangle Area
        {
            get
            {
                return new Rectangle(this._position.X,this._position.Y,this._size.Width, this._size.Height);
            }
        }

        /// <summary>
        /// Gets the direction.
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

        public int MaxSpeed
        {
            get { return _maxSpeed; }
            set
            {
                if (value < 0)
                {
                    _maxSpeed = 0;
                }
                else
                {
                    _maxSpeed = value;
                }


            }
        }

        public int Acceleration
        {
            get { return _acceleration; }
            set
            {
                if (value < 0)
                {
                    _acceleration = 0;
                }
                else
                {
                    _acceleration = value;
                }
            }
        }

        /// <summary>
        /// Gets or sets a value indicating whether is moving.
        /// </summary>
        public bool IsMoving { get; set; }

        /// <summary>
        /// Gets a value indicating whether is radio playing.
        /// </summary>
        public bool IsRadioPlaying
        {
            get
            {
                return this._isRadioPlaying;
            }
        }

        /// <summary>
        /// Gets or sets the moving direction.
        /// </summary>
        public EMovingDirection EMovingDirection { get; set; }

        /// <summary>
        /// Gets or sets the position.
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
        /// Gets the radio song.
        /// </summary>
       

        /// <summary>
        /// Gets or sets the relative position.
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
        /// Gets or sets the relative size.
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
        /// Gets the size.
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

        /// <summary>
        /// Gets or sets the speed.
        /// </summary>
        public int Speed { get; set; }

        /// <summary>
        /// Gets the texture.
        /// </summary>
        public ECarTexture Texture { get; internal set; }

        #endregion

        #region Public Methods and Operators

        /// <summary>
        /// The draw.
        /// </summary>
        /// <param name="g">
        /// The g.
        /// </param>
        /// <param name="target">
        /// The target.
        /// </param>
        /// <param name="viewPort">
        /// The view port.
        /// </param>
        /// <param name="targetMiniMap">
        /// The target mini map.
        /// </param>
        /// <param name="viewPortMiniMap">
        /// The view port mini map.
        /// </param>
        /// <param name="texture">
        /// The texture.
        /// </param>
        public virtual void Draw(
           GraphicsDevice Graphics, SpriteBatch spriteBatch, Rectangle target, Rectangle viewPort, Rectangle viewPortMiniMap, Rectangle targetMiniMap, GameTime gameTime)
        {
            if (this.IsMoving)
            {
                for (int i = 0; i < this.BoxList.Count; i++)
                {
                    for (int j = 0; j < this._game.Animals.Count; j++)
                    {
                        if (this._game.Animals[j].Area.Intersects(this.Area)
                            && this._game.Animals[j].Texture != EAnimalTexture.Eagle)
                        {
                            this._game.Animals[j].Die();
                        }
                    }
                }
            }

            this.BoxList = this._game.GetOverlappedBoxes(this.Area);

           

            var newWidth = (int)((this.Area.Width / (double)viewPort.Width) * target.Width + 1);
            var newHeight = (int)((this.Area.Height / (double)viewPort.Width) * target.Width + 1);
            int newXpos =
                (int)(this.Area.X / (this.Area.Width / ((this.Area.Width / (double)viewPort.Width) * target.Width)))
                - (int)(viewPort.X / (this.Area.Width / ((this.Area.Width / (double)viewPort.Width) * target.Width)));
            int newYpos =
                (int)(this.Area.Y / (this.Area.Width / ((this.Area.Width / (double)viewPort.Width) * target.Width)))
                - (int)(viewPort.Y / (this.Area.Width / ((this.Area.Width / (double)viewPort.Width) * target.Width)));

            this.RelativePosition = new Point(newXpos, newYpos);
            this.RelativeSize = new Rectangle(0,0, newWidth, newHeight);

            this.Position = new Point(
                this._position.X + (int)(this.Direction.X * this.Speed),
                this._position.Y + (int)(this.Direction.Y * this.Speed));

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
            if (this.Area.Intersects(viewPort))
            {
                spriteBatch.Draw(_game.GameTexture.GetTexture(this), new Rectangle(RelativePosition.X, RelativePosition.Y, newWidth, newHeight),  Color.White);
            }


        }

        /// <summary>
        /// The toggle radio.
        /// </summary>
       

        #endregion
    }
}