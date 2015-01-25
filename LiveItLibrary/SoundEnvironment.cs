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
        SoundEffect _windSound;
        SoundEffectInstance _windSoundLoop;
        MainGame _game;
        SoundEffectInstance _backGroundLoop;
        bool _waterVisible;
        List<Box> _visibleBoxes;

        List<SoundEffectInstance> _allSounds = new List<SoundEffectInstance>();
        public SoundEnvironment(MainGame Game)
        {
            _game = Game;
            _backGroundSound = _game.Content.Load<SoundEffect>("Sounds/background");
            _backGroundLoop = _backGroundSound.CreateInstance();
            _allSounds.Add(_backGroundLoop);

            _backGroundLoop.IsLooped = true;

            _waterSound = _game.Content.Load<SoundEffect>("Sounds/river");
            _waterSoundLoop = _waterSound.CreateInstance();
            _waterSoundLoop.IsLooped = true;
            _allSounds.Add(_waterSoundLoop);

            _windSound = _game.Content.Load<SoundEffect>("Sounds/wind");
            _windSoundLoop = _windSound.CreateInstance();
            _windSoundLoop.IsLooped = true;

            _allSounds.Add(_windSoundLoop);

        }

        public void Update()
        {
            float t = 1f - (1f / (float)_game.MapSize) * _game.Camera.ViewPort.Width;
            float windVolume = (1f / (float)_game.MapSize) * _game.Camera.ViewPort.Width;

            _windSoundLoop.Volume = windVolume;

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

        public void StopAllSounds()
        {
            foreach (SoundEffectInstance s in this._allSounds)
            {
                if (s.State == SoundState.Playing)
                {
                    s.Stop();
                }
            }
        }

        public void Mute()
        {
            foreach (SoundEffectInstance s in this._allSounds)
            {
                if (s.State == SoundState.Playing)
                {
                    s.Volume = 0;
                }
            }
        }
        public void UnMute()
        {
            foreach (SoundEffectInstance s in this._allSounds)
            {
                if (s.State == SoundState.Playing)
                {
                    s.Volume = 1;
                }
            }
        }
        public void PlayAllSounds()
        {
            foreach (SoundEffectInstance s in this._allSounds)
            {
                if (s.State == SoundState.Stopped || s.State == SoundState.Paused)
                {
                    s.Play();
                }
            }
        }

    }
}
