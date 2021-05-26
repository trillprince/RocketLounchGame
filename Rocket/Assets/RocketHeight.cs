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
    private float _rocketSpeed;
    private Vector2 _heightDir;
    private void Awake()
    {
        _bg = FindObjectOfType<BGScroll>();
        _rocketSpeed = FindObjectOfType<RocketMovement>().RocketSpeed;
    }

    private void Update()
    {
        HeightValueUpdate();
    }

    private void HeightValueUpdate()
    {
        _height += _bg._yVelocity * _rocketSpeed * Time.deltaTime;
        _text.text = Mathf.Floor(_height).ToString();
    }
}
