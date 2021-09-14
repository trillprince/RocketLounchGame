using System;

namespace Common.Scripts.Rocket
{
    public class EndOfGameEventListener: IEventSubscriber<LandingStatus>
    {
        public void Subscribe(Action<LandingStatus> action)
        {
            RocketMovementController.OnLanding += action;
        }

        public void Unsubscribe(Action<LandingStatus> action)
        {
            RocketMovementController.OnLanding -= action;
        }

        public void Callback()
        {
            
        }
    }
}