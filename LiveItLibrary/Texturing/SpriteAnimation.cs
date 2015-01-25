using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace WindowsGame1.Texturing
{
    public class SpriteAnimation
    {   
        float _timer = 0f;
        float _interval = 100f;
        int currentFrame = 1;
        int _spriteWidth;
        int _spriteHeight;
        Rectangle _sourceRect;
        Rectangle _destRect;
        int _offset;
        float _elapsed;
        Texture2D _texture;
        MainGame _game;
        int _frames;
        int _yPos;
        int _numberOfFrames;
        public SpriteAnimation(MainGame MainGame,Texture2D SpriteSheet,Rectangle DestinationRectangle, int SpriteWidth, int SpriteHeight, int Offset, int YPos, int NumberOfFrames)
        {
            _game = MainGame;
            _yPos = YPos;
            _offset = Offset;
            _spriteWidth = SpriteWidth;
            _spriteHeight = SpriteHeight;
            _texture = SpriteSheet;
            _numberOfFrames = NumberOfFrames;
        }
        public SpriteAnimation(MainGame mainGame, Texture2D SpriteSheet, Rectangle DestinationRectangle, int SpriteWidth, int SpriteHeight, int Offset, int YPos, int NumberOfFrames, float interval)
            :this(mainGame, SpriteSheet, DestinationRectangle, SpriteWidth, SpriteHeight, Offset, YPos, NumberOfFrames)
        {
            _interval = interval;
        }

        public void Update(GameTime gameTime)
        {
            _elapsed += (float)gameTime.ElapsedGameTime.TotalMilliseconds;
            if (_elapsed >= _interval)
            {
                if (_frames >= _numberOfFrames - 1)
                {
                    _frames = 0;
                }
                else
                {
                    _frames++;
                }

                _elapsed = 0;
            }

            _sourceRect = new Rectangle(_offset * _frames, _yPos, _spriteWidth, _spriteHeight);
        }

        public Rectangle SourceRect
        {
            get 
            {
                return _sourceRect;
            }
        }

        public void Draw(GraphicsDevice Graphics, SpriteBatch spriteBatch, GameTime gameTime)
        {
            spriteBatch.Draw(_texture, _destRect, _sourceRect, Color.White);
        }
    }
}
