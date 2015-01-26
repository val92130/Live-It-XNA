using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Input;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using WindowsGame1.Enums;

namespace WindowsGame1
{
    public class KeyControl
    {
        MainGame _game;
        MouseState _mouseState;
        Point _mousePosition;
        MouseState _prevMouseState;
        MouseState _currMouseState;
        bool up, down, left, right, zoomPlus, zoomMinus, clicked;
        public KeyControl(MainGame Game)
        {
            _game = Game;
        }
        public bool Up
        {
            get
            {
                return up;
            }
        }
        public bool Down
        {
            get
            {
                return down;
            }
        }
        public bool Left
        {
            get
            {
                return left;
            }
        }
        public bool Right
        {
            get
            {
                return right;
            }
        }

        public MouseState MouseState
        {
            get
            {
                return _mouseState;
            }
        }
        public Point MousePosition
        {
            get
            {
                return _mousePosition;
            }
        }
        public bool Clicked
        {
            get
            {
                return clicked;
            }
        }
        public void UpdateActions()
        {
            _prevMouseState = _currMouseState;
            _currMouseState = Mouse.GetState();

            if (_currMouseState.LeftButton == ButtonState.Pressed && _prevMouseState.LeftButton == ButtonState.Released)
            {
                clicked = true;
                ButtonHandling();
            } else if(_currMouseState.LeftButton == ButtonState.Released && _prevMouseState.LeftButton == ButtonState.Pressed)
            {
                clicked = false;
            }
            if (up)
            {
                this._game.Camera.MoveViewPortY(-(int)(GameVariables.CameraSpeed * _game.DeltaTime));
            }
            if (down)
            {
                this._game.Camera.MoveViewPortY((int)(GameVariables.CameraSpeed * _game.DeltaTime));
            }
            if (left)
            {
                this._game.Camera.MoveViewPortX(-(int)(GameVariables.CameraSpeed * _game.DeltaTime));
            }
            if (right)
            {
                this._game.Camera.MoveViewPortX((int)(GameVariables.CameraSpeed * _game.DeltaTime));
            }
            if (zoomPlus)
            {
                this._game.Camera.Zoom(-(int)(GameVariables.ZoomValue * _game.DeltaTime));
            }
            if (zoomMinus)
            {
                this._game.Camera.Zoom((int)(GameVariables.ZoomValue * _game.DeltaTime));
            }

        }

        private void ButtonHandling()
        {
            bool intersect = false;
            // If we clicked on an action Button
            foreach (Button b in _game.ButtonsActions)
            {
                if (b.Area.Contains(this.MousePosition))
                {
                    _game.ButtonAction = b.Action;
                    b.Active = true;
                    foreach (Button bu in _game.AllButtons)
                    {
                        if (bu != b)
                        {
                            bu.Active = false;
                        }                    
                    }
                    if (b.SelectedAnimal != EAnimalTexture.None)
                    {
                        _game.SelectedAnimal = b.SelectedAnimal;
                    }
                }
            }

            foreach (Button butts in _game.AllButtons)
            {
                if (butts.Area.Contains(this.MousePosition))
                {
                    intersect = true;
                }
            }
            if (!intersect)
            {
                switch (_game.ButtonAction)
                {
                    case EButtonAction.ChangeTexture:
                        foreach (Box b in _game.Camera.BoxList)
                        {
                            if (b.RelativeArea.Contains(this.MousePosition))
                            {
                                b.Ground = _game.SelectedTexture;
                            }
                        }
                        break;
                    case EButtonAction.FillTexture:
                        foreach (Box b in _game.Camera.BoxList)
                        {
                            if (b.RelativeArea.Contains(this.MousePosition))
                            {
                                _game.FillBox(b, b.Ground, _game.SelectedTexture);
                            }
                        }
                        break;
                    case EButtonAction.FreeMode:
                        if( _game.IsPlayer)
                        {
                            _game.IsPlayer = false;
                        }
                        else
                        {
                            _game.IsPlayer = true;
                        }
                        break;
                    case EButtonAction.AddAnimal:
                        foreach (Box b in _game.Camera.BoxList)
                        {
                            if (b.RelativeArea.Intersects(new Rectangle(MousePosition.X, MousePosition.Y, 50, 50)))
                            {
                                _game.CreateAnimal(_game.SelectedAnimal, b.Position);
                                break;
                            }
                        }                       
                        break;
                    case EButtonAction.AddTree:
                        foreach (Box b in _game.Camera.BoxList)
                        {
                            if (b.RelativeArea.Intersects(new Rectangle(MousePosition.X, MousePosition.Y, 50, 50)))
                            {
                                _game.CreateMapElement(EmapElements.Tree, b.Position);
                                break;
                            }
                        }
                        break;
                    case EButtonAction.AddRock:
                        foreach (Box b in _game.Camera.BoxList)
                        {
                            if (b.RelativeArea.Intersects(new Rectangle(MousePosition.X, MousePosition.Y, 50, 50)))
                            {
                                _game.CreateMapElement(EmapElements.Rock, b.Position);
                                break;
                            }
                        }
                        break;
                }
            }
            intersect = false;
        }
        public void UpdateInput()
        {
            UpdateActions();
            GamePadState GamePadState = GamePad.GetState(PlayerIndex.One);
            KeyboardState newState = Keyboard.GetState();
            _mouseState = Mouse.GetState();
            _mousePosition = new Point(_mouseState.X, _mouseState.Y);

            KeyboardState keyState = Keyboard.GetState();

            if (keyState.IsKeyDown(GameVariables.UpKey))
            {
                up = true;
            }
            if (keyState.IsKeyUp(GameVariables.UpKey))
            {
                up = false;
            }

            if (keyState.IsKeyDown(GameVariables.DownKey))
            {
                down = true;
            }
            if (keyState.IsKeyUp(GameVariables.DownKey))
            {
                down = false;
            }

            if (keyState.IsKeyDown(GameVariables.LeftKey))
            {
                left = true;
            }
            if (keyState.IsKeyUp(GameVariables.LeftKey))
            {
                left = false;
            }
            if (keyState.IsKeyDown(GameVariables.RightKey))
            {
                right = true;
            }
            if (keyState.IsKeyUp(GameVariables.RightKey))
            {
                right = false;
            }

            if (keyState.IsKeyDown(Keys.Add))
            {
                zoomPlus = true;
            }
            if (keyState.IsKeyUp(Keys.Add))
            {
                zoomPlus = false;
            }

            if (keyState.IsKeyDown(Keys.Subtract))
            {
                zoomMinus = true;
            }

            if (keyState.IsKeyUp(Keys.Subtract))
            {
                zoomMinus = false;
            }

        }
    }
}
