using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BGScroll : MonoBehaviour
{

    private float _scrollSpeed = 5f;
    private bool _rocketLounched = false;
    private Vector3 _startPos;
    [SerializeField] private GameObject _bgObj;
    
    

    void Update()
    {
        
        if (_rocketLounched)
        {
            _bgObj.transform.Translate(Vector3.down * _scrollSpeed * Time.deltaTime);
        }
        
    }

    public void StartScroll()
    {
        _rocketLounched = true;
    }
}
