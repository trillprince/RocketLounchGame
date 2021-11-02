namespace Common.Scripts.Audio
{
    public interface IAudioManager
    {
        public void FxSetActive(bool isActive);
        public void MusicSetActive(bool isActive);

        public void FxAudioClipIsActive(string soundClipName,bool isActive);
        public void MusicAudioClipIsActive(string soundClipName,bool isActive);


    }
}