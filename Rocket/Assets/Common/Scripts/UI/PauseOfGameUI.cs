using System;
using Common.Scripts.Rocket;

public class PauseOfGameUI: IEventSubscriber,IUICreator<IPauseWindow>
{
    private IWindowModel _windowModel;
    private readonly Action<IUICreator<IPauseWindow>> _action;

    public PauseOfGameUI(IWindowModel windowModel, Action<IUICreator<IPauseWindow>> action)
    {
        _windowModel = windowModel;
        _action = action;
        Subscribe();
    }

    public IWindowModel GetWindowModel()
    {
        return _windowModel;
    }

    public void Subscribe()
    {
        PauseButton.OnGamePause += OnPause;
    }

    public void Unsubscribe()
    {
        PauseButton.OnGamePause -= OnPause;
    }
    
    private void OnPause()
    {
        _action?.Invoke(this);
    }

}