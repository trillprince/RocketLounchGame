using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Boundaries : MonoBehaviour
{
    private Vector2 _screenBounds; 
    private float objectWidth;
    private float objectHeight;
    private Rigidbody _rb;
    
    private void Awake()
    {
        _rb = GetComponent<Rigidbody>();
        _screenBounds =
            Camera.main.ScreenToWorldPoint(new Vector3(
                Screen.width, 
                Screen.height,
                Camera.main.transform.position.z - transform.position.z));
        objectWidth = _rb.ClosestPointOnBounds(transform.position).x;
        objectHeight = _rb.ClosestPointOnBounds(transform.position).y;

    }

    void FixedUpdate(){
        Vector3 viewPos = transform.position;
        viewPos.x = Mathf.Clamp(viewPos.x, _screenBounds.x + objectWidth, _screenBounds.x * -1 - objectWidth);
        viewPos.y = Mathf.Clamp(viewPos.y, _screenBounds.y + objectHeight, _screenBounds.y * -1 + objectHeight);
        transform.position = viewPos;
    }
}
