using Common.Scripts.Audio;
using UnityEngine.Serialization;
using Zenject;

namespace Common.Scripts.Infrastructure
{
    public class AudioContext : MonoInstaller
    {
        [FormerlySerializedAs("AudioController")]
        public AudioManager audioManager;

        public override void InstallBindings()
        {
            if (AudioManager.Instance == null)
            {
                Container
                    .Bind(typeof(IAudioManager))
                    .FromComponentInNewPrefab(audioManager)
                    .AsSingle()
                    .NonLazy();
            }
        }
    }
}