using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace WindowsGame1
{
    public class GameTexture
    {
        MainGame _game;
        Texture2D _textureGrass;
        Texture2D _textureSnow;
        Texture2D _textureDirt;
        Texture2D _textureMountain;
        Texture2D _textureWater;
        Texture2D _textureDesert;

        Texture2D _textureCat;

        ContentManager _content;
        public GameTexture(MainGame Game, ContentManager Content)
        {
            _game = Game;
            _content = Content;
            _textureGrass = _content.Load<Texture2D>("Textures/Grass");
            _textureDirt = _content.Load<Texture2D>( "Textures/Dirt" );
            _textureSnow = _content.Load<Texture2D>( "Textures/Snow" );
            _textureDesert = _content.Load<Texture2D>( "Textures/Desert" );
            _textureWater = _content.Load<Texture2D>( "Textures/Water" );
            _textureMountain = _content.Load<Texture2D>( "Textures/Mountain" );
            _textureCat = _content.Load<Texture2D>( "Animals/Cat" );
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
                default:
                    throw new ArgumentException("Unknown texture type");
            }
        }
    }
}
