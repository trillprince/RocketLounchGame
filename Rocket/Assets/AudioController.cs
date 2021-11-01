using System;
using System.Collections;
using System.Collections.Generic;
using Common.Scripts.Rocket;
using UnityEngine;

public class AudioController : MonoBehaviour
{
    [SerializeField] private CustomAudio[] _audioClips;

    private void Awake()
    {
        foreach (var audioClip in _audioClips)
        {
            var audioSource = gameObject.AddComponent<AudioSource>();
            audioClip.Source = audioSource;
            
            audioSource.name = audioClip.Name;
            audioSource.clip = audioClip.AudioClip;
            audioSource.loop = audioClip.Loop;
            audioSource.volume = audioClip.Volume;
        }
    }

    private void OnEnable()
    {
        LaunchManager.RocketLaunching += RocketLaunchingSound;
        LaunchManager.OnRocketLaunch += OnRocketLaunch;
    }

    private void OnDisable()
    {
        LaunchManager.RocketLaunching -= RocketLaunchingSound;
        LaunchManager.OnRocketLaunch -= OnRocketLaunch;
    }

    private void OnRocketLaunch()
    {
        PlayAudioClip("Rocket Fly Loop");
    }

    private void RocketLaunchingSound()
    {
        PlayAudioClip("Launch Sound");
    }

    void Start()
    {
        PlayAudioClip("Music");
    }

    private void PlayAudioClip(string audioName)
    {
        foreach (var customAudio in _audioClips)
        {
            if (customAudio.Name == audioName)
            {
                customAudio.Play();
            }
        }
    }
}