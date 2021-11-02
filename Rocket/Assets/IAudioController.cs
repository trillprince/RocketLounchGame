public interface IAudioController
{
    public void CreateAudioSources();
    public void AudioClipIsActive(string audioName, bool isActive);
    public void AudioClipsAreMuted(bool muted);
}