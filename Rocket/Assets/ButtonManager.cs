using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ButtonManager : MonoBehaviour,IButtonController
{
    private IControlledButton[] _controlledButtons;

    private void OnEnable()
    {
        _controlledButtons = GetComponentsInChildren<IControlledButton>();
    }

    public void ButtonsActive(bool isActive)
    {
        foreach (IControlledButton button in _controlledButtons)
        {
            button.SetInteractStatus(isActive);
        }
    }
}