using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class RocketHeight : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI _text;
    [SerializeField] private float _height = 0;
    private BGScroll _bg;
    private RocketMovement _rocket;
    private Vector2 _heightDir;
    private bool _rocketLounched;

    private void OnEnable()
    {
        LounchManager.RocketLounch += LounchRocket;
    }

    private void OnDisable()
    {
        LounchManager.RocketLounch -= LounchRocket;
    }

    private void LounchRocket()
    {
        _rocketLounched = true;
    }

    public float Height
    {
        get => _height;
        set => _height = value;
    }

    private void Awake()
    {
        _bg = FindObjectOfType<BGScroll>();
        _rocket = GetComponent<RocketMovement>();
        _text = FindObjectOfType<TextMeshProUGUI>();
    }

    private void Update()
    {
        if (!_rocketLounched)
        {
            return;
        }
        HeightValueUpdate();
    }

    private void HeightValueUpdate()
    {
        Height += _bg._yVelocity * _rocket.RocketSpeed * Time.deltaTime;
        _text.text = Mathf.Floor(Height).ToString();
    }
    
}
