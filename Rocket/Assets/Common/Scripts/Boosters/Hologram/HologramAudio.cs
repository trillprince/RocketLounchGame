using Common.Scripts.Audio;
using Common.Scripts.Rocket;

public class HologramAudio: IEffectAudio
{
    private readonly IAudioManager _audioManager;
    private readonly RocketCollisionController _rocketCollisionController;

    public HologramAudio(IAudioManager audioManager)
    {
        _audioManager = audioManager;
    }
    public void SoundActive(bool isActive)
    {
        
    }

    public void PlayFxAudioClip()
    {
        _audioManager.FxAudioClipSetActive("Hologram Collision",true);
    }
}