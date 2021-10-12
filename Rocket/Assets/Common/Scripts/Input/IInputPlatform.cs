using System;
using UnityEngine;
using UnityEngine.InputSystem;

namespace Common.Scripts.Input
{
    public interface IInputPlatform : IControllableInput
    {
        public event Action <Vector3> OnInput;
    }
}