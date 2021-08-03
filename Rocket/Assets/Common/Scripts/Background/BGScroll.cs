using System;
using System.Collections;
using System.Collections.Generic;
using Common.Scripts;
using Common.Scripts.Rocket;
using TMPro;
using UnityEngine;
using UnityEngine.Serialization;
using Zenject;

public class BGScroll : MonoBehaviour
{
    private Material _material;
    private Vector2 _offset;
    [Range(-2, 2)] public float _xVelocity = 0;
    [Range(-2, 2)] public float _yVelocity;
    [FormerlySerializedAs("onTouchRocketMovement")] [FormerlySerializedAs("_rocketMovement")] [SerializeField] private OnTouchRocketMove onTouchRocketMove;
    private bool _rocketLounched;
    [FormerlySerializedAs("_smoothness")] [SerializeField] private float _moveSmoothness = 100;

    [Inject]
    private void Construct(OnTouchRocketMove onTouchRocketMove)
    {
        this.onTouchRocketMove = onTouchRocketMove;
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
        _offset = new Vector2(_xVelocity, _yVelocity).normalized * onTouchRocketMove.RocketSpeed/_moveSmoothness;
    }

    public void ScrollFromRocketDir()
    {
        _xVelocity = onTouchRocketMove.GetRocketDirection().x;
        _yVelocity = onTouchRocketMove.GetRocketDirection().y;
        Invoke("ReinitializeOffset",1f);;
    }
}