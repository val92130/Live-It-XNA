using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace WindowsGame1
{
    public partial class MainGame
    {
        List<Button> _allButtons = new List<Button>();
        public void CreateTextureButton(string Text, EBoxGround TextureToSelect)
        {
            int maxYpos = 0;
            foreach (Button b in _allButtons)
            {
                if (b.Area.Y > maxYpos)
                {
                    maxYpos = b.Area.Y;
                }
            }

            Point newPos = new Point(GameVariables.ButtonMarginLeft, maxYpos + GameVariables.ButtonMarginTop + GameVariables.ButtonHeight);
            Button button = new Button(this, _gameTexture.GetTexture(TextureToSelect), _content.Load<SpriteFont>("Impact"), _spriteBatch, Text, newPos, TextureToSelect);
            _buttonsTextures.Add(button);
            _allButtons.Add(button);
        }
        public void CreateActionButton(string Text, EBoxGround ButtonTexture, EButtonAction ActionToDo)
        {
            int maxYpos = 0;
            foreach (Button b in _allButtons)
            {
                if (b.Area.Y > maxYpos)
                {
                    maxYpos = b.Area.Y;
                }
            }
            Point newPos = new Point(GameVariables.ButtonMarginLeft, maxYpos + GameVariables.ButtonMarginTop + GameVariables.ButtonHeight);
            Button button = new Button(this, _gameTexture.GetTexture(ButtonTexture), _content.Load<SpriteFont>("Impact"), _spriteBatch, Text, newPos, ActionToDo);
            _buttonsActions.Add(button);
            _allButtons.Add(button);
        }

        public EButtonAction ButtonAction
        {
            get
            {
                return _buttonAction;
            }
            set
            {
                _buttonAction = value;
            }
        }

        public List<Button> AllButtons
        {
            get
            {
                return _allButtons;
            }
        }

        public EBoxGround SelectedTexture
        {
            get
            {
                return _selectedTexture;
            }
            set
            {
                _selectedTexture = value;
            }
        }
    }
}
