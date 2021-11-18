namespace Common.Scripts.Audio
{
    public interface IAudioController
    {
        public void CreateAudioSources();
        public void AudioClipSetActive(string audioName, bool isActive);
        public void AudioClipsAreMuted(bool muted);
    }
}