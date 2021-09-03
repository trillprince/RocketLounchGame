using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

public class LaunchButton : MonoBehaviour
{
    private Button _launchButton;

    private void Awake()
    {
        _launchButton = GetComponent<Button>();
    }

    public void AddEventOnButton(UnityAction unityAction)
    {
        _launchButton.onClick.AddListener(unityAction);
    }
}
