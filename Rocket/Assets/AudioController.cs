using UnityEngine;

public class AudioController : IAudioController
{
    private readonly CustomAudio[] _customAudios;
    private readonly GameObject _parent;

    protected AudioController(CustomAudio[] customAudios,GameObject parent)
    {
        _customAudios = customAudios;
        _parent = parent;
    }
    
    public void CreateAudioSources()
    {
        foreach (var customAudio in _customAudios)
        {
            var audioSource = _parent.AddComponent<AudioSource>();
            customAudio.Source = audioSource;

            audioSource.outputAudioMixerGroup = customAudio.AudioMixerGroup;
            audioSource.clip = customAudio.AudioClip;
            audioSource.loop = customAudio.Loop;
            audioSource.volume = customAudio.Volume;
        }
    }
    
    public void AudioClipIsActive(string audioName,bool isActive)
    {
        foreach (var customAudio in _customAudios)
        {
            if (customAudio.Name == audioName)
            {
                switch (isActive)
                {
                    case true : customAudio.Play();
                        break;
                    case false : customAudio.Stop();
                        break;
 
                }
            }
        }
    }

    public void AudioClipsAreMuted(bool muted)
    {
        foreach (var customAudio in _customAudios)
        {
            if (muted)
            {
                customAudio.MuteVolume();
                return;
            }
            customAudio.UnmuteVolume();
        }
    }
}