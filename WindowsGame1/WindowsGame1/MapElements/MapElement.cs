using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using WindowsGame1.Enums;

namespace WindowsGame1.MapElements
{
    public class MapElement
    {
        #region Fields

        private MainGame _game;

        private Point _position;

        private Point _relativePosition;

        private Rectangle _relativeSize;

        private Rectangle _size;

        private EmapElements _texture;

        #endregion

        #region Constructors and Destructors

        /// </param>
        public MapElement(MainGame Game, Point StartPosition)
        {
            this._game = Game;
            this._position = StartPosition;
            this._texture = EmapElements.Tree;
            this._size = new Rectangle(0, 0, 400, 400);
            _position.X = _position.X - this._size.Width / 2;
            _position.Y = _position.Y - this._size.Height / 2;
        }

        #endregion

        #region Public Properties

        /// <summary>
        ///     Gets the area.
        /// </summary>
        public Rectangle Area
        {
            get
            {
                return new Rectangle(this.Position.X, this.Position.Y, this.Size.Width, this.Size.Height);
            }
        }

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
        ///     Gets or sets the size.
        /// </summary>
        public Rectangle Size
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
        ///     Gets or sets the texture.
        /// </summary>
        public EmapElements Texture
        {
            get
            {
                return this._texture;
            }

            set
            {
                this._texture = value;
            }
        }

        #endregion

        #region Public Methods and Operators

        public virtual void Draw(
            GraphicsDevice g,
            SpriteBatch spriteBatch,
            Rectangle target,
            Rectangle viewPort)
        {
            var newWidth = (int)((this.Area.Width / (double)viewPort.Width) * target.Width + 1);
            var newHeight = (int)((this.Area.Height / (double)viewPort.Width) * target.Width + 1);
            int newXpos =
                (int)(this.Area.X / (this.Area.Width / ((this.Area.Width / (double)viewPort.Width) * target.Width)))
                - (int)(viewPort.X / (this.Area.Width / ((this.Area.Width / (double)viewPort.Width) * target.Width)));
            int newYpos =
                (int)(this.Area.Y / (this.Area.Width / ((this.Area.Width / (double)viewPort.Width) * target.Width)))
                - (int)(viewPort.Y / (this.Area.Width / ((this.Area.Width / (double)viewPort.Width) * target.Width)));

            this.RelativePosition = new Point(newXpos, newYpos);
            this.RelativeSize = new Rectangle(0, 0, newWidth, newHeight);



            if (this.Area.Intersects(viewPort))
            {
                spriteBatch.Draw(_game.GameTexture.GetTexture(this.Texture), new Rectangle(this.RelativePosition.X + target.X, this.RelativePosition.Y + target.Y, this.RelativeSize.Width, this.RelativeSize.Height), Color.White);
            }
        }

        #endregion
    }
}
