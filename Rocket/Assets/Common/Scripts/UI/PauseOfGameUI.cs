using System;
using Common.Scripts.Rocket;

public class PauseOfGameUI: IUICreator<IPauseWindow>
{
    private IWindowModel _windowModel;
    private readonly Action<IUICreator<IPauseWindow>> _windowCreating;
    private readonly IEventSubscriber _eventSubscriber;

    public PauseOfGameUI(IWindowModel windowModel, Action<IUICreator<IPauseWindow>> windowCreating, IEventSubscriber eventSubscriber)
    {
        _windowModel = windowModel;
        _windowCreating = windowCreating;
        _eventSubscriber = eventSubscriber;
        OnCreatorUse();
    }
    
    public IWindowModel GetWindowModel()
    {
        return _windowModel;
    }

    public void OnWindowClose()
    {
        _eventSubscriber.Callback();
    }

    public void OnCreatorUse()
    {
        _eventSubscriber.Subscribe(OnPause);
    }

    private void OnPause()
    {
        _windowCreating?.Invoke(this);
    }

}