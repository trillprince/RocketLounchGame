using System;

namespace Common.Scripts.Rocket
{
    public interface IRocketMoveComponent
    {
        public void Move(Action<MovementState> changeState = null);

        public void Enable();
    }
} 
    