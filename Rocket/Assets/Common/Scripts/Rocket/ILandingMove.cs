using System;
using Common.Scripts.MissionSystem;

namespace Common.Scripts.Rocket
{
    public interface IRocketMoveComponent: IEnablable
    {
        public void Move(Action<MovementState> changeState = null);

    }
} 
    