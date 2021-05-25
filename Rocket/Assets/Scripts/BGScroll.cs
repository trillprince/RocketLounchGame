using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public class BGScroll : MonoBehaviour
{
    private Material _material;
    private Vector2 _offset;
    [Range(-2,2)] public  float _xVelocity;
    [Range(-2,2)] public  float _yVelocity = 0.5f;

    public float XVelocity
    {
        get => _xVelocity;
        set => _xVelocity = value;
    }

    public float YVelocity
    {
        get => _yVelocity;
        set => _yVelocity = value;
    }

    private void Awake()
    {
        _material = GetComponent<Renderer>().material;
        _offset = new Vector2(XVelocity, YVelocity);
    }
    
    void Update()
    {
        _material.mainTextureOffset += _offset * Time.deltaTime;
    }

    public void MoveLeftScroll()
    {
        XVelocity = 0.3f;
        ReinitializeOffset();
    }

    public void MoveRightScroll()
    {
        XVelocity = -0.3f;
        ReinitializeOffset();
    }

    private void ReinitializeOffset()
    {
        _offset = new Vector2(XVelocity, YVelocity);
    }
}