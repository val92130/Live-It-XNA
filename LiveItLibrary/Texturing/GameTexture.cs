using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using WindowsGame1.Enums;
using WindowsGame1.Texturing;

namespace WindowsGame1
{
    public class GameTexture
    {
        MainGame _game;
        Texture2D _textureGrass, _textureGrassLow;
        Texture2D _textureSnow, _textureSnowLow;
        Texture2D _textureDirt, _textureDirtLow;
        Texture2D _textureMountain, _textureMountainLow;
        Texture2D _textureWater, _textureWaterLow;
        Texture2D _textureDesert, _textureDesertLow;
        Texture2D _textureMetalButton;

        Texture2D _textureCat;
        Texture2D _textureDog;

        Texture2D _textureTree;
        Texture2D _textureTree2;
        Texture2D _textureTree3;

        Texture2D _textureRock;

        Texture2D _textureFog;

        Texture2D _playerTexture;

        ContentManager _content;
        public GameTexture(MainGame Game, ContentManager Content)
        {
            _game = Game;
            _content = Content;

            _textureGrass = _content.Load<Texture2D>("Textures/Grass");
            _textureGrassLow = _content.Load<Texture2D>("Textures/LowRes/Grass");
            _textureDirt = _content.Load<Texture2D>( "Textures/Dirt" );
            _textureDirtLow = _content.Load<Texture2D>("Textures/LowRes/Dirt");
            _textureSnow = _content.Load<Texture2D>( "Textures/Snow" );
            _textureSnowLow = _content.Load<Texture2D>("Textures/LowRes/Snow");
            _textureDesert = _content.Load<Texture2D>( "Textures/Desert" );
            _textureDesertLow = _content.Load<Texture2D>("Textures/LowRes/Desert");
            _textureWater = _content.Load<Texture2D>( "Textures/Water" );
            _textureWaterLow = _content.Load<Texture2D>("Textures/LowRes/Water");
            _textureMountain = _content.Load<Texture2D>( "Textures/Mountain" );
            _textureMountainLow = _content.Load<Texture2D>("Textures/LowRes/Mountain");

            _textureCat = _content.Load<Texture2D>( "Animals/Cat" );
            _textureDog = _content.Load<Texture2D>("Animals/Dog");

            _textureTree = _content.Load<Texture2D>("Textures/Vegetation/Tree");
            _textureTree2 = _content.Load<Texture2D>("Textures/Vegetation/Tree2");
            _textureTree3 = _content.Load<Texture2D>("Textures/Vegetation/Tree3");

            _textureRock = _content.Load<Texture2D>("Textures/Vegetation/Rock");

            _textureMetalButton = _content.Load<Texture2D>("Textures/GUI/Button-Metal");

            _textureFog = _content.Load<Texture2D>("Textures/Misc/Fog");
        }

        public Texture2D GetTexture(Box b)
        {
            switch (b.Ground)
            {
                case EBoxGround.Grass:
                    return this._textureGrass;
                case EBoxGround.Snow:
                    return this._textureSnow;
                case EBoxGround.Dirt:
                    return this._textureDirt;
                case EBoxGround.Water:
                    return this._textureWater;
                case EBoxGround.Mountain:
                    return this._textureMountain;
                case EBoxGround.Desert:
                    return this._textureDesert;    
                default:
                    throw new ArgumentException("Unknown texture type");
            }
        }
        public Texture2D GetTexture( Animal a )
        {
            switch( a.Texture )
            {
                case EAnimalTexture.Cat:
                    return this._textureCat;
                case EAnimalTexture.Dog:
                    return this._textureDog;
                default:
                    throw new ArgumentException( "Unknown texture type" );
            }
        }

        public Texture2D GetTexture(EBoxGround e)
        {
            switch (e)
            {
                case EBoxGround.Grass:
                    return this._textureGrass;
                case EBoxGround.Snow:
                    return this._textureSnow;
                case EBoxGround.Dirt:
                    return this._textureDirt;
                case EBoxGround.Water:
                    return this._textureWater;
                case EBoxGround.Mountain:
                    return this._textureMountain;
                case EBoxGround.Desert:
                    return this._textureDesert;
                case EBoxGround.Metal:
                    return this._textureMetalButton;
                case EBoxGround.Fog:
                    return this._textureFog; 
                default:
                    throw new ArgumentException("Unknown texture type");
            }
        }
        public Texture2D GetTexture(EBoxGround Ground, bool LowRes)
        {
            switch (Ground)
            {
                case EBoxGround.Grass:
                    return this._textureGrassLow;
                case EBoxGround.Snow:
                    return this._textureSnowLow;
                case EBoxGround.Dirt:
                    return this._textureDirtLow;
                case EBoxGround.Water:
                    return this._textureWaterLow;
                case EBoxGround.Mountain:
                    return this._textureMountainLow;
                case EBoxGround.Desert:
                    return this._textureDesertLow;
                default:
                    throw new ArgumentException("Unknown texture type");
            }
        }
        public Texture2D GetTexture(EmapElements e)
        {
            switch (e)
            {
                case EmapElements.Tree:
                    return this._textureTree;
                case EmapElements.Tree2:
                    return this._textureTree2;
                case EmapElements.Tree3:
                    return this._textureTree3;
                case EmapElements.Rock:
                    return this._textureRock;
                default:
                    throw new ArgumentException("Unknown texture type");
            }
        }
        public Texture2D GetTexture( EPlayerTexture e )
        {
            switch( e )
            {
                case EPlayerTexture.MainPlayer:
                    return this._textureTree;
                default:
                    throw new ArgumentException( "Unknown texture type" );
            }
        }


    }
}
