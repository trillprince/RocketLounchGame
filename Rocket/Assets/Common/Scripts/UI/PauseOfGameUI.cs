using System;
using Common.Scripts.Rocket;
using Common.Scripts.UI;
using UnityEngine;

public class PauseOfGameUI: IUICreator<IPauseWindow>
{
    private IWindowModel _windowModel;
    private readonly Action<IUICreator<IPauseWindow>> _windowCreating;
    private readonly Action _constructorAction;

    public PauseOfGameUI(IWindowModel windowModel, Action<IUICreator<IPauseWindow>> windowCreating)
    {
        _windowModel = windowModel;
        _windowCreating = windowCreating;
        OnCreatorCreate();
    }
    
    public IWindowModel GetWindowModel()
    {
        return _windowModel;
    }

    public void OnCreatorDestroy()
    {
        PauseButton.OnGamePause -= OnPause;
    }

    public void OnCreatorCreate()
    {
        PauseButton.OnGamePause += OnPause;
    }

    private void OnPause()
    {
        _windowCreating?.Invoke(this);
    }

}