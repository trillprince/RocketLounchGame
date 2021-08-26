using System;

namespace Common.Scripts.Rocket
{
    public interface IMoveComponent
    {
        public void Move(Action<MovementState> changeState = null);

        public void Enable();
    }
} 
    