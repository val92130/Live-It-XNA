using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using WindowsGame1.Texturing;
using Microsoft.Xna.Framework.Graphics;

namespace WindowsGame1
{
    public class Cat : Wild
    {       
        public Cat( MainGame Game, Point startPosition )
            : base( Game, startPosition )
        {
            this.Texture = EAnimalTexture.Cat;
            this.Size = new Rectangle(0,0, 120, 120 );
            this.FavoriteEnvironnment = EBoxGround.Grass;
            this.Speed = 1000000;
            this.ViewDistance = 300;
            _spriteSheet = Game.Content.Load<Texture2D>("Animals/Animations/Cat-SpriteSheet");
            _animationUp = new SpriteAnimation(Game, _spriteSheet, this.RelativeArea, 30, 32, 32,100, 3);
            _animationDown = new SpriteAnimation(Game, _spriteSheet, this.RelativeArea, 30, 32, 32,0,3);
            _animationLeft = new SpriteAnimation(Game, _spriteSheet, this.RelativeArea, 30, 32, 32,40,3);
            _animationRight = new SpriteAnimation(Game, _spriteSheet, this.RelativeArea, 30, 32, 32,70,3);
            this.TargetAnimals = new List<EAnimalTexture> { EAnimalTexture.Rabbit };
        }
        public override int MaxHunger
        {
            get
            {
                return 60;
            }
        }

        public Rectangle SourceRect
        {
            get
            {
                return _sourceRect;
            }
        }

        public override void Behavior()
        {
            // TODO : use abstract class instead of base
            base.Behavior();

            // Specific call
            if( this.Hunger <= this.MaxHunger )
            {
                return;
            }

            if( this.AnimalsAround.Count == 0 )
            {
                return;
            }

            for( int i = 0; i < this.AnimalsAround.Count; i++ )
            {
                if( !this.TargetAnimals.Contains( this.AnimalsAround[i].Texture ) )
                {
                    continue;
                }

                this.ChangePosition( this.AnimalsAround[i].Position );
                this.AnimalsAround[i].Speed = (int)(this.Speed * 2.5);
                if( this.Area.Intersects( this.AnimalsAround[i].Area ) )
                {
                    Random r = new Random();
                    this.AnimalsAround[i].Die();
                    this.Hunger -= r.Next( 30, 40 );
                }
            }
        }
    }

}
