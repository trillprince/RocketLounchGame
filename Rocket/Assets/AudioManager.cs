using Common.Scripts.Rocket;
using UnityEngine;

public class AudioManager : MonoBehaviour, IAudioManager
{
    [SerializeField] private CustomAudio[] _fxAudioClips;
    [SerializeField] private CustomAudio[] _musicAudioClips;
    public static AudioManager instance = null;
    private FxAudioController _fxAudioController;
    private MusicAudioController _musicAudioController;

    private void Awake()
    {
        DontDestroyOnLoad(gameObject);
        _fxAudioController = new FxAudioController(_fxAudioClips, gameObject);
        _musicAudioController = new MusicAudioController(_musicAudioClips, gameObject);
        _fxAudioController.CreateAudioSources();
        _musicAudioController.CreateAudioSources();
    }

    private void OnEnable()
    {
        LaunchManager.RocketLaunching += RocketLaunchingSound;
        LaunchManager.OnRocketLaunch += OnRocketLaunch;
    }

    private void OnDisable()
    {
        LaunchManager.RocketLaunching -= RocketLaunchingSound;
        LaunchManager.OnRocketLaunch -= OnRocketLaunch;
    }

    private void OnRocketLaunch()
    {
        _fxAudioController.AudioClipIsActive("Rocket Fly Loop", true);
    }

    private void RocketLaunchingSound()
    {
        _fxAudioController.AudioClipIsActive("Launch Sound", true);
    }

    void Start()
    {
        if (instance == null)
        {
            instance = this;
        }
        else if (instance == this)
        {
            Destroy(gameObject);
        }

        _musicAudioController.AudioClipIsActive("Music", true);
    }

    public void FxSetActive(bool isActive)
    {
        AudioSetActive(isActive, _fxAudioController);
    }

    public void MusicSetActive(bool isActive)
    {
        AudioSetActive(isActive, _musicAudioController);
    }

    private void AudioSetActive(bool isActive,IAudioController audioController)
    {
        if (isActive)
        {
            audioController.AudioClipsAreMuted(false);
            return;
        }
        audioController.AudioClipsAreMuted(true);
    }
}