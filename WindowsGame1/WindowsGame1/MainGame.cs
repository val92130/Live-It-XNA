﻿using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace WindowsGame1
{
    public partial class MainGame
    {
        private readonly int _boxCountPerLine;
        private readonly int _boxSize;
        private Box[] _boxes;
        private KeyControl _keyControl;
        ContentManager _content;
        Camera _camera;
        GameTexture _gameTexture;
        GameTime _gameTime;
        List<Button> _buttonsTextures;
        List<Button> _buttonsActions;
        SpriteBatch _spriteBatch;
        EBoxGround _selectedTexture = EBoxGround.Grass;
        EButtonAction _buttonAction;
        public MainGame(int boxCountPerLine, int boxSizeInMeter, ContentManager Content, SpriteBatch SpriteBatch)
        {
            _content = Content;
            _spriteBatch = SpriteBatch;
            _keyControl = new KeyControl(this);
            _gameTexture = new GameTexture(this, _content);
            _buttonsTextures = new List<Button>();
            _buttonsActions = new List<Button>();
            this.CreateTextureButton(new Point(10, 50), "Snow", EBoxGround.Snow);
            this.CreateTextureButton(new Point(10, 150), "Grass", EBoxGround.Grass);
            this.CreateTextureButton(new Point(10, 250), "Dirt", EBoxGround.Dirt);

            this.CreateActionButton(new Point(10, 350), "Change", EBoxGround.Snow, EButtonAction.ChangeTexture);
            this.CreateActionButton(new Point(10, 450), "Fill", EBoxGround.Snow, EButtonAction.FillTexture);

            this._boxCountPerLine = boxCountPerLine;
            this._boxes = new Box[boxCountPerLine * boxCountPerLine];
            this._boxSize = boxSizeInMeter;
            int count = 0;
            for (int i = 0; i < this._boxCountPerLine; i++)
            {
                for (int j = 0; j < this._boxCountPerLine; j++)
                {
                    this._boxes[count++] = new Box(new Point(i, j), 150, 150, this);
                }
            }
        }

        public int BoxSize
        {
            get
            {
                return _boxSize;
            }
        }

        public GameTime GameTime
        {
            get
            {
                return _gameTime;
            }
        }
        public double DeltaTime
        {
            get
            {
                return GameTime.ElapsedGameTime.TotalSeconds;
            }
        }
        public GameTexture GameTexture
        {
            get
            {
                return _gameTexture;
            }
        }
        public Camera Camera
        {
            get
            {
                return _camera;
            }
            internal set
            {
                _camera = value;
            }
        }
        public List<Button> ButtonsTextures
        {
            get
            {
                return _buttonsTextures;
            }
        }
        public List<Button> ButtonsActions
        {
            get
            {
                return _buttonsActions;
            }
        }
        public Box[] Boxes
        {
            get
            {
                return _boxes;
            }
        }

        public KeyControl KeyControl
        {
            get
            {
                return _keyControl;
            }
        }

        public Box this[int line, int column]
        {
            get
            {
                if (line < 0 || line >= this._boxCountPerLine || column < 0 || column >= this._boxCountPerLine)
                {
                    return null;
                }

                return this._boxes[line * this._boxCountPerLine + column];
            }
        }

        public List<Box> GetOverlappedBoxes(Rectangle r)
        {
            var boxList = new List<Box>();
            int top = r.Top / this.BoxSize;
            int left = r.Left / this.BoxSize;
            int bottom = (r.Bottom - 1) / this.BoxSize;
            int right = (r.Right - 1) / this.BoxSize;
            for (int i = top; i <= bottom; ++i)
            {
                for (int j = left; j <= right; ++j)
                {
                    if (this[i, j] != null)
                    {
                        Box b = this[j, i];
                        boxList.Add(b);
                    }
                }
            }

            return boxList;
        }

    }
}
