using System;
using UnityEngine;

[Serializable]
public class CustomAudio
{
    public string Name;
    public AudioClip AudioClip;
    public AudioSource Source { get; set; }
    public bool Loop;
    [Range(0,1)] public float Volume;

    public void Play()
    {
        if (Source == null && AudioClip == null)
        {
            return;
        }
        Source.Play();
    }
}