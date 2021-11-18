using Common.Scripts.Audio;
using UnityEngine;

public class AudioController : IAudioController
{
    private readonly CustomAudio[] _customAudios;
    private readonly GameObject _parent;

    protected AudioController(CustomAudio[] customAudios, GameObject parent)
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
    

    public void AudioClipSetActive(string audioName, bool isActive)
    {
        var audioClip = FindAudioClip(audioName);

        if (audioClip != null)
        {
            switch (isActive)
            {
                case true:
                    audioClip.Play();
                    break;
                case false:
                    audioClip.Stop();
                    break;
            }
        }
    }
    
    public void RandomPitchAudioClipSetActive(string soundClipName, bool isActive, float min, float max)
    {
        var audioClip = FindAudioClip(soundClipName);

        if (audioClip != null)
        {
            switch (isActive)
            {
                case true:
                    audioClip.pitch = Random.Range(min, max);
                    audioClip.Play();
                    break;
                case false:
                    audioClip.Stop();
                    break;
            }
        }
    }

    public bool AudioIsPlaying(string audioName)
    {
        var audioClip = FindAudioClip(audioName);
        if (audioClip != null)
        {
            return audioClip.isPlaying;
        }
        Debug.Log("is null audioisplaying");
        return false;
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

    private AudioSource FindAudioClip(string audioName)
    {
        foreach (var customAudio in _customAudios)
        {
            if (customAudio.Name == audioName)
            {
                return customAudio.Source;
            }
        }
        Debug.Log("is null find audio clip null");
        return null;
    }
}