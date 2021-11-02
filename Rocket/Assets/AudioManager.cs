using Common.Scripts.Rocket;
using UnityEngine;
using UnityEngine.SceneManagement;

public class AudioManager : MonoBehaviour, IAudioManager
{
    [SerializeField] private CustomAudio[] _fxAudioClips;
    [SerializeField] private CustomAudio[] _musicAudioClips;
    public static AudioManager Instance;
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

    public void FxAudioClipIsActive(string soundClipName, bool isActive)
    {
        _fxAudioController.AudioClipIsActive(soundClipName, isActive);
    }

    public void MusicAudioClipIsActive(string soundClipName, bool isActive)
    {
        _musicAudioController.AudioClipIsActive(soundClipName, isActive);
    }

    void Start()
    {
        if (Instance == null)
        {
            Instance = this;
        }
        else if (Instance == this)
        {
            Destroy(gameObject);
        }
    }

    public void FxSetActive(bool isActive)
    {
        AudioSetActive(isActive, _fxAudioController);
    }

    public void MusicSetActive(bool isActive)
    {
        AudioSetActive(isActive, _musicAudioController);
    }

    private void AudioSetActive(bool isActive, IAudioController audioController)
    {
        if (isActive)
        {
            audioController.AudioClipsAreMuted(false);
            return;
        }

        audioController.AudioClipsAreMuted(true);
    }
}