using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input; 

namespace WindowsGame1
{
    public class Button
    {
        Texture2D image;
        SpriteFont font;
        Rectangle location;
        string text;
        Vector2 textLocation;
        SpriteBatch spriteBatch;
        MouseState mouse;
        MouseState oldMouse;
        EBoxGround _textureToSelect;
        EButtonAction _actionToDo = EButtonAction.None;
        bool clicked = false;

        MainGame _game;
        public Button(MainGame Game, Texture2D texture, SpriteFont font, SpriteBatch sBatch, string Text, Point Location, EBoxGround TextureToSelect)
        {
            _game = Game;
            _textureToSelect = TextureToSelect;
            image = texture;
            this.font = font;
            location = new Rectangle(Location.X, Location.Y, 100, GameVariables.ButtonHeight);
            spriteBatch = sBatch;
            this.Text = Text;
        }
        public Button(MainGame Game, Texture2D texture, SpriteFont font, SpriteBatch sBatch, string Text, Point Location, EButtonAction Action)
        {
            _game = Game;
            _actionToDo = Action;
            image = texture;
            this.font = font;
            location = new Rectangle(Location.X, Location.Y, 100, 50);
            spriteBatch = sBatch;
            this.Text = Text;
        }

        public string Text
        {
            get { return text; }
            set
            {
                text = value;
                Vector2 size = font.MeasureString(text);
                textLocation = new Vector2();
                textLocation.Y = location.Y + ((location.Height / 2) - (size.Y / 2));
                textLocation.X = location.X + ((location.Width / 2) - (size.X / 2));
            }
        }

        public void Location(int x, int y)
        {
            location.X = x;
            location.Y = y;
        }

        public Rectangle Area
        {
            get
            {
                return location;
            }
        }
        public EButtonAction Action
        {
            get
            {
                return _actionToDo;
            }
        }
        public void Update()
        {
            mouse = Mouse.GetState();

            if (_game.KeyControl.MouseState.LeftButton == ButtonState.Pressed)
            {
                if (_actionToDo == EButtonAction.None)
                {
                    if (location.Contains(new Point(mouse.X, mouse.Y)))
                    {
                        clicked = true;
                        _game.SelectedTexture = _textureToSelect;
                    }
                }

            }

            oldMouse = mouse;
        }

        public void Draw()
        {

            if (location.Contains(new Point(mouse.X, mouse.Y)))
            {
                spriteBatch.Draw(image,
                    location,
                    Color.Silver);
            }
            else
            {
                spriteBatch.Draw(image,
                    location,
                    Color.White);
            }

            spriteBatch.DrawString(font,
                text,
                textLocation,
                Color.Black);

        }
    }
}
