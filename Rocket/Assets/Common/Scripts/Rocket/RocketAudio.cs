using Common.Scripts.Audio;
using Common.Scripts.MissionSystem;

namespace Common.Scripts.Rocket
{
    public class RocketAudio : IEnablable
    {
        private readonly IAudioManager _audioManager;
        private readonly LaunchManager _launchManager;

        public RocketAudio(IAudioManager audioManager, LaunchManager launchManager)
        {
            _audioManager = audioManager;
            _launchManager = launchManager;
        }

        private void PlayLaunchSound()
        {
            _audioManager.FxAudioClipIsActive("Launch Sound", true);
        }

        private void PlayFlyLoopSound()
        {
            _audioManager.FxAudioClipIsActive("Rocket Fly Loop", true);
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
            _audioManager.FxAudioClipIsActive("Rocket Fly Loop", false);
        }
    }
}