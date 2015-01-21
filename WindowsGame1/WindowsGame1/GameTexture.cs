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
        ContentManager _content;
        public GameTexture(MainGame Game, ContentManager Content)
        {
            _game = Game;
            _content = Content;
            _textureGrass = _content.Load<Texture2D>("Grass");
            _textureDirt = _content.Load<Texture2D>("Dirt");
            _textureSnow = _content.Load<Texture2D>("Snow");
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
                default:
                    throw new ArgumentException("Unknown texture type");
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
                default:
                    throw new ArgumentException("Unknown texture type");
            }
        }
    }
}
