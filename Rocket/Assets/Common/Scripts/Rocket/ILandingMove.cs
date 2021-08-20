using System;
using UnityEngine;

public interface IMoveComponent
{
    public void Move(Action<MovementState> changeState = null);
} 
    