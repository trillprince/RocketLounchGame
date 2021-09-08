using System;
using Common.Scripts.Rocket;

public class PauseOfGameUI: IUICreator<IWindow>
{
    private IWindowModel _windowModel;
    private readonly Action<IUICreator<IWindow>> _action;

    public PauseOfGameUI(IWindowModel windowModel, Action<IUICreator<IWindow>> action)
    {
        _windowModel = windowModel;
        _action = action;
    }

    public IWindowModel GetWindowModel()
    {
        return _windowModel;
    }
}