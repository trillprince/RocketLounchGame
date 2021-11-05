using System;
using System.Collections;
using System.Collections.Generic;
using Common.Scripts.UI;
using UnityEngine;
using UnityEngine.UI;
using Zenject;

public class PauseButton : MonoBehaviour
{
    [SerializeField] private Button _pauseButton;
    private IUIController _uiController;
    public static event Action OnGamePause;

    [Inject]
    public void Constructor(IUIController uiController)
    {
        _uiController = uiController;
    }

    private void OnEnable()
    {
        _pauseButton.onClick.AddListener(OnPause);
        _uiController.OnUIActive += SetInteractStatus;
    }

    private void OnDisable()
    {
        _pauseButton.onClick.RemoveListener(OnPause);
        _uiController.OnUIActive -= SetInteractStatus;
    }

    private void OnPause()
    {
        OnGamePause?.Invoke();
    }
    
    private void SetInteractStatus(bool isActive)
    {
        _pauseButton.gameObject.SetActive(isActive);
    }
}