using Microsoft.Xna.Framework.Audio;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace WindowsGame1
{
    public class SoundEnvironment
    {
        SoundEffect _backGroundSound;
        SoundEffect _waterSound;
        SoundEffectInstance _waterSoundLoop;
        MainGame _game;
        SoundEffectInstance _backGroundLoop;
        bool _waterVisible;
        List<Box> _visibleBoxes;
        public SoundEnvironment(MainGame Game)
        {
            _game = Game;
            _backGroundSound = _game.Content.Load<SoundEffect>("Sounds/background");
            _backGroundLoop = _backGroundSound.CreateInstance();
            _backGroundLoop.IsLooped = true;

            _waterSound = _game.Content.Load<SoundEffect>("Sounds/river");
            _waterSoundLoop = _waterSound.CreateInstance();
            _waterSoundLoop.IsLooped = true;
            _backGroundLoop.Play();
        }

        public void Update()
        {
            float t = 1f - (1f / (float)_game.MapSize) * _game.Camera.ViewPort.Width;
            _backGroundLoop.Volume = t;
            _waterSoundLoop.Volume = t;
            _visibleBoxes = _game.Camera.BoxList;

            foreach (Box b in _visibleBoxes)
            {
                if (b.Ground == EBoxGround.Water)
                {
                    _waterVisible = true;
                    break;
                }
                else
                {
                    _waterVisible = false;
                }
            }

            if (_waterVisible)
            {

                if (_waterSoundLoop.State == SoundState.Stopped || _waterSoundLoop.State == SoundState.Paused)
                {
                    _waterSoundLoop.Play();
                }
            }
            else
            {
                if (_waterSoundLoop.State == SoundState.Playing)
                {
                    _waterSoundLoop.Pause();
                }
            }
        }

    }
}
