using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PauseButton : MonoBehaviour
{
    private Button _pauseButton;
    public static event Action <Action> OnGamePause;

    private void OnEnable()
    {
        _pauseButton = GetComponent<Button>();
        _pauseButton.onClick.AddListener(OnPause);
    }

    private void OnDisable()
    {
        _pauseButton.onClick.RemoveListener(OnPause);
    }
    private void OnPause()
    {
        _pauseButton.interactable = false;
        OnGamePause?.Invoke(OnUnpause);
    }
    
    private void OnUnpause()
    {
        _pauseButton.interactable = true;
    }
    
}

