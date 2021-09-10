using System;
using System.Collections;
using System.Collections.Generic;
using Common.Scripts.Rocket;
using UnityEngine;

public class GameWindowManager : MonoBehaviour
{
    private IPauseWindow _currentWindow;
    private IUICreator<IPauseWindow> _currentUiCreator;
    private Dictionary<Type, IUICreator<IPauseWindow>> _uiCreators;
    private IUICreator<IPauseWindow> _uiCreator;
    [SerializeField] private WindowModels _windowModels;
    private IButtonController _buttonController;

    private void Awake()
    {
        _buttonController = FindObjectOfType<ButtonManager>();
        InitUiCreatorDictionary();
    }

    private void InitUiCreatorDictionary()
    {
        _uiCreators = new Dictionary<Type, IUICreator<IPauseWindow>>
        {
            [typeof(EndOfGameUI)] = 
                new EndOfGameUI(_windowModels.GetSpecificModel("EndOfGame"),
                UpdateWindow, 
                new EndOfGameEventListener()),
            [typeof(PauseOfGameUI)] = 
                new PauseOfGameUI(_windowModels.GetSpecificModel("PauseOfGame"),
                    UpdateWindow, 
                    new PauseOfGameEventSubscriber())
        };
    }

    private void UpdateWindow(IUICreator<IPauseWindow> uiCreator)
    {
        _buttonController.ButtonsActive(false);
        _currentUiCreator?.OnWindowClose();
        _currentUiCreator = uiCreator;
        _currentWindow = CreateUI(_currentUiCreator);
        _currentWindow.Constructor((() =>
        {
            _buttonController.ButtonsActive(true);
        }));
    }

    private IPauseWindow CreateUI(IUICreator<IPauseWindow> uiCreator)
    {
        return Instantiate(uiCreator.GetWindowModel().GetWindowObject()).GetComponentInChildren<IPauseWindow>();
    }
    
}