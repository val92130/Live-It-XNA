﻿using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using WindowsGame1.Texturing;

namespace WindowsGame1.Animals
{
    public class Dog : Wild
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
        public Dog(MainGame Game, Point startPosition)
            : base(Game, startPosition)
        {
            this.Texture = EAnimalTexture.Dog;
            this.Size = new Rectangle(0,0,150, 150);
            this.FavoriteEnvironnment = EBoxGround.Forest;
            this.Speed = 4000000;
            this.DefaultSpeed = this.Speed;
            this.ViewDistance = 400;
            _spriteSheet = Game.Content.Load<Texture2D>("Animals/Animations/Dog-SpriteSheet");
            _animationUp = new SpriteAnimation(Game, _spriteSheet, this.RelativeArea, 40, 40, 40, 120, 4);
            _animationDown = new SpriteAnimation(Game, _spriteSheet, this.RelativeArea, 40, 40, 40, 0, 4);
            _animationLeft = new SpriteAnimation(Game, _spriteSheet, this.RelativeArea, 40, 40, 40, 40, 4);
            _animationRight = new SpriteAnimation(Game, _spriteSheet, this.RelativeArea, 38, 40, 40, 80, 4);
            this.TargetAnimals = new List<EAnimalTexture> { EAnimalTexture.Rabbit, EAnimalTexture.Cow };
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
            if (this.Hunger <= this.MaxHunger)
            {
                return;
            }

            if (this.AnimalsAround.Count == 0)
            {
                return;
            }

            for (int i = 0; i < this.AnimalsAround.Count(); i++)
            {
                if (!this.TargetAnimals.Contains(this.AnimalsAround[i].Texture))
                {
                    continue;
                }

                this.ChangePosition(this.AnimalsAround[i].Position);
                if (!this.Area.Intersects(this.AnimalsAround[i].Area))
                {
                    continue;
                }

                this.AnimalsAround[i].Die();
                this.Hunger -= 50;
            }
        }

        #endregion
    }
}
