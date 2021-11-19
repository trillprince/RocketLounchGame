using Common.Scripts.Audio;
using Common.Scripts.Cargo;
using Common.Scripts.MissionSystem;
using UnityEditor;
using UnityEngine;

namespace Common.Scripts.Rocket
{
    public class RocketAudio : IEnablable,IUpdatable
    {
        private readonly IAudioManager _audioManager;
        private readonly LaunchManager _launchManager;
        private readonly RocketHealth _rocketHealth;
        private bool _flyLoopReady;

        public RocketAudio(IAudioManager audioManager, LaunchManager launchManager,RocketHealth rocketHealth)
        {
            _audioManager = audioManager;
            _launchManager = launchManager;
            _rocketHealth = rocketHealth;
            _rocketHealth.OnRocketDestroy += PlayRocketDestroySound;
        }

        private void PlayLaunchSound()
        {
            _audioManager.FxAudioClipSetActive("Launch Count Down", true);
        }

        private void PlayFlyLoopSound()
        {
            _audioManager.FxAudioClipSetActive("Launch Sound", true);
            _flyLoopReady = true;
        }


        private void PlayRocketDestroySound()
        {
            _audioManager.FxAudioClipSetActive("Rocket Destroy",true);
            _rocketHealth.OnRocketDestroy -= PlayRocketDestroySound;
        }

        public void Enable()
        {
            _launchManager.RocketLaunching += PlayLaunchSound;
            _launchManager.OnRocketLaunch += PlayFlyLoopSound;
        }

        public void Disable()
        {
            _launchManager.RocketLaunching -= PlayLaunchSound;
            _launchManager.OnRocketLaunch -= PlayFlyLoopSound;
            _audioManager.FxAudioClipSetActive("Rocket Fly Loop", false);
            _audioManager.FxAudioClipSetActive("Space Ship Sound", false);
        }

        public void Execute()
        {
            if (_flyLoopReady)
            {
                bool isActive = _audioManager.FxIsPlaying("Launch Sound");
                if (!isActive)
                {
                    _audioManager.FxAudioClipSetActive("Rocket Fly Loop", true);
                    _flyLoopReady = false;
                }
            }
        }

        public IAudioManager GetAudioManager()
        {
            return _audioManager;
        }
    }
}