using System;
using System.Collections;
using System.Collections.Generic;
using Common.Scripts;
using Common.Scripts.Rocket;
using TMPro;
using UnityEngine;
using Zenject;

public class BGScroll : MonoBehaviour
{
    private Material _material;
    private Vector2 _offset;
    [Range(-2, 2)] public float _xVelocity = 0;
    [Range(-2, 2)] public float _yVelocity;
    [SerializeField] private RocketMovement _rocketMovement;
    private bool _rocketLounched;
    [SerializeField] private float _smoothness = 100;

    [Inject]
    private void Construct(RocketMovement rocketMovement)
    {
        _rocketMovement = rocketMovement;
    }
    
    private void OnEnable()
    {
        LounchManager.MiddleEngineEnable += LounchRocket;
    }

    private void OnDisable()
    {
        LounchManager.MiddleEngineEnable -= LounchRocket;
    }

    private void LounchRocket(bool isLounched)
    {
        _rocketLounched = isLounched;
    }

    private void Awake()
    {
        _material = GetComponent<Renderer>().material;
        _offset = new Vector2(_xVelocity, _yVelocity);
    }

    void Update()
    {
        if (!_rocketLounched)
        {
            return;
        }
        ScrollFromRocketDir();
        _material.mainTextureOffset += _offset * Time.deltaTime;
    }


    private void ReinitializeOffset()
    {
        _offset = new Vector2(_xVelocity, _yVelocity).normalized * _rocketMovement.RocketSpeed/_smoothness;
    }

    public void ScrollFromRocketDir()
    {
        _xVelocity = _rocketMovement.GetRocketDirection().x;
        _yVelocity = _rocketMovement.GetRocketDirection().y;
        Invoke("ReinitializeOffset",1f);;
    }
}