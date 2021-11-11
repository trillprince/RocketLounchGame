using Common.Scripts.Audio;

public class BlueShieldAudio: IEffectAudio
{
    private readonly IAudioManager _audioManager;

    public BlueShieldAudio(IAudioManager audioManager)
    {
        _audioManager = audioManager;
    }

    public void SoundActive(bool isActive)
    {
        _audioManager.FxAudioClipIsActive("Energy Shield", isActive);
    }

    public void PlayFxAudioClip(string name)
    {
        _audioManager.FxAudioClipIsActive(name, true);
    }
}