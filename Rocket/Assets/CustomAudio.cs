using System;
using UnityEngine;
using UnityEngine.Audio;

[Serializable]
public class CustomAudio
{
    public string Name;
    public AudioClip AudioClip;
    public AudioSource Source { get; set; }

    public AudioMixerGroup AudioMixerGroup;
    public bool Loop;
    [Range(0, 1)] public float Volume;

    private float _savedVolume;

    public void Play()
    {
        if (Source == null && AudioClip == null)
        {
            return;
        }

        Source.Play();
    }

    public void Stop()
    {
        if (Source == null && AudioClip == null)
        {
            return;
        }

        Source.Stop();
    }

    public void MuteVolume()
    {
        Debug.Log($"{AudioMixerGroup}Volume");
        AudioMixerGroup.audioMixer.GetFloat($"{AudioMixerGroup}Volume", out _savedVolume);
        AudioMixerGroup.audioMixer.SetFloat($"{AudioMixerGroup}Volume", -80);
    }

    public void UnmuteVolume()
    {
        AudioMixerGroup.audioMixer.SetFloat($"{AudioMixerGroup}Volume", _savedVolume);
    }
}