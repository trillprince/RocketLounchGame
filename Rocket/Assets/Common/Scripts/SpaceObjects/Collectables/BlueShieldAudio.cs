﻿using Common.Scripts.Audio;

public class BlueShieldAudio: IEffectAudio
{
    private readonly IAudioManager _audioManager;

    public BlueShieldAudio(IAudioManager audioManager)
    {
        _audioManager = audioManager;
    }

    public void SoundActive(bool isActive)
    {
        _audioManager.FxAudioClipSetActive("Energy Shield", isActive);
    }

    public void PlayFxAudioClip()
    {
        _audioManager.FxAudioClipSetActive("Energy Shield Damage", true);
    }
}