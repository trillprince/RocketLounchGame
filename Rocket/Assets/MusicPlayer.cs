using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MusicPlayer : MonoBehaviour
{
    private IAudioManager _audioManager;
    private void Awake()
    {
        _audioManager = GetComponent<AudioManager>();
    }

    private void Start()
    {
        _audioManager.MusicAudioClipIsActive("Music",true);
    }
}
