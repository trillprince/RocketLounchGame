using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PauseButton : MonoBehaviour
{
    private Button _pauseButton;
    public static event Action OnGamePause;

    private void Awake()
    {
        _pauseButton = GetComponent<Button>();
    }

    private void Start()
    {
        _pauseButton.onClick.AddListener(OnPause);
    }

    private void OnPause()
    {
        OnGamePause?.Invoke();
    }
}
