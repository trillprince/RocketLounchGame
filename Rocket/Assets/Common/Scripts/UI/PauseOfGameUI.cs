using System;
using Common.Scripts.Rocket;

public class PauseOfGameUI: IUICreator<IPauseWindow>
{
    private IWindowModel _windowModel;
    private readonly Action<IUICreator<IPauseWindow>> _action;

    public PauseOfGameUI(IWindowModel windowModel, Action<IUICreator<IPauseWindow>> action, IEventSubscriber eventSubscriber)
    {
        _windowModel = windowModel;
        _action = action;
        eventSubscriber.Subscribe(OnPause);
    }
    
    public IWindowModel GetWindowModel()
    {
        return _windowModel;
    }
   
    private void OnPause()
    {
        _action?.Invoke(this);
    }

}