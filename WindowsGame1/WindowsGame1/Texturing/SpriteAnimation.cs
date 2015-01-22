using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace WindowsGame1.Texturing
{
    public class SpriteAnimation
    {   //A Timer variable
        float timer = 0f;
        //The interval (100 milliseconds)
        float interval = 100f;
        //Current frame holder (start at 1)
        int currentFrame = 1;
        //Width of a single sprite image, not the whole Sprite Sheet
        int _spriteWidth;
        //Height of a single sprite image, not the whole Sprite Sheet
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

        public void Update(GameTime gameTime)
        {
            _elapsed += (float)gameTime.ElapsedGameTime.TotalMilliseconds;
            if (_elapsed >= interval)
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
