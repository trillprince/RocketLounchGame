using UnityEngine;

namespace Common.Scripts.Audio
{
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

        public void FxAudioClipSetActive(string soundClipName, bool isActive)
        {
            _fxAudioController.AudioClipSetActive(soundClipName, isActive);
        }
        
        public void FxAudioClipSetActiveWithRandomPitch(string soundClipName, bool isActive,float min,float max)
        {
            _fxAudioController.RandomPitchAudioClipSetActive(soundClipName,isActive,min,max);
        }

        public void MusicAudioClipSetActive(string soundClipName, bool isActive)
        {
            _musicAudioController.AudioClipSetActive(soundClipName, isActive);
        }

        public bool FxIsPlaying(string audioName)
        {
            return _fxAudioController.AudioIsPlaying(name);
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
}