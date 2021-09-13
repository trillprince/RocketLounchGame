using System;
using Common.Scripts.Rocket;
using UnityEngine;

public class PauseOfGameUI: IUICreator<IPauseWindow>
{
    private IWindowModel _windowModel;
    private readonly Action<IUICreator<IPauseWindow>> _windowCreating;
    private readonly IEventSubscriber _eventSubscriber;
    private readonly Action _constructorAction;

    public PauseOfGameUI(IWindowModel windowModel, Action<IUICreator<IPauseWindow>> windowCreating, IEventSubscriber eventSubscriber,Action constructorAction)
    {
        _windowModel = windowModel;
        _windowCreating = windowCreating;
        _eventSubscriber = eventSubscriber;
        _constructorAction = constructorAction;
        OnCreatorUse();
    }
    
    public IWindowModel GetWindowModel()
    {
        return _windowModel;
    }

    public void OnCreatorDestroy()
    {
        _eventSubscriber.Unsubscribe(OnPause);
    }

    public void OnCreatorUse()
    {
        _eventSubscriber.Subscribe(OnPause);
    }

    public void ConstructorAction()
    {
        _constructorAction?.Invoke();
    }

    private void OnPause()
    {
        _windowCreating?.Invoke(this);
    }

}