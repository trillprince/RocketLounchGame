using UnityEngine;

namespace Common.Scripts.Audio
{
    public class MusicPlayer : MonoBehaviour
    {
        private IAudioManager _audioManager;
        private void Awake()
        {
            _audioManager = GetComponent<AudioManager>();
        }

        private void Start()
        {
            _audioManager.MusicAudioClipSetActive("Space Ambient",true);
        }
    }
}
