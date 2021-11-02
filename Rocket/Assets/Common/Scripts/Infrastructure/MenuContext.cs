using Common.Scripts.Audio;
using Common.Scripts.Infrastructure;
using UnityEngine;
using UnityEngine.Serialization;
using Zenject;

public class MenuContext : MonoInstaller
{
    [FormerlySerializedAs("AudioController")]
    public AudioManager audioManager;

    public override void InstallBindings()
    {
        Container.Bind<GameStateMachine>().FromInstance(FindObjectOfType<GameBootstrapper>().StateMachine);

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