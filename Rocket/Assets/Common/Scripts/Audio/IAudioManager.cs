namespace Common.Scripts.Audio
{
    public interface IAudioManager
    {
        public void FxSetActive(bool isActive);
        public void MusicSetActive(bool isActive);

        public void FxAudioClipSetActive(string soundClipName,bool isActive);
        public void MusicAudioClipSetActive(string soundClipName,bool isActive);

        public bool FxIsPlaying(string audioName);
        public void FxAudioClipSetActiveWithRandomPitch(string soundClipName, bool isActive,float min,float max);


    }
}