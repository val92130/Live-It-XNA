using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using WindowsGame1.Texturing;

namespace WindowsGame1.Animals
{
    public class Eagle : Wild
    {
        #region Constructors and Destructors

        /// <summary>
        /// Initializes a new instance of the <see cref="Dog"/> class.
        /// </summary>
        /// <param name="map">
        /// The map.
        /// </param>
        /// <param name="starPosition">
        /// The star position.
        /// </param>
        public Eagle(MainGame Game, Point startPosition)
            : base(Game, startPosition)
        {
            this.Texture = EAnimalTexture.Dog;
            this.Size = new Rectangle(0, 0, 230, 230);
            this.FavoriteEnvironnment = EBoxGround.Forest;
            this.Speed = 2000000;
            this.DefaultSpeed = this.Speed;
            this.ViewDistance = 400;
            this.Hunger = 49;
            _spriteSheet = Game.Content.Load<Texture2D>("Animals/Animations/Eagle-SpriteSheet");
            _animationUp = new SpriteAnimation(Game, _spriteSheet, this.RelativeArea, 40, 40, 40, 120, 3);
            _animationDown = new SpriteAnimation(Game, _spriteSheet, this.RelativeArea, 40, 40, 40, 0, 3);
            _animationLeft = new SpriteAnimation(Game, _spriteSheet, this.RelativeArea, 40, 40, 40, 40, 3);
            _animationRight = new SpriteAnimation(Game, _spriteSheet, this.RelativeArea, 40, 40, 40, 80, 3);
            this.TargetAnimals = new List<EAnimalTexture> { EAnimalTexture.Rabbit, EAnimalTexture.Cow, EAnimalTexture.Dog };
        }

        #endregion

        #region Public Properties

        /// <summary>
        ///     Gets the max hunger.
        /// </summary>
        public override int MaxHunger
        {
            get
            {
                return 55;
            }
        }

        #endregion

        #region Public Methods and Operators

        /// <summary>
        ///     The behavior.
        /// </summary>
        public override void Behavior()
        {
            base.Behavior();
            if (this.Hunger > 50)
            {
                if (this.AnimalsAround.Count != 0)
                {
                    for (int i = 0; i < AnimalsAround.Count(); i++)
                    {
                        if (TargetAnimals.Contains(AnimalsAround[i].Texture))
                        {
                            ChangePosition(AnimalsAround[i].Position);
                            if (this.Area.Intersects(AnimalsAround[i].Area))
                            {
                                AnimalsAround[i].Die();
                                this.Hunger -= 50;
                            }
                        }
                    }
                }
            }

        }
    }

        #endregion
}

