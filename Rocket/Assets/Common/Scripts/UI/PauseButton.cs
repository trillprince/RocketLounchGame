using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PauseButton : MonoBehaviour,IControlledButton
{
    [SerializeField] private Button _pauseButton;
    public static event Action OnGamePause;

    private void OnEnable()
    {
        _pauseButton.onClick.AddListener(OnPause);
    }

    private void OnDisable()
    {
        _pauseButton.onClick.RemoveListener(OnPause);
    }

    private void OnPause()
    {
        OnGamePause?.Invoke();
    }
    
    public void SetInteractStatus(bool isActive)
    {
        _pauseButton.gameObject.SetActive(isActive);
    }
}