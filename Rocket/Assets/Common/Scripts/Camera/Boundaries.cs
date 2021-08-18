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
                Camera.main.transform.position.z - _rb.position.z));
    }

    private void FixedUpdate()
    {
        OnScreenBoundaries();
    }

    public void OnScreenBoundaries()
    {
        Vector3 viewPos = _rb.position;
        viewPos.x = Mathf.Clamp(viewPos.x, 
            _screenBounds.x + _boundOfMesh.max.x,
            _screenBounds.x * -1 - _boundOfMesh.max.x);
        viewPos.y = Mathf.Clamp(viewPos.y, 
            _screenBounds.y + _boundOfMesh.max.y,
            _screenBounds.y * -1 - _boundOfMesh.max.y);
        _rb.position = viewPos;
        
    }
}