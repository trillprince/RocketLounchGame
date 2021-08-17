using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Boundaries : MonoBehaviour
{
    private Vector2 _screenBounds; 
    private float _objectWidth;
    private float _objectHeight;
    private Rigidbody _rb;
    private Bounds _boundOfMesh;
    [SerializeField] private MeshCollider _meshCollider;
    
    private void Awake()
    {
        _rb = GetComponent<Rigidbody>();
        _boundOfMesh = _meshCollider.bounds;
        _screenBounds =
            Camera.main.ScreenToWorldPoint(new Vector3(
                Screen.width, 
                Screen.height,
                Camera.main.transform.position.z - transform.position.z));
        _objectWidth = _boundOfMesh.max.x;
        _objectHeight = _boundOfMesh.max.y;
    }

    void LateUpdate(){
        Vector3 viewPos = transform.position;
        viewPos.x = Mathf.Clamp(viewPos.x, _screenBounds.x + _objectWidth, _screenBounds.x * -1 + _objectWidth);
        viewPos.y = Mathf.Clamp(viewPos.y, _screenBounds.y + _objectHeight, _screenBounds.y * -1 - _objectHeight);
        transform.position = viewPos;
    }
}
