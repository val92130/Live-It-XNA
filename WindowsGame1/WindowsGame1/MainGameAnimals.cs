﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using WindowsGame1.Animals;

namespace WindowsGame1
{
    public partial class MainGame
    {
        private  List<Animal> _animals = new List<Animal>();
        public List<Animal> Animals
        {
            get
            {
                return this._animals;
            }

            set
            {
                this._animals = value;
            }
        }

        public void CreateAnimal( EAnimalTexture eAnimalType, Point StartPosition )
        {
            Animal a;
            switch( eAnimalType.ToString() )
            {
                case "Cat":
                    a = new Cat( this, StartPosition );
                    break;
                case "Dog":
                    a = new Dog(this, StartPosition);
                    break;
                default:
                    throw new NotSupportedException( "Unknown animal type" );
            }

            this.Animals.Add( a );
        }
    }
}
