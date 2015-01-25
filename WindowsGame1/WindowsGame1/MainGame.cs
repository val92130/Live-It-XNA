using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Media;
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
        GraphicsDevice _graphics;
        public  int DeadAnimals;
        SoundEnvironment _soundEnvironment;
        Video _introVideo;
        VideoPlayer _videoPlayer;
        Texture2D _videoTexture;
        public MainGame(int boxCountPerLine, int boxSizeInMeter, ContentManager Content, SpriteBatch SpriteBatch, GraphicsDevice Graphics)
        {
            _graphics = Graphics;
            _content = Content;
            _spriteBatch = SpriteBatch;
            _keyControl = new KeyControl(this);
            _soundEnvironment = new SoundEnvironment(this);
            _gameTexture = new GameTexture(this, _content);
            _buttonsTextures = new List<Button>();
            _buttonsActions = new List<Button>();
            CreateButtons();

            this._boxCountPerLine = boxCountPerLine;
            this._boxes = new Box[boxCountPerLine * boxCountPerLine];
            this._boxSize = boxSizeInMeter * 100;
            int count = 0;
            for (int i = 0; i < this._boxCountPerLine; i++)
            {
                for (int j = 0; j < this._boxCountPerLine; j++)
                {
                    this._boxes[count++] = new Box(i,j, this);
                }
            }
            _camera = new Camera(this, _spriteBatch, _graphics);
        }

        public int BoxCountPerLine
        {
            get
            {
                return _boxCountPerLine;
            }
        }
        private void CreateButtons()
        {
            this.CreateTextureButton( "Snow", EBoxGround.Snow );
            this.CreateTextureButton( "Grass", EBoxGround.Grass );
            this.CreateTextureButton( "Dirt", EBoxGround.Dirt );
            this.CreateTextureButton( "Mountain", EBoxGround.Mountain );
            this.CreateTextureButton( "Water", EBoxGround.Water );
            this.CreateTextureButton( "Desert", EBoxGround.Desert );
            this.CreateActionButton("Change", EBoxGround.Metal, EButtonAction.ChangeTexture);
            this.CreateActionButton("Add Cat", EBoxGround.Metal, EButtonAction.AddAnimal, EAnimalTexture.Cat);
            this.CreateActionButton("Add Dog", EBoxGround.Metal, EButtonAction.AddAnimal, EAnimalTexture.Dog );
            this.CreateActionButton("Add Tree", EBoxGround.Metal, EButtonAction.AddTree);
            this.CreateActionButton("Add Rock", EBoxGround.Metal, EButtonAction.AddRock);
            this.CreateActionButton("Fill", EBoxGround.Metal, EButtonAction.FillTexture);

        }

        public int BoxSize
        {
            get
            {
                return _boxSize;
            }
        }

        public ContentManager Content
        {
            get
            {
                return _content;
            }
        }
        public int MapSize
        {
            get
            {
                return this._boxCountPerLine * this._boxSize;
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

        public GraphicsDevice Graphics
        {
            get
            {
                return _graphics;
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
                        b.Source = b.Area;
                        boxList.Add(b);
                    }
                }
            }

            return boxList;
        }

        public int ScreenWidth
        {
            get
            {
                return _graphics.Viewport.Width;
            }
        }
        public int ScreenHeight
        {
            get
            {
                return Graphics.Viewport.Width;
            }
        }

    }
}
