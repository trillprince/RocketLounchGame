using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class BGScroll : MonoBehaviour
{
    private Material _material;
    private Vector2 _offset;
    [Range(-2, 2)] public float _xVelocity = 0;
    [Range(-2, 2)] public float _yVelocity;
    [SerializeField] private RocketMovement _rocketMovement;
    private bool _rocketLounched;
    

    private void Awake()
    {
        enabled = false;
        _rocketMovement = FindObjectOfType<RocketMovement>();
        _material = GetComponent<Renderer>().material;
        _offset = new Vector2(_xVelocity, _yVelocity);
    }

    void Update()
    {
        
        ScrollFromRocketDir();
        _material.mainTextureOffset += _offset * Time.deltaTime;
    }


    private void ReinitializeOffset()
    {
        _offset = new Vector2(_xVelocity, _yVelocity).normalized * 0.5f;
    }

    public void ScrollFromRocketDir()
    {
        _xVelocity = _rocketMovement.GetRocketDirection().x;
        _yVelocity = _rocketMovement.GetRocketDirection().y;
        ReinitializeOffset();
    }
}