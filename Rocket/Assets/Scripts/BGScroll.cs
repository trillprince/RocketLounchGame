using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BGScroll : MonoBehaviour
{
    private float _scrollSpeed = 5f;
    private Vector3 _startPos;
    [SerializeField] private GameObject _bgObj;
    private RocketMovement _rocketMovement;
    


    void Update()
    {
        _bgObj.transform.Translate(Vector3.down * _scrollSpeed * Time.deltaTime);
    }
}