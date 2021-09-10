using System;
using Common.Scripts.Rocket;

public class PauseOfGameEventSubscriber: IEventSubscriber
{

    public void Subscribe(Action action)
    {
        PauseButton.OnGamePause += action;
    }

    public void Unsubscribe(Action action)
    {
        PauseButton.OnGamePause -= action;
    }
    
    public void Callback()
    {
        
    }
}             
