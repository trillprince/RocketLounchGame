using System;
using Common.Scripts.Rocket;
using Common.Scripts.UI;
using UnityEngine;

public class PauseOfGameUI: IUICreator<IPauseWindow>
{
    private IWindowModel _windowModel;
    private readonly Action<IUICreator<IPauseWindow>> _windowCreating;
    private readonly IEventSubscriber _eventSubscriber;
    private readonly Action _constructorAction;

    public PauseOfGameUI(IWindowModel windowModel, Action<IUICreator<IPauseWindow>> windowCreating, IEventSubscriber eventSubscriber)
    {
        _windowModel = windowModel;
        _windowCreating = windowCreating;
        _eventSubscriber = eventSubscriber;
        OnCreatorCreate();
    }
    
    public IWindowModel GetWindowModel()
    {
        return _windowModel;
    }

    public void OnCreatorDestroy()
    {
        _eventSubscriber.Unsubscribe(OnPause);
    }

    public void OnCreatorCreate()
    {
        _eventSubscriber.Subscribe(OnPause);
    }

    private void OnPause()
    {
        _windowCreating?.Invoke(this);
    }

}