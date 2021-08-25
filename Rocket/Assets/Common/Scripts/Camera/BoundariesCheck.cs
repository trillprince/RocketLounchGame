using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BoundariesCheck
{
    private Vector2 _screenBounds;
    private float _objectWidth;
    private float _objectHeight;
    private Rigidbody _rb;
    private Bounds _boundOfMesh;
    private MeshCollider _meshCollider;

    public BoundariesCheck(Rigidbody rigidbody, MeshCollider meshCollider, Camera camera)
    {
        _rb = rigidbody;
        _boundOfMesh = meshCollider.bounds;
        _screenBounds =
            camera.ScreenToWorldPoint(new Vector3(
                Screen.width,
                Screen.height,
                Camera.main.transform.position.z - _rb.position.z));
    }

    public bool OnScreenBoundaries()
    {
        Vector3 viewPos = _rb.position;
        if (viewPos.x <= _screenBounds.x + _boundOfMesh.size.x ||
            viewPos.x >= _screenBounds.x * -1 - _boundOfMesh.size.x ||
            viewPos.y <= _screenBounds.y + _boundOfMesh.size.y ||
            viewPos.y >= _screenBounds.y * -1 - _boundOfMesh.size.y)
        {
            viewPos.x = Mathf.Clamp(viewPos.x,
                _screenBounds.x + _boundOfMesh.size.x,
                _screenBounds.x * -1 - _boundOfMesh.size.x);
            viewPos.y = Mathf.Clamp(viewPos.y,
                _screenBounds.y + _boundOfMesh.size.y,
                _screenBounds.y * -1 - _boundOfMesh.size.y);
            _rb.position = viewPos;
            Debug.Log("aye");
            return false;
        }
        return true;
    }
}