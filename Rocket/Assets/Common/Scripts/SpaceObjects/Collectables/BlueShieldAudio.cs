using Common.Scripts.Audio;

public class BlueShieldAudio
{
    private readonly IAudioManager _audioManager;

    public BlueShieldAudio(IAudioManager audioManager)
    {
        _audioManager = audioManager;
    }

    public void ShieldSoundActive(bool isActive)
    {
        _audioManager.FxAudioClipIsActive("Energy Shield", isActive);
    }

    public void ShieldDamageSound()
    {
        _audioManager.FxAudioClipIsActive("Energy Shield Damage", true);
    }
}