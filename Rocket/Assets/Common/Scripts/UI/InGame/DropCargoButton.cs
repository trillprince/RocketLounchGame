using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class DropCargoButton : MonoBehaviour
{
    private Button _dropCargoButton;
    private TextMeshProUGUI _textMesh;
    public delegate void DropCargo();

    public static event DropCargo CargoDropped;

    public Button GetButton()
    {
        return _dropCargoButton;
    }

    private void Awake()
    {
        _textMesh = GetComponent<TextMeshProUGUI>();
        _dropCargoButton = GetComponent<Button>();
        _dropCargoButton.onClick.AddListener((() =>
        {
            CargoDropped?.Invoke();
        }));
    }
    
    
    
}
